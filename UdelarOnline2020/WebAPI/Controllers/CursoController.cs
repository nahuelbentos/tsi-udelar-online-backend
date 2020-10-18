
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using Business.Cursos;
using Models;
using System;

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

    [HttpGet]
    public async Task<ActionResult<List<Curso>>> GetCursos()
    {
      return await this.Mediator.Send(new Consulta.Ejecuta());
    }


    [HttpGet("{id}")]
    public async Task<ActionResult<Curso>> GetCurso(Guid Id)
    {
      return await this.Mediator.Send(new ConsultaById.Ejecuta { CursoId = Id });
    }



    [HttpPut("{id}")]
    public async Task<ActionResult<Unit>> ModificarCurso(Guid Id, Editar.Ejecuta data)
    {
      data.CursoId = Id;
      return await this.Mediator.Send(data);
    }

  }

}