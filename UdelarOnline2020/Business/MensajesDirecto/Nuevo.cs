using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Aplicacion.ManejadorError;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Models;
using Persistence;

namespace Business.MensajesDirecto
{
    public class Nuevo
    {
        public class Ejecuta : IRequest
        {

            public string Contenido { get; set; }
            public DateTime FechaDeEnviado { get; set; }
            public Guid EmisorId { get; set; }
            public Guid ReceptorId { get; set; }

        }

        public class EjecutaValidator : AbstractValidator<Ejecuta>
        {
        public EjecutaValidator()
        {
            RuleFor(c => c.Contenido).NotEmpty().WithMessage("El Contenido es requerido.");
            RuleFor(c => c.ReceptorId).NotEmpty().WithMessage("El ReceptorId es requerido");
            RuleFor(c => c.EmisorId).NotEmpty().WithMessage("El EmisorId es requerido");
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
                var emisor = await this.context.Usuario.Where(e => e.Id == request.EmisorId.ToString()).FirstOrDefaultAsync();
                if (emisor == null)
                {
                    throw new ManejadorExcepcion(HttpStatusCode.BadRequest, new { mensaje = "No existe el emisor ingresado" });
                }
                var receptor = await this.context.Usuario.Where(e => e.Id == request.ReceptorId.ToString()).FirstOrDefaultAsync();
                if (receptor == null)
                {
                    throw new ManejadorExcepcion(HttpStatusCode.BadRequest, new { mensaje = "No existe el receptor ingresado" });
                }

                var mensajeDirecto = new MensajeDirecto {
                    Contenido = request.Contenido,
                    FechaDeEnviado = request.FechaDeEnviado,
                    ReceptorId = request.ReceptorId,
                    EmisorId = request.EmisorId,
                    Emisor = emisor,
                    Receptor = receptor
                };
                
                context.MensajeDirecto.Add(mensajeDirecto);
                var res = await this.context.SaveChangesAsync();
                if (res > 0)
                {
                    return Unit.Value;
                }
                throw new ManejadorExcepcion(HttpStatusCode.InternalServerError, new { mensaje = "Ocurrio un error al insertar el mensaje directo" });
            }
        }
    }
}