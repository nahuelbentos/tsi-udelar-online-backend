using System;

namespace Models
{
    public class Mensaje
    {
        public Guid Id { get; set; }

        public String Contenido { get; set; }

        public DateTime FechaDeEnviado { get; set; }
    }
}