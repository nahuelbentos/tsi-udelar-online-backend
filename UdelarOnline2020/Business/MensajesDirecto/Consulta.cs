using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Models;
using Persistence;

namespace Business.MensajesDirecto
{
    public class Consulta
    {
        public class Ejecuta : IRequest<List<MensajeDirecto>>
        {
        }

        public class Manejador : IRequestHandler<Ejecuta, List<MensajeDirecto>>
        {
            private readonly UdelarOnlineContext context;
            public Manejador(UdelarOnlineContext context)
            {
                this.context = context;
            }

            public async Task<List<MensajeDirecto>> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var mensajeDirecto = await this.context.MensajeDirecto.Include( m => m.Emisor).Include(r => r.Receptor).ToListAsync();
                return mensajeDirecto;
            }
        }
    }
}