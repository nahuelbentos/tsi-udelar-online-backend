using System.Net;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Aplicacion.ManejadorError;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Models;
using Persistence;

namespace Business.Cursos
{
  public class ConsultaById
  {
    public class Ejecuta : IRequest<Curso>
    {
      public Guid CursoId { get; set; }
    }
    public class Manejador : IRequestHandler<Ejecuta, Curso>
    {
      private readonly UdelarOnlineContext context;

      public Manejador(UdelarOnlineContext context)
      {
        this.context = context;
      }

      public async Task<Curso> Handle(Ejecuta request, CancellationToken cancellationToken)
      {
        // Esto cambia para devolver una lista de DataTypes, en breves lo cambio.
        var curso = await this.context.Curso
                                      .FirstOrDefaultAsync(c => c.CursoId == request.CursoId);
        if (curso == null)
        {
          throw new ManejadorExcepcion(HttpStatusCode.Forbidden, new { mensaje = "No existe un curso con el CursoId ingresado" });
        }
        return curso;
      }

    }
  }
}