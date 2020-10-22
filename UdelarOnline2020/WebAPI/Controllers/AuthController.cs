using System.Threading.Tasks;
using Business.Datatypes;
using Business.Seguridad;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
  [AllowAnonymous]
  public class AuthController : MiControllerBase
  {
    //http://localhost:5000/api/auth/login
    [HttpPost("login")]

    public async Task<ActionResult<DtUsuario>> Login(Login.Ejecuta data)
    {
      return await Mediator.Send(data);
    }


    // //http://localhost:5000/api/auth/register
    // [HttpPost("register")]

    // public async Task<ActionResult<DtUsuario>> Registrar(Registrar.Ejecuta data)
    // {
    //   return await Mediator.Send(data);
    // }

  }
}