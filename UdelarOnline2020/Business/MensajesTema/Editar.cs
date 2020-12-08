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

namespace Business.MensajesTema
{
    public class Editar
    {
        public class Ejecuta : IRequest
        {
            public string Contenido { get; set; }
            public DateTime FechaDeEnviado { get; set; }
            public Guid MensajeId { get; set; }
            

            public bool? MensajeBloqueado { get; set; }

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


                mensajeTema.Contenido = request.Contenido ?? mensajeTema.Contenido;
                mensajeTema.MensajeBloqueado = request.MensajeBloqueado ?? mensajeTema.MensajeBloqueado;
                mensajeTema.FechaDeEnviado = DateTime.UtcNow;

                var res = await this.context.SaveChangesAsync();
                if (res > 0)
                    return Unit.Value;
                throw new ManejadorExcepcion(HttpStatusCode.InternalServerError, new { mensaje = "Ocurrio un error al editar el mensaje tema" });
            }
        }
    }
}