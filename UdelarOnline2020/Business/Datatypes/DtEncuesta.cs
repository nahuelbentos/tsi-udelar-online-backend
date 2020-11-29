using System;
using System.Collections.Generic;
using Models;

namespace Business.Datatypes
{
    public class DtEncuesta : DtActividad
    {

    public bool EsAdministrador { get; set; }

    public ICollection<Pregunta> PreguntaLista { get; set; }

    public Facultad Facultad { get; set; }
    public Guid? FacultadId { get; set; }
        
    }
}