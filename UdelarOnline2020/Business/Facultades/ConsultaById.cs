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

namespace Business.Facultades
{
  public class ConsultaById
  {

    public class Ejecuta : IRequest<Facultad>
    {
      public Guid FacultadId { get; set; }
    }

    public class Manejador : IRequestHandler<Ejecuta, Facultad>
    {
      private readonly UdelarOnlineContext context;

      public Manejador(UdelarOnlineContext context)
      {
        this.context = context;
      }


      public async Task<Facultad> Handle(Ejecuta request, CancellationToken cancellationToken)
      {

        var facultad = await this.context.Facultad.Where(f => f.FacultadId == request.FacultadId).FirstOrDefaultAsync();

        if (facultad == null)
          throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { mensaje = "No existe una facultad con el Id ingresado" });

        return facultad;
      }
    }

  }
}