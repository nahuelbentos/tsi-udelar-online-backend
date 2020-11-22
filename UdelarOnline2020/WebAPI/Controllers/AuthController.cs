using System;
using System.Threading.Tasks;
using Business.Datatypes;
using Business.Seguridad;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
  [AllowAnonymous]
  [Route("api/[controller]")]
  [ApiController]
  public class AuthController : MiControllerBase
  {
    //http://localhost:5000/api/auth/login
    [HttpPost("login")]
    public async Task<ActionResult<DtUsuario>> Login(Login.Ejecuta data) => await Mediator.Send(data);

    [HttpPost("validate-token")]
    public async Task<ActionResult<Boolean>> ValidateToken(ValidateToken.Ejecuta data) => await Mediator.Send(data);

    [HttpPut("forgot-password")]
    public async Task<ActionResult<DtUsuario>> ForgotPassword(Login.Ejecuta data) => await Mediator.Send(data);

  }
}