﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Models
{
  public class Encuesta : Actividad
  {


    public string Nombre { get; set; }
    public string Descripcion { get; set; }
    public bool EsAdministrador { get; set; }

    public ICollection<Pregunta> PreguntaLista { get; set; }
    
    public Facultad Facultad { get; set; }
    public Guid FacultadId { get; set; }
    
    
    
  }
}
