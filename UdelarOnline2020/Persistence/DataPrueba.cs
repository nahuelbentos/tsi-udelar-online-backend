using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Models;

namespace Persistence
{
  public class DataPrueba
  {
    public static async Task InsertarData(UdelarOnlineContext context, UserManager<Usuario> usuarioManager)
    {
      if (!usuarioManager.Users.Any())
      {
        var usuario = new Usuario
        {
          Nombres = "Nahuel",
          Apellidos = "Bentos Gnocchi",
          UserName = "nbentos",
          Email = "nbentos@fing.edu.uy",
          EmailPersonal = "nahuelbentosgnocchi@gmail.com",
          Direccion = "Chana 2021",
          Telefono = "098765432",
        };
        await usuarioManager.CreateAsync(usuario, "Password123$");
      }
    }

  }
}