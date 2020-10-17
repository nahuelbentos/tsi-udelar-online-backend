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
  public class CursoController : MiControllerBase
  {
    [HttpPost]

    public async Task<ActionResult<Unit>> AltaCurso(Nuevo.Ejecuta data)
    {

      return await this.Mediator.Send(data);

    }

  }

}