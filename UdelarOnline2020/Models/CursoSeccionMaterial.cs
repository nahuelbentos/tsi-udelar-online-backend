using System;

namespace Models
{
  public class CursoSeccionMaterial
  {
    public Guid CursoId { get; set; }
    public Curso Curso { get; set; }
    public Guid SeccionId { get; set; }
    public Seccion Seccion { get; set; }
    public Guid MaterialId { get; set; }
    public Material Material { get; set; }

  }
}