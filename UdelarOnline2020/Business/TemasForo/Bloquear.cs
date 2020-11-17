using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Models;
using Persistence;

namespace Business.TemasForo {
  public class Bloquear {
    public class Ejecuta : IRequest {
      public Guid MensajeId { get; set; }
      public bool MensajeBloqueado { get; set; }
    }

    public class EjecutaValidator : AbstractValidator<Ejecuta> {
      public EjecutaValidator () {;
      }
    }

    public class Manejador : IRequestHandler<Ejecuta> {
      private readonly UdelarOnlineContext context;

      public Manejador (UdelarOnlineContext context) {
        this.context = context;
      }

      public async Task<Unit> Handle (Ejecuta request, CancellationToken cancellationToken) {

        var mensaje = await this.context.MensajeTema.FindAsync (request.MensajeId);
        if (mensaje == null) {
          throw new Exception ("El mensaje no existe");
        }
        mensaje.MensajeBloqueado = request.MensajeBloqueado;

        var res = await this.context.SaveChangesAsync ();
        if (res > 0) {
          return Unit.Value;
        }

        throw new Exception ("No se pudo bloquear o desbloquear el mensaje");

      }
    }
  }
}