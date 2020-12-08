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

    // Actividades
    [HttpPost]
    public async Task<ActionResult<Unit>> AltaActividad(Nuevo.Ejecuta data) => await this.Mediator.Send(data);


    [HttpPut("trabajo/{id}")]
    public async Task<ActionResult<Unit>> AltaTrabajo(Guid id, NuevoTrabajo.Ejecuta data)
    {
      data.ActividadId = id;
      return await this.Mediator.Send(data);
    } 

    [HttpGet]
    public async Task<ActionResult<List<DtActividad>>> GetActividades() => await this.Mediator.Send(new Consulta.Ejecuta());
    
    [HttpGet("tipo/{tipo}")]
    public async Task<ActionResult<List<Actividad>>> GetActividadesByTipo(string tipo) => await this.Mediator.Send(new ConsultaByTipo.Ejecuta { Tipo = tipo });

    [HttpGet("alumno/{id}")]
    public async Task<ActionResult<List<Actividad>>> GetActividadesByAlumno(Guid id) => await this.Mediator.Send(new ConsultaByAlumno.Ejecuta { AlumnoId = id });
    
    [HttpGet("curso/{id}")]
    public async Task<ActionResult<List<Actividad>>> GetActividadesByCurso(Guid id) => await this.Mediator.Send(new ConsultaByCurso.Ejecuta { CursoId = id });


    [HttpGet("{id}")]
    public async Task<ActionResult<DtActividad>> GetActividad(Guid id) => await this.Mediator.Send(new ConsultaById.Ejecuta { ActividadId = id });

    [HttpPut("{id}")]
    public async Task<ActionResult<Unit>> ModificarActividad(Guid id, Editar.Ejecuta data)
    {
      data.ActividadId = id;
      return await this.Mediator.Send(data);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<Unit>> EliminarActividad(Guid id) => await this.Mediator.Send(new Eliminar.Ejecuta { ActividadId = id });

    //Prueba Online
    [HttpPost("pruebaonline")]
    public async Task<ActionResult<Unit>> AltaPruebaOnline(NuevaPruebaOnline.Ejecuta data) => await this.Mediator.Send(data);

    [HttpGet("pruebaonline/usuario/{id}")]
    public async Task<ActionResult<List<DtPruebaOnline>>> GetPruebasOnline(string id) => await this.Mediator.Send(new ConsultaPruebaOnline.Ejecuta{ UsuarioId = id });
    
    [HttpGet("pruebaonline/{id}")]
    public async Task<ActionResult<DtPruebaOnline>> GetPruebasOnlineById(Guid id) => await this.Mediator.Send(new GetPruebaOnlineById.Ejecuta{ Id = id });
 

    //Encuesta
    [HttpGet("encuesta/{id}")]
    public async Task<ActionResult<DtEncuesta>> GetEncuesta(Guid id) => await this.Mediator.Send(new ConsultaEncuestaById.Ejecuta { Id = id });

    [HttpPost("encuesta")]
    public async Task<ActionResult<Unit>> AltaEncuesta(NuevaEncuesta.Ejecuta data) => await this.Mediator.Send(data);

    [HttpPut("encuesta/{id}")]
    public async Task<ActionResult<Unit>> ModificarEncuesta(Guid id, EditarEncuesta.Ejecuta data)
    {
      data.ActividadId = id;
      return await this.Mediator.Send(data);
    }
  }
}