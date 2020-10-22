using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MediatR;
using Models;
using Persistence;

namespace Business.ClasesDictadas {
    public class Consulta {
        public class Ejecuta : IRequest<List<ClaseDictada>> { }
        public class Manejador : IRequestHandler<Ejecuta, List<ClaseDictada>> {
            private readonly UdelarOnlineContext context;

            public Manejador (UdelarOnlineContext context) {
                this.context = context;
            }

            public async Task<List<ClaseDictada>> Handle (Ejecuta request, CancellationToken cancellationToken) {
                // Esto cambia para devolver una lista de DataTypes, en breves lo cambio.
                var claseDictada = await this.context.ClaseDictada
                                        .ToListAsync ();
                return claseDictada;
            }
        }
    }
}