using System;
using System.Collections.Generic;
using Models;

namespace Business.Datatypes
{
  public class DtPruebaOnline
  {

    public Guid ActividadId { get; set; }
    public DateTime FechaRealizada { get; set; }
    public DateTime FechaFinalizada { get; set; }

    public string Nombre { get; set; }
    public string Descripcion { get; set; }

    public string UsuarioId { get; set; }
    public Usuario Usuario { get; set; }


    public DateTime Fecha { get; set; }
    public string Url { get; set; }
    public int MinutosExpiracion { get; set; }
    public bool Activa { get; set; }


    public ICollection<PreguntaRespuesta> ListaPreguntaRespuesta { get; set; }
  }
}