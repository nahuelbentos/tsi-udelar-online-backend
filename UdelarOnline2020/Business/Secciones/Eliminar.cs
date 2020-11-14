using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Aplicacion.ManejadorError;
using FluentValidation;
using MediatR;
using Persistence;

namespace Business.Secciones
{
    public class Eliminar
    {
        public class Ejecuta : IRequest
        {
            public Guid SeccionId { get; set; }
        }

        public class EjecutaValidator : AbstractValidator<Ejecuta>
        {
            public EjecutaValidator()
            {
                RuleFor(s => s.SeccionId).NotEmpty().WithMessage("Es necesario el SeccionId para eliminar una seccion");
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
                var seccion = await this.context.Seccion.FindAsync(request.SeccionId);

                if (seccion == null)
                {
                    throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { mensaje = "La seccion no existe" });
                }
                this.context.Seccion.Remove(seccion);

                var res = await this.context.SaveChangesAsync();

                if (res > 0)
                    return Unit.Value;

                throw new ManejadorExcepcion(HttpStatusCode.InternalServerError, new { mensaje = "Ocurrio un error al eliminar la seccion" });
            }
        }
    }
}