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
  public class Nuevo
  {
    public class Ejecuta : IRequest
    {
      public string Nombres { get; set; }
      public string Apellidos { get; set; }
      public string Cedula { get; set; }
      public DateTime FechaNacimiento { get; set; }
      public string Direccion { get; set; }
      public string Telefono { get; set; }
      public string UserNameUdelar { get; set; }
      public string UserName { get; set; }
      public string Password { get; set; }
      public string Email { get; set; }

      // Esto para unificar el tipo y no hacer uno por cada uno
      public string Tipo { get; set; }

      /*       
      "comunicadoLista": [], 
      */

    }

    public class EjecutaValidator : AbstractValidator<Ejecuta>
    {
      public EjecutaValidator()
      {
        RuleFor(u => u.Nombres).NotEmpty();
        RuleFor(u => u.Apellidos).NotEmpty();
        RuleFor(u => u.Cedula).NotEmpty();
        RuleFor(u => u.FechaNacimiento).NotEmpty();
        RuleFor(u => u.Direccion).NotEmpty();
        RuleFor(u => u.Telefono).NotEmpty();
        RuleFor(u => u.UserName).NotEmpty();
        RuleFor(u => u.UserNameUdelar).NotEmpty();
        RuleFor(u => u.Password).NotEmpty();
        RuleFor(u => u.Email).NotEmpty();
        RuleFor(u => u.Tipo).NotEmpty();
      }
    }

    public class Manejador : IRequestHandler<Ejecuta>
    {
      private readonly UdelarOnlineContext context;
      private readonly UserManager<Usuario> userManager;

      public Manejador(UdelarOnlineContext context, UserManager<Usuario> userManager)
      {
        this.context = context;
        this.userManager = userManager;
      }

      public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
      {

        var existe = await this.context.Users.Where(user => user.Email == request.Email).AnyAsync();

        if (existe)
          throw new ManejadorExcepcion(HttpStatusCode.BadRequest, new { mensaje = "El email ingresado ya existe" });

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
        usuario.CI = request.Cedula;
        usuario.FechaNacimiento = request.FechaNacimiento;
        usuario.Direccion = request.Direccion;
        usuario.Telefono = request.Telefono;
        usuario.UserName_udelar = request.UserNameUdelar;
        usuario.UserName = request.UserName;
        usuario.Email = request.Email;
        usuario.ComunicadoLista = null;

        var result = await this.userManager.CreateAsync(usuario, request.Password);

        if (result.Succeeded)
          // Esto va a cambiar, luego devuelvo un dataType de usuario con el token.
          return Unit.Value;

        throw new ManejadorExcepcion(HttpStatusCode.InternalServerError, new { mensaje = "Ocurrió un error, no se pudo dar de alta el usuario" });

      }
    }
  }
}