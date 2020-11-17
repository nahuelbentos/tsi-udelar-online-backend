using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Aplicacion.ManejadorError;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using Models;
using Persistence;

namespace Business.Preguntas
{
    public class Nuevo
    {
        public class Ejecuta : IRequest
        {
            public String Texto { get; set; }
            public Guid EncuestaId { get; set; }
            
        }

        public class EjecutaValidator : AbstractValidator<Ejecuta>
        {
            public EjecutaValidator()
            {
                RuleFor(c => c.Texto).NotEmpty().WithMessage("La respuesta no puede ser vacia.");
                RuleFor(c => c.EncuestaId).NotEmpty().WithMessage("La respuesta tiene que estar asociada a una encuesta.");
            }
        }

        public class Manejador : IRequestHandler<Ejecuta>
        {
            private readonly UdelarOnlineContext context;
            private readonly ILogger<Manejador> logger;

            public Manejador(UdelarOnlineContext context, ILogger<Manejador> logger)
            {
                this.context = context;
                this.logger = logger;
            }

            public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                Encuesta encuesta = await this.context.Encuesta.FindAsync(request.EncuestaId);
                if (encuesta == null){
                    throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { mensaje  = "No se encontró la encuesta." });
                }
                var pregunta = new Pregunta
                {
                    PreguntaId = new Guid(),
                    EncuestaId = request.EncuestaId,
                    Encuesta = encuesta,
                    Texto = request.Texto
                };

                this.context.Pregunta.Add(pregunta);

                var res = await this.context.SaveChangesAsync();
                if (res > 0)
                {
                    return Unit.Value;
                }

                throw new ManejadorExcepcion(HttpStatusCode.InternalServerError, new { mensaje = "Ocurrio un error al insertar la pregunta" });
            }
        }
    }
}