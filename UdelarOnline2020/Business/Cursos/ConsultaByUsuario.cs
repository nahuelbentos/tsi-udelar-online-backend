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

namespace Business.Cursos
{
  public class ConsultaByUsuario
  {
    public class Ejecuta : IRequest<List<Curso>>
    {
      public Guid Id { get; set; }

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
        var usuario = await this.context.Usuario.FindAsync(request.Id.ToString());
        if (usuario == null)
          throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { mensaje = "No existe un usuario con ese Id." });

        List<Curso> cursos = new List<Curso>();

        if( usuario is Alumno)
            cursos = await this.context.AlumnoCurso.Include(uc => uc.Curso)
                                                    .Where(u => u.AlumnoId == request.Id)
                                                    .Select(c => c.Curso)
                                                    .ToListAsync();
        else
          cursos = await this.context.UsuarioCurso.Include(uc => uc.Curso)
                                                  .Where(u => u.UsuarioId == request.Id)
                                                  .Select(c => c.Curso)
                                                  .ToListAsync();


        return cursos;



      }
    }
  }
}