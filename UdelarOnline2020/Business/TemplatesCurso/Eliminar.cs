using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Aplicacion.ManejadorError;
using FluentValidation;
using MediatR;
using Persistence;

namespace Business.TemplatesCurso {
  public class Eliminar {

    public class Ejecuta : IRequest {
      public Guid TemplateCursoId { get; set; }
    }

    public class EjecutaValidator : AbstractValidator<Ejecuta> {
      public EjecutaValidator () {
        RuleFor (t => t.TemplateCursoId).NotEmpty ().WithMessage ("Es necesario el TemplateCursoId para eliminar un template de curso.");
      }
    }

    public class Manejador : IRequestHandler<Ejecuta> {
      private readonly UdelarOnlineContext context;

      public Manejador (UdelarOnlineContext context) {
        this.context = context;
      }

      public async Task<Unit> Handle (Ejecuta request, CancellationToken cancellationToken) {
        var templateCurso = await this.context.TemplateCurso.FindAsync (request.TemplateCursoId);

        if (templateCurso == null)
          throw new ManejadorExcepcion (HttpStatusCode.NotFound, new { mensaje = "El template de curso no existe. " });

        this.context.TemplateCurso.Remove (templateCurso);

        var res = await this.context.SaveChangesAsync ();

        if (res > 0)
          return Unit.Value;

        throw new ManejadorExcepcion (HttpStatusCode.InternalServerError, new { mensaje = "Ocurrio un error al eliminar el template de curso" });
      }
    }
  }
}