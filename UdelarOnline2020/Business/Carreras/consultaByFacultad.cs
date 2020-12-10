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

namespace Business.Carreras
{
    public class consultaByFacultad
    {
        public class Ejecuta : IRequest<List<Carrera>>
        {
            public Guid FacultadId { get; set; }            
            
        }

    public class Manejador : IRequestHandler<Ejecuta, List<Carrera>>
    {
      private readonly UdelarOnlineContext context;

      public Manejador(UdelarOnlineContext context)
      {
        this.context = context;
      }

      public async Task<List<Carrera>> Handle(Ejecuta request, CancellationToken cancellationToken)
      {

        var facultad = await this.context.Facultad.Where(f => f.FacultadId == request.FacultadId).FirstOrDefaultAsync();

        if (facultad == null)
          throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { mensaje = "No existe una facultad con el Id ingresado" });

        var carreras = await this.context.Carrera.Include(c => c.Facultad).Where(c => c.FacultadId == facultad.FacultadId).ToListAsync();

        return carreras;
      }
    }
  }
}