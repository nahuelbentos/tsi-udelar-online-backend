using System;

namespace Models
{
    public class DocenteTrabajo
    {
        public Guid DocenteId { get; set; }

        public Docente Docente { get; set; }

        public Guid TrabajoId { get; set; }

        public Trabajo Trabajo { get; set; }
    }
}