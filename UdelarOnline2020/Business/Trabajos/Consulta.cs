using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MediatR;
using Models;
using Persistence;

namespace Business.Trabajos {
    public class Consulta {
        public class Ejecuta : IRequest<List<Trabajo>> { }
        public class Manejador : IRequestHandler<Ejecuta, List<Trabajo>> {
            private readonly UdelarOnlineContext context;

            public Manejador (UdelarOnlineContext context) {
                this.context = context;
            }

            public async Task<List<Trabajo>> Handle (Ejecuta request, CancellationToken cancellationToken) {
                // Esto cambia para devolver una lista de DataTypes, en breves lo cambio.
                var trabajos = await this.context.Trabajo
                                        .ToListAsync ();
                return trabajos;
            }
        }
    }
}