
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using Business.Cursos;
using Models;
using System;
using Microsoft.AspNetCore.Authorization;

namespace WebAPI.Controllers
{
  
  [Route("api/[controller]")]
  [ApiController]
  public class CursoController : MiControllerBase
  {

    [HttpPost]

    public async Task<ActionResult<Unit>> AltaCurso(Nuevo.Ejecuta data) => await this.Mediator.Send(data);

    [HttpGet]
    public async Task<ActionResult<List<Curso>>> GetCursos() => await this.Mediator.Send(new Consulta.Ejecuta());


    [HttpGet("{id}")]
    public async Task<ActionResult<Curso>> GetCurso(Guid Id) =>  await this.Mediator.Send(new ConsultaById.Ejecuta { CursoId = Id });

    [HttpPut("{id}")]
    public async Task<ActionResult<Unit>> ModificarCurso(Guid Id, Editar.Ejecuta data)
    {
      data.CursoId = Id;
      return await this.Mediator.Send(data);
    }

    [HttpPut("asignar-docente")]
    public async Task<ActionResult<Unit>> AsignarDocente(AsignarDocente.Ejecuta data) =>  await this.Mediator.Send(data);

    [HttpDelete("{id}")]
    public async Task<ActionResult<Unit>> Eliminar(Guid Id) => await this.Mediator.Send(new Eliminar.Ejecuta { CursoId = Id });

  }

}