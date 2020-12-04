
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using Business.Cursos;
using Models;
using System;
using Microsoft.AspNetCore.Authorization;
using Business.Datatypes;

namespace WebAPI.Controllers
{
  
  [Route("api/[controller]")]
  [ApiController]
  public class CursoController : MiControllerBase
  {

    [HttpPost]

    public async Task<ActionResult<Unit>> AltaCurso(Nuevo.Ejecuta data) => await this.Mediator.Send(data);

    [HttpGet("usuario/{id}")]
    public async Task<ActionResult<List<DtCurso>>> GetCursosByUsuario(Guid id) => await this.Mediator.Send(new ConsultaByUsuario.Ejecuta{ Id = id });
    
    [HttpGet("facultad/{id}")]
    public async Task<ActionResult<List<DtCurso>>> GetCursosByFacultad(Guid id ) => await this.Mediator.Send(new ConsultaByFacultad.Ejecuta{ Id = id });

    [HttpGet("carrera/{id}")]
    public async Task<ActionResult<List<DtCurso>>> GetCursosByCarrera(Guid id ) => await this.Mediator.Send(new ConsultaByCarrera.Ejecuta{ Id = id });
    
    [HttpGet]
    public async Task<ActionResult<List<DtCurso>>> GetCursos() => await this.Mediator.Send(new Consulta.Ejecuta());
    
    [HttpGet("filter/{filter}")]
    public async Task<ActionResult<List<DtCurso>>> GetCursosByFilter(string filter) => await this.Mediator.Send(new ConsultaByFilter.Ejecuta{ Filter = filter });


    [HttpGet("{id}")]
    public async Task<ActionResult<DtCurso>> GetCurso(Guid Id) =>  await this.Mediator.Send(new ConsultaById.Ejecuta { CursoId = Id });

    [HttpPut("{id}")]
    public async Task<ActionResult<Unit>> ModificarCurso(Guid Id, Editar.Ejecuta data)
    {
      data.CursoId = Id;
      return await this.Mediator.Send(data);
    }

    [HttpPut("cerrar-actas")]
    public async Task<ActionResult<Unit>> CerrarActas( CerrarActas.Ejecuta data) => await this.Mediator.Send(data);


    [HttpPost("docente")]
    public async Task<ActionResult<Unit>> AsignarDocente(AsignarDocente.Ejecuta data) =>  await this.Mediator.Send(data);

    [HttpDelete("{id}")]
    public async Task<ActionResult<Unit>> Eliminar(Guid Id) => await this.Mediator.Send(new Eliminar.Ejecuta { CursoId = Id });

  }

}