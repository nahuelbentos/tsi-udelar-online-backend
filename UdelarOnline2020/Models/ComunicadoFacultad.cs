using System;

namespace Models
{
    public class ComunicadoFacultad
    {
        public Guid ComunicadoId { get; set; }

        public Comunicado Comunicado { get; set; }

        public Guid FacultadId { get; set; }

        public Facultad Facultad { get; set; }
    }
}