using System.Net;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Models;
using Persistence;
using Microsoft.Extensions.Logging;
using Business.ManejadorError;

namespace Business.CursosSeccion
{
  public class Editar
  {
    public class Ejecuta : IRequest
    {
      public Guid CursoSeccionId { get; set; }
      public Guid CursoId { get; set; }
      public Guid SeccionId { get; set; }
    }


    public class EjecutaValidacion : AbstractValidator<Ejecuta>
    {
      public EjecutaValidacion()
      {
        RuleFor(c => c.CursoId).NotEmpty();
        RuleFor(c => c.SeccionId).NotEmpty();
      }
    }

    public class Manejador : IRequestHandler<Ejecuta>
    {
      private readonly UdelarOnlineContext context;

      public Manejador(UdelarOnlineContext context)
      {
        this.context = context;
      }

      public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
      {
        var cursoSeccion = await this.context.CursoSeccion.FindAsync(request.CursoSeccionId);

        if (cursoSeccion == null)
        {
          throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { mensaje = "El curso seccion no existe" });

        }
         var curso = await this.context.Curso.FindAsync(request.CursoId);

        if (curso == null)
        {
          throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { mensaje = "El curso no existe" });

        }
         var seccion = await this.context.Seccion.FindAsync(request.SeccionId);

        if (seccion == null)
        {
          throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { mensaje = "La seccion no existe" });

        }

        
        cursoSeccion.CursoId = request.CursoId;
        cursoSeccion.Curso = curso ?? cursoSeccion.Curso;
        cursoSeccion.SeccionId = request.SeccionId;
        cursoSeccion.Seccion = seccion;


        var res = await this.context.SaveChangesAsync();



        if (res > 0)
          return Unit.Value;

        throw new ManejadorExcepcion(HttpStatusCode.InternalServerError, new { mensaje = "Ocurrio un error al editar el curso" });

      }
    }




  }
}