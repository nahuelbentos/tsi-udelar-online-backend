using System;

namespace Models
{
  public class AlumnoTrabajo
  {
    public Guid AlumnoId { get; set; }

    public Alumno Alumno { get; set; }
    //Aca tiene que ser de tipo alumno pero me tira error

    public Guid TrabajoId { get; set; }

    public Trabajo Trabajo { get; set; }
  }
}