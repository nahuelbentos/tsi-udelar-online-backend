using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Models;
using Persistence;

namespace Business.Secciones
{
    public class Consulta
    {
        public class Ejecuta : IRequest<List<Seccion>> { }
        public class Manejador : IRequestHandler<Ejecuta, List<Seccion>>
        {
            private readonly UdelarOnlineContext context;

            public Manejador(UdelarOnlineContext context)
            {
                this.context = context;
            }

            public override bool Equals(object obj)
            {
                return base.Equals(obj);
            }

            public override int GetHashCode()
            {
                return base.GetHashCode();
            }

            public async Task<List<Seccion>> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                //Hay que devolver DataTypes
                var secciones = await this.context.Seccion
                                                .ToListAsync();
                return secciones;    
            }

            public override string ToString()
            {
                return base.ToString();
            }
        }
    }
}