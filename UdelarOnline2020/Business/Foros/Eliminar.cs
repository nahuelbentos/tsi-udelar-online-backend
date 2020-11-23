using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Business.ManejadorError;
using FluentValidation;
using MediatR;
using Persistence;

namespace Business.Foros
{
    public class Eliminar
    {
        public class Ejecuta : IRequest
        {
            public Guid ForoId { get; set; }
        }

        public class EjecutaValidator : AbstractValidator<Ejecuta>
        {
            public EjecutaValidator()
            {
                RuleFor(f => f.ForoId).NotEmpty().WithMessage("Es necesario el ForoId para eliminar un curso");
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
                var foro = await this.context.Foro.FindAsync(request.ForoId);

                if (foro == null)
                    throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { mensaje = "El foro no existe" } );
                this.context.Foro.Remove(foro);

                var res = await this.context.SaveChangesAsync();

                if (res > 0)
                    return Unit.Value;
                
                throw new ManejadorExcepcion(HttpStatusCode.InternalServerError, new { mensaje = "Ocurrio un error al eliminar el foro" });
            }
        }
    }
}