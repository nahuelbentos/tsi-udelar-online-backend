using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Aplicacion.ManejadorError;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Models;
using Persistence;

namespace Business.Usuarios
{
  public class Eliminar
  {
    public class Ejecuta : IRequest
    {
      public string Email { get; set; }
    }

    public class EjecutaValidator : AbstractValidator<Ejecuta>
    {
      public EjecutaValidator()
      {
        RuleFor(u => u.Email).NotEmpty();
      }
    }

    public class Manejador : IRequestHandler<Ejecuta>
    {
      private readonly UserManager<Usuario> userManager;
      private readonly UdelarOnlineContext context;

      public Manejador(UserManager<Usuario> userManager, UdelarOnlineContext context)
      {
        this.userManager = userManager;
        this.context = context;
      }

      public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
      {
        var usuario = await this.userManager.FindByEmailAsync(request.Email);

        if (usuario == null)
          throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { mensaje = "No existe un usuario con el email ingresado" });

        var res = await this.userManager.DeleteAsync(usuario);
        if (res.Succeeded)
        {
          return Unit.Value;
        }

        throw new ManejadorExcepcion(HttpStatusCode.InternalServerError, new { mensaje = "Ocurrió un error al eliminar el usuario." });
      }
    }
  }
}