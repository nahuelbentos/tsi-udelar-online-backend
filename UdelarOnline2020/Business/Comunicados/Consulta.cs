using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
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
                var comunicados = await this.context.Comunicado.ToListAsync ();
                return comunicados;
            }
        }
    }
}