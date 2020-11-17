using System;
namespace Models
{
  public class TemplateCursoSeccion
  {

  public Guid TemplateCursoSeccionId { get; set; }
    public Guid TemplateCursoId { get; set; }
    public TemplateCurso TemplateCurso { get; set; }
    public Guid SeccionId { get; set; }
    public Seccion Seccion { get; set; }
  }
}