using System.Net;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Aplicacion.ManejadorError;
using MediatR;
using Persistence;
using Models;
using Microsoft.Extensions.Logging;
using FluentValidation;

namespace Business.Encuestas
{
  public class Nuevo
  {
    public class Ejecuta : IRequest
    {

      public DateTime FechaRealizada { get; set; }
      public DateTime FechaFinalizada { get; set; }
      public Guid CursoId { get; set; }

      public string Nombre { get; set; }
      public string Descripcion { get; set; }
      public bool EsAdministradorFacultad { get; set; }
    }

    public class EjecutaValidator : AbstractValidator<Ejecuta>
    {
      public EjecutaValidator()
      {
        RuleFor(e => e.CursoId).NotEmpty();
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
        var curso = await this.context.Curso.FindAsync(request.CursoId);


        if (curso == null)
          throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { mensaje = "No existe un curso con el CursoId ingresado" });

        var encuesta = new Encuesta
        {
          ActividadId = Guid.NewGuid(),
          Curso = curso,
          CursoId = request.CursoId,
          FechaFinalizada = request.FechaFinalizada,
          FechaRealizada = request.FechaRealizada,
          Nombre = request.Nombre,
          Descripcion = request.Descripcion,
          EsAdministrador = request.EsAdministradorFacultad
        };

        await this.context.Encuesta.AddAsync(encuesta);

        var res = await this.context.SaveChangesAsync();

        if (res > 0)
          return Unit.Value;

        throw new ManejadorExcepcion(HttpStatusCode.InternalServerError, new { mensaje = "No se pudo insertar la encuesta" });
      }
    }

  }
}