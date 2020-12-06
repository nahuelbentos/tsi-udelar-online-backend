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

namespace Business.CursosSeccion
{
    public class SeccionesByCurso
    {
        public class Ejecuta : IRequest<List<Seccion>>
        {
            public Guid CursoId { get; set; }
            
        }

    public class Manejador : IRequestHandler<Ejecuta, List<Seccion>>
    {
      private readonly UdelarOnlineContext context;

      public Manejador(UdelarOnlineContext context)
      {
        this.context = context;
      }

      public async Task<List<Seccion>> Handle(Ejecuta request, CancellationToken cancellationToken)
      {
        var curso = await this.context.Curso.FindAsync(request.CursoId);
        if(curso == null)
          throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { mensaje = "No existe un curso con el CursoId ingresado" });

        var secciones = await this.context.CursoSeccion
                                                .Include( cs => cs.Seccion)
                                                .Where(cs => cs.CursoId == curso.CursoId)
                                                .Select( cs => cs.Seccion)
                                                .ToListAsync();
        return secciones;    
        
      }
    }
  }
}