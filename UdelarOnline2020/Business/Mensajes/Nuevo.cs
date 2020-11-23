using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Business.ManejadorError;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Models;
using Persistence;

namespace Business.Mensajes
{
    public class Nuevo
    {
        public class Ejecuta : IRequest
        {

            public string Contenido { get; set; }
            public DateTime FechaDeEnviado { get; set; }
            public Guid EmisorId { get; set; }

        }

        public class EjecutaValidator : AbstractValidator<Ejecuta>
        {
        public EjecutaValidator()
        {
            RuleFor(c => c.Contenido).NotEmpty().WithMessage("El Contenido es requerido.");
            RuleFor(c => c.EmisorId).NotEmpty().WithMessage("El EmisorId es requerido");

        }
        }

        public class Manejador : IRequestHandler<Ejecuta>
        {
            private readonly UdelarOnlineContext context;
            private readonly ILogger<Manejador> logger;

            public Manejador( UdelarOnlineContext context, ILogger<Manejador> logger)
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
                //Guid emisorIdGuid = Guid.Parse(request.EmisorId);
                var mensaje = new Mensaje {
                    Contenido = request.Contenido,
                    FechaDeEnviado = request.FechaDeEnviado,
                    EmisorId = request.EmisorId,
                    Emisor = emisor
                };
                
                context.Mensaje.Add(mensaje);
                var res = await this.context.SaveChangesAsync();
                if (res > 0)
                {
                    return Unit.Value;
                }
                throw new ManejadorExcepcion(HttpStatusCode.InternalServerError, new { mensaje = "Ocurrio un error al insertar el mensaje" });
            }
        }
    }
}