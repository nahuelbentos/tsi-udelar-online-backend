using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Business.ManejadorError;
using MediatR;
using Models;
using Persistence;

namespace Business.Materiales
{
    public class ConsultaById
    {
        public class Ejecuta : IRequest<Material>
        {
            public Guid MaterialId { get; set; }

        }

        public class Manejador : IRequestHandler<Ejecuta, Material>
        {
            private readonly UdelarOnlineContext context;
            public Manejador(UdelarOnlineContext context)
            {
                this.context = context;
            }

            public async Task<Material> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var material = await this.context.Material.FindAsync(request.MaterialId);

                if (material == null)
                    throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { mensaje = "El material no existe. " });

                return material;
            }
        }
    }
}