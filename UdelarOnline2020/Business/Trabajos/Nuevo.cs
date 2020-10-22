using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Models;
using Persistence;

namespace Business.TemplatesCurso
{
    public class Trabajos
    {
        public class Ejecuta : IRequest
    {
      public bool EsIndividual { get; set; }
      public int Calificacion { get; set; }
      public string Nota {get; set;}

    }

    public class EjecutaValidator : AbstractValidator<Ejecuta>
    {
      public EjecutaValidator()
      {
        RuleFor(t => t.Calificacion).NotEmpty().WithMessage("El nombre es requerido.");
        RuleFor(t => t.Nota).NotEmpty();
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
        var trabajo = new Trabajo
        {
          ActividadId = Guid.NewGuid(),
          EsIndividual = request.EsIndividual,
          Calificacion = request.Calificacion,
          Nota = request.Nota
        };

        this.context.Trabajo.Add(trabajo);

        var res = await this.context.SaveChangesAsync();
        if (res > 0)
        {
          return Unit.Value;
        }

        throw new Exception("No se pudo dar de alta el trabajo");


      }
    }
    }
}