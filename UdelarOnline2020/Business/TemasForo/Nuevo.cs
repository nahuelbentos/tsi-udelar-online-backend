using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Aplicacion.ManejadorError;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Models;
using Persistence;

namespace Business.TemasForo
{
  public class Nuevo
  {

    public class Ejecuta : IRequest
    {
      public string Asunto { get; set; }
      public string Mensaje { get; set; }
      public string EmisorId { get; set; }
      public string ArchivoData { get; set; }
      public string ArchivoNombre { get; set; }
      public string ArchivoExtension { get; set; }
      public bool SuscripcionADiscusion { get; set; }

    }

    public class EjecutaValidator : AbstractValidator<Ejecuta>
    {
      public EjecutaValidator()
      {
        RuleFor(t => t.Asunto).NotEmpty().WithMessage("El asunto es requerido.");
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

        if (request.EmisorId != string.Empty)
        {

          var emisorId = await this.context.Usuario.Where(u => u.Id == request.EmisorId).FirstOrDefaultAsync();

          if (emisorId == null)
          {
            throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { mensaje = "No existe el emisor ingresado." });
          }
        }
        var temaForo = new TemaForo
        {
          TemaForoId = Guid.NewGuid(),
          Asunto = request.Asunto,
          Mensaje = request.Mensaje,
          EmisorId = request.EmisorId,
          ArchivoData = Convert.FromBase64String(request.ArchivoData),
          ArchivoNombre = request.ArchivoNombre,
          ArchivoExtension = request.ArchivoExtension,
          SubscripcionADiscusion = request.SuscripcionADiscusion,
        };

        this.context.TemaForo.Add(temaForo);

        var res = await this.context.SaveChangesAsync();
        if (res > 0)
        {
          return Unit.Value;
        }

        throw new Exception("No se pudo dar de alta el tema de foro");

      }
    }
  }
}