using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Business.ManejadorError;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using Persistence;

namespace Business.Respuestas
{
    public class Eliminar
    {
        public class Ejecuta : IRequest
        {
            public Guid RespuestaId { get; set; }

        }

        public class EjecutaValidator : AbstractValidator<Ejecuta>
        {
        public EjecutaValidator()
        {
            RuleFor(c => c.RespuestaId).NotEmpty().WithMessage("Es necesario el RespuestaId para eliminar una respuesta.");
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
                var respuesta = await this.context.Respuesta.FindAsync(request.RespuestaId);

                if (respuesta == null)
                    throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { mensaje = "La respuesta no existe. " });

                this.context.Respuesta.Remove(respuesta);

                var res = await this.context.SaveChangesAsync();

                if (res > 0)
                    return Unit.Value;

                throw new ManejadorExcepcion(HttpStatusCode.InternalServerError, new { mensaje = "Ocurrio un error al eliminar la respuesta" });
            }
        }
    }
}