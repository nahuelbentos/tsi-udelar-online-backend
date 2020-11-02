using System;
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
  }
}
