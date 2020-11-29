using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Business.ManejadorError;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Models;
using Persistence;

namespace Business.Actividades
{
    public class ConsultaByTipo
    {
        public class Ejecuta : IRequest<List<Actividad>>
        {
            public string Tipo { get; set; }

        }

    public class Manejador : IRequestHandler<Ejecuta, List<Actividad>>
    {
      private readonly UdelarOnlineContext context;

      public Manejador(UdelarOnlineContext context)
      {
        this.context = context;
      }

      public async Task<List<Actividad>> Handle(Ejecuta request, CancellationToken cancellationToken)
      {
        List<Actividad> actividades = null;

        switch (request.Tipo)
        {
          case "Trabajo":
            actividades = await this.context.Trabajo.Include(a => a.Usuario).ToListAsync<Actividad>();
            break;
          case "Encuesta":
            actividades = await this.context.Encuesta.Include(a => a.Usuario).ToListAsync<Actividad>();
            break;
          case "ClaseDictada":
            actividades = await this.context.ClaseDictada.Include(a => a.Usuario).ToListAsync<Actividad>();
            break;
          case "PruebaOnline":
            actividades = await this.context.PruebaOnline.Include(a => a.Usuario).ToListAsync<Actividad>();
            break;
          default:
            throw new ManejadorExcepcion(HttpStatusCode.BadRequest, new { mensaje = "El tipo de usuario debe ser Trabajo, Encuesta, ClaseDictada o PruebaOnline" });

        }

        return actividades;
      }
    }
  }
}