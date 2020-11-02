using System;

namespace Models
{
  public class CursoSeccionActividad
  {
    public Guid CursoId { get; set; }
    public Curso Curso { get; set; }
    public Guid SeccionId { get; set; }
    public Seccion Seccion { get; set; }
    public Guid ActividadId { get; set; }
    public Actividad Actividad { get; set; }

  }
}