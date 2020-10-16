using System;

namespace Models
{
    public class CarreraCurso
    {
        public Guid CarreraId { get; set; }

        public Carrera Carrera { get; set; }

        public Guid CursoId { get; set; }

        public Curso Curso { get; set; }
    }
}