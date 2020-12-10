using System;
using System.Collections.Generic;

namespace Business.Datatypes
{
    public class DtRespuestaPrueba
    {
        public Guid PreguntaRespuestaId { get; set; }
        public string Pregunta { get; set; }
        public List<string> RespuestasPosibles { get; set; }
        public int RespuestaId { get; set; }
        public int RespuestaCorrecta { get; set; }
        
        public int Puntos { get; set; }
        public bool Resta { get; set; }
       
  }
}