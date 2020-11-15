using System;
using System.Collections.Generic;

namespace Models
{
    public class Pregunta
    {
        public Guid PreguntaId { get; set; }

        public string Texto { get; set; }

        public Encuesta Encuesta { get; set; }

        public Guid EncuestaId { get; set; }

        public ICollection<Respuesta> RespuestaLista { get; set; }
    }
}