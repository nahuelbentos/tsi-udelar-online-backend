using System;
using System.Threading;
using System.Threading.Tasks;
using Business.Interfaces;
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
      private readonly IBedeliasGenerator bedeliasGenerator;

      public Manejador(IBedeliasGenerator bedeliasGenerator)
      {
        this.bedeliasGenerator = bedeliasGenerator;
      }

      public Task<bool> Handle(Ejecuta request, CancellationToken cancellationToken){
            Console.WriteLine( "voy a cerrar acta:: ");
            return this.bedeliasGenerator.CerrarActa(request.coleccionCI, request.cursoId);
      }
    }
  }
}