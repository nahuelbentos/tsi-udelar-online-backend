using System;

namespace Business.Datatypes
{
  public class DtFacultad
  {

    public Guid FacultadId { get; set; }
    public string Nombre { get; set; }
    public string Descripcion { get; set; }
    public string UrlAcceso { get; set; }
    public string DominioMail { get; set; }
    public string LogoNombre { get; set; }
    public string LogoExtension { get; set; }
    public string LogoData { get; set; }
    public string ColorCodigo { get; set; }
  }
}