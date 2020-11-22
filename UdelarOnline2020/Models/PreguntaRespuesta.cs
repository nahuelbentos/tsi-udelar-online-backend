using System;
using System.Collections.Generic;

namespace Models
{
  public class PreguntaRespuesta
  {
    public Guid PreguntaRespuestaId { get; set; }

    public string Pregunta { get; set; }
    public ICollection<DtPruebaRespuesta> Respuestas { get; set; }

    public int RespuestaCorrecta { get; set; }
    public int Puntos { get; set; }
    public bool Resta { get; set; }
  }
}