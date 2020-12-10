using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Business.ManejadorError;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Models;
using Persistence;

namespace Business.MensajesTema
{
    public class GetMensajesByTemaForo
    {
        public class Ejecuta : IRequest<List<MensajeTema>>
        {
            public Guid TemaForoId { get; set; }            
            
        }

    public class Manejador : IRequestHandler<Ejecuta, List<MensajeTema>>
    {
      private readonly UdelarOnlineContext context;

      public Manejador(UdelarOnlineContext context)
      {
        this.context = context;
      }

      public async Task<List<MensajeTema>> Handle(Ejecuta request, CancellationToken cancellationToken)
      {
        var temaForo = await this.context.TemaForo
                                            .Where( tf => tf.TemaForoId == request.TemaForoId )
                                            .FirstOrDefaultAsync();
        if (temaForo == null)
          throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { mensaje = "No existe el tema foro ingresado." });

        var mensajesTema = await this.context.MensajeTema
                                                .Include( mt => mt.Emisor)
                                                .Include( mt => mt.TemaForo)
                                                .Where( mt => mt.TemaForoId == request.TemaForoId)
                                                .ToListAsync();
        return mensajesTema;
      }
    }
  }
}