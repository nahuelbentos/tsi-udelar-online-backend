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
using Persistence;

namespace Business.Mensajes
{
    public class Editar
    {
        public class Ejecuta : IRequest
        {
            public string Contenido { get; set; }
            public DateTime? FechaDeEnviado { get; set; }
            public Guid MensajeId { get; set; }
            public Guid EmisorId { get; set; }

        }

        public class EjecutaValidator : AbstractValidator<Ejecuta>
        {
        public EjecutaValidator()
        {

            RuleFor(c => c.Contenido).NotEmpty().WithMessage("El Contenido es requerido.");
            RuleFor(c => c.FechaDeEnviado).NotEmpty().WithMessage("La Fecha de enviado es requerido");

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
                var mensaje = await this.context.Mensaje.FindAsync(request.MensajeId);
                if (mensaje == null)
                {
                    throw new ManejadorExcepcion(HttpStatusCode.BadRequest, new { mensaje = "No se encontro el mensaje directo" });
                }
                var emisor = await this.context.Usuario.Where(u => u.Id == request.EmisorId.ToString()).FirstOrDefaultAsync();
                // var emisor = await this.context.Usuario.FindAsync(request.EmisorId);
                if (emisor == null)
                {
                    throw new ManejadorExcepcion(HttpStatusCode.BadRequest, new { mensaje = "No existe el emisor ingresado" });
                }

                mensaje.Contenido = request.Contenido ?? mensaje.Contenido;
                mensaje.FechaDeEnviado = request.FechaDeEnviado ?? mensaje.FechaDeEnviado;

                var res = await this.context.SaveChangesAsync();
                if (res > 0)
                {
                    return Unit.Value;
                }
                throw new ManejadorExcepcion(HttpStatusCode.InternalServerError, new { mensaje = "Ocurrio un error al editar el mensaje" });
            }
        }
    }
}