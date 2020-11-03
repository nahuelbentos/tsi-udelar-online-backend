using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Models;
using Persistence;

namespace Business.CursosSeccion
{
  public class Consulta
  {
    public class Ejecuta : IRequest<List<CursoSeccion>> { }
    public class Manejador : IRequestHandler<Ejecuta, List<CursoSeccion>>
    {
      private readonly UdelarOnlineContext context;

      public Manejador(UdelarOnlineContext context)
      {
        this.context = context;
      }

      public async Task<List<CursoSeccion>> Handle(Ejecuta request, CancellationToken cancellationToken)
      {
        // Esto cambia para devolver una lista de DataTypes, en breves lo cambio.
        var cursos = await this.context.CursoSeccion
                                      .ToListAsync();
        return cursos;
      }
    }


  }
}