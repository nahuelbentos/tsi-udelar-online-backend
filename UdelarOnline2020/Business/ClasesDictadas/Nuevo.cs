using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Models;
using Persistence;

namespace Business.ClasesDictadas
{
    public class Nuevo
    {
        public class Ejecuta : IRequest
    {

    }

    public class EjecutaValidator : AbstractValidator<Ejecuta>
    {
      public EjecutaValidator()
      {
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
        var ClaseDictada = new ClaseDictada
        {
          ActividadId = Guid.NewGuid(),
        };

        this.context.ClaseDictada.Add(ClaseDictada);

        var res = await this.context.SaveChangesAsync();
        if (res > 0)
        {
          return Unit.Value;
        }

        throw new Exception("No se pudo dar de alta la clase");


      }
    }
    }
}