using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using Business.Cursos;

namespace WebAPI.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class TemaForoController : MiControllerBase
  {
    [HttpPost]

    public async Task<ActionResult<Unit>> AltaTemaForo(Nuevo.Ejecuta data)
    {

      return await this.Mediator.Send(data);

    }

  }

}