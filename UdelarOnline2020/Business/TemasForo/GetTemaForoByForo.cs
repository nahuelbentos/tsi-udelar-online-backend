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

namespace Business.TemasForo
{
    public class GetTemaForoByForo
    {
        public class Ejecuta : IRequest<List<TemaForo>>
        {
            public Guid ForoId { get; set; }           
            
        }

    public class Manejador : IRequestHandler<Ejecuta, List<TemaForo>>
    {
      private readonly UdelarOnlineContext context;

      public Manejador(UdelarOnlineContext context)
      {
        this.context = context;
      }

      public async Task<List<TemaForo>> Handle(Ejecuta request, CancellationToken cancellationToken)
      {
        var foro = await this.context.Foro.Where(f => f.ForoId == request.ForoId).FirstOrDefaultAsync();

        if(foro == null)
          throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { mensaje = "No existe el foro ingresado." });

        var temasForo = await this.context.TemaForo
                                            .Include( tf => tf.Foro )
                                            .Include(tf => tf.Emisor )
                                            .Where( temaForo => temaForo.ForoId == request.ForoId)
                                            .ToListAsync();
        return temasForo;    
      }
    }
  }
}