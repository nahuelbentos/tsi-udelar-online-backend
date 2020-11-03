using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Models;
using Persistence;

namespace Business.TemplatesCursoSeccion {
    public class Consulta {

        public class Ejecuta : IRequest<List<TemplateCursoSeccion>> { }
        public class Manejador : IRequestHandler<Ejecuta, List<TemplateCursoSeccion>> {
            private readonly UdelarOnlineContext context;

            public Manejador (UdelarOnlineContext context) {
                this.context = context;
            }

            public async Task<List<TemplateCursoSeccion>> Handle (Ejecuta request, CancellationToken cancellationToken) {
                var templatesCursoSeccion = await this.context.TemplateCursoSeccion.ToListAsync ();
                return templatesCursoSeccion;
            }
        }
    }
}