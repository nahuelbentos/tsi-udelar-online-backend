using System;
using System.Collections.Generic;

namespace Models
{
  public class AlumnoPruebaOnline
  {

    public Guid AlumnoId { get; set; }

    public Alumno Alumno { get; set; } 
    public Guid PruebaOnlineId { get; set; }

    public PruebaOnline PruebaOnline { get; set; }

    public DateTime FechaInicio { get; set; }
    public DateTime FechaFin { get; set; }
    public DateTime FechaExpiracion { get; set; }
    public int Nota { get; set; }
    public bool Inscripto { get; set; }

    public List<RespuestaPrueba> ListaRespuestas { get; set; }   
    


  }
}