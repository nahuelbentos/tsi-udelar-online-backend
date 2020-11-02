using System;
using System.Collections.Generic;

namespace Models
{
  public class CursoSeccion
  {
    public Guid CursoId { get; set; }
    public Curso Curso { get; set; }
    public Guid SeccionId { get; set; }
    public Seccion Seccion { get; set; }


    public ICollection<Material> MaterialLista { get; set; }
    public ICollection<Foro> ForoLista { get; set; }
    public ICollection<Actividad> ActividadLista { get; set; }
  }
}