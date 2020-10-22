using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Models;
using Persistence;

namespace Business.TemplatesCurso
{
    public class Nuevo
    {
        public class Ejecuta : IRequest
    {
      public string Nombre { get; set; }
      public string Descripcion { get; set; }

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
        var templateCurso = new TemplateCurso
        {
          TemplateCursoId = Guid.NewGuid(),
          Nombre = request.Nombre,
          Descripcion = request.Descripcion
        };

        this.context.TemplateCurso.Add(templateCurso);

        var res = await this.context.SaveChangesAsync();
        if (res > 0)
        {
          return Unit.Value;
        }

        throw new Exception("No se pudo dar de alta el template de curso");


      }
    }
    }
}