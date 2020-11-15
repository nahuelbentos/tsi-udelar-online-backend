using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Models;
using Persistence;

namespace Business.Preguntas
{
    public class Consulta
    {
        public class Ejecuta : IRequest<List<Pregunta>> { }
        public class Manejador : IRequestHandler<Ejecuta, List<Pregunta>>
        {
            private readonly UdelarOnlineContext context;

            public Manejador(UdelarOnlineContext context)
            {
                this.context = context;
            }

            public async Task<List<Pregunta>> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                //Hay que devolver DataTypes
                var preguntas = await this.context.Pregunta
                                                .Include(p => p.Encuesta)
                                                .ToListAsync();
                return preguntas;    
            }
        }
    }
}