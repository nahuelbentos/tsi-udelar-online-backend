using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Aplicacion.ManejadorError;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Models;
using Persistence;

namespace Business.Respuestas
{
  public class Nuevo
  {

    public class Ejecuta : IRequest
    {

      public string Mensaje { get; set; }
      public Guid AlumnoId { get; set; }

      public Guid EncuestaId { get; set; }

    }

    public class EjecutaValidator : AbstractValidator<Ejecuta>
    {
      public EjecutaValidator()
      {
        RuleFor(c => c.Mensaje).NotEmpty().WithMessage("El Mensaje es requerido.");
        RuleFor(c => c.AlumnoId).NotEmpty().WithMessage("El Alumno es Requerido");
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
        var alumno = await this.context.Alumno.Where(e => e.Id == request.AlumnoId.ToString()).FirstOrDefaultAsync();
        if (alumno == null)
        {
          throw new ManejadorExcepcion(HttpStatusCode.BadRequest, new { mensaje = "No existe el alumno ingresado" });
        }
        var encuesta = await this.context.Encuesta.FindAsync(request.EncuestaId);
        if (encuesta == null)
        {
          throw new ManejadorExcepcion(HttpStatusCode.BadRequest, new { mensaje = "No existe la encuesta ingresada" });
        }
        var resp = new Respuesta
        {
          Mensaje = request.Mensaje,
          Alumno = alumno,
          Encuesta = encuesta,
          FechaRealizada = new DateTime()
        };

        context.Respuesta.Add(resp);
        var res = await this.context.SaveChangesAsync();
        if (res > 0)
        {
          return Unit.Value;
        }
        throw new ManejadorExcepcion(HttpStatusCode.InternalServerError, new { mensaje = "Ocurrio un error al insertar la respuesta" });
      }
    }
  }
}