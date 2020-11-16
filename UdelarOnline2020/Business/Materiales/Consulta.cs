using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Business.Datatypes;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Models;
using Persistence;

namespace Business.Materiales
{
    public class Consulta
    {
        public class Ejecuta : IRequest<List<DtMaterial>>
        {
        }

        public class Manejador : IRequestHandler<Ejecuta, List<DtMaterial>>
        {
            private readonly UdelarOnlineContext context;
            public Manejador(UdelarOnlineContext context)
            {
                this.context = context;
            }

            public async Task<List<DtMaterial>> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var materiales = await this.context.Material.ToListAsync();

                List<DtMaterial> dtMateriales = new List<DtMaterial>();

                foreach (var material in materiales)
                {
                    var dtMaterial = new DtMaterial { 
                        Descripcion = material.Descripcion,
                        Nombre = material.Nombre,
                        MaterialId = material.MaterialId
                    };

                    dtMateriales.Add(dtMaterial);
                }

                return dtMateriales;
            }
        }
    }
}