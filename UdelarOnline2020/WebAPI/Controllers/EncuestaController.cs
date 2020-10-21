using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Business.Encuestas;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace WebAPI.Controllers
{
  public class EncuestaController : MiControllerBase
  {

    [HttpPost]
    public async Task<ActionResult<Unit>> AltaEncuesta(Nuevo.Ejecuta data)
    {
      return await this.Mediator.Send(data);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Unit>> EditarEncuesta(Guid id, Editar.Ejecuta data)
    {
      data.EncuestaId = id;
      return await this.Mediator.Send(data);
    }

    [HttpGet]
    public async Task<ActionResult<List<Encuesta>>> GetEncuestas()
    {
      return await this.Mediator.Send(new Consulta.Ejecuta());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Encuesta>> GetEncuestaById(Guid id)
    {
      return await this.Mediator.Send(new ConsultaById.Ejecuta { EncuestaId = id });
    }


    [HttpDelete("{id}")]
    public async Task<ActionResult<Unit>> EliminarEncuesta(Guid id)
    {
      return await this.Mediator.Send(new Eliminar.Ejecuta { EncuestaId = id });
    }
  }
}