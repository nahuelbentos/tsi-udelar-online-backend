using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Business.Datatypes;
using Business.ManejadorError;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Models;
using Persistence;

namespace Business.Actividades
{
    public class ConsultaById
    {
        public class Ejecuta : IRequest<DtActividad>
        {
            public Guid ActividadId { get; set; }
        }

        public class Manejador : IRequestHandler<Ejecuta, DtActividad>
        {
            private readonly UdelarOnlineContext context;
            public Manejador(UdelarOnlineContext context)
            {
                this.context = context;
            }
            public async Task<DtActividad> Handle(Ejecuta request,
                                                CancellationToken cancellationToken)
            {
                var actividad = await this.context.Actividad
                                                    .Include(a => a.Usuario)
                                                    .Where(a => a.ActividadId == request.ActividadId)
                                                    .FirstOrDefaultAsync();
                //Hay que devolver datatypes
                if (actividad == null)
                    throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { mensaje = "No existe una actividad con el CursoId ingresado" });
                    
                return new DtActividad
                {
                  ActividadId = actividad.ActividadId,
                  Descripcion = actividad.Descripcion,
                  FechaFinalizada = actividad.FechaFinalizada,
                  FechaRealizada = actividad.FechaRealizada,
                  Nombre = actividad.Nombre,
                  Usuario = actividad.Usuario,
                  UsuarioId = actividad.UsuarioId,
                  Tipo = actividad.GetType().ToString().Split('.')[1]
                };
            }
        }
    }
}