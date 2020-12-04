using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Business.ManejadorError;
using FluentValidation;
using MediatR;
using Models;
using Persistence;

namespace Business.TemplatesCursoSeccion {
  public class Nuevo {

    public class Ejecuta : IRequest {
      public TemplateCurso TemplateCurso { get; set; } 
      public List<Guid> Secciones { get; set; }

    }
 

    public class Manejador : IRequestHandler<Ejecuta> {
      private readonly UdelarOnlineContext context;

      public Manejador (UdelarOnlineContext context) {
        this.context = context;
      }

      public async Task<Unit> Handle (Ejecuta request, CancellationToken cancellationToken) {
        var templateCurso = new TemplateCurso {
          Descripcion = request.TemplateCurso.Descripcion,
          Nombre = request.TemplateCurso.Nombre,
          TemplateCursoId = Guid.NewGuid()
        };

        this.context.TemplateCurso.Add(templateCurso);

        foreach (var seccionId in request.Secciones)
        {
          var seccion = await this.context.Seccion.FindAsync (seccionId);

          if (seccion == null) 
            throw new ManejadorExcepcion (HttpStatusCode.NotFound, new { mensaje = "La seccion no existe" });

          var templateCursoSeccion = new TemplateCursoSeccion {
            TemplateCursoSeccionId = Guid.NewGuid (),
            TemplateCursoId = templateCurso.TemplateCursoId,
            TemplateCurso = templateCurso,
            SeccionId = seccion.SeccionId,
            Seccion = seccion
          };

          this.context.TemplateCursoSeccion.Add (templateCursoSeccion);
          
        }

        var res = await this.context.SaveChangesAsync ();
        if (res > 0) {
          return Unit.Value;
        }

        throw new Exception ("No se pudo dar de alta el template de curso seccion");

      }
    }
  }
}