using System;

namespace Models
{
  public class Respuesta
  {
    public Guid RespuestaId { get; set; }

    public string Mensaje { get; set; }

    public Usuario Alumno { get; set; }
    //Consultar si va encuesta o no 
    public Encuesta Encuesta { get; set; }
  }
}