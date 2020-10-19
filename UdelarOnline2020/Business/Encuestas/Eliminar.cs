using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Aplicacion.ManejadorError;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Business.Encuestas
{
  public class Eliminar
  {
    public class Ejecuta : IRequest
    {
      public Guid EncuestaId { get; set; }
    }

    public class Manejdaor : IRequestHandler<Ejecuta>
    {
      private readonly UdelarOnlineContext context;

      public Manejdaor(UdelarOnlineContext context)
      {
        this.context = context;
      }

      public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
      {
        var encuesta = await this.context.Encuesta.Where(e => e.ActividadId == request.EncuestaId).FirstOrDefaultAsync();

        if (encuesta == null)
          throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { mensaje = "No existe una encuesta con el Id ingresado" });

        this.context.Encuesta.Remove(encuesta);

        var res = await this.context.SaveChangesAsync();

        if (res > 0)
          return Unit.Value;

        throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { mensaje = "Ocurrió un error al eliminar la encuesta." });

      }
    }


  }
}