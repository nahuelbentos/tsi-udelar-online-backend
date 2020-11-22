using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Business.ManejadorError;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Models;
using Persistence;

namespace Business.Alumnos
{
  public class GetMisCursos
  {
    public class Ejecuta : IRequest<List<Curso>>
    {
      public Guid AlumnoId { get; set; }
    }

    public class Manejador : IRequestHandler<Ejecuta, List<Curso>>
    {
      private readonly UdelarOnlineContext context;

      public Manejador(UdelarOnlineContext context)
      {
        this.context = context;
      }

      public async Task<List<Curso>> Handle(Ejecuta request, CancellationToken cancellationToken)
      {

        var alumno = await this.context.Alumno.FindAsync(request.AlumnoId);
        if (alumno == null)
          throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { mensaje = "El alumno no existe." });

        var cursos = await this.context.AlumnoCurso
                                              .Where(ac => ac.AlumnoId == request.AlumnoId)
                                              .Select(ac => ac.Curso)
                                              .ToListAsync();

        return cursos;

      }
    }
  }
}