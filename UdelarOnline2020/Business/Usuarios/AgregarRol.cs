using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Business.ManejadorError;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Models;

namespace Business.Usuarios
{
  public class AgregarRol
  {
    public class Ejecuta : IRequest
    {
      public string Email { get; set; }
      public string RolNombre { get; set; }
    }

    public class EjecutaValida : AbstractValidator<Ejecuta>
    {
      public EjecutaValida()
      {
        RuleFor(u => u.Email).NotEmpty();
        RuleFor(u => u.RolNombre).NotEmpty();
      }

    }

    public class Manejador : IRequestHandler<Ejecuta>
    {
      private readonly UserManager<Usuario> userManager;
      private readonly RoleManager<IdentityRole> roleManager;

      public Manejador(UserManager<Usuario> userManager, RoleManager<IdentityRole> roleManager)
      {
        this.userManager = userManager;
        this.roleManager = roleManager;
      }

      public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
      {
        var role = await this.roleManager.FindByNameAsync(request.RolNombre);

        if (role == null)
          throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { mensaje = "No existe el rol" });

        var usuario = await this.userManager.FindByEmailAsync(request.Email);

        if (usuario == null)
          throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { mensaje = "No existe el usuario" });

        var resultado = await userManager.AddToRoleAsync(usuario, request.RolNombre);

        if (resultado.Succeeded)
          return Unit.Value;

        throw new ManejadorExcepcion(HttpStatusCode.InternalServerError, new { mensaje = "No se pudo agregar el rol al usuario" });

      }
    }
  }
}