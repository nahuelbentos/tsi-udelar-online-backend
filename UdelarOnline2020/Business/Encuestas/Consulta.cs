using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Models;
using Persistence;

namespace Business.Encuestas
{
  public class Consulta
  {
    public class Ejecuta : IRequest<List<Encuesta>> { }

    public class Manejador : IRequestHandler<Ejecuta, List<Encuesta>>
    {
      private readonly UdelarOnlineContext context;

      public Manejador(UdelarOnlineContext context)
      {
        this.context = context;
      }

      public async Task<List<Encuesta>> Handle(Ejecuta request, CancellationToken cancellationToken)
      {

        var encuestas = await this.context.Encuesta.ToListAsync();
        return encuestas;
      }
    }
  }
}