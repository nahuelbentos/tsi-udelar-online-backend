using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Models;
using Persistence;

namespace Business.Actividades
{
    public class ConsultaPruebaOnline
    {
         public class Ejecuta : IRequest<List<PruebaOnline>> { }
        public class Manejador : IRequestHandler<Ejecuta, List<PruebaOnline>>
        {
            private readonly UdelarOnlineContext context;

            public Manejador(UdelarOnlineContext context)
            {
                this.context = context;
            }

            public async Task<List<PruebaOnline>> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                //Hay que devolver Datatypes
                var pruebasOnlines = await this.context.PruebaOnline.Include(a => a.Usuario).ToListAsync();
                return pruebasOnlines;                                                        
            }
        }
    }
}