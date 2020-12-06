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
using System.Collections.Generic;

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
      private readonly IPushGenerator pushGenerator;

      public Manejador(UdelarOnlineContext context, IBedeliasGenerator bedelias, IPushGenerator pushGenerator)
      {
        this.context = context;
        this.bedelias = bedelias;
        this.pushGenerator = pushGenerator;
      }

      public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
      {
        var curso = await this.context.Curso.FindAsync(request.CursoId);
        if (curso == null)
          throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { mensaje = "El curso no existe." });

        var alumnosDelCurso = await this.context.AlumnoCurso.Include(ac => ac.Alumno).Where(ac => ac.CursoId == curso.CursoId).ToListAsync();
        if (alumnosDelCurso == null)
          throw new ManejadorExcepcion(HttpStatusCode.BadRequest, new { mensaje = "El curso no tiene alumnos inscriptos." });
          
        var ciAlumnos = alumnosDelCurso.Select(a => a.Alumno.CI).ToArray();
        var actaCerrada = await this.bedelias.CerrarActa(ciAlumnos, curso.CursoId);
        
        curso.ActaCerrada = actaCerrada;
        if (!actaCerrada)
          throw new ManejadorExcepcion(HttpStatusCode.BadRequest, new { mensaje = "No se pudo cerrar el acta, verifique la lista de alumnos." });
       
         List<string> tokens = new List<string>();

          foreach (var ac in alumnosDelCurso)
          {
              ac.FechaActaCerrada = DateTime.UtcNow;
              if(ac.Alumno.TokenPush != "")
                tokens.Add(ac.Alumno.TokenPush);
              
          }

        pushGenerator.SendPushNotifications ("Ya están las notas!", "Ya fueron cerradas las actas del curso " + curso.Nombre, tokens);
        
        var result = await this.context.SaveChangesAsync();
        if (result > 0)
          return Unit.Value;

        throw new ManejadorExcepcion(HttpStatusCode.InternalServerError, new { mensaje = "Ocurrio un error al cerrar el acta del curso." });





      }
    }
  }
}