using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Models
{
  public class Actividad
  {

    public Guid Id { get; set; }
    public DateTime FechaRealizada { get; set; }
    public DateTime FechaFinalizada { get; set; }

    public Curso Curso { get; set; }
    public Guid CursoId { get; set; }
  }
}
