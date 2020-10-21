using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Aplicacion.ManejadorError;
using Business.Datatypes;
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

      public Manejador(UdelarOnlineContext context, UserManager<Usuario> userManager, SignInManager<Usuario> signInManager)
      {
        this.context = context;
        this.userManager = userManager;
        this.signInManager = signInManager;
      }

      public async Task<DtUsuario> Handle(Ejecuta request, CancellationToken cancellationToken)
      {
        var usuario = await this.userManager.FindByEmailAsync(request.Email);

        if (usuario == null)
        {
          throw new ManejadorExcepcion(HttpStatusCode.Unauthorized);
        }

        var resultado = await signInManager.CheckPasswordSignInAsync(usuario, request.Password, false);


        if (resultado.Succeeded)
        {

          var listaRoles = await this.userManager.GetRolesAsync(usuario);
          var roles = new List<string>(listaRoles);



          return new DtUsuario
          {
            Nombres = usuario.Nombres,
            Apellidos = usuario.Apellidos,
            emailPersonal = usuario.EmailPersonal,
            Token = "Aca va el token",
            Email = usuario.Email,
            UserName = usuario.UserName,
            FacultadId = usuario.FacultadId,
            Facultad = usuario.Facultad,
          };
        }

        throw new ManejadorExcepcion(HttpStatusCode.Unauthorized);
      }
    }
  }
}