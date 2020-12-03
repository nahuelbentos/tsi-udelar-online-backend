using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Business.ManejadorError;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Models;
using Persistence;

namespace Business.Comunicados {
    public class PublicarCurso {
        public class Ejecuta : IRequest {

            public Guid ComunicadoId { get; set; }

            //public Comunicado Comunicado { get; set; }

            public Guid CursoId { get; set; }

            //public Curso Curso { get; set; }

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
                var comunicadoExiste = await this.context.Comunicado.Where(e => e.ComunicadoId == request.ComunicadoId).FirstOrDefaultAsync();
      
                if (comunicadoExiste == null)
                {
                    throw new ManejadorExcepcion(HttpStatusCode.BadRequest, new { mensaje = "No existe el comunicado ingresado" });
                }

                var curso = await this.context.Curso.Where(e => e.CursoId == request.CursoId).FirstOrDefaultAsync();
      
                if (comunicadoExiste == null)
                {
                    throw new ManejadorExcepcion(HttpStatusCode.BadRequest, new { mensaje = "No existe el curso ingresado" });
                }
                var comunicado = new ComunicadoCurso {
                    ComunicadoId = Guid.NewGuid (),
                    Comunicado = comunicadoExiste,
                    CursoId = request.CursoId,
                    Curso = curso
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