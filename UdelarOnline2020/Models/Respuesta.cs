using System;

namespace Models
{
  public class Respuesta
  {
    public Guid Id { get; set; }

    public string Mensaje { get; set; }

    public Usuario Alumno { get; set; }
    public Guid AlumnoId { get; set; }
  }
}