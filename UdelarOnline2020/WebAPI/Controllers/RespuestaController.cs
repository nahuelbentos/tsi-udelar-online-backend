using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Business.Respuestas;
using System.Collections.Generic;
using Models;
using System;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RespuestaController : MiControllerBase
  {
    [HttpPost]
    public async Task<ActionResult<Unit>> AltaRespuesta(Nuevo.Ejecuta data)
    {
      return await this.Mediator.Send(data);
    }

    /*[HttpGet]
    public async Task<ActionResult<List<Respuesta>>> GetRespuestas()
    {
      return await this.Mediator.Send(new Consulta.Ejecuta());
    }*/


    /*[HttpGet("{id}")]
    public async Task<ActionResult<Curso>> GetRespuesta(Guid Id)
    {
      return await this.Mediator.Send(new ConsultaById.Ejecuta { CursoId = Id });
    }*/

    [HttpPut("{id}")]
    public async Task<ActionResult<Unit>> ModificarRespuesta(Guid Id, Editar.Ejecuta data)
    {
      data.RespuestaId = Id;
      return await this.Mediator.Send(data);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<Unit>> Eliminar(Guid Id)
    {
      return await this.Mediator.Send(new Eliminar.Ejecuta { RespuestaId = Id });
    }

  }
}