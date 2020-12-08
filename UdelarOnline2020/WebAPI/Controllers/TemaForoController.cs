using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.TemasForo;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using Models;
using Microsoft.AspNetCore.Authorization;

namespace WebAPI.Controllers
{

  
  [Route("api/[controller]")]
  [ApiController]
  public class TemaForoController : MiControllerBase
  {

    [HttpPost]
    public async Task<ActionResult<Unit>> AltaTemaForo(Nuevo.Ejecuta data) => await this.Mediator.Send(data);

    [HttpGet]
    public async Task<ActionResult<List<TemaForo>>> GetTemasForo() => await this.Mediator.Send(new Consulta.Ejecuta());
    
    [HttpGet("foro/{id}/{usuarioId}")]
    public async Task<ActionResult<List<TemaForo>>> GetTemaForoByForo(Guid id, string usuarioId) => 
          await this.Mediator.Send(new GetTemaForoByForo.Ejecuta { ForoId = id, UsuarioId = usuarioId });

    [HttpGet("id/{id}")]
    public async Task<ActionResult<TemaForo>> GetTemaForoById(Guid id) => await this.Mediator.Send(new ConsultaById.Ejecuta { TemaForoId = id });

    [HttpPut("{id}")]
    public async Task<ActionResult<Unit>> ModificarTemaForo(Guid id, Editar.Ejecuta data)
    {
      data.TemaForoId = id;
      return await this.Mediator.Send(data);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<Unit>> Eliminar(Guid id) => await this.Mediator.Send(new Eliminar.Ejecuta { TemaForoId = id });

    // [HttpPost]
    public async Task<ActionResult<Unit>> Responder(Responder.Ejecuta data) => await this.Mediator.Send(data);

    [HttpPost("{id}")]
    public async Task<ActionResult<Unit>> Bloquear(Guid id, Bloquear.Ejecuta data)
    {
      data.MensajeId = id;
      return await this.Mediator.Send(data);
    }

  }
}