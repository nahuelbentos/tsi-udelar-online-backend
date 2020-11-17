using System;
using System.IO;

namespace Models
{
  public class ClaseDictada : Actividad
  {

    public string Nombre { get; set; }
    public string Descripcion { get; set; }
    public byte[] ArchivoData { get; set; }
    public string ArchivoNombre { get; set; }
    public string ArchivoExtension { get; set; }

  }
}