using System;
using Models;

namespace Business.Datatypes
{
    public class DtEvaluacion
    {

    public Guid AlumnoId { get; set; }

    public Alumno Alumno { get; set; }
    public Guid PruebaOnlineId { get; set; }

    // Nombre + desc de la actividad
    public string Evaluacion { get; set; }
    
    
    public PruebaOnline PruebaOnlineData { get; set; }

    public DateTime FechaInicio { get; set; }
    public DateTime FechaFin { get; set; }
    public DateTime FechaExpiracion { get; set; }
    public int Nota { get; set; }
    public bool Inscripto { get; set; }

  }
}