using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Aplicacion.ManejadorError;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Models;
using Persistence;

namespace Business.ClasesDictadas {
    public class ConsultaById {

        public class Ejecuta : IRequest<ClaseDictada> {
            public Guid ClaseDictadaId { get; set; }
        }
        public class Manejador : IRequestHandler<Ejecuta, ClaseDictada> {
            private readonly UdelarOnlineContext context;

            public Manejador (UdelarOnlineContext context) {
                this.context = context;
            }

            public async Task<ClaseDictada> Handle (Ejecuta request, CancellationToken cancellationToken) {
                // Esto cambia para devolver una lista de DataTypes, en breves lo cambio.
                var claseDictada = await this.context.ClaseDictada
                    .FirstOrDefaultAsync (t => t.ActividadId == request.ClaseDictadaId);
                if (claseDictada == null) {
                    throw new ManejadorExcepcion (HttpStatusCode.Forbidden, new { mensaje = "No existe una clase dictada con ese Id" });
                }
                return claseDictada;
            }
        }
    }
}