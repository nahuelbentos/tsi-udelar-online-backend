using System;
using System.Collections.Generic;
using Models;

namespace Business.Datatypes
{
    public class DtCurso
    {

        public Guid CursoId { get; set; }
        public string Descripcion { get; set; }
        public string Nombre { get; set; }
        public ModalidadEnum ModalidadId { get; set; }
        public string Modalidad { get; set; }
        public bool RequiereMatriculacion { get; set; }
        public string SalaVirtual { get; set; }
        public TemplateCurso TemplateCurso { get; set; }
        public Guid? TemplateCursoId { get; set; }
        public bool ActaCerrada { get; set; }

        public List<CursoSeccion> CursoSecciones { get; set; }
        public List<Seccion> Secciones { get; set; }
        public List<Comunicado> Comunicados { get; set; }
        public List<Alumno> Alumnos { get; set; }
        public List<Usuario> Docentes { get; set; }
        
        
    }
}