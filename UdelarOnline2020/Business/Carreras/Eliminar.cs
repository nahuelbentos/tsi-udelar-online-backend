using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Aplicacion.ManejadorError;
using FluentValidation;
using MediatR;
using Persistence;

namespace Business.Carreras
{
    public class Eliminar
    {
        public class Ejecuta : IRequest
        {
            public Guid CarreraId { get; set; }
        }

        public class EjecutaValidator : AbstractValidator<Ejecuta>
        {
            public EjecutaValidator()
            {
                RuleFor(c => c.CarreraId).NotEmpty().WithMessage("Es necesario el CarreraId para eliminar una carrera");
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
                var carrera = await this.context.Carrera.FindAsync(request.CarreraId);

                if (carrera == null)
                {
                    throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { mensaje = "La carrera no existe" });
                }
                this.context.Carrera.Remove(carrera);

                var res = await this.context.SaveChangesAsync();

                if (res > 0)
                    return Unit.Value;

                throw new ManejadorExcepcion(HttpStatusCode.InternalServerError, new { mensaje = "Ocurrio un error al eliminar la carrera" });
            }
        }
    }
}