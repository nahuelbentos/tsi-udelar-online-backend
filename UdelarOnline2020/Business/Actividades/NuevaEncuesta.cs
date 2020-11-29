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

namespace Business.Actividades
{
    public class NuevaEncuesta
    {
        public class Ejecuta : IRequest
        {
            public Guid ActividadId { get; set; }
            public DateTime FechaRealizada { get; set; }
            public DateTime FechaFinalizada { get; set; }
            public string Nombre { get; set; }
            public string Descripcion { get; set; }
            public bool? EsAdministrador { get; set; }
            public List<string> PreguntaLista { get; set; }
            public Guid? FacultadId { get; set; }


        }   

        public class EjecutaValidator : AbstractValidator<Ejecuta>
        {
        public EjecutaValidator()
        {
            RuleFor(a => a.FechaRealizada).NotEmpty();
            RuleFor(a => a.FechaFinalizada).NotEmpty();
            RuleFor(a => a.Nombre).NotEmpty();
            RuleFor(a => a.Descripcion).NotEmpty();
            RuleFor(a => a.PreguntaLista).NotEmpty();
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
                Encuesta e = new Encuesta
                {
                    ActividadId = Guid.NewGuid(),
                    FechaFinalizada = request.FechaFinalizada,
                    FechaRealizada = request.FechaRealizada,
                    Nombre = request.Nombre,
                    Descripcion = request.Descripcion,
                    EsAdministrador = request.EsAdministrador.GetValueOrDefault(),
                };              
                //creo las preguntas
                foreach (var pregunta in request.PreguntaLista){
                    Pregunta p = new Pregunta
                    {
                        PreguntaId = Guid.NewGuid(),
                        Texto = pregunta,
                        Encuesta = e,
                        EncuestaId = e.ActividadId
                    };
                    this.context.Pregunta.Add(p);
                    e.PreguntaLista.Add(p);
                }

                //asigno la facultad si me lo mandan
                if (request.FacultadId.GetValueOrDefault() != default(Guid))
                {
                    var facultad = await this.context.Facultad.FindAsync(request.FacultadId.GetValueOrDefault());
                    e.Facultad = facultad;
                    e.FacultadId = facultad.FacultadId;
                }
                this.context.Encuesta.Add(e);

                var res = await this.context.SaveChangesAsync();
                if (res > 0)
                {
                    return Unit.Value;
                }

                throw new ManejadorExcepcion(HttpStatusCode.InternalServerError, new { mensaje = "Ocurrio un error al insertar la Encuesta" });
            }
        }
    }
}