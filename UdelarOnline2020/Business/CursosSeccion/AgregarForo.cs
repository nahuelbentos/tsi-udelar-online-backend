
using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Business.ManejadorError;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Models;
using Persistence;

namespace Business.CursosSeccion {

  public class AgregarForo {

    public class Ejecuta : IRequest {
      public Guid CursoId { get; set; }
      public Guid SeccionId {get; set;}
      public Guid ForoId { get; set; }

    }

 

    public class Manejador : IRequestHandler<Ejecuta> {
      private readonly UdelarOnlineContext context;
      

      public Manejador (UdelarOnlineContext context) {
        this.context = context;
      }

      public async Task<Unit> Handle (Ejecuta request, CancellationToken cancellationToken) {

        var cursoSeccion = await this.context.CursoSeccion.Where (tc => tc.CursoId == request.CursoId && tc.SeccionId == request.SeccionId).FirstOrDefaultAsync ();
        if (cursoSeccion == null) 
          throw new ManejadorExcepcion (HttpStatusCode.BadRequest, new { mensaje = "No existe el curso ingresado" });
        
        var foro = await this.context.Foro.Where (tc => tc.ForoId == request.ForoId).FirstOrDefaultAsync ();
        if (foro == null) 
        throw new ManejadorExcepcion (HttpStatusCode.BadRequest, new { mensaje = "No existe el foro ingresado" });
        
        this.context.CursoSeccionForo.Add(new CursoSeccionForo{
          Curso = cursoSeccion.Curso,
          CursoId = cursoSeccion.CursoId,
          Seccion = cursoSeccion.Seccion,
          SeccionId = cursoSeccion.SeccionId,
          Foro = foro,
          ForoId = foro.ForoId,
        }); 

        var res = await this.context.SaveChangesAsync ();
        if (res > 0) {
          return Unit.Value;
        }

        throw new ManejadorExcepcion (HttpStatusCode.InternalServerError, new { mensaje = "Ocurrio un error al insertar el curso" });

      }
    }

  }
}