using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Business.ManejadorError;
using FluentValidation;
using MediatR;
using Persistence;

namespace Business.Preguntas
{
    public class Editar
    {
        public class Ejecuta : IRequest
        {
            public Guid PreguntaId { get; set; }
            public String Texto { get; set; }
        }

        public class EjecutaValidacion : AbstractValidator<Ejecuta>
        {
            public EjecutaValidacion()
            {
                RuleFor(p => p.PreguntaId).NotEmpty().WithMessage("PreguntaId es requerido");
                RuleFor(p => p.Texto).NotEmpty().WithMessage("El Texto es requerido");
            }
        }

        public class Manejador : IRequestHandler<Ejecuta>
        {
            private readonly UdelarOnlineContext context;

            public Manejador(UdelarOnlineContext context)
            {
                this.context = context;
            }

            public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var pregunta = await this.context.Pregunta.FindAsync(request.PreguntaId);

                if (pregunta == null)
                    throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { mensaje = "La pregunta no existe"});
                pregunta.Texto = request.Texto ?? pregunta.Texto;

                var res = await this.context.SaveChangesAsync();

                if (res < 0)
                    throw new ManejadorExcepcion(HttpStatusCode.InternalServerError, new { mensaje = "Ocurrio un error al editar la pregunta" });    
                return Unit.Value;
                

            }
        }
    }
}
