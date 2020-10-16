using System;

namespace Models
{
    public class DocenteTrabajo
    {
        public String DocenteCI { get; set; }

        public Docente Docente { get; set; }

        public Guid TrabajoId { get; set; }

        public Trabajo Trabajo { get; set; }
    }
}