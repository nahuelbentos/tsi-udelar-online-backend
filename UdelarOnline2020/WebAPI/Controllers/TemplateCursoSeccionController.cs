using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.TemplatesCursoSeccion;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace WebAPI.Controllers {

  [AllowAnonymous]
  [Route ("api/[controller]")]
  [ApiController]
  public class TemplateCursoSeccionController : MiControllerBase {
    [HttpPost]
    public async Task<ActionResult<Unit>> AltaTemplateCursoSeccion (Nuevo.Ejecuta data) {
      return await this.Mediator.Send (data);
    }

    [HttpGet]
    public async Task<ActionResult<List<TemplateCursoSeccion>>> GetTemplateCursoSeccion () {
      return await this.Mediator.Send (new Consulta.Ejecuta ());
    }

    [HttpGet ("id/{id}")]
    public async Task<ActionResult<TemplateCursoSeccion>> GetTemplateCursoSeccionById (Guid id) {
      return await this.Mediator.Send (new ConsultaById.Ejecuta { TemplateCursoSeccionId = id });
    }

    [HttpGet ("seccionId/{id}")]
    public async Task<ActionResult<TemplateCursoSeccion>> GetTemplateCursoSeccionBySeccion (Guid id) {
      return await this.Mediator.Send (new ConsultaBySeccion.Ejecuta { SeccionId = id });
    }

    [HttpGet ("templateCursoId/{id}")]
    public async Task<ActionResult<TemplateCursoSeccion>> GetTemplateCursoSeccionByTemplateCurso (Guid id) {
      return await this.Mediator.Send (new ConsultaByTemplateCurso.Ejecuta { TemplateCursoId = id });
    }

    [HttpPut ("{id}")]
    public async Task<ActionResult<Unit>> ModificarTemplateCursoSeccion (Guid id, Editar.Ejecuta data) {
      data.TemplateCursoSeccionId = id;
      return await this.Mediator.Send (data);
    }

    [HttpDelete ("{id}")]
    public async Task<ActionResult<Unit>> Eliminar (Guid id) {
      return await this.Mediator.Send (new Eliminar.Ejecuta { TemplateCursoSeccionId = id });
    }

  }

}