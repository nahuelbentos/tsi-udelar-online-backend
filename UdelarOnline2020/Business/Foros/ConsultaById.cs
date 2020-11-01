using System.Net;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Aplicacion.ManejadorError;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Models;
using Persistence;

namespace Business.Foros
{
    public class ConsultaById
    {
        public class Ejecuta : IRequest<Foro>
        {
            public Guid ForoId { get; set; }
        }

        public class Manejador : IRequestHandler<Ejecuta, Foro>
        {
            private readonly UdelarOnlineContext context;

            public Manejador(UdelarOnlineContext context)
            {
                this.context = context;
            }

            public async Task<Foro> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                // Hay que reemplazarlo con DataTypes
                var foro = await this.context.Foro
                                                .Include(f => f.TemaForoLista)
                                                .FirstOrDefaultAsync(f => f.ForoId == request.ForoId);
                if (foro == null)
                {
                    throw new ManejadorExcepcion(HttpStatusCode.Forbidden, new { mensaje = "No existe un curso con el CursoId ingresado" });
                }
                return foro;
            }
        }
    }
}