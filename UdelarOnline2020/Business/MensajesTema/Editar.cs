using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Aplicacion.ManejadorError;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using Persistence;

namespace Business.MensajesTema
{
    public class Editar
    {
        public class Ejecuta : IRequest
        {
            public string Contenido { get; set; }
            public DateTime FechaDeEnviado { get; set; }
            public Guid MensajeId { get; set; }
            public Guid TemaForoId { get; set; }
            public Guid EmisorId { get; set; }
            public bool MensajeBloqueado { get; set; }

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
                var mensajeTema = await this.context.MensajeTema.FindAsync(request.MensajeId);
                if (mensajeTema == null)
                {
                    throw new ManejadorExcepcion(HttpStatusCode.BadRequest, new { mensaje = "No se encontro el mensaje tema" });
                }

                var usuario = await this.context.Usuario.FindAsync(request.EmisorId);
                if (usuario == null)
                {
                    throw new ManejadorExcepcion(HttpStatusCode.BadRequest, new { mensaje = "No existe el emisor ingresado" });
                }

                var temaForo = await this.context.TemaForo.FindAsync(request.TemaForoId);
                if (usuario == null)
                {
                    throw new ManejadorExcepcion(HttpStatusCode.BadRequest, new { mensaje = "No existe el tema foro ingresado" });
                }

                mensajeTema.Contenido = request.Contenido ?? mensajeTema.Contenido;
                //mensajeTema.MensajeBloqueado = request.MensajeBloqueado ?? mensajeTema.MensajeBloqueado;

                var res = await this.context.SaveChangesAsync();
                if (res > 0)
                {
                    return Unit.Value;
                }
                throw new ManejadorExcepcion(HttpStatusCode.InternalServerError, new { mensaje = "Ocurrio un error al editar el mensaje tema" });
            }
        }
    }
}