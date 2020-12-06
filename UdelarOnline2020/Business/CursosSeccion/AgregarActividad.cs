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

namespace Business.CursosSeccion
{
  public class AgregarActividad
  {

    public class Ejecuta : IRequest
    {
      public Guid CursoId { get; set; }
      public Guid SeccionId { get; set; }
      public Guid ActividadId { get; set; }

    }

    public class EjecutaValidator : AbstractValidator<Ejecuta>
    {
      public EjecutaValidator()
      {
        RuleFor(c => c.CursoId).NotEmpty();
        RuleFor(c => c.SeccionId).NotEmpty();
        RuleFor(c => c.ActividadId).NotEmpty();
      }
    }

    public class Manejador : IRequestHandler<Ejecuta>
    {
      private readonly UdelarOnlineContext context;
      private readonly ILogger<Manejador> logger;

      public Manejador(UdelarOnlineContext context, ILogger<Manejador> logger)
      {
        this.context = context;
        this.logger = logger;
      }

      public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
      {
        Console.WriteLine("0");
        var cursoSeccion = await this.context.CursoSeccion
                                              .Include(cs => cs.Curso)
                                              .Include(cs => cs.Seccion)
                                              .Where(tc => tc.CursoId == request.CursoId && tc.SeccionId == request.SeccionId)
                                              .FirstOrDefaultAsync();
        if (cursoSeccion == null)
          throw new ManejadorExcepcion(HttpStatusCode.BadRequest, new { mensaje = "No existe el curso ingresado" });
        Console.WriteLine("1");
        var actividad = await this.context.Actividad.Where(tc => tc.ActividadId == request.ActividadId).FirstOrDefaultAsync();
        if (actividad == null)
          throw new ManejadorExcepcion(HttpStatusCode.BadRequest, new { mensaje = "No existe la actividad ingresada" });
        Console.WriteLine("2");

        var cursoSeccionActividad = await this.context.CursoSeccionActividad
                                                          .Where(tc => tc.ActividadId == request.ActividadId
                                                                    && tc.CursoId == cursoSeccion.CursoId
                                                                    && tc.SeccionId == cursoSeccion.SeccionId)
                                                          .FirstOrDefaultAsync();
        Console.WriteLine("3");
        if (cursoSeccionActividad != null)
          throw new ManejadorExcepcion(HttpStatusCode.BadRequest, new { mensaje = "Ya se encuentra asignada la actividad al curso-seccion" });
        Console.WriteLine("4");
        var csa = new CursoSeccionActividad
        {
          Actividad = actividad,
          ActividadId = actividad.ActividadId,
          Curso = cursoSeccion.Curso,
          CursoId = cursoSeccion.Curso.CursoId,
          Seccion = cursoSeccion.Seccion,
          SeccionId = cursoSeccion.Seccion.SeccionId,
        };
        Console.WriteLine("5");
        this.context.CursoSeccionActividad.Add(csa);
        Console.WriteLine("6");
        var res = await this.context.SaveChangesAsync();

        if (res > 0)
          return Unit.Value;


        throw new ManejadorExcepcion(HttpStatusCode.InternalServerError, new { mensaje = "Ocurrio un error al insertar el curso" });

      }
    }

  }
}