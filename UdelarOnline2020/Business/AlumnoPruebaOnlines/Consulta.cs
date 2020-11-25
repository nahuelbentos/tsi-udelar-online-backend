using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Models;
using Persistence;

namespace Business.AlumnoPruebaOnlines
{
    public class Consulta
    {
        public class Ejecuta : IRequest<List<AlumnoPruebaOnline>> { }
        public class Manejador : IRequestHandler<Ejecuta, List<AlumnoPruebaOnline>>
        {
            private readonly UdelarOnlineContext context;

            public Manejador(UdelarOnlineContext context)
            {
                this.context = context;
            }

            public async Task<List<AlumnoPruebaOnline>> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                //Hay que devolver DataTypes
                var alumnocursos = await this.context.AlumnoPruebaOnline
                                                        .Include(ac => ac.Alumno)
                                                        .Include(ac => ac.PruebaOnline)
                                                        
                                                        .ToListAsync();
                return alumnocursos;    
            }
        }
    }
}