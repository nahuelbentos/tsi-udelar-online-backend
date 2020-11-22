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

      public string[] coleccionCI { get; set; }
      public Guid cursoId { get; set; }
    }

    public class Manejador : IRequestHandler<Ejecuta, Boolean>
    {
      public async Task<bool> Handle(Ejecuta request, CancellationToken cancellationToken) => request.coleccionCI.Length % 2 == 0;
    }
  }
}