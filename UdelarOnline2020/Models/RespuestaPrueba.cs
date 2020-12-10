using System;
namespace Models
{
    public class RespuestaPrueba
    {
        public Guid RespuestaPruebaId { get; set; }
        
        
        public Guid PreguntaId { get; set; }

        public PreguntaRespuesta PreguntaRespuesta { get; set; }

        public int RespuestaId  { get; set; }

        // Me olvide de agregarlos en la primer migracion y los agrego así a SQLServer, 
        // con el prefijo AlumnoPruebaOnline
        public Guid AlumnoPruebaOnlineAlumnoId { get; set; }

        public Guid AlumnoPruebaOnlinePruebaOnlineId { get; set; }
        public Alumno Alumno { get; set; }

        public PruebaOnline PruebaOnline { get; set; }
        
    }
}