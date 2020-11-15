using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Aplicacion.ManejadorError;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Models;
using Persistence;

namespace Business.Preguntas
{
    public class ConsultaById
    {
        public class Ejecuta : IRequest<Pregunta>
        {
            public Guid PreguntaId { get; set; }
        }

        public class Manejador : IRequestHandler<Ejecuta, Pregunta>
        {
            private readonly UdelarOnlineContext context;

            public Manejador(UdelarOnlineContext context)
            {
                this.context = context;
            }

            public async Task<Pregunta> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                //Hay que reemplazarlo con DataTypes
                var pregunta = await this.context.Pregunta
                                                    .Include(p => p.Encuesta)
                                                    .FirstOrDefaultAsync(p => p.PreguntaId == request.PreguntaId);
                if (pregunta == null)
                {
                    throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { mensaje = "No existe una pregunta con el PreguntaId ingresado" });
                }
                return pregunta;
            }
        }
    }
}