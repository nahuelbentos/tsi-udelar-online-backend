using System.Net;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Business.ManejadorError;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Models;
using Persistence;
using Business.Datatypes;

namespace Business.Cursos
{
  public class ConsultaById
  {
    public class Ejecuta : IRequest<DtCurso>
    {
      public Guid CursoId { get; set; }
    }
    public class Manejador : IRequestHandler<Ejecuta, DtCurso>
    {
      private readonly UdelarOnlineContext context;

      public Manejador(UdelarOnlineContext context)
      {
        this.context = context;
      }

      public async Task<DtCurso> Handle(Ejecuta request, CancellationToken cancellationToken)
      {
        // Esto cambia para devolver una lista de DataTypes, en breves lo cambio.
        var curso = await this.context.Curso.Include(c => c.TemplateCurso).Where(c => c.CursoId == request.CursoId).FirstOrDefaultAsync();
        
        if (curso == null)
          throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { mensaje = "No existe un curso con el CursoId ingresado" });
        
        var docentes = await this.context.UsuarioCurso
                                                .Where(uc => uc.CursoId == curso.CursoId && uc.Usuario is Docente)
                                                .Select(c => c.Usuario)
                                                .ToListAsync();

        var alumnos = await this.context.AlumnoCurso
                                            .Where(ac => ac.CursoId == curso.CursoId)
                                            .Select(c => c.Alumno)
                                            .ToListAsync();

        var cursoSecciones = await this.context.CursoSeccion
                                            .Include( cs => cs.Seccion)
                                            .Include( cs => cs.ActividadLista)
                                            .Include( cs => cs.ForoLista)
                                            .Include( cs => cs.MaterialLista)
                                            .Where(sc => sc.CursoId == curso.CursoId) 
                                            .ToListAsync();
        var comunicados = await this.context.ComunicadoCurso
        
                                            .Where(cc => cc.CursoId == curso.CursoId)
                                            .Select(c => c.Comunicado)
                                            .ToListAsync();

        foreach (var cs in cursoSecciones)
        {
          var materialLista = await this.context.CursoSeccionMaterial
                                                  .Where( csm => csm.CursoId == cs.CursoId && csm.Seccion == cs.Seccion)
                                                  .Select( csm => csm.Material)
                                                  .ToListAsync();
          Console.WriteLine( "counte:: " + materialLista.Count)  ;
          cs.MaterialLista = materialLista;
 
          
        }  

        var dtCurso = new DtCurso
        {
          CursoId = curso.CursoId,
          Descripcion = curso.Descripcion,
          ModalidadId = curso.Modalidad,
          Docentes = docentes,
          Alumnos = alumnos,
          CursoSecciones = cursoSecciones,
          Comunicados = comunicados,
          Modalidad = Enum.GetName(typeof(ModalidadEnum), curso.Modalidad),
          Nombre = curso.Nombre,
          RequiereMatriculacion = curso.RequiereMatriculacion,
          SalaVirtual = curso.SalaVirtual,
          TemplateCurso = curso.TemplateCurso,
          TemplateCursoId = curso.TemplateCursoId,
          ActaCerrada = curso.ActaCerrada,
        };
        return dtCurso;
      }

    }
  }
}