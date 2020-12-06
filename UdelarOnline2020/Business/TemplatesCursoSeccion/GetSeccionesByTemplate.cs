using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Models;
using Persistence;

namespace Business.TemplatesCursoSeccion
{
    public class GetSeccionesByTemplate
    {
        public class Ejecuta : IRequest<List<Seccion>>
        {
            public Guid TemplateCursoId { get; set; }
            
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
        var secciones = await this.context.TemplateCursoSeccion
                                                            .Include( tcs => tcs.Seccion)
                                                            .Where( tcs => tcs.TemplateCursoId == request.TemplateCursoId)
                                                            .Select(tcs => tcs.Seccion)
                                                            .ToListAsync();
        return secciones;    
      }
    }
  }
}