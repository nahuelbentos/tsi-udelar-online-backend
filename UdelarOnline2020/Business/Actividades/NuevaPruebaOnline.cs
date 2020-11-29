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
    public class NuevaPruebaOnline
    {
        public class Ejecuta : IRequest
        {
            public DateTime FechaRealizada { get; set; }
            public DateTime FechaFinalizada { get; set; }
            public string Nombre { get; set; }
            public string Descripcion { get; set; }
            public DateTime Fecha { get; set; }
            public string Url { get; set; }
            public int MinutosExpiracion { get; set; }
            public bool Activa { get; set; }
            public ICollection<PreguntaRespuesta> ListaPreguntaRespuesta { get; set; }

        }   

        public class EjecutaValidator : AbstractValidator<Ejecuta>
        {
            public EjecutaValidator()
            {
                RuleFor(a => a.FechaRealizada).NotEmpty();
                RuleFor(a => a.FechaFinalizada).NotEmpty();
                RuleFor(a => a.Nombre).NotEmpty();
                RuleFor(a => a.Descripcion).NotEmpty();
                RuleFor(a => a.Fecha).NotEmpty();
                RuleFor(a => a.Url).NotEmpty();
                RuleFor(a => a.MinutosExpiracion).NotEmpty();
                RuleFor(a => a.ListaPreguntaRespuesta).NotEmpty();
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
                PruebaOnline po = new PruebaOnline
                {
                    FechaFinalizada = request.FechaFinalizada,
                    FechaRealizada = request.FechaRealizada,
                    Nombre = request.Nombre,
                    Descripcion = request.Descripcion,
                    Fecha = request.Fecha,
                    Url = request.Url,
                    MinutosExpiracion = request.MinutosExpiracion,
                    Activa = request.Activa,
                    ListaPreguntaRespuesta = request.ListaPreguntaRespuesta
                };              
                this.context.PruebaOnline.Add(po);

                var res = await this.context.SaveChangesAsync();
                if (res > 0)
                {
                    return Unit.Value;
                }

                throw new ManejadorExcepcion(HttpStatusCode.InternalServerError, new { mensaje = "Ocurrio un error al insertar la PruebaOnline" });
            }
        }
    }
}