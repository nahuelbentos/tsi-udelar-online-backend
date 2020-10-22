using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Aplicacion.ManejadorError;
using FluentValidation;
using MediatR;
using Persistence;

namespace Business.TemplatesCurso {
    public class Editar {
        public class Ejecuta : IRequest {
            public Guid TemplateCursoId { get; set; }
            public string Nombre { get; set; }
            public string Descripcion { get; set; }
        }

        public class EjecutaValidacion : AbstractValidator<Ejecuta> {
            public EjecutaValidacion () {
                RuleFor (t => t.Nombre).NotEmpty ().WithMessage ("El nombre es requerido");
                RuleFor (t => t.Descripcion).NotEmpty ().WithMessage ("");

            }
        }

        public class Manejador : IRequestHandler<Ejecuta> {
            private readonly UdelarOnlineContext context;

            public Manejador (UdelarOnlineContext context) {
                this.context = context;
            }

            public async Task<Unit> Handle (Ejecuta request, CancellationToken cancellationToken) {
                var templateCurso = await this.context.TemplateCurso.FindAsync (request.TemplateCursoId);

                if (templateCurso == null) {
                    throw new ManejadorExcepcion (HttpStatusCode.NotFound, new { mensaje = "El template de curso no existe" });

                }


                templateCurso.Nombre = request.Nombre ?? templateCurso.Nombre;
                templateCurso.Descripcion = request.Descripcion ?? templateCurso.Descripcion;

                var res = await this.context.SaveChangesAsync ();

                if (res > 0)
                    return Unit.Value;

                throw new ManejadorExcepcion (HttpStatusCode.InternalServerError, new { mensaje = "Ocurrio un error al editar el temaplate de curso" });

            }
        }
    }
}