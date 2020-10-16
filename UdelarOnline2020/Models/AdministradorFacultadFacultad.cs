using System;

namespace Models
{
    public class AdministradorFacultadFacultad
    {
        public Guid AdministradorFacultadId { get; set; }

        public Usuario AdministradorFacultad { get; set; }
        //Puse usuario porque me tira error si pongo de tipo AdministradorFacultad

        public Guid FacultadId { get; set; }

        public Facultad Facultad { get; set; }

    }
}