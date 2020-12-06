using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Business.ManejadorError;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Models;
using Persistence;

namespace Business.TemasForo {
    public class ConsultaById {

        public class Ejecuta : IRequest<TemaForo> {
            public Guid TemaForoId { get; set; }
        }
        public class Manejador : IRequestHandler<Ejecuta, TemaForo> {
            private readonly UdelarOnlineContext context;

            public Manejador (UdelarOnlineContext context) {
                this.context = context;
            }

            public async Task<TemaForo> Handle (Ejecuta request, CancellationToken cancellationToken) {
                var temaForo = await this.context.TemaForo
                    .FirstOrDefaultAsync (t => t.TemaForoId == request.TemaForoId);
                if (temaForo == null) {
                    throw new ManejadorExcepcion (HttpStatusCode.NotFound, new { mensaje = "No existe un tema de foro con el TemaForoId ingresado" });
                }
                return temaForo;
            }
        }
    }
}