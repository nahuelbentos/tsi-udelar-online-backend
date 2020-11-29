using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Business.ManejadorError;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Models;
using Persistence;

namespace Business.AlumnoPruebaOnlines
{
    public class Nuevo
    {
        public class Ejecuta : IRequest
        {
            public Guid AlumnoId { get; set; }
            public Guid PruebaOnlineId { get; set; }
            public DateTime FechaInicio { get; set; }
            public DateTime? FechaFin { get; set; }
            public DateTime FechaExpiracion { get; set; }
            public int? Nota { get; set; }
            public bool? Inscripto { get; set; }
        }

        public class EjecutaValidator : AbstractValidator<Ejecuta>
        {
            public EjecutaValidator()
            {
                RuleFor(ap => ap.AlumnoId).NotEmpty().WithMessage("AlumnoId es requerido.");
                RuleFor(ap => ap.PruebaOnlineId).NotEmpty().WithMessage("PruebaOnlineId es requerido.");
                RuleFor(ap => ap.FechaInicio).NotEmpty().WithMessage("FechaInicio es requerido.");
                RuleFor(ap => ap.FechaExpiracion).NotEmpty().WithMessage("FechaExpiracion es requerido.");    
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
                var alumno = await this.context.Alumno.Where(a => a.Id == request.AlumnoId.ToString()).FirstOrDefaultAsync();

                if (alumno == null)
                {
                    throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { mensaje = "No existe el alumno ingresado" });
                }

                var pruebaOnline = (PruebaOnline) await this.context.Actividad.FindAsync(request.PruebaOnlineId);

                if (pruebaOnline == null)
                {
                    throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { mensaje = "No existe la pruebaOnline ingresada" });
                }

                var control = await this.context.AlumnoPruebaOnline.Where(ac => ac.PruebaOnlineId == request.PruebaOnlineId && ac.AlumnoId == request.AlumnoId).FirstOrDefaultAsync();
                if (control != null)
                {
                    throw new ManejadorExcepcion(HttpStatusCode.Conflict, new { mensaje = "Ya existe la relacion entre Alumno y PruebaOnline." });
                }
                
                var ac = new AlumnoPruebaOnline
                {
                    AlumnoId = request.AlumnoId,
                    Alumno = alumno,
                    PruebaOnlineId = request.PruebaOnlineId,
                    PruebaOnline = pruebaOnline,
                    FechaInicio = request.FechaInicio,
                    FechaFin = request.FechaFin.GetValueOrDefault(),
                    FechaExpiracion = request.FechaExpiracion,
                    Nota = request.Nota.GetValueOrDefault(),
                    Inscripto = request.Inscripto.GetValueOrDefault()
                };

                context.AlumnoPruebaOnline.Add(ac);
                var res = await this.context.SaveChangesAsync();
                if (res > 0)
                {
                    return Unit.Value;
                }
                throw new ManejadorExcepcion(HttpStatusCode.InternalServerError, new { mensaje = "Ocurrio un error al inscribir el alumno la prueba." });

            }
        }
    }
}