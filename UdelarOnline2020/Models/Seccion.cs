
using System.Collections.Generic;
using System;

namespace Models
{
  public class Seccion
  {
    public Guid SeccionId { get; set; }

    public string Nombre { get; set; }

    public string Descripcion { get; set; }

    public bool IsDefault { get; set; }
    public bool IsVisible { get; set; }



  }
}