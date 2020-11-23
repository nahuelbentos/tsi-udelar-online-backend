using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Business.ManejadorError;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Models;
using Persistence;

namespace Business.Cursos
{
  public class AsignarDocente
  {
    public class Ejecuta : IRequest
    {
      public Guid CursoId { get; set; }
      public Guid DocenteId { get; set; }

    }

    public class Manejador : IRequestHandler<Ejecuta>
    {
      private readonly UdelarOnlineContext context;
      private readonly UserManager<Usuario> userManager;
      public Manejador(UdelarOnlineContext context, UserManager<Usuario> userManager)
      {
        this.userManager = userManager;
        this.context = context;
      }

      public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
      {
        var docente = await this.context.Users.Where(u =>  u.Id  == request.DocenteId.ToString() ).FirstOrDefaultAsync();

        if (docente == null)
          throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { mensaje = "No existe un usuario con ese Id." });

        var curso = await this.context.Curso.FindAsync(request.CursoId);

        if (curso == null)
          throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { mensaje = "No existe un curso con ese Id." });

        if (docente.GetType() != typeof(Docente))
          throw new ManejadorExcepcion(HttpStatusCode.BadRequest, new { mensaje = "El usuario ingresado no es Docente." });

        var existeDocenteEnCurso = await this.context.UsuarioCurso
                                                        .Where(dc => dc.CursoId == request.CursoId && dc.UsuarioId ==  request.DocenteId)
                                                        .FirstOrDefaultAsync();
        
        if(existeDocenteEnCurso != null)
          throw new ManejadorExcepcion(HttpStatusCode.BadRequest, new { mensaje = "Ya se encuentra asignado el docente ingresado en el curso." });

        var docenteCurso = new UsuarioCurso
        {
          Curso = curso,
          CursoId = curso.CursoId,
          Usuario = docente,
          UsuarioId = Guid.Parse(docente.Id)
        };

        this.context.UsuarioCurso.Add(docenteCurso);
        var res = await this.context.SaveChangesAsync();


        if (res > 0)
          return Unit.Value;

        throw new ManejadorExcepcion(HttpStatusCode.InternalServerError, new { mensaje = "Ocurrio un error al agregar el docanete al curso" }); 


      }
    }
  }
}