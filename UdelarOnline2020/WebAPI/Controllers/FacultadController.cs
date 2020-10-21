using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Business.Facultades;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace WebAPI.Controllers
{
  public class FacultadController : MiControllerBase
  {

    [HttpPost]
    public async Task<ActionResult<Unit>> AltaFacultad(Nuevo.Ejecuta data)
    {
      return await this.Mediator.Send(data);
    }


    [HttpPut("{id}")]
    public async Task<ActionResult<Unit>> EditarFacultad(Guid id, Editar.Ejecuta data)
    {
      data.FacultadId = id;
      return await this.Mediator.Send(data);
    }

    [HttpGet]
    public async Task<ActionResult<List<Facultad>>> GetFacultades()
    {
      return await this.Mediator.Send(new Consulta.Ejecuta());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Facultad>> GetFacultadById(Guid id)
    {
      return await this.Mediator.Send(new ConsultaById.Ejecuta { FacultadId = id });
    }


    [HttpDelete("{id}")]
    public async Task<ActionResult<Unit>> EliminarFacultad(Guid id)
    {
      return await this.Mediator.Send(new Eliminar.Ejecuta { FacultadId = id });
    }


  }
}