using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MediatR;
using Models;
using Persistence;

namespace Business.TemplatesCurso {
    public class Consulta {
        public class Ejecuta : IRequest<List<TemplateCurso>> { }
        public class Manejador : IRequestHandler<Ejecuta, List<TemplateCurso>> {
            private readonly UdelarOnlineContext context;

            public Manejador (UdelarOnlineContext context) {
                this.context = context;
            }

            public async Task<List<TemplateCurso>> Handle (Ejecuta request, CancellationToken cancellationToken) {
                // Esto cambia para devolver una lista de DataTypes, en breves lo cambio.
                var templatesCurso = await this.context.TemplateCurso
                                        .ToListAsync ();
                return templatesCurso;
            }
        }
    }
}