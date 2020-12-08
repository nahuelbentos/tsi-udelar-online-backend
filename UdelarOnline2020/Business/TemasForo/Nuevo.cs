using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Business.ManejadorError;
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
      public Guid ForoId { get; set; }

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


          var emisor = await this.context.Usuario.Where(u => u.Id == request.EmisorId).FirstOrDefaultAsync();

          if (emisor == null)
            throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { mensaje = "No existe el emisor ingresado." });

          var foro = await this.context.Foro.Where( f => f.ForoId == request.ForoId).FirstOrDefaultAsync();

          if (foro == null)
            throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { mensaje = "No existe el foro ingresado." });

        var temaForo = new TemaForo
        {
          TemaForoId = Guid.NewGuid(),
          Asunto = request.Asunto,
          Mensaje = request.Mensaje,
          EmisorId = request.EmisorId,
          Emisor = emisor,
          ArchivoData = Convert.FromBase64String(request.ArchivoData),
          ArchivoNombre = request.ArchivoNombre,
          ArchivoExtension = request.ArchivoExtension,
          SubscripcionADiscusion = request.SuscripcionADiscusion,
          Foro = foro,
          ForoId = foro.ForoId,
        };

        this.context.TemaForo.Add(temaForo);

        var res = await this.context.SaveChangesAsync();
        if (res > 0)
          return Unit.Value;
        
        throw new ManejadorExcepcion(HttpStatusCode.InternalServerError, new { mensaje = "No se pudo dar de alta el tema de foro." });

      }
    }
  }
}