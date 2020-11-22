using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Business.ManejadorError;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Models;
using Persistence;

namespace Business.MensajesTema
{
    public class ConsultaById
    {
        public class Ejecuta : IRequest<MensajeTema>
        {
            public Guid MensajeId { get; set; }

        }

        public class Manejador : IRequestHandler<Ejecuta, MensajeTema>
        {
            private readonly UdelarOnlineContext context;
            public Manejador(UdelarOnlineContext context)
            {
                this.context = context;
            }

            public async Task<MensajeTema> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var mensajeTema = await this.context.MensajeTema.Include( m => m.Emisor).FirstOrDefaultAsync(m => m.MensajeId == request.MensajeId);

                if (mensajeTema == null)
                    throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { mensaje = "El mensaje tema no existe. " });

                return mensajeTema;
            }
        }
    }
}