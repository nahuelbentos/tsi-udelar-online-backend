using System;
using System.Threading;
using System.Threading.Tasks;
using Aplicacion.ManejadorError;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using Persistence;
using Models;
using System.Net;

namespace Business.Foros
{
    public class Nuevo
    {
        public class Ejecuta : IRequest
        {
            public String Titulo { get; set; }
            public String Descripcion { get; set; }

        }
    

        public class EjecutaValidator : AbstractValidator<Ejecuta>
        {
        public EjecutaValidator()
        {
            RuleFor(c => c.Titulo).NotEmpty().WithMessage("El Titulo es requerido.");
            RuleFor(c => c.Descripcion).NotEmpty();
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
                var foro = new Foro
                {
                    ForoId = Guid.NewGuid(),
                    Titulo = request.Titulo,
                    Descripcion = request.Descripcion
                };

                this.context.Foro.Add(foro);

                var res = await this.context.SaveChangesAsync();
                if (res > 0)
                    return Unit.Value;
                throw new ManejadorExcepcion(HttpStatusCode.InternalServerError, new { mensaje = "Ocurrio un error al insertar el foro "});
            }
        }
    }
}
