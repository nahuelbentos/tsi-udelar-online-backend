using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Aplicacion.ManejadorError;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using Models;
using Persistence;

namespace Business.Actividades
{
    public class Nuevo
    {
        public class Ejecuta : IRequest
        {
            public DateTime FechaRealizada { get; set; }
            public DateTime FechaFinalizada { get; set; }
        }

        public class EjecutaValidator : AbstractValidator<Ejecuta>
        {
            public EjecutaValidator()
            {
                RuleFor(a => a.FechaRealizada).NotEmpty();
                RuleFor(a => a.FechaFinalizada).NotEmpty();
            }
        }

        public class Manejador : IRequestHandler<Ejecuta>
        {
            private readonly UdelarOnlineContext context;
            private readonly ILogger<Manejador> logger;

            public Manejador(UdelarOnlineContext context, ILogger<Manejador> logger)
            {
                this.context = context;
                this.logger = logger;
            }

            public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var actividad = new Actividad
                {
                    ActividadId = Guid.NewGuid(),
                    FechaRealizada = request.FechaRealizada,
                    FechaFinalizada = request.FechaFinalizada
                };

                this.context.Actividad.Add(actividad);

                var res = await this.context.SaveChangesAsync();
                if (res > 0)
                {
                    return Unit.Value;
                }

                throw new ManejadorExcepcion(HttpStatusCode.InternalServerError, new { mensaje = "Ocurrio un error al insertar la actividad" });
            }
        }
    }
}