using System;

namespace Models
{
    public class AlumnoCurso {

        public Guid AlumnoId { get; set; }

        public Usuario alumno { get; set; }
        //tendria que ir tipo Alumno pero me tiraba error

        public Guid CursoId { get; set; }

        public Curso Curso { get; set; }
    }
}