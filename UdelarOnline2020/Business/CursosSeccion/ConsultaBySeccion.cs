using System.Net;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Business.ManejadorError;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Models;
using Persistence;

namespace Business.CursosSeccion
{
  public class ConsultaBySeccion
  {
    public class Ejecuta : IRequest<CursoSeccion>
    {
      public Guid SeccionId { get; set; }
      public Guid CursoId {get; set;}
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
         var cursoSeccion = await this.context.CursoSeccion.Where (tc => tc.CursoId == request.CursoId && tc.SeccionId == request.SeccionId).FirstOrDefaultAsync ();
        if (cursoSeccion == null)
        {
          throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { mensaje = "No existe un curso con el CursoId ingresado" });
        }
        return cursoSeccion;
      }

    }
  }
}