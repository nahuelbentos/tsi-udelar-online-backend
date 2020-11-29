using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Models
{
  public class TemplateCurso
  {

    public Guid TemplateCursoId { get; set; }
    public string Nombre { get; set; }
    public string Descripcion { get; set; }

    public List<Curso> ListaCursos { get; set; }
    
    
  }
}
