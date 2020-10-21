using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Aplicacion.ManejadorError;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Business.Actividades
{
    public class Editar
    {
        public class Ejecuta : IRequest
        {
            public Guid ActividadId { get; set; }
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
            
            public Manejador(UdelarOnlineContext context)
            {
                this.context = context;
            }

            public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var actividad = await this.context.Actividad.FindAsync(request.ActividadId);
                if (actividad == null)
                {
                    throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { mensaje = "No existe la actividad ingresada." });
                }
                                                    
                actividad.FechaRealizada = request.FechaRealizada ?? actividad.FechaRealizada;
                actividad.FechaFinalizada = request.FechaFinalizada ?? actividad.FechaFinalizada;

                var result = await this.context.SaveChangesAsync();
                if (result > 0)
                {
                    return Unit.Value;
                }
                throw new ManejadorExcepcion(HttpStatusCode.InternalServerError, new { mensaje = "No se pudo editar la actividad" });
            }
        }
    }
}