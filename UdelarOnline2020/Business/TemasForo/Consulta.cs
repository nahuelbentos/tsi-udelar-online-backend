using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MediatR;
using Models;
using Persistence;

namespace Business.TemasForo {
    public class Consulta {
        public class Ejecuta : IRequest<List<TemaForo>> { }
        public class Manejador : IRequestHandler<Ejecuta, List<TemaForo>> {
            private readonly UdelarOnlineContext context;

            public Manejador (UdelarOnlineContext context) {
                this.context = context;
            }

            public async Task<List<TemaForo>> Handle (Ejecuta request, CancellationToken cancellationToken) {
                var temasForo = await this.context.TemaForo
                                        .ToListAsync ();
                return temasForo;
            }
        }
    }
}