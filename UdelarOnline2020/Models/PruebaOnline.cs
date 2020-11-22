using System;
using System.Collections.Generic;

namespace Models
{
  public class PruebaOnline : Actividad
  {
    
    public string Nombre { get; set; }
    public string Descripcion { get; set; }
    public DateTime Fecha { get; set; }
    public string Url { get; set; }
    public DateTime MinutosExpiracion { get; set; }
    public bool Activa { get; set; }


    public ICollection<PreguntaRespuesta> ListaPreguntaRespuesta { get; set; } 





  }
}