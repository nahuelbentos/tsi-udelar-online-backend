using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Models;
using Persistence;

namespace Business.Mensajes
{
    public class Consulta
    {
        public class Ejecuta : IRequest<List<Mensaje>>
        {
        }

        public class Manejador : IRequestHandler<Ejecuta, List<Mensaje>>
        {
            private readonly UdelarOnlineContext context;
            public Manejador(UdelarOnlineContext context)
            {
                this.context = context;
            }

            public async Task<List<Mensaje>> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var mensaje = await this.context.Mensaje.ToListAsync();
                return mensaje;
            }
        }
    }
}