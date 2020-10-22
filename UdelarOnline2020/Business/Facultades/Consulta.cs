using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Models;
using Persistence;

namespace Business.Facultades
{
  public class Consulta
  {
    public class Ejecuta : IRequest<List<Facultad>> { }

    public class Manejador : IRequestHandler<Ejecuta, List<Facultad>>
    {
      private readonly UdelarOnlineContext context;

      public Manejador(UdelarOnlineContext context)
      {
        this.context = context;
      }


      public async Task<List<Facultad>> Handle(Ejecuta request, CancellationToken cancellationToken)
      {

        var facultades = await this.context.Facultad.ToListAsync();
        return facultades;
      }
    }
  }
}