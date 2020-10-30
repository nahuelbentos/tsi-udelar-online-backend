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
    public byte[] Archivo { get; set; }
  }
}
