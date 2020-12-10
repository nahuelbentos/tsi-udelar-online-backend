using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Models
{
  public class Curso
  {

    public Guid CursoId { get; set; }
    public string Descripcion { get; set; }
    public string Nombre { get; set; }
    public ModalidadEnum Modalidad { get; set; }
    public bool RequiereMatriculacion { get; set; }
    public string SalaVirtual { get; set; }
    public string ZoomId { get; set; }
    public string ZoomPassword { get; set; }


    [AllowNull]
    public TemplateCurso TemplateCurso { get; set; }
    public Guid? TemplateCursoId { get; set; }
    public bool ActaCerrada { get; set; }


  }


  public enum ModalidadEnum
  {
    Online,
    Presencial,
    Mixto,
  }
}
