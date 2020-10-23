using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Models;
using Persistence;

namespace Business.Materiales
{
    public class Consulta
    {
        public class Ejecuta : IRequest<List<Material>>
        {
        }

        public class Manejador : IRequestHandler<Ejecuta, List<Material>>
        {
            private readonly UdelarOnlineContext context;
            public Manejador(UdelarOnlineContext context)
            {
                this.context = context;
            }

            public async Task<List<Material>> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var material = await this.context.Material.ToListAsync();
                return material;
            }
        }
    }
}