using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Aplicacion.ManejadorError;
using FluentValidation;
using MediatR;
using Persistence;

namespace Business.Trabajos {
    public class Editar {
        public class Ejecuta : IRequest {
            public Guid ActividadId { get; set; }
            public bool EsIndividual { get; set; }
            public int Calificacion  { get; set; }
            public string Nota {get; set;}
        }

        public class EjecutaValidacion : AbstractValidator<Ejecuta> {
            public EjecutaValidacion () {
                RuleFor (t => t.Calificacion).NotEmpty ().WithMessage ("El nombre es requerido");
                RuleFor (t => t.Nota).NotEmpty ().WithMessage ("");

            }
        }

        public class Manejador : IRequestHandler<Ejecuta> {
            private readonly UdelarOnlineContext context;

            public Manejador (UdelarOnlineContext context) {
                this.context = context;
            }

            public async Task<Unit> Handle (Ejecuta request, CancellationToken cancellationToken) {
                var trabajo = await this.context.Trabajo.FindAsync (request.ActividadId);

                if (trabajo == null) {
                    throw new ManejadorExcepcion (HttpStatusCode.NotFound, new { mensaje = "El trabajo no existe" });

                }


                trabajo.Calificacion = request.Calificacion;
                trabajo.Nota = request.Nota ?? trabajo.Nota;

                var res = await this.context.SaveChangesAsync ();

                if (res > 0)
                    return Unit.Value;

                throw new ManejadorExcepcion (HttpStatusCode.InternalServerError, new { mensaje = "Ocurrio un error al editar el trabajo" });

            }
        }
    }
}