using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Aplicacion.ManejadorError;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Business.TemasForo
{
  public class Editar
  {

    public class Ejecuta : IRequest
    {
      public Guid TemaForoId { get; set; }
      public string Asunto { get; set; }
      public string Mensaje { get; set; }
      public string EmisorId { get; set; }
      public string ArchivoAdjunto { get; set; }
      public bool SubscripcionADiscusion { get; set; }
    }

    public class EjecutaValidacion : AbstractValidator<Ejecuta>
    {
      public EjecutaValidacion()
      {
        RuleFor(t => t.Asunto).NotEmpty().WithMessage("El asunto es requerido");
        RuleFor(t => t.Mensaje).NotEmpty();
        RuleFor(t => t.EmisorId).NotEmpty();
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

        var temaForo = await this.context.TemaForo.FindAsync(request.TemaForoId);

        if (temaForo == null)
        {
          throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { mensaje = "El tema del foro no existe" });

        }

        if (request.EmisorId != string.Empty)
        {

          var emisorId = await this.context.Usuario.Where(u => u.Id == request.EmisorId).FirstOrDefaultAsync();

          if (emisorId == null)
          {
            throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { mensaje = "El emisor enviado no existe." });
          }

          temaForo.Emisor = emisorId;

        }

        temaForo.Asunto = request.Asunto ?? temaForo.Asunto;
        temaForo.Mensaje = request.Mensaje ?? temaForo.Mensaje;
        temaForo.ArchivoAdjunto = Convert.FromBase64String(request.ArchivoAdjunto) ?? temaForo.ArchivoAdjunto;
        temaForo.SubscripcionADiscusion = request.SubscripcionADiscusion;

        var res = await this.context.SaveChangesAsync();

        if (res > 0)
          return Unit.Value;

        throw new ManejadorExcepcion(HttpStatusCode.InternalServerError, new { mensaje = "Ocurrio un error al editar el tema del foro" });

      }
    }
  }
}