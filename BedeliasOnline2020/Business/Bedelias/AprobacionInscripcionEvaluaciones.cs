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
        
        
    }

    public class Manejador : IRequestHandler<Ejecuta, Boolean>
    {
      public Task<bool> Handle(Ejecuta request, CancellationToken cancellationToken)
      {

        var validCI = new List<string>();

        var init = 11111111;
        for (int i = 1; i < 9; i++)
        {
          init *= i;
          validCI.Add(init.ToString());
          Console.WriteLine(init);
        };
        // validCI.Add("11111111");
        // validCI.Add("22222222");
        // validCI.Add("33333333");
        // validCI.Add("44444444");
        // validCI.Add("55555555");
        // validCI.Add("66666666");
        // validCI.Add("77777777");
        // validCI.Add("88888888");
        // validCI.Add("99999999");


        if (validCI.Contains(request.ci))
          return Task.FromResult(true);

        return Task.FromResult(CiValidator.Validate(request.ci));
      }
    }

  }
}