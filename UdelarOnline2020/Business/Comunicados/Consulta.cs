using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MediatR;
using Models;
using Persistence;

namespace Business.Comunicados {
    public class Consulta {
        public class Ejecuta : IRequest<List<Comunicado>> { }
        public class Manejador : IRequestHandler<Ejecuta, List<Comunicado>> {
            private readonly UdelarOnlineContext context;

            public Manejador (UdelarOnlineContext context) {
                this.context = context;
            }

            public async Task<List<Comunicado>> Handle (Ejecuta request, CancellationToken cancellationToken) {
                // Esto cambia para devolver una lista de DataTypes, en breves lo cambio.
                var templatesCurso = await this.context.Comunicado
                                        .ToListAsync ();
                return templatesCurso;
            }
        }
    }
}