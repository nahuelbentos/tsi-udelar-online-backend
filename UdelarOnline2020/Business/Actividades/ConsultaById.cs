using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Business.ManejadorError;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Models;
using Persistence;

namespace Business.Actividades
{
    public class ConsultaById
    {
        public class Ejecuta : IRequest<Actividad>
        {
            public Guid ActividadId { get; set; }
        }

        public class Manejador : IRequestHandler<Ejecuta, Actividad>
        {
            private readonly UdelarOnlineContext context;
            public Manejador(UdelarOnlineContext context)
            {
                this.context = context;
            }
            public async Task<Actividad> Handle(Ejecuta request,
                                                CancellationToken cancellationToken)
            {
                //Hay que devolver datatypes
                var actividad = await this.context.Actividad.FirstOrDefaultAsync(a => a.ActividadId == request.ActividadId);
                if (actividad == null)
                {
                    throw new ManejadorExcepcion(HttpStatusCode.Forbidden, new { mensaje = "No existe una actividad con el CursoId ingresado" });
                }
                return actividad;
            }
        }
    }
}