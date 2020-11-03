using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Aplicacion.ManejadorError;
using FluentValidation;
using MediatR;
using Persistence;

namespace Business.TemplatesCursoSeccion {
    public class Editar {

        public class Ejecuta : IRequest {
            public Guid TemplateCursoSeccionId { get; set; }
            public Guid TemplateCursoId { get; set; }
            public Guid SeccionId { get; set; }
        }

        public class EjecutaValidacion : AbstractValidator<Ejecuta> {
            public EjecutaValidacion () {
                RuleFor (t => t.TemplateCursoSeccionId).NotEmpty ();
                RuleFor (t => t.TemplateCursoId).NotEmpty ();
                RuleFor (t => t.SeccionId).NotEmpty ();

            }
        }

        public class Manejador : IRequestHandler<Ejecuta> {
            private readonly UdelarOnlineContext context;

            public Manejador (UdelarOnlineContext context) {
                this.context = context;
            }

            public async Task<Unit> Handle (Ejecuta request, CancellationToken cancellationToken) {
                var templateCursoSeccion = await this.context.TemplateCursoSeccion.FindAsync (request.TemplateCursoSeccionId);

                if (templateCursoSeccion == null) {
                    throw new ManejadorExcepcion (HttpStatusCode.NotFound, new { mensaje = "El template de curso no existe" });

                }

                var templateCurso = await this.context.TemplateCurso.FindAsync (request.TemplateCursoId);

                if (templateCurso == null) {
                    throw new ManejadorExcepcion (HttpStatusCode.NotFound, new { mensaje = "El template de curso no existe" });

                }

                var secccion = await this.context.Seccion.FindAsync (request.SeccionId);

                if (secccion == null) {
                    throw new ManejadorExcepcion (HttpStatusCode.NotFound, new { mensaje = "La seccion no existe" });

                }

                templateCursoSeccion.TemplateCursoId = request.TemplateCursoId;
                templateCursoSeccion.TemplateCurso = templateCurso ?? templateCursoSeccion.TemplateCurso;
                templateCursoSeccion.SeccionId = request.SeccionId;
                templateCursoSeccion.Seccion = secccion ?? templateCursoSeccion.Seccion;

                var res = await this.context.SaveChangesAsync ();

                if (res > 0)
                    return Unit.Value;

                throw new ManejadorExcepcion (HttpStatusCode.InternalServerError, new { mensaje = "Ocurrio un error al editar el temaplate de curso" });

            }
        }
    }
}