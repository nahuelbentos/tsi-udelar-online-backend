using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Models;
using Persistence;

namespace Business.Actividades
{
    public class Consulta
    {
        public class Ejecuta : IRequest<List<Actividad>> { }
        public class Manejador : IRequestHandler<Ejecuta, List<Actividad>>
        {
            private readonly UdelarOnlineContext context;

            public Manejador(UdelarOnlineContext context)
            {
                this.context = context;
            }

            public async Task<List<Actividad>> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                //Hay que devolver Datatypes
                var actividades = await this.context.Actividad
                                                        .Include(a => a.Curso)
                                                        .ToListAsync();
                return actividades;                                                        
            }
        }
    }
}