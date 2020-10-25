using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
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
                var templatesCurso = await this.context.TemplateCurso.ToListAsync ();
                return templatesCurso;
            }
        }
    }
}