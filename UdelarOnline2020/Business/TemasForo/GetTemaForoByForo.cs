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
            public string UsuarioId { get; set; }           
            
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
        var usuario = await this.context.Usuario
                                            .Include(u => u.Facultad)
                                            .Where(u => u.Id == request.UsuarioId)
                                            .FirstOrDefaultAsync();

        if(usuario == null)
          throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { mensaje = "No existe el usuario ingresado." });

        var foro = await this.context.Foro.Where(f => f.ForoId == request.ForoId).FirstOrDefaultAsync();

        if(foro == null)
          throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { mensaje = "No existe el foro ingresado." });

        var temasForo = await this.context.TemaForo
                                            .Include( tf => tf.Foro )
                                            .Include(tf => tf.Emisor ).ThenInclude( e => e.Facultad)
                                            .Where( temaForo => temaForo.ForoId == request.ForoId 
                                            && temaForo.Emisor.Facultad.FacultadId == usuario.Facultad.FacultadId)
                                            .ToListAsync();
        return temasForo;    
      }
    }
  }
}