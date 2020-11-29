using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace Business.Actividades
{
    public class NuevaPruebaOnline
    {
        public class Ejecuta : IRequest
        {
            
        }

    public class Manejador : IRequestHandler<Ejecuta>
    {
      public Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
      {
        throw new System.NotImplementedException();
      }
    }
  }
}