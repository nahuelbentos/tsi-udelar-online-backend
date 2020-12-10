using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Business.Datatypes;
using Business.ManejadorError;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Models;
using Persistence;

namespace Business.Actividades
{
  public class EditarPruebaOnline
  {
    public class Ejecuta : IRequest
    {
      public Guid PruebaOnlineId { get; set; }


      public DateTime? FechaRealizada { get; set; }
      public DateTime? FechaFinalizada { get; set; }
      public string Nombre { get; set; }
      public string Descripcion { get; set; }
      public DateTime? Fecha { get; set; }
      public string Url { get; set; }
      public int MinutosExpiracion { get; set; }
      public bool Activa { get; set; }
      public List<DtPreguntaRespuesta> ListaPreguntaRespuesta { get; set; }
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
        var pruebaOnline = await this.context.PruebaOnline.FindAsync(request.PruebaOnlineId);
        if(pruebaOnline == null)
          throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { mensaje = "No existe la prueba online ingresada." });

          pruebaOnline.Nombre = request.Nombre ?? pruebaOnline.Nombre;
          pruebaOnline.Descripcion = request.Descripcion ?? pruebaOnline.Descripcion;
          pruebaOnline.Fecha = request.Fecha ?? pruebaOnline.Fecha;
          pruebaOnline.FechaFinalizada = request.FechaFinalizada ?? pruebaOnline.FechaFinalizada;
          pruebaOnline.FechaRealizada = request.FechaRealizada ?? pruebaOnline.FechaRealizada;
          pruebaOnline.Activa = request.Activa ? true : false;
          pruebaOnline.Url = request.Url ?? pruebaOnline.Url;

          if(request.MinutosExpiracion != 0 )
            pruebaOnline.MinutosExpiracion = request.MinutosExpiracion;
          pruebaOnline.Descripcion = request.Descripcion ?? pruebaOnline.Descripcion;

        this.context.PruebaOnline.Update(pruebaOnline);


        var res = await this.context.SaveChangesAsync();
        if (res > 0)
        {

          var preguntasEliminar = await this.context.PreguntaRespuesta
                                                    .Where(pr => pr.PruebaOnlineActividadId == pruebaOnline.ActividadId)
                                                    .ToListAsync();
          bool continuar = true;
          if (preguntasEliminar.Count > 0)
          {

            this.context.RemoveRange(preguntasEliminar);

            var result = await this.context.SaveChangesAsync();
            if (result <= 0) continuar = false;
          }


          if (continuar)
          {

            foreach (var pregunta in request.ListaPreguntaRespuesta)
            {
          
            var pr = new PreguntaRespuesta
            {
                Pregunta = pregunta.Pregunta,
                PreguntaRespuestaId = pregunta.PreguntaRespuestaId,
                PruebaOnline = pruebaOnline,
                PruebaOnlineActividadId = pruebaOnline.ActividadId,
                Puntos = pregunta.Puntos,
                Respuesta1 = pregunta.Respuesta1,
                Respuesta2 = pregunta.Respuesta2,
                Respuesta3 = pregunta.Respuesta3,
                Respuesta4 = pregunta.Respuesta4,
                RespuestaCorrecta = pregunta.RespuestaCorrecta,
                Resta = pregunta.Resta
            };

            this.context.PreguntaRespuesta.Add(pr);
              
            }

            var response = await this.context.SaveChangesAsync();
            if (response > 0)
              return Unit.Value;
          }
 
        }

        throw new ManejadorExcepcion(HttpStatusCode.InternalServerError, new { mensaje = "Ocurrio un error al editar la PruebaOnline" });

      }
    }


    public static Ejecuta GetData(Guid id, Ejecuta data) => new Ejecuta
    {
      PruebaOnlineId = id,
      Activa = data.Activa,
      Descripcion = data.Descripcion,
      Fecha = data.Fecha,
      FechaFinalizada = data.FechaFinalizada,
      FechaRealizada = data.FechaRealizada,
      ListaPreguntaRespuesta = data.ListaPreguntaRespuesta,
      MinutosExpiracion = data.MinutosExpiracion,
      Nombre = data.Nombre,
      Url = data.Url,
    };

  }
}