using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Models
{
  class Encuesta : Actividad
  {

    // public Guid Id { get; set; }
    public string Nombre { get; set; }
    public string Descripcion { get; set; }
    public bool EsAdministrador { get; set; }


    public ICollection<Respuesta> RespuestaLista { get; set; }
  }
}
