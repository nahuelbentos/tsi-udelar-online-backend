using System.Net;
using System;
using System.Threading;
using System.Threading.Tasks;
using Business.Interfaces;
using MediatR;
using Persistence;
using Business.ManejadorError;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Business.Cursos
{
    public class CerrarActas
    {
        public class Ejecuta : IRequest        
        {
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
        var curso = await this.context.Curso.FindAsync( request.CursoId );
        if(curso == null)
            throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { mensaje = "El curso no existe."} );

        var alumnosDelCurso = await this.context.AlumnoCurso.Where( ac => ac.CursoId == curso.CursoId).ToListAsync();
        var ciAlumnos = alumnosDelCurso.Select( a => a.Alumno.CI).ToArray();
        var actaCerrada = await this.bedelias.CerrarActa(ciAlumnos, curso.CursoId);

        curso.ActaCerrada = actaCerrada;
        if(!actaCerrada)
            throw new ManejadorExcepcion(HttpStatusCode.BadRequest, new { mensaje = "No se pudo cerrar el acta, verifique la lista de alumnos."} );

        if(await this.context.SaveChangesAsync() > 0)
            return Unit.Value;

        throw new ManejadorExcepcion(HttpStatusCode.InternalServerError, new { mensaje = "Ocurrio un error al cerrar el acta del curso."} );
            
        



      }
    }
  }
}