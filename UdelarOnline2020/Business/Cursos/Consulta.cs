using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Models;
using Persistence;

namespace Business.Cursos
{
  public class Consulta
  {
    public class Ejecuta : IRequest<List<Curso>> { }
    public class Manejador : IRequestHandler<Ejecuta, List<Curso>>
    {
      private readonly UdelarOnlineContext context;

      public Manejador(UdelarOnlineContext context)
      {
        this.context = context;
      }

      public async Task<List<Curso>> Handle(Ejecuta request, CancellationToken cancellationToken)
      {
        // Esto cambia para devolver una lista de DataTypes, en breves lo cambio.
        var cursos = await this.context.Curso.ToListAsync();
        return cursos;
      }
    }


  }
}