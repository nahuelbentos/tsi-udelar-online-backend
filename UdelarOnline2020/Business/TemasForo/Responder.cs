using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Models;
using Persistence;


namespace Business.TemasForo
{
    public class Responder
    {
        public class Ejecuta : IRequest {

    }

    public class EjecutaValidator : AbstractValidator<Ejecuta> {
      public EjecutaValidator () {
      }
    }

    public class Manejador : IRequestHandler<Ejecuta> {
      private readonly UdelarOnlineContext context;

      public Manejador (UdelarOnlineContext context) {
        this.context = context;
      }

      public async Task<Unit> Handle (Ejecuta request, CancellationToken cancellationToken) {

        var res = await this.context.SaveChangesAsync ();
        if (res > 0) {
          return Unit.Value;
        }

        throw new Exception ("No se pudo dar de alta el tema de foro");

      }
    }
    }
}