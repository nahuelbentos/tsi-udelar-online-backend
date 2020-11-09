using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Text;

namespace Models
{
  public class Material
  {
    public Guid MaterialId { get; set; }

    public string Nombre { get; set; }
    public string Descripcion { get; set; }
    public byte[] ArchivoData { get; set; }
    public string ArchivoNombre { get; set; }
    public string ArchivoExtension { get; set; }
  }
}
