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

namespace Business.AlumnoCursos
{
    public class ConsultaByAlumnoId
    {
        public class Ejecuta : IRequest<List<AlumnoCurso>>
        {
            public Guid AlumnoId { get; set; }

        }

        public class Manejador : IRequestHandler<Ejecuta, List<AlumnoCurso>>
        {
            private readonly UdelarOnlineContext context;

            public Manejador(UdelarOnlineContext context)
            {
                this.context = context;
            }

            public async Task<List<AlumnoCurso>> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var ac = await this.context.AlumnoCurso.Include(ac => ac.Alumno).Include(ac => ac.Curso).Where(ac => ac.AlumnoId == request.AlumnoId).ToListAsync();
                if (!ac.Any())
                {
                    throw new ManejadorExcepcion(HttpStatusCode.BadRequest, new { mensaje = "No se encontro inscripciones asociadas al Alumno" });
                }
                return ac;
            }
        }
    }
}