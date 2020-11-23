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

  public class AgregarForo {

    public class Ejecuta : IRequest {
      public Guid CursoSeccionId { get; set; }
      public Guid ForoId { get; set; }

    }

    public class EjecutaValidator : AbstractValidator<Ejecuta> {
      public EjecutaValidator () {
        RuleFor (c => c.CursoSeccionId).NotEmpty ();
        RuleFor (c => c.ForoId).NotEmpty ();
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

        var cursoSeccion = await this.context.CursoSeccion.Where (tc => tc.CursoSeccionId == request.CursoSeccionId).FirstOrDefaultAsync ();
        if (cursoSeccion == null) {
          throw new ManejadorExcepcion (HttpStatusCode.BadRequest, new { mensaje = "No existe el curso ingresado" });
        }
        var foro = await this.context.Foro.Where (tc => tc.ForoId == request.ForoId).FirstOrDefaultAsync ();
        if (foro == null) {
          throw new ManejadorExcepcion (HttpStatusCode.BadRequest, new { mensaje = "No existe el foro ingresado" });
        }

        cursoSeccion.ForoLista.Add (foro);

        var res = await this.context.SaveChangesAsync ();
        if (res > 0) {
          return Unit.Value;
        }

        throw new ManejadorExcepcion (HttpStatusCode.InternalServerError, new { mensaje = "Ocurrio un error al insertar el curso" });

      }
    }

  }
}