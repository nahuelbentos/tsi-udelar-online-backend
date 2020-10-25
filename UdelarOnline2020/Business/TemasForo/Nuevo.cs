using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Models;
using Persistence;

namespace Business.TemasForo {
  public class Nuevo {
    
    public class Ejecuta : IRequest {
      public string Asunto { get; set; }
      public string Mensaje { get; set; }
      public Guid EmisorId { get; set; }
      public string ArchivoAdjunto { get; set; }
      public bool SuscripcionADiscusion { get; set; }

    }

    public class EjecutaValidator : AbstractValidator<Ejecuta> {
      public EjecutaValidator () {
        RuleFor (t => t.Asunto).NotEmpty ().WithMessage ("El asunto es requerido.");
        RuleFor (t => t.Mensaje).NotEmpty ();
        RuleFor (t => t.EmisorId).NotEmpty ();
        RuleFor (t => t.SuscripcionADiscusion).NotEmpty ();
      }
    }

    public class Manejador : IRequestHandler<Ejecuta> {
      private readonly UdelarOnlineContext context;

      public Manejador (UdelarOnlineContext context) {
        this.context = context;
      }

      public async Task<Unit> Handle (Ejecuta request, CancellationToken cancellationToken) {
        var temaForo = new TemaForo {
          TemaForoId = Guid.NewGuid (),
          Asunto = request.Asunto,
          Mensaje = request.Mensaje,
          EmisorId = request.EmisorId,
          ArchivoAdjunto = request.ArchivoAdjunto,
          SubscripcionADiscusion = request.SuscripcionADiscusion,
        };

        this.context.TemaForo.Add (temaForo);

        var res = await this.context.SaveChangesAsync ();
        if (res > 0) {
          return Unit.Value;
        }

        throw new Exception ("No se pudo dar de alta el tema de foro");

      }
    }
  }
}