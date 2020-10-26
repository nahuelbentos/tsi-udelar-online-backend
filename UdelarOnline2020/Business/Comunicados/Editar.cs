using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Aplicacion.ManejadorError;
using FluentValidation;
using MediatR;
using Persistence;

namespace Business.Comunicados {
    public class Editar {
        public class Ejecuta : IRequest {
            public Guid ComunicadoId { get; set; }
            public string Nombre { get; set; }
            public string Descripcion { get; set; }
            public string Url { get; set; }
        }

        public class EjecutaValidacion : AbstractValidator<Ejecuta> {
            public EjecutaValidacion () {
                RuleFor (t => t.Nombre).NotEmpty ().WithMessage ("El nombre es requerido");
                RuleFor (t => t.Descripcion).NotEmpty ();
                RuleFor (t => t.Url).NotEmpty ();
            }
        }

        public class Manejador : IRequestHandler<Ejecuta> {
            private readonly UdelarOnlineContext context;

            public Manejador (UdelarOnlineContext context) {
                this.context = context;
            }

            public async Task<Unit> Handle (Ejecuta request, CancellationToken cancellationToken) {
                var comunicado = await this.context.Comunicado.FindAsync (request.ComunicadoId);

                if (comunicado == null) {
                    throw new ManejadorExcepcion (HttpStatusCode.NotFound, new { mensaje = "El comunicado no existe" });

                }

                comunicado.Nombre = request.Nombre ?? comunicado.Nombre;
                comunicado.Descripcion = request.Descripcion ?? comunicado.Descripcion;
                comunicado.Url = request.Url ?? comunicado.Url;

                var res = await this.context.SaveChangesAsync ();

                if (res > 0)
                    return Unit.Value;

                throw new ManejadorExcepcion (HttpStatusCode.InternalServerError, new { mensaje = "Ocurrio un error al editar el comunicado" });

            }
        }
    }
}