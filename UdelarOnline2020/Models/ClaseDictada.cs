using System;
using System.IO;

namespace Models
{
  public class ClaseDictada : Actividad
  {

    public byte[] ArchivoData { get; set; }
    public string ArchivoNombre { get; set; }
    public string ArchivoExtension { get; set; }

  }
}