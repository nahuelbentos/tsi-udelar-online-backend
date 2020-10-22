using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Aplicacion.ManejadorError;
using FluentValidation;
using MediatR;
using Persistence;

namespace Business.ClasesDictadas {
    public class Editar {
        public class Ejecuta : IRequest {
            public Guid ActividadId { get; set; }
        }

        public class EjecutaValidacion : AbstractValidator<Ejecuta> {
            public EjecutaValidacion () {

            }
        }

        public class Manejador : IRequestHandler<Ejecuta> {
            private readonly UdelarOnlineContext context;

            public Manejador (UdelarOnlineContext context) {
                this.context = context;
            }

            public async Task<Unit> Handle (Ejecuta request, CancellationToken cancellationToken) {
                var claseDictada = await this.context.ClaseDictada.FindAsync (request.ActividadId);

                if (claseDictada == null) {
                    throw new ManejadorExcepcion (HttpStatusCode.NotFound, new { mensaje = "La clase no existe" });

                }


                var res = await this.context.SaveChangesAsync ();

                if (res > 0)
                    return Unit.Value;

                throw new ManejadorExcepcion (HttpStatusCode.InternalServerError, new { mensaje = "Ocurrio un error al editar la clase" });

            }
        }
    }
}