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
      public string? Nombres { get; set; }
      public string? Apellidos { get; set; }
      public string? Cedula { get; set; }
      public DateTime? FechaNacimiento { get; set; }
      public string? Direccion { get; set; }
      public string? Telefono { get; set; }
      public string? UserNameUdelar { get; set; }
      public string UserName { get; set; }
      public string? Password { get; set; }
      public string? Email { get; set; }


    }

    public class EjecutaValidator : AbstractValidator<Ejecuta>
    {
      public EjecutaValidator()
      {
        RuleFor(u => u.UserName).NotEmpty();
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

        var usuario = await this.userManager.FindByNameAsync(request.UserName);

        if (usuario == null)
          throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { mensaje = "No existe el usuario ingresado." });

        var existeOtroUsuario = await this.context.Users.Where(u => u.Email == request.Email && u.UserName != request.UserName).AnyAsync();

        if (existeOtroUsuario)
          throw new ManejadorExcepcion(HttpStatusCode.InternalServerError, new { mensaje = "Ya existe ese mail en otro usuario" });


        // Usuario usuarioActualizar = null;

        // switch (request.Tipo)
        // {
        //   case "Administrador":
        //     usuarioActualizar = new Administrador();
        //     break;
        //   case "AdministradorFacultad":
        //     usuarioActualizar = new AdministradorFacultad();
        //     break;
        //   case "Alumno":
        //     usuarioActualizar = new Alumno();
        //     break;
        //   case "Docente":
        //     usuarioActualizar = new Docente();
        //     break;
        //   default:
        //     throw new ManejadorExcepcion(HttpStatusCode.BadRequest, new { mensaje = "El tipo de usuario debe ser Administrador, AdministradorFacultad, Alumno o Docente" });
        // }

        usuario.Nombres = request.Nombres ?? usuario.Nombres;
        usuario.Apellidos = request.Apellidos ?? usuario.Apellidos;
        usuario.CI = request.Cedula ?? usuario.CI;
        usuario.FechaNacimiento = request.FechaNacimiento ?? usuario.FechaNacimiento;
        usuario.Direccion = request.Direccion ?? usuario.Nombres;
        usuario.Telefono = request.Telefono ?? usuario.Telefono;
        usuario.UserName = request.UserName ?? usuario.UserName; // No debería ir si optamos por la solucion de que este sea el unico
        usuario.UserName_udelar = request.UserNameUdelar ?? usuario.UserName_udelar;
        usuario.Email = request.Email ?? usuario.Email;

        if (request.Password != null)
          usuario.PasswordHash = this.passwordHasher.HashPassword(usuario, request.Password);


        usuario.Nombres = request.Nombres ?? usuario.Nombres;

        var result = await this.userManager.UpdateAsync(usuario);

        if (result.Succeeded)
          return Unit.Value; // Esto va a cambiar, luego devuelvo un dataType de usuario con el token.        

        throw new ManejadorExcepcion(HttpStatusCode.InternalServerError, new { mensaje = "No se pudo editar el usuario" });
      }
    }
  }
}