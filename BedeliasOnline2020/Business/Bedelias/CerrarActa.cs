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
      public Task<bool> Handle(Ejecuta request, CancellationToken cancellationToken) => 
        Task.FromResult(request.coleccionCI.Length % 2 == 0);
    }
  }
}