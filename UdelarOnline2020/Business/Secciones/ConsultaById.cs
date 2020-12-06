using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Business.ManejadorError;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Models;
using Persistence;

namespace Business.Secciones {
    public class ConsultaById
    {
        public class Ejecuta : IRequest<Seccion>
        {
            public Guid SeccionId { get; set; }
        }

        public class Manejador : IRequestHandler<Ejecuta, Seccion>
        {
            private readonly UdelarOnlineContext context;

            public Manejador(UdelarOnlineContext context)
            {
                this.context = context;
            }

            public async Task<Seccion> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                //Hay que reemplazarlo con DataTypes
                var carrera = await this.context.Seccion
                                                    .FirstOrDefaultAsync(c => c.SeccionId == request.SeccionId);
                if (carrera == null)
                {
                    throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { mensaje = "No existe una seccion con el SeccionId ingresado" });
                }
                return carrera;
            }
        }
    }
}