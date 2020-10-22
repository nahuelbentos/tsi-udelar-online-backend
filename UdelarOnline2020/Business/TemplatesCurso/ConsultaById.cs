using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Aplicacion.ManejadorError;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Models;
using Persistence;

namespace Business.TemplatesCurso {
    public class ConsultaById {

        public class Ejecuta : IRequest<TemplateCurso> {
            public Guid TemplateCursoId { get; set; }
        }
        public class Manejador : IRequestHandler<Ejecuta, TemplateCurso> {
            private readonly UdelarOnlineContext context;

            public Manejador (UdelarOnlineContext context) {
                this.context = context;
            }

            public async Task<TemplateCurso> Handle (Ejecuta request, CancellationToken cancellationToken) {
                // Esto cambia para devolver una lista de DataTypes, en breves lo cambio.
                var templateCurso = await this.context.TemplateCurso
                    .FirstOrDefaultAsync (t => t.TemplateCursoId == request.TemplateCursoId);
                if (templateCurso == null) {
                    throw new ManejadorExcepcion (HttpStatusCode.Forbidden, new { mensaje = "No existe un template de curso con el TemplateCursoId ingresado" });
                }
                return templateCurso;
            }
        }
    }
}