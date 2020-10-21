using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Aplicacion.ManejadorError;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Models;
using Persistence;

namespace Business.Encuestas
{
  public class ConsultaById
  {
    public class Ejecuta : IRequest<Encuesta>
    {
      public Guid EncuestaId { get; set; }
    }

    public class Manejador : IRequestHandler<Ejecuta, Encuesta>
    {
      private readonly UdelarOnlineContext context;

      public Manejador(UdelarOnlineContext context)
      {
        this.context = context;
      }

      public async Task<Encuesta> Handle(Ejecuta request, CancellationToken cancellationToken)
      {
        var encuesta = await this.context.Encuesta.Where(e => e.ActividadId == request.EncuestaId).FirstOrDefaultAsync();

        if (encuesta == null)
          throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { mensaje = "No existe una encuesta con el Id ingresado" });

        return encuesta;

      }
    }


  }
}