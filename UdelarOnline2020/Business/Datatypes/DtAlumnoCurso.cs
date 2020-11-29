using System;
using Models;

namespace Business.Datatypes
{
    public class DtAlumnoCurso
    {

    public Guid AlumnoId { get; set; }

    public Alumno DataAlumno { get; set; }
    public string Alumno { get; set; }     

    public Guid CursoId { get; set; }

    public string Curso { get; set; }
    public Curso DataCurso { get; set; }

    public bool Inscripto { get; set; }
    public int Calificacion { get; set; }
    public string Feedback { get; set; }
    public DateTime FechaActaCerrada { get; set; }
    }
}