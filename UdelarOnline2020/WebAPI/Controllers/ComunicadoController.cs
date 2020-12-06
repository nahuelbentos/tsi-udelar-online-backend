using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Comunicados;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using Models;
using Microsoft.AspNetCore.Authorization;

namespace WebAPI.Controllers
{
  
  [Route("api/[controller]")]
  [ApiController]
  public class ComunicadoController : MiControllerBase
  {
    [HttpPost]
    public async Task<ActionResult<Unit>> AltaComunicado(Nuevo.Ejecuta data)
    { 
      return await this.Mediator.Send(data);
    }

    [HttpGet]
     public async Task<ActionResult<List<Comunicado>>> GetComunicados()
    {
      return await this.Mediator.Send(new Consulta.Ejecuta());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Comunicado>> GetComunicadoById(Guid id)
    {
      return await this.Mediator.Send(new ConsultaById.Ejecuta { ComunicadoId = id });
    }

    [HttpGet("facultad/{id}")]
    public async Task<ActionResult<List<Comunicado>>> GetComunicadosByFacultadId(Guid id)
    {
      return await this.Mediator.Send(new ConsultaByFacultadId.Ejecuta { FacultadId = id });
    }

      [HttpPut("{id}")]
    public async Task<ActionResult<Unit>> ModificarComunicado(Guid id, Editar.Ejecuta data)
    {
        data.ComunicadoId = id;
        return await this.Mediator.Send(data);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<Unit>> Eliminar(Guid id)
    {
      return await this.Mediator.Send(new Eliminar.Ejecuta { ComunicadoId = id });
    }

    [HttpPost("general")]
    public async Task<ActionResult<Unit>> PublicarGeneral(PublicarGeneral.Ejecuta data)
    {
      return await this.Mediator.Send(data);
    }

    [HttpPost("facultad")]
    public async Task<ActionResult<Unit>> PublicarFacultad(PublicarFacultad.Ejecuta data)
    {
      return await this.Mediator.Send(data);
    }

    [HttpPost("curso")]
    public async Task<ActionResult<Unit>> PublicarCurso(PublicarCurso.Ejecuta data)
    {
      return await this.Mediator.Send(data);
    }

  }

}