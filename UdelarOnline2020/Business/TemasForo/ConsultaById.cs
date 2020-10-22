using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Aplicacion.ManejadorError;
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
                // Esto cambia para devolver una lista de DataTypes, en breves lo cambio.
                var temaForo = await this.context.TemaForo
                    .FirstOrDefaultAsync (t => t.TemaForoId == request.TemaForoId);
                if (temaForo == null) {
                    throw new ManejadorExcepcion (HttpStatusCode.Forbidden, new { mensaje = "No existe un curso con el CursoId ingresado" });
                }
                return temaForo;
            }
        }
    }
}