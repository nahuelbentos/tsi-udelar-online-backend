using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Business.Interfaces;
using MediatR;
using Microsoft.IdentityModel.Tokens;

namespace Business.Seguridad
{
  public class ValidateToken
  {
    public class Ejecuta : IRequest<Boolean>
    {
      public string Token { get; set; }
    }

    public class Manejador : IRequestHandler<Ejecuta, Boolean>
    {
      private readonly IJwtGenerador jwtGenerador;

      public Manejador(IJwtGenerador jwtGenerador)
      {
        this.jwtGenerador = jwtGenerador;
      }

      public Task<Boolean> Handle(Ejecuta request, CancellationToken cancellationToken)
      {
        
        var isValid = this.jwtGenerador.ValidarToken(request.Token);
        return Task.FromResult(isValid);
      }
    }
  }
}