using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Aplicacion.ManejadorError;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Models;
using Persistence;

namespace Business.Usuarios
{
  public class Editar
  {
    public class Ejecuta : IRequest
    {
      public string Nombres { get; set; }
      public string Apellidos { get; set; }
      public string Cedula { get; set; }
      public DateTime? FechaNacimiento { get; set; }
      public string Direccion { get; set; }
      public string Telefono { get; set; }
      public string EmailPersonal { get; set; }

      public string Email { get; set; }
      public string Password { get; set; }
      public string Tipo { get; set; }


    }

    public class EjecutaValidator : AbstractValidator<Ejecuta>
    {
      public EjecutaValidator()
      {
        RuleFor(u => u.Email).NotEmpty();
        RuleFor(u => u.Tipo).NotEmpty();
      }
    }

    public class Manejador : IRequestHandler<Ejecuta>
    {
      private readonly UserManager<Usuario> userManager;
      private readonly UdelarOnlineContext context;
      private readonly IPasswordHasher<Usuario> passwordHasher;

      public Manejador(UserManager<Usuario> userManager, UdelarOnlineContext context, IPasswordHasher<Usuario> passwordHasher)
      {
        this.userManager = userManager;
        this.context = context;
        this.passwordHasher = passwordHasher;
      }

      public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
      {

        var usuario = await this.userManager.FindByEmailAsync(request.Email);

        if (usuario == null)
          throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { mensaje = "No existe el usuario ingresado." });

        // Agrego Models. para poder comparar con el namespaces completo.
        var tipo = "Models." + request.Tipo;


        if (!tipo.Equals(usuario.GetType().ToString()))
        {

          Usuario usuarioActualizar = null;

          switch (request.Tipo)
          {
            case "Administrador":
              usuarioActualizar = new Administrador();
              break;
            case "AdministradorFacultad":
              usuarioActualizar = new AdministradorFacultad();
              break;
            case "Alumno":
              usuarioActualizar = new Alumno();
              break;
            case "Docente":
              usuarioActualizar = new Docente();
              break;
            default:
              throw new ManejadorExcepcion(HttpStatusCode.BadRequest, new { mensaje = "El tipo de usuario debe ser Administrador, AdministradorFacultad, Alumno o Docente" });
          }

          if (request.Password == null)
            throw new ManejadorExcepcion(HttpStatusCode.BadRequest, new { mensaje = "Para cambiar el tipo de usuairo, es necesario enviar el password" });


          usuarioActualizar = await this.editarUsuario(usuario, usuarioActualizar, request);

          var result = await this.userManager.CreateAsync(usuarioActualizar, request.Password);

          if (result.Succeeded)
            // Esto va a cambiar, luego devuelvo un dataType de usuario con el token.
            return Unit.Value;

        }
        else
        {

          usuario = await this.editarUsuario(usuario, null, request);

          if (request.Password != null)
            usuario.PasswordHash = this.passwordHasher.HashPassword(usuario, request.Password);


          var result = await this.userManager.UpdateAsync(usuario);

          if (result.Succeeded)
            return Unit.Value; // Esto va a cambiar, luego devuelvo un dataType de usuario con el token.
        }


        throw new ManejadorExcepcion(HttpStatusCode.InternalServerError, new { mensaje = "No se pudo editar el usuario" });
      }

      private async Task<Usuario> editarUsuario(Usuario usuarioOld, Usuario usuarioActualizar, Ejecuta request)
      {

        Usuario usuario = usuarioActualizar ?? usuarioOld;

        usuario.Nombres = request.Nombres ?? usuarioOld.Nombres;
        usuario.Apellidos = request.Apellidos ?? usuarioOld.Apellidos;
        usuario.CI = request.Cedula ?? usuarioOld.CI;
        usuario.FechaNacimiento = request.FechaNacimiento ?? usuarioOld.FechaNacimiento;
        usuario.Direccion = request.Direccion ?? usuarioOld.Nombres;
        usuario.Telefono = request.Telefono ?? usuarioOld.Telefono;

        // Solamente cambiamos el EmailPersonal, la propiedad Email y Username no se pueden cambiar.
        usuario.EmailPersonal = request.EmailPersonal ?? usuarioOld.EmailPersonal;

        if (usuarioActualizar != null)
        {
          usuario.FacultadId = usuarioOld.FacultadId;
          usuario.Facultad = usuarioOld.Facultad;
          usuario.EmailPersonal = usuarioOld.EmailPersonal;
          usuario.UserName = usuarioOld.UserName;
          usuario.Email = usuarioOld.Email;
          usuario.ComunicadoLista = usuarioOld.ComunicadoLista;


          await this.eliminarUsuario(usuarioOld);
        }

        return usuario;

      }


      private async Task eliminarUsuario(Usuario usuario)
      {
        var res = await this.userManager.DeleteAsync(usuario);
        if (!res.Succeeded)
          throw new ManejadorExcepcion(HttpStatusCode.InternalServerError, new { mensaje = "Ocurrió un error al eliminar el usuario." });
      }

    }
  }
}