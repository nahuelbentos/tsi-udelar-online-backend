using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.TemplatesCurso;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using Models;
using Microsoft.AspNetCore.Authorization;

namespace WebAPI.Controllers
{

  [AllowAnonymous]
  [Route("api/[controller]")]
  [ApiController]
  public class TemplateCursoController : MiControllerBase
  {
    [HttpPost]
    public async Task<ActionResult<Unit>> AltaTemplateCurso(Nuevo.Ejecuta data)
    {
      return await this.Mediator.Send(data);
    }

    [HttpGet]
     public async Task<ActionResult<List<TemplateCurso>>> GetTemplateCurso()
    {
      return await this.Mediator.Send(new Consulta.Ejecuta());
    }

    [HttpGet("id/{id}")]
    public async Task<ActionResult<TemplateCurso>> GetTemplateCursoById(Guid id)
    {
      return await this.Mediator.Send(new ConsultaById.Ejecuta { TemplateCursoId = id });
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Unit>> ModificarTemplateCurso(Guid id, Editar.Ejecuta data)
    {
      data.TemplateCursoId = id;
      return await this.Mediator.Send(data);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<Unit>> Eliminar(Guid id)
    {
      return await this.Mediator.Send(new Eliminar.Ejecuta { TemplateCursoId = id });
    }

  }

}