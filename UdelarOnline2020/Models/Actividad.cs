using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Models
{
  public class Actividad
  {
    // public Actividad(Guid actividadId, DateTime fechaRealizada, DateTime fechaFinalizada, Curso curso, Guid cursoId)
    // {
    //   ActividadId = actividadId;
    //   FechaRealizada = fechaRealizada;
    //   FechaFinalizada = fechaFinalizada;
    //   Curso = curso;
    //   CursoId = cursoId;
    // }

    public Guid ActividadId { get; set; }
    public DateTime FechaRealizada { get; set; }
    public DateTime FechaFinalizada { get; set; }

    public Curso Curso { get; set; }
    public Guid CursoId { get; set; }
  }
}
