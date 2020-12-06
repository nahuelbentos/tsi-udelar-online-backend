using System.Net;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Business.Datatypes;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Business.ManejadorError;
using System;

namespace Business.AlumnoCursos
{
    public class ConsultaById
    {
        public class Ejecuta : IRequest<DtAlumnoCurso>
        {
            public string AlumnoId { get; set; }
            public Guid CursoId { get; set; }           
            
        }

    public class Manejador : IRequestHandler<Ejecuta, DtAlumnoCurso>
    {
      private readonly UdelarOnlineContext context;

      public Manejador(UdelarOnlineContext context)
      {
        this.context = context;
      }

      public async Task<DtAlumnoCurso> Handle(Ejecuta request, CancellationToken cancellationToken)
      {
        var alumno = await this.context.Alumno.Where( a => a.Id == request.AlumnoId ).FirstOrDefaultAsync();
        if(alumno == null)
            throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { mensaje = "No existe un alumno con el id ingresado"});
        
        var curso = await this.context.Curso.Where( c => c.CursoId == request.CursoId ).FirstOrDefaultAsync();
        if(curso == null)
            throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { mensaje = "No existe un curso con el id ingresado"});


        var alumnoCurso = await this.context.AlumnoCurso.Where( ac => ac.AlumnoId == Guid.Parse( request.AlumnoId ) && ac.CursoId == ac.CursoId ).FirstOrDefaultAsync();

        if(alumnoCurso == null)
          throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { mensaje = "No existe un registro de calificación para ese alumno en ese curso." });

          return new DtAlumnoCurso
          {
            Alumno = $" {alumnoCurso.Alumno.Nombres}  {alumnoCurso.Alumno.Apellidos}",
            AlumnoId = alumnoCurso.AlumnoId,
            Calificacion = alumnoCurso.Calificacion,
            Curso = $" {alumnoCurso.Curso.Nombre} - {alumnoCurso.Curso.Descripcion}",
            CursoId = alumnoCurso.CursoId,
            DataAlumno = alumnoCurso.Alumno,
            DataCurso = alumnoCurso.Curso,
            FechaActaCerrada = alumnoCurso.FechaActaCerrada,
            Feedback = alumnoCurso.Feedback,
            Inscripto = alumnoCurso.Inscripto
          };
      }
    }
  }
}