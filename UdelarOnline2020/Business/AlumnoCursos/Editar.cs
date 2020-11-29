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
using Persistence;

namespace Business.AlumnoCursos
{
    public class Editar
    {
        public class Ejecuta : IRequest
    {
      public Guid AlumnoCursoId {get; set;}
      public Guid CursoId { get; set; }
      public Guid AlumnoId { get; set; }
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
        var ac = await this.context.AlumnoCurso.Where(ac => ac.AlumnoId == request.AlumnoId && ac.CursoId == request.CursoId).FirstOrDefaultAsync();
        
        if (ac == null)
        {
          throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { mensaje = "No se encontro la relacion entre Alumno y Curso." });
        }

        ac.Calificacion = request.Calificacion ?? ac.Calificacion;
        ac.FechaActaCerrada = request.FechaActaCerrada ?? ac.FechaActaCerrada;
        ac.Feedback = request.Feedback ?? ac.Feedback;
        ac.Inscripto = request.Inscripto ?? ac.Inscripto;


        var res = await this.context.SaveChangesAsync();
        if (res < 0)
        {
            throw new ManejadorExcepcion(HttpStatusCode.InternalServerError, new { mensaje = "Ocurrio un error al editar la inscripcion" });
          
        }
        return Unit.Value;
      }
    }
    }
}