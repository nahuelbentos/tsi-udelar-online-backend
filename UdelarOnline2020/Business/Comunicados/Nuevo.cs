using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Models;
using Persistence;

namespace Business.Comunicados
{
    public class Nuevo
    {
        public class Ejecuta : IRequest
    {
      public string Nombre { get; set; }
      public string Descripcion { get; set; }
      public string Url {get; set;}

    }

    public class EjecutaValidator : AbstractValidator<Ejecuta>
    {
      public EjecutaValidator()
      {
        RuleFor(t => t.Nombre).NotEmpty().WithMessage("El nombre es requerido.");
        RuleFor(t => t.Descripcion).NotEmpty();
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
        var comunicado = new Comunicado
        {
          ComunicadoId = Guid.NewGuid(),
          Nombre = request.Nombre,
          Descripcion = request.Descripcion,
          Url = request.Url
        };

        this.context.Comunicado.Add(comunicado);

        var res = await this.context.SaveChangesAsync();
        if (res > 0)
        {
          return Unit.Value;
        }

        throw new Exception("No se pudo dar de alta el comunicado");


      }
    }
    }
}