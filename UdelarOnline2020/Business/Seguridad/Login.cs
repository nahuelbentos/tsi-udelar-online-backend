using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Aplicacion.ManejadorError;
using Business.Datatypes;
using Business.Interfaces;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Models;
using Persistence;

namespace Business.Seguridad
{
  public class Login
  {
    public class Ejecuta : IRequest<DtUsuario>
    {
      public string Email { get; set; }
      public string Password { get; set; }
    }

    public class EjecutaValidator : AbstractValidator<Ejecuta>
    {
      public EjecutaValidator()
      {
        RuleFor(u => u.Email).NotEmpty();
        RuleFor(u => u.Password).NotEmpty();
      }
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
        var usuario = await this.userManager.FindByEmailAsync(request.Email);

        if (usuario == null)
        {
          throw new ManejadorExcepcion(HttpStatusCode.Unauthorized, new { mensaje = "El usuario no existe en el sistema." });
        }

        var resultado = await signInManager.CheckPasswordSignInAsync(usuario, request.Password, false);


        if (resultado.Succeeded)
        {

          var listaRoles = await this.userManager.GetRolesAsync(usuario);
          var roles = new List<string>(listaRoles);
          var facultad = await this.context.Facultad.FindAsync(usuario.Facultad.FacultadId);

          var dtFacultad = new DtFacultad
          {
            Nombre = facultad.Nombre,
            Descripcion = facultad.Descripcion,
            DominioMail = facultad.DominioMail,
            UrlAcceso = facultad.UrlAcceso,
            FacultadId = facultad.FacultadId
          };

          return new DtUsuario
          {
            Nombres = usuario.Nombres,
            Apellidos = usuario.Apellidos,
            emailPersonal = usuario.EmailPersonal,
            CI = usuario.CI,
            Token = this.jwtGenerador.CrearToken(usuario, roles),
            Email = usuario.Email,
            UserName = usuario.UserName,
            Tipo = usuario.GetType().ToString().Split('.')[1],
            Facultad = dtFacultad
          };
        }

        throw new ManejadorExcepcion(HttpStatusCode.Unauthorized, new { mensaje = "Ocurrio un error al loguearse: " + resultado.ToString() });
      }
    }
  }
}