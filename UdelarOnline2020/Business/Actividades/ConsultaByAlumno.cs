using System;
using System.Collections.Generic;
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
    public class ConsultaByAlumno
    {
        public class Ejecuta : IRequest<List<Actividad>> 
        { 
            public Guid AlumnoId { get; set; }
        }
        public class Manejador : IRequestHandler<Ejecuta, List<Actividad>>
        {
            private readonly UdelarOnlineContext context;

            public Manejador(UdelarOnlineContext context)
            {
                this.context = context;
            }

            public async Task<List<Actividad>> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var alumno = await this.context.Alumno.Where(e => e.Id == request.AlumnoId.ToString()).FirstOrDefaultAsync();
                if (alumno == null)
                {
                    throw new ManejadorExcepcion(HttpStatusCode.BadRequest, new { mensaje = "No existe el alumno ingresado" });
                }

                List<Actividad> actividades = new List<Actividad>();

                var alumnoTrabajo = await this.context.AlumnoTrabajo.Include(t => t.Trabajo).Where(a => a.AlumnoId == request.AlumnoId).ToListAsync();

                foreach (var actividad in alumnoTrabajo)
                {
                    actividades.Add(actividad.Trabajo);
                }

                var alumnoPruebaOnline = await this.context.AlumnoPruebaOnline.Include(t => t.PruebaOnline).Where(a => a.AlumnoId == request.AlumnoId).ToListAsync();

                foreach (var actividad in alumnoPruebaOnline)
                {
                    actividades.Add(actividad.PruebaOnline);
                }

                var alumnoClaseDictada = await this.context.AlumnoClaseDictada.Include(t => t.ClaseDictada).Where(a => a.AlumnoId == request.AlumnoId).ToListAsync();

                foreach (var actividad in alumnoClaseDictada)
                {
                    actividades.Add(actividad.ClaseDictada);
                }
                
                return actividades;                                                        
            }
        }
    }
}