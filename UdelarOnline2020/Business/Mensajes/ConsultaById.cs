using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Aplicacion.ManejadorError;
using MediatR;
using Models;
using Persistence;

namespace Business.Mensajes
{
    public class ConsultaById
    {
        public class Ejecuta : IRequest<Mensaje>
        {
            public Guid MensajeId { get; set; }

        }

        public class Manejador : IRequestHandler<Ejecuta, Mensaje>
        {
            private readonly UdelarOnlineContext context;
            public Manejador(UdelarOnlineContext context)
            {
                this.context = context;
            }

            public async Task<Mensaje> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var mensaje = await this.context.Mensaje.FindAsync(request.MensajeId);

                if (mensaje == null)
                    throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { mensaje = "El mensaje no existe. " });

                return mensaje;
            }
        }
    }
}