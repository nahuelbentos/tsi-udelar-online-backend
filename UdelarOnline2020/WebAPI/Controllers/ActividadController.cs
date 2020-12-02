using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Business.Actividades;
using Business.Datatypes;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace WebAPI.Controllers
{

  [Route("api/[controller]")]
  [ApiController]
  public class ActividadController : MiControllerBase
  {
    [HttpPost]
    public async Task<ActionResult<Unit>> AltaActividad(Nuevo.Ejecuta data) => await this.Mediator.Send(data);

    [HttpPost("pruebaonline")]
    public async Task<ActionResult<Unit>> AltaPruebaOnline(NuevaPruebaOnline.Ejecuta data) => await this.Mediator.Send(data);

    [HttpPut("trabajo/{id}")]
    public async Task<ActionResult<Unit>> AltaTrabajo(Guid id, NuevoTrabajo.Ejecuta data)
    {
      data.ActividadId = id;
      return await this.Mediator.Send(data);
    } 

    [HttpGet]
    public async Task<ActionResult<List<Actividad>>> GetActividades() => await this.Mediator.Send(new Consulta.Ejecuta());
    
    [HttpGet("tipo/{tipo}")]
    public async Task<ActionResult<List<Actividad>>> GetActividadesByTipo(string tipo) => await this.Mediator.Send(new ConsultaByTipo.Ejecuta { Tipo = tipo });


    [HttpGet("{id}")]
    public async Task<ActionResult<Actividad>> GetActividad(Guid id) => await this.Mediator.Send(new ConsultaById.Ejecuta { ActividadId = id });

    [HttpPut("{id}")]
    public async Task<ActionResult<Unit>> ModificarActividad(Guid id, Editar.Ejecuta data)
    {
      data.ActividadId = id;
      return await this.Mediator.Send(data);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<Unit>> EliminarActividad(Guid id) => await this.Mediator.Send(new Eliminar.Ejecuta { ActividadId = id });


    // Encuesta

    [HttpGet("encuesta/{id}")]
    public async Task<ActionResult<DtEncuesta>> GetEncuesta(Guid id) => await this.Mediator.Send(new ConsultaEncuestaById.Ejecuta { Id = id });

    [HttpPost("encuesta")]
    public async Task<ActionResult<Unit>> AltaEncuesta(NuevaEncuesta.Ejecuta data) => await this.Mediator.Send(data);

    [HttpPut("encuesta/{id}")]
    public async Task<ActionResult<Unit>> ModificarEncuesta(Guid id, EditarEncuesta.Ejecuta data)
    {
      data.EncuestaId = id;
      return await this.Mediator.Send(data);
    }
  }
}