using System;
using System.Collections.Generic;
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

namespace Business.AlumnoPruebaOnlines
{
    public class ConsultaByAlumnoId
    {
        public class Ejecuta : IRequest<List<DtEvaluacion>>
        {
            public Guid AlumnoId { get; set; }

        }

        public class Manejador : IRequestHandler<Ejecuta, List<DtEvaluacion>>
        {
            private readonly UdelarOnlineContext context;

            public Manejador(UdelarOnlineContext context)
            {
                this.context = context;
            }

            public async Task<List<DtEvaluacion>> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var alumnoPruebasOnline = await this.context.AlumnoPruebaOnline.Include(ap => ap.Alumno).Include(ap => ap.PruebaOnline).Include(ap => ap.ListaRespuestas).Where(ap => ap.AlumnoId == request.AlumnoId).ToListAsync();
                if (!alumnoPruebasOnline.Any())
                    throw new ManejadorExcepcion(HttpStatusCode.BadRequest, new { mensaje = "No se encontro inscripciones a Pruebas Online asociadas al Alumno" });
                
                List<DtEvaluacion> dtEvaluacions =  new List<DtEvaluacion>();

                foreach (var ap in alumnoPruebasOnline)
                {
                    dtEvaluacions.Add( new DtEvaluacion {
                        Alumno = ap.Alumno,
                        AlumnoId = ap.AlumnoId,
                        Evaluacion = $"{ap.PruebaOnline.Nombre} - {ap.PruebaOnline.Descripcion}",
                        FechaExpiracion = ap.FechaExpiracion,
                        FechaFin = ap.FechaFin,
                        FechaInicio = ap.FechaInicio,
                        Inscripto = ap.Inscripto,
                        Nota = ap.Nota,
                        PruebaOnlineData = ap.PruebaOnline,
                        PruebaOnlineId = ap.PruebaOnlineId
                    });
                }

                return dtEvaluacions;
            }
        }
    }
}