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

    [HttpGet ("{templateCursoId}/{seccionId}")]
    public async Task<ActionResult<TemplateCursoSeccion>> GetTemplateCursoSeccionById (Guid templateCursoId, Guid seccionId) {
      return await this.Mediator.Send (new ConsultaById.Ejecuta { TemplateCursoId = templateCursoId, SeccionId = seccionId });
    }

    [HttpGet ("seccionId/{id}")]
    public async Task<ActionResult<TemplateCursoSeccion>> GetTemplateCursoSeccionBySeccion (Guid id) {
      return await this.Mediator.Send (new ConsultaBySeccion.Ejecuta { SeccionId = id });
    }

    [HttpGet ("templateCursoId/{id}")]
    public async Task<ActionResult<TemplateCursoSeccion>> GetTemplateCursoSeccionByTemplateCurso (Guid id) {
      return await this.Mediator.Send (new ConsultaByTemplateCurso.Ejecuta { TemplateCursoId = id });
    }

    [HttpPut ("{templateCursoId}/{seccionId}")]
    public async Task<ActionResult<Unit>> ModificarTemplateCursoSeccion (Guid templateCursoId, Guid seccionId, Editar.Ejecuta data) {
      data.TemplateCursoId = templateCursoId;
      data.SeccionId = seccionId;
      return await this.Mediator.Send (data);
    }

    [HttpDelete ("{templateCursoId}/{seccionId}")]
    public async Task<ActionResult<Unit>> Eliminar (Guid templateCusoId, Guid seccionId) {
      return await this.Mediator.Send (new Eliminar.Ejecuta { TemplateCursoId = templateCusoId, SeccionId = seccionId });
    }

  }

}