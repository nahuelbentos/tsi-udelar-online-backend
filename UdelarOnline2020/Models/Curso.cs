﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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

    public Guid TemplateCursoId { get; set; }
    public TemplateCurso TemplateCurso { get; set; }
    public ICollection<Material> MaterialLista { get; set; }
    public ICollection<Actividad> ActividadLista { get; set; }



  }


  public enum ModalidadEnum
  {
    Online,
    Presencial,
    Mixto,
  }
}
