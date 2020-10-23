using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Aplicacion.ManejadorError;
using MediatR;
using Models;
using Persistence;

namespace Business.Respuestas
{
    public class ConsultaByEncuesta
    {
        public class Ejecuta : IRequest<Respuesta>
        {
            
            //public Guid RespuestaId { get; set; }
            public Guid ActividadId { get; set; }

        }

        public class Manejador : IRequestHandler<Ejecuta, Respuesta>
        {
            private readonly UdelarOnlineContext context;

            public Manejador(UdelarOnlineContext context)
            {
                this.context = context;
            }

            public async Task<Respuesta> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                //var resp = await this.context.Encuesta.Where(enc => enc.RespuestaLista.FirstOrDefault<Respuesta>(request.RespuestaId) == request.RespuestaId).AnyAsync();
                var respuesta = await this.context.Respuesta.FindAsync(request.ActividadId);
                if (respuesta == null)
                {
                    throw new ManejadorExcepcion(HttpStatusCode.BadRequest, new { mensaje = "No se encontro la respuesta para esa encuesta" });
                }
                return respuesta;
            }
        }
    }
}