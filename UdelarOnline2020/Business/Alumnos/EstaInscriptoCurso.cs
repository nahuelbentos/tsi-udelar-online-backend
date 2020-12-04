using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Business.ManejadorError;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Business.Alumnos
{
    public class EstaInscriptoCurso
    {
        public class Ejecuta : IRequest<bool>
        {
            public string AlumnoId { get; set; }
            public Guid CursoId { get; set; }

        }

    public class Manejador : IRequestHandler<Ejecuta, bool>
    {
      private readonly UdelarOnlineContext context;

      public Manejador(UdelarOnlineContext context)
      {
        this.context = context;
      }

      public async Task<bool> Handle(Ejecuta request, CancellationToken cancellationToken)
      {

        var alumno = await this.context.Alumno.FindAsync(request.AlumnoId);
        if (alumno == null)
          throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { mensaje = "El alumno no existe." });


        var curso = await this.context.Curso.FindAsync(request.CursoId);

        if (curso == null)
          throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { mensaje = "El curso no existe" });


        var estaInscripto = await this.context.AlumnoCurso
                                                        .Where(ac => ac.CursoId == request.CursoId && ac.Alumno.Id == request.AlumnoId)
                                                        .FirstOrDefaultAsync();

        return estaInscripto != null;
          
      }
    }
  }
}