using System;
using Models;

namespace Business.Datatypes
{
  public class DtActividad
  {

    public Guid ActividadId { get; set; }
    public DateTime FechaRealizada { get; set; }
    public DateTime FechaFinalizada { get; set; }

    public string Nombre { get; set; }
    public string Descripcion { get; set; }

    public string UsuarioId { get; set; }
    public Usuario Usuario { get; set; }

    public string Tipo { get; set; }
    
    
  }
}