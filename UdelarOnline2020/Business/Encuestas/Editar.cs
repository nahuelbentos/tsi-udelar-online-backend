using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Aplicacion.ManejadorError;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Persistence;

namespace Business.Encuestas
{
  public class Editar
  {
    public class Ejecuta : IRequest
    {
      public Guid EncuestaId { get; set; }
      public DateTime? FechaRealizada { get; set; }
      public DateTime? FechaFinalizada { get; set; }
      public Guid? CursoId { get; set; }

      public string Nombre { get; set; }
      public string Descripcion { get; set; }
      public bool? EsAdministradorFacultad { get; set; }

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
        var encuesta = await this.context.Encuesta.Where(e => e.ActividadId == request.EncuestaId).FirstOrDefaultAsync();

        if (encuesta == null)
          throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { mensaje = "No existe una encuesta con el Id ingresado" });


        if (request.CursoId != null && request.CursoId != Guid.Empty)
        {
          var curso = await this.context.Curso.Where(e => e.CursoId == request.CursoId).FirstOrDefaultAsync();

          if (curso == null)
            throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { mensaje = "No existe un curso con el CursoId ingresado" });

          encuesta.Curso = curso ?? encuesta.Curso;
        }


        encuesta.Nombre = request.Nombre ?? encuesta.Nombre;
        encuesta.Descripcion = request.Descripcion ?? encuesta.Descripcion;
        encuesta.FechaRealizada = request.FechaRealizada ?? encuesta.FechaRealizada;
        encuesta.FechaFinalizada = request.FechaFinalizada ?? encuesta.FechaFinalizada;
        encuesta.CursoId = request.CursoId ?? encuesta.CursoId;
        encuesta.EsAdministrador = request.EsAdministradorFacultad ?? encuesta.EsAdministrador;

        var res = await this.context.SaveChangesAsync();

        if (res > 0)
          return Unit.Value;


        throw new ManejadorExcepcion(HttpStatusCode.InternalServerError, new { mensaje = "Ocurrio un error al editar la encuesta" });
      }
    }
  }
}