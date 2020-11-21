using System.Collections;
using System;
using System.Threading;
using System.Threading.Tasks;
using Business.utils;
using MediatR;
using System.Collections.Generic;

namespace Business.Bedelias
{
  public class AprobarInscripcionCurso
  {
    public class Ejecuta : IRequest<Boolean>
    {
      public string ci { get; set; }
      public Guid cursoId { get; set; }
      
      
    }

    public class Manejador : IRequestHandler<Ejecuta, Boolean>
    {
      private readonly CiValidator ciValidator;

      public Manejador(CiValidator ciValidator)
      {
        this.ciValidator = ciValidator;
      }

      public Task<bool> Handle(Ejecuta request, CancellationToken cancellationToken) 
              =>  Task.FromResult( this.ciValidator.ciInCollectionValids(request.ci) ? true : CiValidator.Validate(request.ci) );
      
    }
  }
}