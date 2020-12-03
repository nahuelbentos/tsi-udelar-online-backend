using System;
using System.Collections.Generic;
using Models;

namespace Business.Datatypes
{
    public class DtCursoSeccion
    {
    public Guid CursoSeccionId { get; set; }
    public Guid CursoId { get; set; }
    public string Curso {get; set;} 
    public Curso CursoData { get; set; }
    public Guid SeccionId { get; set; }
    public Seccion SeccionData { get; set; }
     public string Seccion {get; set;} 


    public ICollection<Material> MaterialLista { get; set; }
    public ICollection<Foro> ForoLista { get; set; }
    public ICollection<DtActividadLista> ActividadLista { get; set; }
    }
}