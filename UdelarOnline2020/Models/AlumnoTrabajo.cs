using System;

namespace Models
{
    public class AlumnoTrabajo
    {
        public String AlumnoCI { get; set; }

        public Usuario Alumno { get; set; }
        //Aca tiene que ser de tipo alumno pero me tira error

        public Guid TrabajoId { get; set; }

        public Trabajo Trabajo { get; set; }
    }
}