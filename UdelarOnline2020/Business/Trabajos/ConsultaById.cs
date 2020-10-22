using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Aplicacion.ManejadorError;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Models;
using Persistence;

namespace Business.Trabajos {
    public class ConsultaById {

        public class Ejecuta : IRequest<Trabajo> {
            public Guid ActividadId { get; set; }
        }
        public class Manejador : IRequestHandler<Ejecuta, Trabajo> {
            private readonly UdelarOnlineContext context;

            public Manejador (UdelarOnlineContext context) {
                this.context = context;
            }

            public async Task<Trabajo> Handle (Ejecuta request, CancellationToken cancellationToken) {
                // Esto cambia para devolver una lista de DataTypes, en breves lo cambio.
                var trabajo = await this.context.Trabajo
                    .FirstOrDefaultAsync (t => t.ActividadId == request.ActividadId);
                if (trabajo == null) {
                    throw new ManejadorExcepcion (HttpStatusCode.Forbidden, new { mensaje = "No existe un template de curso con el TrabajoId ingresado" });
                }
                return trabajo;
            }
        }
    }
}