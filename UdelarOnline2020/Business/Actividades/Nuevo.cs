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

namespace Business.Actividades
{
    public class Nuevo
    {
        public class Ejecuta : IRequest
        {
            public DateTime FechaRealizada { get; set; }
            public DateTime FechaFinalizada { get; set; }
            public String Tipo { get; set; }
            //public File Archivo { get; set; }
            public String Nombre { get; set; }
            public String Descripcion { get; set; }
            public bool EsAdministrador { get; set; }
            public bool EsIndividual { get; set; }
            public int Calificacion { get; set; }
            public String Nota { get; set; }
        }

        public class EjecutaValidator : AbstractValidator<Ejecuta>
        {
            public EjecutaValidator()
            {
                RuleFor(a => a.FechaRealizada).NotEmpty();
                RuleFor(a => a.FechaFinalizada).NotEmpty();
                RuleFor(a => a.Tipo).NotEmpty().WithMessage("Los tipos son Encuesta, Trabajo o ClaseDictada");
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
                Actividad actividad = null;
                
                switch (request.Tipo)
                {
                    case "ClaseDictada":
                        actividad = new ClaseDictada
                        {
                            FechaFinalizada = request.FechaFinalizada,
                            FechaRealizada = request.FechaRealizada,
                            //Y tambien tiene un file
                            //Archivo = request.Archivo
                        };
                        break;
                    case "Encuesta":
                        actividad = new Encuesta
                        {
                            FechaFinalizada = request.FechaFinalizada,
                            FechaRealizada = request.FechaRealizada,
                            
                            Nombre = request.Nombre,
                            Descripcion = request.Descripcion,
                            EsAdministrador = request.EsAdministrador
                            
                        };
                        break;
                    case "Trabajo":
                        actividad = new Trabajo
                        {
                            FechaFinalizada = request.FechaFinalizada,
                            FechaRealizada = request.FechaRealizada,
                            
                            EsIndividual = request.EsIndividual,
                            Calificacion = request.Calificacion,
                            Nota = request.Nota
                            
                        };
                        break;
                    default:
                        throw new ManejadorExcepcion(HttpStatusCode.BadRequest, new { mensaje = "El tipo de actividad debe ser ClaseDictada, Encuesta o Trabajo" });
                }

                this.context.Actividad.Add(actividad);

                var res = await this.context.SaveChangesAsync();
                if (res > 0)
                {
                    return Unit.Value;
                }

                throw new ManejadorExcepcion(HttpStatusCode.InternalServerError, new { mensaje = "Ocurrio un error al insertar la actividad" });
            }
        }
    }
}