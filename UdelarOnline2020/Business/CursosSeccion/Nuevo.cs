using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Business.ManejadorError;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Models;
using Persistence;

namespace Business.CursosSeccion {
  public class Nuevo {

    public class Ejecuta : IRequest {
      public Guid CursoId { get; set; }
      public Guid SeccionId { get; set; }

    }

    public class EjecutaValidator : AbstractValidator<Ejecuta> {
      public EjecutaValidator () {
        RuleFor (c => c.CursoId).NotEmpty ();
        RuleFor (c => c.SeccionId).NotEmpty ();
      }
    }

    public class Manejador : IRequestHandler<Ejecuta> {
      private readonly UdelarOnlineContext context;
      private readonly ILogger<Manejador> logger;

      public Manejador (UdelarOnlineContext context, ILogger<Manejador> logger) {
        this.context = context;
        this.logger = logger;
      }

      public async Task<Unit> Handle (Ejecuta request, CancellationToken cancellationToken) {

        var curso = await this.context.Curso.Where (tc => tc.CursoId == request.CursoId).FirstOrDefaultAsync ();
        if (curso == null)
          throw new ManejadorExcepcion (HttpStatusCode.BadRequest, new { mensaje = "No existe el curso ingresado" });

        var seccion = await this.context.Seccion.Where (tc => tc.SeccionId == request.SeccionId).FirstOrDefaultAsync ();
        if (seccion == null) 
          throw new ManejadorExcepcion (HttpStatusCode.BadRequest, new { mensaje = "No existe la seccion ingresada" });
        
        var existeCursoSeccion = await this.context.CursoSeccion
                                                        .Where (cs => cs.SeccionId == request.SeccionId && cs.CursoId == curso.CursoId)
                                                        .FirstOrDefaultAsync ();
        if (existeCursoSeccion != null) 
          throw new ManejadorExcepcion (HttpStatusCode.BadRequest, new { mensaje = "La sección ya se encuentra vinculada al curso." });


        var cursoSeccion = new CursoSeccion {
          CursoId = request.CursoId,
          Curso = curso,
          SeccionId = request.SeccionId,
          Seccion = seccion
        };

        this.context.CursoSeccion.Add (cursoSeccion);

        var res = await this.context.SaveChangesAsync ();
        if (res > 0) {
          return Unit.Value;
        }

        throw new ManejadorExcepcion (HttpStatusCode.InternalServerError, new { mensaje = "Ocurrio un error al insertar el curso" });

      }
    }

  }
}