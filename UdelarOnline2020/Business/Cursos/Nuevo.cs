using System.Net;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Aplicacion.ManejadorError;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
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
      public Guid TemplateCursoId { get; set; }

    }

    public class EjecutaValidator : AbstractValidator<Ejecuta>
    {
      public EjecutaValidator()
      {
        RuleFor(c => c.Nombre).NotEmpty().WithMessage("El nombre es requerido.");
        RuleFor(c => c.Descripcion).NotEmpty();
        RuleFor(c => c.ModalidadCurso).NotEmpty();
        RuleFor(c => c.SalaVirtual).NotEmpty();
        RuleFor(c => c.TemplateCursoId).NotEmpty().WithMessage("El template del curso es Requerido");
      }
    }


    public class Manejador : IRequestHandler<Ejecuta>
    {
      private readonly UdelarOnlineContext context;
      private readonly ILogger<Manejador> logger;

      public Manejador(UdelarOnlineContext context, ILogger<Manejador> logger)
      {
        this.context = context;
        this.logger = logger;
      }

      public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
      {


        var templateCurso = await this.context.TemplateCurso.Where(tc => tc.TemplateCursoId == request.TemplateCursoId).FirstOrDefaultAsync();
        if (templateCurso == null)
        {
          throw new ManejadorExcepcion(HttpStatusCode.BadRequest, new { mensaje = "No existe el template ingresado" });
        }
        var curso = new Curso
        {
          CursoId = Guid.NewGuid(),
          Descripcion = request.Descripcion,
          Nombre = request.Nombre,
          Modalidad = request.ModalidadCurso,
          RequiereMatriculacion = request.RequiereMatriculacion,
          SalaVirtual = request.SalaVirtual,
          TemplateCursoId = request.TemplateCursoId,
          TemplateCurso = templateCurso
        };

        this.context.Curso.Add(curso);

        var res = await this.context.SaveChangesAsync();
        if (res > 0)
        {
          return Unit.Value;
        }

        throw new ManejadorExcepcion(HttpStatusCode.InternalServerError, new { mensaje = "Ocurrio un error al insertar el curso" });


      }
    }

  }
}