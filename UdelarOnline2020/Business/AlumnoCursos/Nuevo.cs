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

namespace Business.AlumnoCursos
{
    public class Nuevo
    {
        public class Ejecuta : IRequest
    {
      public Guid AlumnoId { get; set; }

      public Guid CursoId { get; set; }
      public bool? Inscripto { get; set; }
      public int? Calificacion { get; set; }
      public string Feedback { get; set; }
      public DateTime? FechaActaCerrada { get; set; }

    }

    public class EjecutaValidator : AbstractValidator<Ejecuta>
    {
      public EjecutaValidator()
      {
        RuleFor(ac => ac.AlumnoId).NotEmpty().WithMessage("AlumnoId es requerido."); 
        RuleFor(ac => ac.CursoId).NotEmpty().WithMessage("CursoId es requerido.");
        RuleFor(ac => ac.Inscripto).NotEmpty();
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
        var alumno = await this.context.Alumno.Where(a => a.Id == request.AlumnoId.ToString()).FirstOrDefaultAsync();

        if (alumno == null)
        {
          throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { mensaje = "No existe el alumno ingresado" });
        }

        var curso = await this.context.Curso.FindAsync(request.CursoId);

        if (curso == null)
        {
          throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { mensaje = "No existe el curso ingresada" });
        }

        var control = await this.context.AlumnoCurso.Where(ac => ac.CursoId == request.CursoId && ac.AlumnoId == request.AlumnoId).FirstOrDefaultAsync();
        if (control != null)
        {
            throw new ManejadorExcepcion(HttpStatusCode.Conflict, new { mensaje = "Ya existe la relacion entre Alumno y Curso." });
        }
        
        var ac = new AlumnoCurso
        {
            CursoId = request.CursoId,
            Curso = curso,
            AlumnoId = request.AlumnoId,
            Alumno = alumno,
            Inscripto = request.Inscripto.GetValueOrDefault(),
            Calificacion = request.Calificacion.GetValueOrDefault(),
            Feedback = request.Feedback,
            FechaActaCerrada = request.FechaActaCerrada.GetValueOrDefault()
        };

        context.AlumnoCurso.Add(ac);
        var res = await this.context.SaveChangesAsync();
        if (res > 0)
        {
          return Unit.Value;
        }
        throw new ManejadorExcepcion(HttpStatusCode.InternalServerError, new { mensaje = "Ocurrio un error al inscribir el alumno al curso." });

      }
    }
    }
}