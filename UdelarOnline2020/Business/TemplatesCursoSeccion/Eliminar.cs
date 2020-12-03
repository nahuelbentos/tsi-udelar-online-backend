using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Business.ManejadorError;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Business.TemplatesCursoSeccion {
  public class Eliminar {

    public class Ejecuta : IRequest {
      public Guid TemplateCursoId {get; set;}
      public Guid SeccionId {get; set;}
    }

    public class EjecutaValidator : AbstractValidator<Ejecuta> {
      public EjecutaValidator () {
        RuleFor (t => t.TemplateCursoId).NotEmpty ().WithMessage ("Es necesario el TemplateCursoId para eliminar un template de curso.");
        RuleFor (t => t.SeccionId).NotEmpty ().WithMessage ("Es necesario el  SeccionId para eliminar un template de curso.");
      }
    }

    public class Manejador : IRequestHandler<Ejecuta> {
      private readonly UdelarOnlineContext context;

      public Manejador (UdelarOnlineContext context) {
        this.context = context;
      }

      public async Task<Unit> Handle (Ejecuta request, CancellationToken cancellationToken) {
        var templateCursoSeccion = await this.context.TemplateCursoSeccion.Where (tc => tc.TemplateCursoId == request.TemplateCursoId && tc.SeccionId == request.SeccionId).FirstOrDefaultAsync ();

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