using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Business.Datatypes;
using Business.ManejadorError;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Models;
using Persistence;

namespace Business.AlumnoPruebaOnlines
{
  public class Editar
  {
    public class Ejecuta : IRequest
    {
      public Guid AlumnoId { get; set; }
      public Guid PruebaOnlineId { get; set; }
      public DateTime FechaInicio { get; set; }
      public DateTime? FechaFin { get; set; }
      public DateTime? FechaExpiracion { get; set; }
      public int? Nota { get; set; }
      public bool? Inscripto { get; set; }

      public float? CalificacionPorcentaje { get; set; }

      public List<DtRespuestaPrueba> RespuestasAlumno { get; set; }
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
        var alumno = await this.context.Alumno.Where(a => a.Id == request.AlumnoId.ToString()).FirstOrDefaultAsync();

        if (alumno == null)
          throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { mensaje = "No existe el alumno ingresado" });

        var pruebaOnline = (PruebaOnline)await this.context.Actividad.FindAsync(request.PruebaOnlineId);
        if (pruebaOnline == null)
          throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { mensaje = "No existe la pruebaOnline ingresada" });

        var inscripcionPrueba = await this.context.AlumnoPruebaOnline.Where(ac => ac.PruebaOnlineId == request.PruebaOnlineId && ac.AlumnoId == request.AlumnoId).FirstOrDefaultAsync();
        if (inscripcionPrueba == null)
          throw new ManejadorExcepcion(HttpStatusCode.Conflict, new { mensaje = "No existe la relacion entre Alumno y PruebaOnline." });

        //hago el edit de los campos que se van a editar
        inscripcionPrueba.FechaInicio = request.FechaInicio;
        inscripcionPrueba.FechaExpiracion = request.FechaExpiracion.GetValueOrDefault();
        inscripcionPrueba.FechaFin = request.FechaFin ?? inscripcionPrueba.FechaFin;
        inscripcionPrueba.Nota = request.Nota ?? inscripcionPrueba.Nota;
        inscripcionPrueba.CalificacionPorcentaje = request.CalificacionPorcentaje ?? inscripcionPrueba.CalificacionPorcentaje;
        inscripcionPrueba.Inscripto = request.Inscripto ?? inscripcionPrueba.Inscripto;
        inscripcionPrueba.RealizadaPorAlumno = true;



        this.context.AlumnoPruebaOnline.Update(inscripcionPrueba);
        var res = await this.context.SaveChangesAsync();
        if (res > 0)
        {
          var respuestas = await this.context.RespuestaPrueba
                                                  .Include(rp => rp.Alumno)
                                                  .Include(rp => rp.PruebaOnline)
                                                  .Include(rp => rp.PreguntaRespuesta)
                                                  .Where(rp => rp.PruebaOnline.ActividadId == pruebaOnline.ActividadId
                                                      && rp.Alumno.Id == alumno.Id)
                                                  .ToListAsync();
          if (respuestas != null)
            this.context.RemoveRange(respuestas);

          foreach (var respuestaAlumno in request.RespuestasAlumno)
          {
            var preguntaRespuesta = await this.context.PreguntaRespuesta.FindAsync(respuestaAlumno.PreguntaRespuestaId);
            this.context.RespuestaPrueba.Add(new RespuestaPrueba
            {
              RespuestaPruebaId = Guid.NewGuid(),
              Alumno = alumno,
              AlumnoPruebaOnlineAlumnoId = Guid.Parse(alumno.Id),
              AlumnoPruebaOnlinePruebaOnlineId = pruebaOnline.ActividadId,
              PreguntaId = respuestaAlumno.PreguntaRespuestaId,
              PreguntaRespuesta = preguntaRespuesta,
              PruebaOnline = pruebaOnline,
              RespuestaId = respuestaAlumno.RespuestaId,
            });

          }
          var result = await this.context.SaveChangesAsync();
          if (result > 0)
            return Unit.Value;
        }
        throw new ManejadorExcepcion(HttpStatusCode.InternalServerError, new { mensaje = "Ocurrio un error al editar AlumnoPruebaOnline ingresado." });

      }
    }
  }
}