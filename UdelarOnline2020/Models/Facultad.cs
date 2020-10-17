using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Models
{
  public class Facultad
  {

    public Guid FacultadId { get; set; }
    public string Nombre { get; set; }
    public string Descripcion { get; set; }
    public string UrlAcceso { get; set; }
  }
}
