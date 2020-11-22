using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Business.Roles;

namespace WebAPI.Controllers
{

  [Route("api/[controller]")]
  [ApiController]
  public class RoleController : MiControllerBase
  {
    [HttpPost]
    public async Task<ActionResult<Unit>> AltaRole(Nuevo.Ejecuta data) => await this.Mediator.Send(data);

    [HttpGet]
    public async Task<ActionResult<List<IdentityRole>>> GetRoles() => await this.Mediator.Send(new Consulta.Ejecuta());

    [HttpDelete]
    public async Task<ActionResult<Unit>> EliminarRole(Eliminar.Ejecuta data) => await this.Mediator.Send(data);


  }
}