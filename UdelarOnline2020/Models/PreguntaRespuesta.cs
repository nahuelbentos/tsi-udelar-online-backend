using System;
using System.Collections.Generic;

namespace Models
{
  public class PreguntaRespuesta
  {
    public Guid PreguntaRespuestaId { get; set; }
    public PruebaOnline PruebaOnline { get; set; }
    public Guid PruebaOnlineActividadId { get; set; }

    public string Pregunta { get; set; }
    // public ICollection<DtPruebaRespuesta> Respuestas { get; set; }
    public string Respuesta1 { get; set; }
    public string Respuesta2 { get; set; }
    public string Respuesta3 { get; set; }
    public string Respuesta4 { get; set; }

    public int RespuestaCorrecta { get; set; }
    public int Puntos { get; set; }
    public bool Resta { get; set; }
  }
}