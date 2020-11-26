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

namespace Business.AlumnoPruebaOnlines
{
    public class ConsultaByPruebaOnlineId
    {
        public class Ejecuta : IRequest<List<AlumnoPruebaOnline>>
        {
            public Guid PruebaOnlineId { get; set; }

        }

        public class Manejador : IRequestHandler<Ejecuta, List<AlumnoPruebaOnline>>
        {
            private readonly UdelarOnlineContext context;

            public Manejador(UdelarOnlineContext context)
            {
                this.context = context;
            }

            public async Task<List<AlumnoPruebaOnline>> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var ap = await this.context.AlumnoPruebaOnline.Include(ap => ap.Alumno).Include(ap => ap.PruebaOnline).Include(ap => ap.ListaRespuestas).Where(ap => ap.PruebaOnlineId == request.PruebaOnlineId).ToListAsync();
                if (!ap.Any())
                {
                    throw new ManejadorExcepcion(HttpStatusCode.BadRequest, new { mensaje = "No se encontraron pruebas relacionadas a la PruebaOnlineId." });
                }
                return ap;
            }
        }
    }
}