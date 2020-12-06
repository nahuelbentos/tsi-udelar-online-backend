using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Models;
using Persistence;

namespace Business.Comunicados {
    public class ConsultaByFacultadId {
        public class Ejecuta : IRequest<List<Comunicado>>
        { 
            public Guid FacultadId { get; set; }
        }
        public class Manejador : IRequestHandler<Ejecuta, List<Comunicado>> {
            private readonly UdelarOnlineContext context;

            public Manejador (UdelarOnlineContext context) {
                this.context = context;
            }

            public async Task<List<Comunicado>> Handle (Ejecuta request, CancellationToken cancellationToken) {
                var comunicadosFac = await this.context.ComunicadoFacultad.Include(uc => uc.Comunicado).Where(f => f.FacultadId == request.FacultadId).ToListAsync ();
                List<Comunicado> comunicados = new List<Comunicado>();
                foreach (var comunicado in comunicadosFac)
                {
                    comunicados.Add(comunicado.Comunicado);
                }
                return comunicados;
            }
        }
    }
}