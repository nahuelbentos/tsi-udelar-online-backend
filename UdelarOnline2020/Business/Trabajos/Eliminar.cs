using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Aplicacion.ManejadorError;
using FluentValidation;
using MediatR;
using Persistence;

namespace Business.Trabajos
{
    public class Eliminar
    {
         public class Ejecuta : IRequest
    {
      public Guid TrabajoId { get; set; }
    }

    public class EjecutaValidator : AbstractValidator<Ejecuta>
    {
      public EjecutaValidator()
      {
        RuleFor(t => t.TrabajoId).NotEmpty().WithMessage("Es necesario el TrabajoId para eliminar un template de curso.");
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
        var trabajo = await this.context.Trabajo.FindAsync(request.TrabajoId);

        if (trabajo == null)
          throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { mensaje = "El trabajo no existe. " });

        this.context.Trabajo.Remove(trabajo);

        var res = await this.context.SaveChangesAsync();

        if (res > 0)
          return Unit.Value;

        throw new ManejadorExcepcion(HttpStatusCode.InternalServerError, new { mensaje = "Ocurrio un error al eliminar el trabajo" });
      }
    }
    }
}