using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Aplicacion.ManejadorError;
using FluentValidation;
using MediatR;
using Persistence;

namespace Business.TemasForo {
  public class Eliminar {
    
    public class Ejecuta : IRequest {
      public Guid TemaForoId { get; set; }
    }

    public class EjecutaValidator : AbstractValidator<Ejecuta> {
      public EjecutaValidator () {
        RuleFor (t => t.TemaForoId).NotEmpty ().WithMessage ("Es necesario el TemaForoId para eliminar un curso.");
      }
    }

    public class Manejador : IRequestHandler<Ejecuta> {
      private readonly UdelarOnlineContext context;

      public Manejador (UdelarOnlineContext context) {
        this.context = context;
      }

      public async Task<Unit> Handle (Ejecuta request, CancellationToken cancellationToken) {
        var temaForo = await this.context.TemaForo.FindAsync (request.TemaForoId);

        if (temaForo == null)
          throw new ManejadorExcepcion (HttpStatusCode.NotFound, new { mensaje = "El tema de foro no existe. " });

        this.context.TemaForo.Remove (temaForo);

        var res = await this.context.SaveChangesAsync ();

        if (res > 0)
          return Unit.Value;

        throw new ManejadorExcepcion (HttpStatusCode.InternalServerError, new { mensaje = "Ocurrio un error al eliminar el tema de foro" });
      }
    }
  }
}