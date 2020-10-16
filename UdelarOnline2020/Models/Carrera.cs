using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Models
{
  public class Carrera
  {
    public Guid Id { get; set; }
    public string Descripcion { get; set; }
    public string Nombre { get; set; }

    public Facultad Facultad { get; set; }
    public Guid FacultadId { get; set; }

  }
}
