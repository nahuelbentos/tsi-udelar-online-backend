using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Aplicacion.ManejadorError;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using Persistence;

namespace Business.Materiales
{
    public class Eliminar
    {
        public class Ejecuta : IRequest
        {
            public Guid MaterialId { get; set; }

        }

        public class EjecutaValidator : AbstractValidator<Ejecuta>
        {
        public EjecutaValidator()
        {
            RuleFor(c => c.MaterialId).NotEmpty().WithMessage("Es necesario el MaterialId para eliminar un material.");
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
                var material = await this.context.Material.FindAsync(request.MaterialId);

                if (material == null)
                    throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { mensaje = "El material no existe. " });

                this.context.Material.Remove(material);

                var res = await this.context.SaveChangesAsync();

                if (res > 0)
                    return Unit.Value;

                throw new ManejadorExcepcion(HttpStatusCode.InternalServerError, new { mensaje = "Ocurrio un error al eliminar el material" });
            }
        }
    }
}