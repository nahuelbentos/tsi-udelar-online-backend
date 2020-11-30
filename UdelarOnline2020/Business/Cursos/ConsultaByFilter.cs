using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Business.Datatypes;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Models;
using Persistence;

namespace Business.Cursos
{
  public class ConsultaByFilter
  {
    public class Ejecuta : IRequest<List<DtCurso>>
    {
      public string Filter { get; set; }


    }

    public class Manejador : IRequestHandler<Ejecuta, List<DtCurso>>
    {
      private readonly UdelarOnlineContext context;

      public Manejador(UdelarOnlineContext context)
      {
        this.context = context;
      }

      public async Task<List<DtCurso>> Handle(Ejecuta request, CancellationToken cancellationToken)
      {
        var filter = request.Filter;
        var cursos = await this.context.Curso
                                    .Include(c => c.TemplateCurso)
                                    .Where(c => c.Nombre.Contains(filter) || c.Descripcion.Contains(filter) || c.SalaVirtual.Contains(filter))
                                    .ToListAsync();
                                    
        List<DtCurso> dtCursos = new List<DtCurso>();
        foreach (var curso in cursos)
        {   
            var docentes = await this.context.UsuarioCurso
                                                .Where( uc => uc.CursoId == curso.CursoId && uc.Usuario is Docente)
                                                .Select(c => c.Usuario)
                                                .ToListAsync();

            var alumnos = await this.context.AlumnoCurso
                                                .Where( ac => ac.CursoId == curso.CursoId )
                                                .Select(c => c.Alumno)
                                                .ToListAsync();

            var secciones = await this.context.CursoSeccion
                                                .Where( sc => sc.CursoId == curso.CursoId )
                                                .Select(c => c.Seccion)
                                                .ToListAsync();
                                    
            var dtCurso = new DtCurso {
                CursoId = curso.CursoId,
                Descripcion = curso.Descripcion,
                Docentes = docentes,
                ModalidadId = curso.Modalidad,
                Modalidad = Enum.GetName( typeof(ModalidadEnum),  curso.Modalidad ) ,
                Nombre = curso.Nombre,
                RequiereMatriculacion = curso.RequiereMatriculacion,
                SalaVirtual = curso.SalaVirtual,
                Alumnos = alumnos,
                Secciones = secciones,
                TemplateCurso = curso.TemplateCurso,
                TemplateCursoId = curso.TemplateCursoId,
                ActaCerrada = curso.ActaCerrada,
            };

            dtCursos.Add( dtCurso );
            
        }

        return dtCursos;    
      }
    }
  }
}