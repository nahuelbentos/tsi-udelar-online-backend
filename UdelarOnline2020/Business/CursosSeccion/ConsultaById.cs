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

namespace Business.CursosSeccion
{
  public class ConsultaById
  {
    public class Ejecuta : IRequest<CursoSeccion>
    {
      public Guid CursoId { get; set; }
    }
    public class Manejador : IRequestHandler<Ejecuta, CursoSeccion>
    {
      private readonly UdelarOnlineContext context;

      public Manejador(UdelarOnlineContext context)
      {
        this.context = context;
      }

      public async Task<CursoSeccion> Handle(Ejecuta request, CancellationToken cancellationToken)
      {
        // Esto cambia para devolver una lista de DataTypes, en breves lo cambio.
        var curso = await this.context.CursoSeccion.FirstOrDefaultAsync(c => c.CursoId == request.CursoId);
        if (curso == null)
        {
          throw new ManejadorExcepcion(HttpStatusCode.Forbidden, new { mensaje = "No existe un curso con el CursoId ingresado" });
        }
        return curso;
      }

    }
  }
}