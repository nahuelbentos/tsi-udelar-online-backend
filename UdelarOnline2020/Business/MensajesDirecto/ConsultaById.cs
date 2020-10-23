using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Aplicacion.ManejadorError;
using MediatR;
using Models;
using Persistence;

namespace Business.MensajesDirecto
{
    public class ConsultaById
    {
        public class Ejecuta : IRequest<MensajeDirecto>
        {
            public Guid MensajeId { get; set; }

        }

        public class Manejador : IRequestHandler<Ejecuta, MensajeDirecto>
        {
            private readonly UdelarOnlineContext context;
            public Manejador(UdelarOnlineContext context)
            {
                this.context = context;
            }

            public async Task<MensajeDirecto> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var mensajeDirecto = await this.context.MensajeDirecto.FindAsync(request.MensajeId);

                if (mensajeDirecto == null)
                    throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { mensaje = "El mensaje directo no existe. " });

                return mensajeDirecto;
            }
        }
    }
}