using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Business.Datatypes;
using Business.Interfaces;
using Business.ManejadorError;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Models;
using Persistence;

namespace Business.Seguridad
{
  public class Register
  {
    public class Ejecuta : IRequest<DtUsuario>
    {
      public string Nombres { get; set; }
      public string Apellidos { get; set; }
      public string CI { get; set; }
      public DateTime FechaNacimiento { get; set; }
      public string Direccion { get; set; }
      public string Telefono { get; set; }
      public string EmailPersonal { get; set; }
      public string UserName { get; set; }
      public string Password { get; set; }
      public Guid FacultadId { get; set; }

      // Esto para unificar el tipo y no hacer uno por cada uno
      public string Tipo { get; set; }

    }

    public class Manejador : IRequestHandler<Ejecuta, DtUsuario>
    {

      private readonly UdelarOnlineContext context;
      private readonly UserManager<Usuario> userManager;
      private readonly SignInManager<Usuario> signInManager;
      private readonly IJwtGenerador jwtGenerador;

      public Manejador(UdelarOnlineContext context, UserManager<Usuario> userManager, SignInManager<Usuario> signInManager, IJwtGenerador jwtGenerador)
      {
        this.context = context;
        this.userManager = userManager;
        this.signInManager = signInManager;
        this.jwtGenerador = jwtGenerador;
      }

      public async Task<DtUsuario> Handle(Ejecuta request, CancellationToken cancellationToken)
      {
        var facultad = await this.context.Facultad.FindAsync(request.FacultadId);
        if (facultad == null)
          throw new ManejadorExcepcion(HttpStatusCode.BadRequest, new { mensaje = "No existe facultad con el FacultadId ingresado." });

        var email = request.UserName + '@' + facultad.DominioMail;

        var existe = await this.context.Users.Where(user => user.Email == email).AnyAsync();

        if (existe)
          throw new ManejadorExcepcion(HttpStatusCode.BadRequest, new { mensaje = "El email de la facultad ya existe en la misma." });

        // habría que ver si tiene sentido esta validación
        var existeUserName = await this.context.Users.Where(user => user.UserName == request.UserName).AnyAsync();

        if (existeUserName)
          throw new ManejadorExcepcion(HttpStatusCode.BadRequest, new { mensaje = "El UserName ingresado ya existe" });

        Usuario usuario = null;

        switch (request.Tipo)
        {
          case "Administrador":
            usuario = new Administrador();
            break;
          case "AdministradorFacultad":
            usuario = new AdministradorFacultad();
            break;
          case "Alumno":
            usuario = new Alumno();
            break;
          case "Docente":
            usuario = new Docente();
            break;
          default:
            throw new ManejadorExcepcion(HttpStatusCode.BadRequest, new { mensaje = "El tipo de usuario debe ser Administrador, AdministradorFacultad, Alumno o Docente" });
        }

        usuario.Nombres = request.Nombres;
        usuario.Apellidos = request.Apellidos;
        usuario.CI = request.CI;
        usuario.FechaNacimiento = request.FechaNacimiento;
        usuario.Direccion = request.Direccion;
        usuario.Telefono = request.Telefono;
        usuario.EmailPersonal = request.EmailPersonal;
        usuario.UserName = request.UserName;
        usuario.Email = email;
        usuario.ComunicadoLista = null;
        usuario.Facultad = facultad;
        var result = await this.userManager.CreateAsync(usuario, request.Password);

        if (result.Succeeded)
        {

          var res = await this.userManager.AddToRoleAsync(usuario, request.Tipo);
          if (res.Succeeded)
          {

            var resultado = await signInManager.CheckPasswordSignInAsync(usuario, request.Password, false);

            if (resultado.Succeeded)
            {


              var listaRoles = await this.userManager.GetRolesAsync(usuario);
              var roles = new List<string>(listaRoles);

              // La idea es que solo haya un único rol por usuario.
              var rol = "";
              if (roles.Count > 0)
                rol = roles[0];
              var dtFacultad = new DtFacultad
              {

                Id = facultad.FacultadId,
                FacultadId = facultad.FacultadId,
                Descripcion = facultad.Descripcion,
                Nombre = facultad.Nombre,
                UrlAcceso = facultad.UrlAcceso,
                DominioMail = facultad.DominioMail,
                ColorCodigo = facultad.ColorCodigo,
              };

              return new DtUsuario
              {
                Id = usuario.Id,
                Nombres = usuario.Nombres,
                Apellidos = usuario.Apellidos,
                emailPersonal = usuario.EmailPersonal,
                CI = usuario.CI,
                Token = this.jwtGenerador.CrearToken(usuario, roles),
                Email = usuario.Email,
                UserName = usuario.UserName,
                Tipo = usuario.GetType().ToString().Split('.')[1],
                Rol = rol,
                Facultad = dtFacultad
              };

            }

          }
          var describer = new IdentityErrorDescriber();

          // Esto va a cambiar, luego devuelvo un dataType de usuario con el token.

        }

        var mensajeErrores = result.Errors.Select(e => e.Description);
        throw new ManejadorExcepcion(HttpStatusCode.InternalServerError, new { mensaje = "Ocurrió un error, no se pudo registrar en el sistema.", mensajeErrores });
      }
    }
  }
}