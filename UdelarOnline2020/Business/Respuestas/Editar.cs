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

using Persistence;

namespace Business.Respuestas
{
  public class Editar
  {
    public class Ejecuta : IRequest
    {
      public Guid RespuestaId { get; set; }
      public string Mensaje { get; set; }
      public Guid AlumnoId { get; set; }

    }

    public class EjecutaValidator : AbstractValidator<Ejecuta>
    {
      public EjecutaValidator()
      {

        RuleFor(c => c.Mensaje).NotEmpty().WithMessage("El Mensaje es requerido.");
        RuleFor(c => c.AlumnoId).NotEmpty().WithMessage("El AlumnoId es Requerido");
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
        var respuesta = await this.context.Respuesta.FindAsync(request.RespuestaId);
        //var alumno = await this.context.Alumno.Where(a => a.UserName == request.Alumno.UserName).FirstOrDefault();
        if (respuesta == null)
        {
          throw new ManejadorExcepcion(HttpStatusCode.BadRequest, new { mensaje = "No se encontro la respuesta" });
        }


        var alumno = await this.context.Alumno.Where(e => e.Id == request.AlumnoId.ToString()).FirstOrDefaultAsync();

        if (alumno == null)
        {
          throw new ManejadorExcepcion(HttpStatusCode.BadRequest, new { mensaje = "No existe el alumno ingresado" });
        }

        respuesta.Mensaje = request.Mensaje ?? respuesta.Mensaje;
        respuesta.Alumno = alumno ?? respuesta.Alumno;
        respuesta.FechaRealizada = DateTime.Now;

        var res = await this.context.SaveChangesAsync();
        if (res > 0)
        {
          return Unit.Value;
        }
        throw new ManejadorExcepcion(HttpStatusCode.InternalServerError, new { mensaje = "Ocurrio un error al editar la respuesta" });
      }
    }
  }

}