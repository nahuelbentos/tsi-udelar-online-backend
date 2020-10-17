using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Models
{
  public class Encuesta : Actividad
  {

    // public Guid EncuestaId { get; set; }
    public string Nombre { get; set; }
    public string Descripcion { get; set; }
    public bool EsAdministrador { get; set; }


    public ICollection<Respuesta> RespuestaLista { get; set; }
  }
}
