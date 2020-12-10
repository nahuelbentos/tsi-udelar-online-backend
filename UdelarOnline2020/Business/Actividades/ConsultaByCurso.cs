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
    public class ConsultaByCurso
    {
        public class Ejecuta : IRequest<List<Actividad>>
        {
            public Guid CursoId { get; set; }            
            
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
        var curso = await this.context.Curso.FindAsync(request.CursoId);
        if(curso == null)
          throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { mensaje = "No existe un curso con el CursoId ingresado" });


        // List<Actividad> actividades = new List<Actividad>();

        var cursoActividades = await this.context.CursoSeccionActividad
                                                .Include(csa => csa.Curso)
                                                .Include(csa => csa.Actividad)
                                                .Where( csa => csa.CursoId == curso.CursoId)
                                                .Select(csa => csa.Actividad).Distinct()
                                                .ToListAsync();

        

        return cursoActividades;
      }
    }
  }
}