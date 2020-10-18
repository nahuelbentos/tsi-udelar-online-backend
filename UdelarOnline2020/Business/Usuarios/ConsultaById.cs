using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Aplicacion.ManejadorError;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Models;
using Persistence;

namespace Business.Usuarios
{
  public class ConsultaById
  {

    public class Ejecuta : IRequest<Usuario>
    {
      public string Id { get; set; }
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
        var usuario = await this.userManager.FindByIdAsync(request.Id);

        if (usuario == null)
          throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { mensaje = "No existe un usuario con ese Id." });

        return usuario;
      }

    }
  }
}