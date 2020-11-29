using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Models;
using Persistence;

namespace Business.AlumnoCursos
{
    public class Consulta
    {
        public class Ejecuta : IRequest<List<AlumnoCurso>> { }
        public class Manejador : IRequestHandler<Ejecuta, List<AlumnoCurso>>
        {
            private readonly UdelarOnlineContext context;

            public Manejador(UdelarOnlineContext context)
            {
                this.context = context;
            }

            public async Task<List<AlumnoCurso>> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                //Hay que devolver DataTypes
                var alumnocursos = await this.context.AlumnoCurso
                                                        .Include(ac => ac.Curso)
                                                        .Include(ac => ac.Alumno)
                                                        .ToListAsync();
                return alumnocursos;    
            }
        }
    }
}