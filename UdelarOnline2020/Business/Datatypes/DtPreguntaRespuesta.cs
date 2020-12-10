using System;

namespace Business.Datatypes
{
    public class DtPreguntaRespuesta
    {

    public Guid PreguntaRespuestaId { get; set; }

    public string Pregunta { get; set; }
    public string Respuesta1 { get; set; }
    public string Respuesta2 { get; set; }
    public string Respuesta3 { get; set; }
    public string Respuesta4 { get; set; } 

    public int RespuestaCorrecta { get; set; }
    public int Puntos { get; set; }
    public bool Resta { get; set; }
    }
}