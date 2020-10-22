using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Aplicacion.ManejadorError;
using FluentValidation;
using MediatR;
using Persistence;

namespace Business.Comunicados
{
    public class Eliminar
    {
         public class Ejecuta : IRequest
    {
      public Guid ComunicadoId { get; set; }
    }

    public class EjecutaValidator : AbstractValidator<Ejecuta>
    {
      public EjecutaValidator()
      {
        RuleFor(c => c.ComunicadoId).NotEmpty().WithMessage("Es necesario el ComunicadoId para eliminar un comunicado.");
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
        var comunicado = await this.context.Comunicado.FindAsync(request.ComunicadoId);

        if (comunicado == null)
          throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { mensaje = "El comunicado no existe. " });

        this.context.Comunicado.Remove(comunicado);

        var res = await this.context.SaveChangesAsync();

        if (res > 0)
          return Unit.Value;

        throw new ManejadorExcepcion(HttpStatusCode.InternalServerError, new { mensaje = "Ocurrio un error al eliminar el Comunicado" });
      }
    }
    }
}