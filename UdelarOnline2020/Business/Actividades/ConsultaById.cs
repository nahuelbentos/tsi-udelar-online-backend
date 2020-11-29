using System;
using System.Linq;
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
                var actividad = await this.context.Actividad
                                                    .Include(a => a.Usuario)
                                                    .Where(a => a.ActividadId == request.ActividadId)
                                                    .FirstOrDefaultAsync();
                //Hay que devolver datatypes
                if (actividad == null)
                    throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { mensaje = "No existe una actividad con el CursoId ingresado" });
                    
                return actividad;
            }
        }
    }
}