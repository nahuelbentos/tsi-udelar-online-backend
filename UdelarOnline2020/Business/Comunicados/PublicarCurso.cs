using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Models;
using Persistence;

namespace Business.Comunicados {
    public class PublicarCurso {
        public class Ejecuta : IRequest {

            public Guid ComunicadoId { get; set; }

            public Comunicado Comunicado { get; set; }

            public Guid CursoId { get; set; }

            public Curso Curso { get; set; }

        }

       public class EjecutaValidator : AbstractValidator<Ejecuta> {
            public EjecutaValidator () {
                RuleFor (c => c.ComunicadoId).NotEmpty ().WithMessage ("El Id del comunicado es requerido.");
                RuleFor (c => c.CursoId).NotEmpty ().WithMessage ("El Id del curso es requerido.");
            }
        }

        public class Manejador : IRequestHandler<Ejecuta> {
            private readonly UdelarOnlineContext context;

            public Manejador (UdelarOnlineContext context) {
                this.context = context;
            }

            public async Task<Unit> Handle (Ejecuta request, CancellationToken cancellationToken) {
                
                var comunicado = new ComunicadoCurso {
                    ComunicadoId = Guid.NewGuid (),
                    Comunicado = request.Comunicado,
                    CursoId = request.CursoId,
                    Curso = request.Curso
                };

                this.context.ComunicadoCurso.Add (comunicado);

                var res = await this.context.SaveChangesAsync ();
                if (res > 0) {
                    return Unit.Value;
                }

                throw new Exception ("No se pudo publicar el comunicado en el curso");

            }
        }
    }
}