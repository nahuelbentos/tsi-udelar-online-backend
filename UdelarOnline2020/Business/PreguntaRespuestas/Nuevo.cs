using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Business.ManejadorError;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using Models;
using Persistence;

namespace Business.PreguntaRespuestas
{
    public class Nuevo
    {
        public class Ejecuta : IRequest
        {
            public string Pregunta { get; set; }
            public List<DtPruebaRespuesta> Respuestas { get; set; }
            public int RespuestaCorrecta { get; set; }
            public int Puntos { get; set; }
            public bool Resta { get; set; }
            public Guid PruebaOnlineId { get; set; }
        }

        public class EjecutaValidator : AbstractValidator<Ejecuta>
        {
            public EjecutaValidator()
            {
                RuleFor(pr => pr.Pregunta).NotEmpty().WithMessage("La pregunta es requerida.");
                RuleFor(pr => pr.Respuestas).NotEmpty().WithMessage("Las Respuestas son requeridas.");
                RuleFor(pr => pr.RespuestaCorrecta).NotEmpty().WithMessage("La RespuestaCorrecta es requerida.");
                RuleFor(pr => pr.Puntos).NotEmpty().WithMessage("Los puntos son requeridos.");
                RuleFor(pr => pr.Resta).NotEmpty().WithMessage("La pregunta es requerida.");
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
                var po = await this.context.PruebaOnline.FindAsync(request.PruebaOnlineId);
                if (po == null)
                {
                    throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { mensaje = "No se encontró la prueba online" });
                }
                var preguntarespuesta = new PreguntaRespuesta
                {
                    PreguntaRespuestaId = new Guid(),
                    Pregunta = request.Pregunta,
                    Respuestas = request.Respuestas,
                    RespuestaCorrecta = request.RespuestaCorrecta,
                    Puntos = request.Puntos,
                    Resta = request.Resta
                };
                po.ListaPreguntaRespuesta.Add(preguntarespuesta);
                this.context.PreguntaRespuesta.Add(preguntarespuesta);

                var res = await this.context.SaveChangesAsync();
                if (res > 0)
                {
                    return Unit.Value;
                }

                throw new ManejadorExcepcion(HttpStatusCode.InternalServerError, new { mensaje = "Ocurrio un error al insertar la PreguntaRespuesta en la PruebaOnline" });
            }
        }
    }
}