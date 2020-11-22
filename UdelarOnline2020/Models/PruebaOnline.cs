using System;
using System.Collections.Generic;

namespace Models
{
  public class PruebaOnline : Actividad
  {
    
    public DateTime Fecha { get; set; }
    public string Url { get; set; }
    public int MinutosExpiracion { get; set; }
    public bool Activa { get; set; }


    public ICollection<PreguntaRespuesta> ListaPreguntaRespuesta { get; set; } 





  }
}