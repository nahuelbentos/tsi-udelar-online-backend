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
  }
}