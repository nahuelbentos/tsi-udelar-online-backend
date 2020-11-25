using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Business.ManejadorError;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using Models;
using Persistence;

namespace Business.Carreras
{
    public class Nuevo
    {
        public class Ejecuta : IRequest
        {
            public String Nombre { get; set; }
            public String Descripcion { get; set; }
            public Guid FacultadId { get; set; }
        }

        public class EjecutaValidator : AbstractValidator<Ejecuta>
        {
            public EjecutaValidator()
            {
                RuleFor(c => c.Nombre).NotEmpty().WithMessage("El Nombre es requerido.");
                RuleFor(c => c.Descripcion).NotEmpty();
                RuleFor(c => c.FacultadId).NotEmpty();
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
                //No se controla si la facultad es vacia, puede arrojar excepcion
                Facultad facultad = await this.context.Facultad.FindAsync(request.FacultadId);
                if(facultad == null)
                    throw new ManejadorExcepcion(HttpStatusCode.InternalServerError, new { mensaje = "No existe la facultad ingresada." });
                var carrera = new Carrera
                {
                    CarreraId = Guid.NewGuid(),
                    Nombre = request.Nombre,
                    Descripcion = request.Descripcion,
                    Facultad = facultad
                };

                this.context.Carrera.Add(carrera);

                var res = await this.context.SaveChangesAsync();
                if (res > 0)
                {
                    return Unit.Value;
                }

                throw new ManejadorExcepcion(HttpStatusCode.InternalServerError, new { mensaje = "Ocurrio un error al insertar la carrera" });
            }
        }
    }
}