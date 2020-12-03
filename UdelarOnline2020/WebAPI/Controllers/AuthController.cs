using System;
using System.Threading.Tasks; 
using Business.Datatypes;
using Business.Seguridad;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MediatR;

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
    
    [HttpPost("register")]
    public async Task<ActionResult<DtUsuario>> Register(Register.Ejecuta data) => await Mediator.Send(data); 

    [HttpPost("validate-token")]
    public async Task<ActionResult<Boolean>> ValidateToken(ValidateToken.Ejecuta data) => await Mediator.Send(data);
    
    [Authorize]
    [HttpPost("forgot-password")]

    // [ValidateAntiForgeryToken]
    public async Task<ActionResult<DtUsuario>> ForgotPassword(ForgotPassword.Ejecuta data) => await Mediator.Send(data);

    [HttpPost("mail-forgot-password")]
    public async Task<ActionResult<Unit>> RenewPassword(MailRenovarPassword.Ejecuta data) => await Mediator.Send(data);

  }
}