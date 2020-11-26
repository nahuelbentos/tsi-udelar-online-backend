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
                var alumnopruebaonline = await this.context.AlumnoPruebaOnline
                                                        .Include(ap => ap.Alumno)
                                                        .Include(ap => ap.PruebaOnline)
                                                        .Include(ap => ap.ListaRespuestas)
                                                        .ToListAsync();
                return alumnopruebaonline;    
            }
        }
    }
}