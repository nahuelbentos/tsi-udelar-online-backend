using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Models
{
  public class Actividad
  {


    public Guid ActividadId { get; set; }
    public DateTime FechaRealizada { get; set; }
    public DateTime FechaFinalizada { get; set; }

    public string Nombre { get; set; }
    public string Descripcion { get; set; }

    public string UsuarioId { get; set; }
    public Usuario Usuario { get; set; }
    
    

  }
}
