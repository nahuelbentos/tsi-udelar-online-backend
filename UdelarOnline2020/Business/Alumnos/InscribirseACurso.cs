using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Business.Interfaces;
using Business.ManejadorError;
using MediatR;
using Models;
using Persistence;

namespace Business.Alumnos
{
    public class InscribirseACurso
    {
        public class Ejecuta : IRequest
        {

      public string AlumnoId { get; set; }

      public Guid CursoId { get; set; }
        }

    public class Manejador : IRequestHandler<Ejecuta>
    {
      private readonly UdelarOnlineContext context;
      private readonly IBedeliasGenerator bedelias;

      public Manejador(UdelarOnlineContext context, IBedeliasGenerator bedelias)
      {
        this.context = context;
        this.bedelias = bedelias;
      }
      public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
      {

        var alumno = await this.context.Alumno.FindAsync(request.AlumnoId);
        if (alumno == null)
          throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { mensaje = "El alumno no existe." });


        var curso = await this.context.Curso.FindAsync(request.CursoId);

        if (curso == null)
          throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { mensaje = "El curso no existe" });

        var inscripto = true;
        if(curso.RequiereMatriculacion){
            inscripto = await this.bedelias.AprobarInscripcionCurso(alumno.CI, curso.CursoId);
            if(!inscripto)
                throw new ManejadorExcepcion(HttpStatusCode.BadRequest, new { mensaje = "Bedelías rechazo la inscripcion, comuniquese con un administrador." });
        }

        var alumnoCurso = new AlumnoCurso {
            Alumno = alumno,
            AlumnoId = Guid.Parse( alumno.Id ),
            Curso = curso,
            CursoId = curso.CursoId,
            Inscripto = inscripto
        };

        this.context.AlumnoCurso.Add( alumnoCurso );

        if(await this.context.SaveChangesAsync() > 0)
            return Unit.Value;

        throw new ManejadorExcepcion(HttpStatusCode.InternalServerError, new { mensaje = "Ocurrió un error al inscribir al alumno en el curso." });


      }
    }
  }
}