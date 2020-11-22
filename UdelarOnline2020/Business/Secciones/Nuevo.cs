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

namespace Business.Secciones
{
    public class Nuevo
    {
        public class Ejecuta : IRequest
        {
            public string Nombre { get; set; }

            public string Descripcion { get; set; }

            public bool IsDefault { get; set; }
            public bool IsVisible { get; set; }
        }

        public class EjecutaValidator : AbstractValidator<Ejecuta>
        {
            public EjecutaValidator()
            {
                RuleFor(s => s.Nombre).NotEmpty().WithMessage("El Nombre es requerido.");
                RuleFor(s => s.Descripcion).NotEmpty();
                RuleFor(s => s.IsDefault).NotEmpty();
                RuleFor(s => s.IsVisible).NotEmpty();
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
                var seccion = new Seccion
                {
                    SeccionId = Guid.NewGuid(),
                    Nombre = request.Nombre,
                    Descripcion = request.Descripcion,
                    IsVisible = request.IsVisible,
                    IsDefault = request.IsDefault
                };

                this.context.Seccion.Add(seccion);

                var res = await this.context.SaveChangesAsync();
                if (res > 0)
                {
                    return Unit.Value;
                }

                throw new ManejadorExcepcion(HttpStatusCode.InternalServerError, new { mensaje = "Ocurrio un error al insertar la seccion" });
            }
        }
    }
}