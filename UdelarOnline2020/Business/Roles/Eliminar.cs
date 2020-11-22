using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Business.ManejadorError;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Business.Roles
{
  public class Eliminar
  {
    public class Ejecuta : IRequest
    {
      public string Nombre { get; set; }
    }

    public class EjecutaValida : AbstractValidator<Ejecuta>
    {
      public EjecutaValida()
      {
        RuleFor(r => r.Nombre).NotEmpty();
      }
    }

    public class Manejador : IRequestHandler<Ejecuta>
    {
      private readonly RoleManager<IdentityRole> roleManager;

      public Manejador(RoleManager<IdentityRole> roleManager)
      {
        this.roleManager = roleManager;
      }

      public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
      {
        var role = await this.roleManager.FindByNameAsync(request.Nombre);
        if (role == null)
          throw new ManejadorExcepcion(HttpStatusCode.BadRequest, new { mensaje = "No existe el rol" });
        var resultado = await this.roleManager.DeleteAsync(role);
        if (resultado.Succeeded)
          return Unit.Value;

        throw new ManejadorExcepcion(HttpStatusCode.InternalServerError, new { mensaje = "No se pudo eliminar el Rol" });
      }
    }

  }
}