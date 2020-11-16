using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace Business.Bedelias
{
  public class CerrarActa
  {
    public class Ejecuta : IRequest<Boolean>
    {

    }

    public class Manejador : IRequestHandler<Ejecuta, Boolean>
    {
      public Task<bool> Handle(Ejecuta request, CancellationToken cancellationToken)
      {
        throw new NotImplementedException();
      }
    }
  }
}