using System.Threading.Tasks;
using Business.Notificaciones;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
  
  public class NotificacionesController : MiControllerBase
  {
    [HttpPost("sendMail")]

    public async Task<ActionResult<bool>> SendMail()
    {
        return await this.Mediator.Send(new SendMail.Ejecuta());
    }
    
  }
}