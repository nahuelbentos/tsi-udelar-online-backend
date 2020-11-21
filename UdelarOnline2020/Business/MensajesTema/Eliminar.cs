using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Business.ManejadorError;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using Persistence;

namespace Business.MensajesTema
{
    public class Eliminar
    {
        public class Ejecuta : IRequest
        {
            public Guid MensajeId { get; set; }

        }

        public class EjecutaValidator : AbstractValidator<Ejecuta>
        {
        public EjecutaValidator()
        {
            RuleFor(c => c.MensajeId).NotEmpty().WithMessage("Es necesario el MensajeId para eliminar un MensajeTema.");
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
                var mensajeTema = await this.context.MensajeTema.FindAsync(request.MensajeId);

                if (mensajeTema == null)
                    throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { mensaje = "El mensaje tema no existe. " });

                this.context.MensajeTema.Remove(mensajeTema);

                var res = await this.context.SaveChangesAsync();

                if (res > 0)
                    return Unit.Value;

                throw new ManejadorExcepcion(HttpStatusCode.InternalServerError, new { mensaje = "Ocurrio un error al eliminar el mensaje tema" });
            }
        }
    }
}