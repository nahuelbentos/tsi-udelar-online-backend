using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Business.ManejadorError;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Models;
using Persistence;

namespace Business.Comunicados {
    public class ConsultaById {

        public class Ejecuta : IRequest<Comunicado> {
            public Guid ComunicadoId { get; set; }
        }
        public class Manejador : IRequestHandler<Ejecuta, Comunicado> {
            private readonly UdelarOnlineContext context;

            public Manejador (UdelarOnlineContext context) {
                this.context = context;
            }

            public async Task<Comunicado> Handle (Ejecuta request, CancellationToken cancellationToken) {
                var comunicado = await this.context.Comunicado
                    .FirstOrDefaultAsync (c => c.ComunicadoId == request.ComunicadoId);
                if (comunicado == null) {
                    throw new ManejadorExcepcion (HttpStatusCode.Forbidden, new { mensaje = "No existe un comunicado con el Id ingresado" });
                }
                return comunicado;
            }
        }
    }
}