using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Business.ManejadorError;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Models;
using Persistence;

namespace Business.Usuarios
{
  public class ConsultaByEmail
  {

    public class Ejecuta : IRequest<Usuario>
    {
      public string Email { get; set; }
    }


    public class Manejador : IRequestHandler<Ejecuta, Usuario>
    {
      private readonly UserManager<Usuario> userManager;
      private readonly UdelarOnlineContext context;

      public Manejador(UserManager<Usuario> userManager, UdelarOnlineContext context)
      {
        this.userManager = userManager;
        this.context = context;
      }

      public async Task<Usuario> Handle(Ejecuta request, CancellationToken cancellationToken)
      {
        var usuario = await this.userManager.FindByEmailAsync(request.Email);

        if (usuario == null)
          throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { mensaje = "No existe un usuario con ese Email." });

        return usuario;
      }

    }
  }
}