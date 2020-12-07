using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Business.Datatypes;
using Business.ManejadorError;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using Models;
using Persistence;

namespace Business.Actividades
{
  public class NuevaPruebaOnline
  {
    public class Ejecuta : IRequest
    {
      public DateTime FechaRealizada { get; set; }
      public DateTime FechaFinalizada { get; set; }
      public string Nombre { get; set; }
      public string Descripcion { get; set; }
      public DateTime Fecha { get; set; }
      public string Url { get; set; }
      public int MinutosExpiracion { get; set; }
      public bool Activa { get; set; }
      public List<DtPreguntaRespuesta> ListaPreguntaRespuesta { get; set; }

      public string UsuarioId { get; set; }

    }



    public class Manejador : IRequestHandler<Ejecuta>
    {
      private readonly UdelarOnlineContext context;
      private readonly ILogger<Manejador> logger;

      public Manejador(UdelarOnlineContext context, ILogger<Manejador> logger)
      {
        this.context = context;
        this.logger = logger;
      }

      public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
      {

  	    Console.WriteLine("ListaPreguntaRespuesta: " + request.ListaPreguntaRespuesta.Count);
        var usuario = await this.context.Usuario.FindAsync(request.UsuarioId);

        if (usuario == null)
          throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { mensaje = "No existe un usuario con ese Id." });
        

        PruebaOnline po = new PruebaOnline
        { 
          ActividadId = Guid.NewGuid(),
          FechaFinalizada = request.FechaFinalizada,
          FechaRealizada = request.FechaRealizada,
          Nombre = request.Nombre,
          Descripcion = request.Descripcion,
          Fecha = request.Fecha,
          Url = request.Url,
          MinutosExpiracion = request.MinutosExpiracion,
          Activa = request.Activa,
          Usuario = usuario,
          UsuarioId = usuario.Id

        };
        this.context.PruebaOnline.Add(po);

        var res = await this.context.SaveChangesAsync();
        if (res > 0)
        {
          foreach (var pr in request.ListaPreguntaRespuesta)
          {
            PreguntaRespuesta preguntaRespuesta = new PreguntaRespuesta{
              PruebaOnline = po,
              PruebaOnlineActividadId = po.ActividadId,
              PreguntaRespuestaId = Guid.NewGuid(),
              Pregunta = pr.Pregunta,
              Puntos = pr.Puntos,
              RespuestaCorrecta = pr.RespuestaCorrecta,
              Resta = pr.Resta,
              Respuesta1 = pr.Respuesta1,
              Respuesta2 = pr.Respuesta2,
              Respuesta3 = pr.Respuesta3,
              Respuesta4 = pr.Respuesta4,

            };
            this.context.PreguntaRespuesta.Add(preguntaRespuesta);
          }

          var response = await this.context.SaveChangesAsync();
          if(response > 0){
            return Unit.Value;
          }
        }

        throw new ManejadorExcepcion(HttpStatusCode.InternalServerError, new { mensaje = "Ocurrio un error al insertar la PruebaOnline" });
      }

    }
  }
}