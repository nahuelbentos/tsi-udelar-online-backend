using System;
using System.Threading;
using System.Threading.Tasks;
using Business.Interfaces;
using MediatR;

namespace Business.Bedelias
{
    public class AprobacionInscripcionEvaluaciones
    {
        public class Ejecuta : IRequest<Boolean>
        {
            public string CI { get; set; }
            public Guid CursoId { get; set; }
            
            
        }

    public class Manejador : IRequestHandler<Ejecuta, Boolean>
    {

      private readonly IBedeliasGenerator bedeliasGenerator;

      public Manejador(IBedeliasGenerator bedeliasGenerator)
      {
        this.bedeliasGenerator = bedeliasGenerator;
      }
      public Task<bool> Handle(Ejecuta request, CancellationToken cancellationToken) => bedeliasGenerator.AprobarInscripcionEvaluacion(request.CI, request.CursoId);
    }
  }
}