using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Models
{
  public class Comunicado
  {

    public Guid ComunicadoId { get; set; }
    public string Nombre { get; set; }
    public string Descripcion { get; set; }
    public string Url { get; set; }
  }
}
