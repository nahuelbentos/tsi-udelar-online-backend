using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Business.ManejadorError;
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
      public string CI { get; set; }
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
      private readonly RoleManager<IdentityRole> roleManager;

      public Manejador(
        UserManager<Usuario> userManager,
        UdelarOnlineContext context,
        IPasswordHasher<Usuario> passwordHasher,
        RoleManager<IdentityRole> roleManager)
      {
        this.userManager = userManager;
        this.context = context;
        this.passwordHasher = passwordHasher;
        this.roleManager = roleManager;
      }

      public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
      {

        var usuario = await this.context.Users.Include(u => u.Facultad).Include(u => u.ComunicadoLista).FirstOrDefaultAsync(u => u.Email == request.Email);



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
          {
            var res = await this.userManager.AddToRoleAsync(usuarioActualizar, request.Tipo);
            if (res.Succeeded)
              return Unit.Value;
          }

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
        usuario.CI = request.CI ?? usuarioOld.CI;
        usuario.FechaNacimiento = request.FechaNacimiento ?? usuarioOld.FechaNacimiento;
        usuario.Direccion = request.Direccion ?? usuarioOld.Nombres;
        usuario.Telefono = request.Telefono ?? usuarioOld.Telefono;

        // Solamente cambiamos el EmailPersonal, la propiedad Email y Username no se pueden cambiar.
        usuario.EmailPersonal = request.EmailPersonal ?? usuarioOld.EmailPersonal;

        if (usuarioActualizar != null)
        {

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
        var roles = await this.userManager.GetRolesAsync(usuario);
        var result = await this.userManager.RemoveFromRolesAsync(usuario, roles);

        if (!result.Succeeded)
          throw new ManejadorExcepcion(HttpStatusCode.InternalServerError, new { mensaje = "Ocurrió un error al eliminar el usuario." });

        var res = await this.userManager.DeleteAsync(usuario);
        if (!res.Succeeded)
          throw new ManejadorExcepcion(HttpStatusCode.InternalServerError, new { mensaje = "Ocurrió un error al eliminar el usuario." });

      }

    }


    public static Ejecuta GetData(string email, Ejecuta data) => new Ejecuta
    {
      Nombres = data.Nombres,
      Apellidos = data.Apellidos,
      FechaNacimiento = data.FechaNacimiento,
      Direccion = data.Direccion,
      Telefono = data.Telefono,
      EmailPersonal = data.EmailPersonal,
      Email = email,
      Password = data.Password,
      Tipo = data.Tipo,
      CI = data.CI,
      

    };
  }
}