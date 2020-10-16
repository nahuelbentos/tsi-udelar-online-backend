using System;

namespace Models
{
    public class AlumnoClasesDictadas
    {
        public String AlumnoCI { get; set; }

        public Usuario Alumno { get; set; }
        //Tiene que ser de tipo Alumno pero me tira error

        public Guid ClasesDictadasId { get; set; }

        public ClaseDictada ClaseDictada { get; set; }
    }
}