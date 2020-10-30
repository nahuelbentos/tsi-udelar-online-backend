using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Aplicacion.ManejadorError;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Models;
using Persistence;

namespace Business.MensajesTema
{
    public class Consulta
    {
        public class Ejecuta : IRequest<List<MensajeTema>>
        {
        }

        public class Manejador : IRequestHandler<Ejecuta, List<MensajeTema>>
        {
            private readonly UdelarOnlineContext context;
            public Manejador(UdelarOnlineContext context)
            {
                this.context = context;
            }

            public async Task<List<MensajeTema>> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var mensajeTemas = await this.context.MensajeTema.Include( m => m.Emisor).ToListAsync();
                return mensajeTemas;
            }
        }
    }
}