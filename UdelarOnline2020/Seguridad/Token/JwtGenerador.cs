using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Business.Interfaces;
using Microsoft.IdentityModel.Tokens;
using Models;


namespace Seguridad.Token
{
  public class JwtGenerador : IJwtGenerador
  {

    public string CrearToken(Usuario usuario, List<string> roles)
    {
      // se puede agregar los claims que se quieran.
      var claims = new List<Claim>{
        new Claim(JwtRegisteredClaimNames.Email, usuario.Email)
      };

      if (roles != null)
      {
        foreach (var role in roles)
        {
          claims.Add(new Claim(ClaimTypes.Role, role));
        }
      }


      var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Udelar Online TSI"));

      var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

      var tokenDescripcion = new SecurityTokenDescriptor
      {
        Subject = new ClaimsIdentity(claims),
        Expires = DateTime.Now.AddDays(30),
        SigningCredentials = credentials
      };

      var tokenManejador = new JwtSecurityTokenHandler();
      var token = tokenManejador.CreateToken(tokenDescripcion);

      return tokenManejador.WriteToken(token);

    }

    public bool ValidarToken(string token)
    {

      
      var mySecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Udelar Online TSI"));


      var tokenHandler = new JwtSecurityTokenHandler();
      try
      {
        tokenHandler.ValidateToken(token, new TokenValidationParameters
        {
          ValidateIssuerSigningKey = true,
          ValidateIssuer = false,
          ValidateAudience = false,
          IssuerSigningKey = mySecurityKey
        }, out SecurityToken validatedToken);
      }
      catch
      {
        return false;
      }

      return true;

    }


  }
}