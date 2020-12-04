using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Business.ManejadorError;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Business.Alumnos
{
    public class EstaInscriptoEvaluacion
    {
        public class Ejecuta : IRequest<bool>
        {
            public string AlumnoId { get; set; }
            public Guid PruebaOnlineId { get; set; }
        }

    public class Manejador : IRequestHandler<Ejecuta, bool>
    {
      private readonly UdelarOnlineContext context;

      public Manejador(UdelarOnlineContext context )
      {
        this.context = context;
      }

      public async Task<bool> Handle(Ejecuta request, CancellationToken cancellationToken)
      {

        var alumno = await this.context.Alumno.FindAsync(request.AlumnoId);
        if (alumno == null)
          throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { mensaje = "El alumno no existe." });

        var pruebaOnline = await this.context.PruebaOnline.FindAsync(request.PruebaOnlineId);
        if (pruebaOnline == null)
          throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { mensaje = "La Prueba Online no existe." });

        var estaInscripto = await this.context.AlumnoPruebaOnline
                                                .Where( apo => apo.Alumno.Id == request.AlumnoId && apo.PruebaOnlineId == request.PruebaOnlineId)  
                                                .FirstOrDefaultAsync();

        return (estaInscripto != null);
      }
    }
  }
}