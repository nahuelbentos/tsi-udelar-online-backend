using System.Threading.Tasks;
using Business.Facultades;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
  public class FacultadController : MiControllerBase
  {

    [HttpPost]
    public async Task<ActionResult<Unit>> AltaEncuesta(Nuevo.Ejecuta data)
    {
      return await this.Mediator.Send(data);
    }



  }
}