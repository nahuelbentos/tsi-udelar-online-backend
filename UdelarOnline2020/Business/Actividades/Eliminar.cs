using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Business.ManejadorError;
using FluentValidation;
using MediatR;
using Persistence;

namespace Business.Actividades
{
    public class Eliminar
    {
        public class Ejecuta : IRequest
        {
            public Guid ActividadId { get; set; }
        }

        public class EjecutaValidator : AbstractValidator<Ejecuta>
        {
            public EjecutaValidator()
            {
                RuleFor(a => a.ActividadId).NotEmpty().WithMessage("Es necesario el ActividadId para eliminar una actividad");
            }
        }

        public class Manejador : IRequestHandler<Ejecuta>
        {
            private readonly UdelarOnlineContext context;

            public Manejador(UdelarOnlineContext context)
            {
                this.context = context;
            }

            public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var actividad = await this.context.Actividad.FindAsync(request.ActividadId);
                if (actividad == null)
                {
                    throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { mensaje = "La actividad no existe" });
                }
                this.context.Actividad.Remove(actividad);

                var res = await this.context.SaveChangesAsync();

                if (res > 0)
                {
                    return Unit.Value;
                }
                throw new ManejadorExcepcion(HttpStatusCode.InternalServerError, new { mensaje = "Ocurrio un error al eliminar la actividad" });
            }
        }
    }
}