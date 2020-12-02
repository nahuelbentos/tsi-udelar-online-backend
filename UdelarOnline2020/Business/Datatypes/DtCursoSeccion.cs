using System;
using System.Collections.Generic;
using Models;

namespace Business.Datatypes
{
    public class DtCursoSeccion
    {
    public Guid CursoSeccionId { get; set; }
    public Guid CursoId { get; set; }
    public Curso Curso { get; set; }
    public Guid SeccionId { get; set; }
    public Seccion Seccion { get; set; }


    public ICollection<Material> MaterialLista { get; set; }
    public ICollection<Foro> ForoLista { get; set; }
    public ICollection<DtActividadLista> ActividadLista { get; set; }
    }
}