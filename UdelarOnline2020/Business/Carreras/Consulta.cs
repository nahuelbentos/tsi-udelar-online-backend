using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Models;
using Persistence;

namespace Business.Carreras
{
    public class Consulta
    {
        public class Ejecuta : IRequest<List<Carrera>> { }
        public class Manejador : IRequestHandler<Ejecuta, List<Carrera>>
        {
            private readonly UdelarOnlineContext context;

            public Manejador(UdelarOnlineContext context)
            {
                this.context = context;
            }

            public async Task<List<Carrera>> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                //Hay que devolver DataTypes
                var carreras = await this.context.Carrera
                                                .Include(c => c.Facultad)
                                                .ToListAsync();
                return carreras;    
            }
        }
    }
}