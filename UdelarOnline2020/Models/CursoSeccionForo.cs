using System;

namespace Models
{
  public class CursoSeccionForo
  {

    public Guid CursoId { get; set; }
    public Curso Curso { get; set; }
    public Guid SeccionId { get; set; }
    public Seccion Seccion { get; set; }
    public Guid ForoId { get; set; }
    public Foro Foro { get; set; }


  }
}