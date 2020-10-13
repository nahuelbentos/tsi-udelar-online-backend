using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Models
{
  public class Carrera
  {
    [Key]
    public Guid Id { get; set; }
    public string Descripcion { get; set; }
    public string Nombre { get; set; }

  }
}
