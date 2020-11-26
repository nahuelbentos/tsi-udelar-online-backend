using System;
using System.Collections.Generic;
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
    public class Editar
    {
        public class Ejecuta : IRequest
        {
            public Guid AlumnoId { get; set; }
            public Guid PruebaOnlineId { get; set; }
            public DateTime FechaInicio { get; set; }
            public DateTime? FechaFin { get; set; }
            public DateTime? FechaExpiracion { get; set; }
            public int? Nota { get; set; }
            public bool? Inscripto { get; set; }

            public List<RespuestaPrueba> Respuestas { get; set; }
        }

        public class EjecutaValidator : AbstractValidator<Ejecuta>
        {
            public EjecutaValidator()
            {
                RuleFor(ap => ap.AlumnoId).NotEmpty().WithMessage("AlumnoId es requerido.");
                RuleFor(ap => ap.PruebaOnlineId).NotEmpty().WithMessage("PruebaOnlineId es requerido.");
                
                
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
                if (control == null)
                {
                    throw new ManejadorExcepcion(HttpStatusCode.Conflict, new { mensaje = "No existe la relacion entre Alumno y PruebaOnline." });
                }
                
                //hago el edit de los campos que se van a editar
                control.FechaExpiracion = request.FechaExpiracion.GetValueOrDefault();
                control.FechaFin = request.FechaFin ?? control.FechaFin;
                control.Nota = request.Nota ?? control.Nota;
                control.Inscripto = request.Inscripto ?? control.Inscripto;
                control.ListaRespuestas = request.Respuestas;

            
                context.AlumnoPruebaOnline.Update(control);
                var res = await this.context.SaveChangesAsync();
                if (res > 0)
                {
                    return Unit.Value;
                }
                throw new ManejadorExcepcion(HttpStatusCode.InternalServerError, new { mensaje = "Ocurrio un error al editar AlumnoPruebaOnline ingresado." });

            }
        }
    }
}