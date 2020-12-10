using System;
using System.Collections.Generic;

namespace Business.Datatypes
{
    public class DtAlumnoPruebaOnline 
    {
        public Guid PruebaOnlineId { get; set; }
        public Guid AlumnoId { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaExpiracion { get; set; }
        public DateTime FechaFin { get; set; }
        public bool Inscripto { get; set; }
        public bool RealizadaPorAlumno { get; set; }
        public int Nota { get; set; }
        public float PorcentajeCalificacion { get; set; }

        public List<DtRespuestaPrueba> RespuestasAlumno { get; set; }
        
        
        
    }
 
}