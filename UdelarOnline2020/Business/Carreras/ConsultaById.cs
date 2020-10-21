using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Aplicacion.ManejadorError;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Models;
using Persistence;

namespace Business.Carreras
{
    public class ConsultaById
    {
        public class Ejecuta : IRequest<Carrera>
        {
            public Guid CarreraId { get; set; }
        }

        public class Manejador : IRequestHandler<Ejecuta, Carrera>
        {
            private readonly UdelarOnlineContext context;

            public Manejador(UdelarOnlineContext context)
            {
                this.context = context;
            }

            public async Task<Carrera> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                //Hay que reemplazarlo con DataTypes
                var carrera = await this.context.Carrera
                                                    .Include(c => c.Facultad)
                                                    .FirstOrDefaultAsync(c => c.CarreraId == request.CarreraId);
                if (carrera == null)
                {
                    throw new ManejadorExcepcion(HttpStatusCode.Forbidden, new { mensaje = "No existe una carrera con el CarreraId ingresado" });
                }
                return carrera;
            }
        }
    }
}