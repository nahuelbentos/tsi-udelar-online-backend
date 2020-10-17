using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Models;
using Persistence;

namespace Business.Cursos
{
  public class Nuevo
  {
    public class Ejecuta : IRequest
    {
      public string Nombre { get; set; }
      public string Descripcion { get; set; }
      public ModalidadEnum ModalidadCurso { get; set; }
      public bool RequiereMatriculacion { get; set; }
      public string SalaVirtual { get; set; }

    }

    public class EjecutaValidator : AbstractValidator<Ejecuta>
    {
      public EjecutaValidator()
      {
        RuleFor(c => c.Nombre).NotEmpty().WithMessage("El nombre es requerido.");
        RuleFor(c => c.Descripcion).NotEmpty();
        RuleFor(c => c.ModalidadCurso).NotEmpty();
        RuleFor(c => c.SalaVirtual).NotEmpty();
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
        var curso = new Curso
        {
          CursoId = Guid.NewGuid(),
          Descripcion = request.Descripcion,
          Nombre = request.Nombre,
          Modalidad = request.ModalidadCurso,
          RequiereMatriculacion = request.RequiereMatriculacion,
          SalaVirtual = request.SalaVirtual,
        };

        this.context.Curso.Add(curso);

        var res = await this.context.SaveChangesAsync();
        if (res > 0)
        {
          return Unit.Value;
        }

        throw new Exception("No se pudo dar de alta el curso");


      }
    }

  }
}