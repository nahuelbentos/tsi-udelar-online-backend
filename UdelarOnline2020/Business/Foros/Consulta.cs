using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Models;
using Persistence;

namespace Business.Foros
{
    public class Consulta
    {
        public class Ejecuta : IRequest<List<Foro>> { }

        public class Manejador : IRequestHandler<Ejecuta, List<Foro>>
        {
            private readonly UdelarOnlineContext context;

            public Manejador(UdelarOnlineContext context)
            {
                this.context = context;
            }

            public async Task<List<Foro>> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                //Hay que devolver DataTypes
                var foros = await this.context.Foro
                                                .Include(f => f.TemaForoLista)
                                                .ToListAsync();
                return foros;    
            }
        }
    }
}