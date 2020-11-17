using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Aplicacion.ManejadorError;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Models;
using Persistence;

namespace Business.TemplatesCursoSeccion {

    public class ConsultaBySeccion {

        public class Ejecuta : IRequest<TemplateCursoSeccion> {
            public Guid SeccionId { get; set; }
        }
        public class Manejador : IRequestHandler<Ejecuta, TemplateCursoSeccion> {
            private readonly UdelarOnlineContext context;

            public Manejador (UdelarOnlineContext context) {
                this.context = context;
            }

            public async Task<TemplateCursoSeccion> Handle (Ejecuta request, CancellationToken cancellationToken) {
                var templateCursoSeccion = await this.context.TemplateCursoSeccion
                    .Include(t => t.Seccion).Include(t => t.TemplateCurso).FirstOrDefaultAsync (t => t.SeccionId == request.SeccionId);
                if (templateCursoSeccion == null) {
                    throw new ManejadorExcepcion (HttpStatusCode.Forbidden, new { mensaje = "No existe un template de curso con el TemplateCursoId ingresado" });
                }
                return templateCursoSeccion;
            }
        }
    }
}