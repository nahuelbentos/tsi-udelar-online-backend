using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Aplicacion.ManejadorError;
using FluentValidation;
using MediatR;
using Models;
using Persistence;

namespace Business.TemplatesCursoSeccion {
  public class Nuevo {

    public class Ejecuta : IRequest {
      public Guid TemplateCursoId { get; set; }
      public Guid SeccionId { get; set; }

    }

    public class EjecutaValidator : AbstractValidator<Ejecuta> {
      public EjecutaValidator () {
        RuleFor (t => t.TemplateCursoId).NotEmpty ();
        RuleFor (t => t.SeccionId).NotEmpty ();
      }
    }

    public class Manejador : IRequestHandler<Ejecuta> {
      private readonly UdelarOnlineContext context;

      public Manejador (UdelarOnlineContext context) {
        this.context = context;
      }

      public async Task<Unit> Handle (Ejecuta request, CancellationToken cancellationToken) {
        var templateCurso = await this.context.TemplateCurso.FindAsync (request.TemplateCursoId);

        if (templateCurso == null) {
          throw new ManejadorExcepcion (HttpStatusCode.NotFound, new { mensaje = "El template de curso no existe" });

        }
        var seccion = await this.context.Seccion.FindAsync (request.SeccionId);

        if (seccion == null) {
          throw new ManejadorExcepcion (HttpStatusCode.NotFound, new { mensaje = "La seccion no existe" });

        }

        var templateCursoSeccion = new TemplateCursoSeccion {
          TemplateCursoId = Guid.NewGuid (),
          TemplateCurso = templateCurso,
          SeccionId = request.SeccionId,
          Seccion = seccion
        };

        this.context.TemplateCursoSeccion.Add (templateCursoSeccion);

        var res = await this.context.SaveChangesAsync ();
        if (res > 0) {
          return Unit.Value;
        }

        throw new Exception ("No se pudo dar de alta el template de curso seccion");

      }
    }
  }
}