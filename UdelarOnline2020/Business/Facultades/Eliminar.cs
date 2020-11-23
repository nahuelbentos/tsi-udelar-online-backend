using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Business.ManejadorError;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Business.Facultades
{
  public class Eliminar
  {

    public class Ejecuta : IRequest
    {
      public Guid FacultadId { get; set; }
    }

    public class Manejador : IRequestHandler<Ejecuta>
    {
      private readonly UdelarOnlineContext context;

      public Manejador(UdelarOnlineContext context)
      {
        this.context = context;
      }

      public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
      {

        var facultad = await this.context.Facultad.Where(f => f.FacultadId == request.FacultadId).FirstOrDefaultAsync();

        if (facultad == null)
          throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { mensaje = "No existe una facultad con el Id ingresado" });



        this.context.Facultad.Remove(facultad);

        var res = await this.context.SaveChangesAsync();

        if (res > 0)
          return Unit.Value;

        throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { mensaje = "Ocurrió un error al eliminar la facultad." });
      }
    }

  }
}