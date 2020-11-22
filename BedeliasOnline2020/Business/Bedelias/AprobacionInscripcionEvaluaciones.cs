using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Business.utils;
using MediatR;

namespace Business.Bedelias
{
  public class AprobacionInscripcionEvaluaciones
  {
    public class Ejecuta : IRequest<Boolean>
    {

      public string ci { get; set; }
      public Guid cursoId { get; set; }

        
        
    }

    public class Manejador : IRequestHandler<Ejecuta, Boolean>
    {
     

      public Task<bool> Handle(Ejecuta request, CancellationToken cancellationToken)
              => Task.FromResult( CiValidator.ciInCollectionValids(request.ci) ? true : CiValidator.Validate(request.ci));
    
    }

  }
}