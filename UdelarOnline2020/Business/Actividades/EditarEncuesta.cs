using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Business.ManejadorError;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Models;
using Persistence;

namespace Business.Actividades
{
  public class EditarEncuesta
  {
    public class Ejecuta : IRequest
    {
      public Guid ActividadId { get; set; }
      public DateTime? FechaRealizada { get; set; }
      public DateTime? FechaFinalizada { get; set; }
      public string Nombre { get; set; }
      public string Descripcion { get; set; } 
      public List<string> PreguntaLista { get; set; }

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
        var encuesta = await this.context.Encuesta.FindAsync(request.ActividadId);

        if (encuesta == null)
          throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { mensaje = "No existe un encuesta con ese Id." });

        var preguntas = await this.context.Pregunta.Include(p => p.RespuestaLista).Where(p => p.EncuestaId == request.ActividadId).ToListAsync();

        foreach (var pregunta in preguntas)
        {
            if( pregunta.RespuestaLista.Count > 0 )
                throw new ManejadorExcepcion(HttpStatusCode.BadRequest, new { mensaje = "La encuesta ya ha sido respondida por un alumno, no se puede modificar." });
        }

        
          encuesta.FechaFinalizada = request.FechaFinalizada ?? encuesta.FechaFinalizada;
          encuesta.FechaRealizada = request.FechaRealizada ?? encuesta.FechaRealizada;
          encuesta.Nombre = request.Nombre ?? encuesta.Nombre;
          encuesta.Descripcion = request.Descripcion ?? encuesta.Descripcion;

        this.context.Encuesta.Update(encuesta);
        var res = await this.context.SaveChangesAsync();
        if (res > 0)
        {
            //eliminoLasPreguntasQueNo tiene
            var preguntasEliminar = await this.context.Pregunta.Where( p => p.EncuestaId == encuesta.ActividadId).ToListAsync();
            this.context.RemoveRange(preguntasEliminar);

            //creo las preguntas
            foreach (var pregunta in request.PreguntaLista)
            {
                Pregunta p = new Pregunta
                {
                PreguntaId = Guid.NewGuid(),
                Texto = pregunta,
                Encuesta = encuesta,
                EncuestaId = encuesta.ActividadId
                };
                this.context.Pregunta.Add(p);
            }
            var response = await this.context.SaveChangesAsync();
          if (response > 0)
            return Unit.Value;
        }



        throw new ManejadorExcepcion(HttpStatusCode.InternalServerError, new { mensaje = "Ocurrio un error al editar la Encuesta" });
      }
    }

  }
}