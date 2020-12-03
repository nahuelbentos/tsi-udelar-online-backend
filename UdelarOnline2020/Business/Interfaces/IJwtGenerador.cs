using System.Collections.Generic;

using Models;

namespace Business.Interfaces
{
  public interface IJwtGenerador
  {
    string CrearToken(Usuario usuario, List<string> roles);
    bool ValidarToken(string token);

    string TokenResetPassword(Usuario usuario, string resetToken);
  }
}