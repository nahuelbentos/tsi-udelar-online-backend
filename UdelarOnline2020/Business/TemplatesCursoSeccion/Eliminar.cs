using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Aplicacion.ManejadorError;
using FluentValidation;
using MediatR;
using Persistence;

namespace Business.TemplatesCursoSeccion {
  public class Eliminar {

    public class Ejecuta : IRequest {
      public Guid TemplateCursoSeccionId { get; set; }
    }

    public class EjecutaValidator : AbstractValidator<Ejecuta> {
      public EjecutaValidator () {
        RuleFor (t => t.TemplateCursoSeccionId).NotEmpty ().WithMessage ("Es necesario el TemplateCursoId para eliminar un template de curso.");
      }
    }

    public class Manejador : IRequestHandler<Ejecuta> {
      private readonly UdelarOnlineContext context;

      public Manejador (UdelarOnlineContext context) {
        this.context = context;
      }

      public async Task<Unit> Handle (Ejecuta request, CancellationToken cancellationToken) {
        var templateCursoSeccion = await this.context.TemplateCursoSeccion.FindAsync (request.TemplateCursoSeccionId);

        if (templateCursoSeccion == null)
          throw new ManejadorExcepcion (HttpStatusCode.NotFound, new { mensaje = "El template de curso seccion no existe. " });

        this.context.TemplateCursoSeccion.Remove (templateCursoSeccion);

        var res = await this.context.SaveChangesAsync ();

        if (res > 0)
          return Unit.Value;

        throw new ManejadorExcepcion (HttpStatusCode.InternalServerError, new { mensaje = "Ocurrio un error al eliminar el template de curso" });
      }
    }
  }
}