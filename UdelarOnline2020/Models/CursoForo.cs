using System;

namespace Models
{
    public class CursoForo
    {
        public Guid CursoId { get; set; }

        public Curso Curso { get; set; }

        public Guid ForoId { get; set; }

        public Foro Foro { get; set; }
    }
}