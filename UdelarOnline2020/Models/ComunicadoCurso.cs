using System;

namespace Models
{
    public class ComunicadoCurso
    {
        public Guid ComunicadoId { get; set; }

        public Comunicado Comunicado { get; set; }

        public Guid CursoId { get; set; }

        public Curso Curso { get; set; }
    }
}