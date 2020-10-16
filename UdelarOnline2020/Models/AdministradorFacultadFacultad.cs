using System;

namespace Models
{
    public class AdministradorFacultadFacultad
    {
        public String AdministradorFacultadCI { get; set; }

        public Usuario AdministradorFacultad { get; set; }
        //Puse usuario porque me tira error si pongo de tipo AdministradorFacultad

        public Guid FacultadId { get; set; }

        public Facultad Facultad { get; set; }

    }
}