using System;
namespace Models
{
    public class RespuestaPrueba
    {
        public Guid RespuestaPruebaId { get; set; }
        
        
        public Guid PreguntaId { get; set; }

        public PreguntaRespuesta PreguntaRespuesta { get; set; }

        public int RespuestaId  { get; set; }
        
    }
}