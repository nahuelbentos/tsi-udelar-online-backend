using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Business.CursosSeccion;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace WebAPI.Controllers {
  [AllowAnonymous]
  [Route ("api/[controller]")]
  [ApiController]
  public class CursoSeccionController : MiControllerBase {

    [HttpPost]
    public async Task<ActionResult<Unit>> AltaCursoSeccion (Nuevo.Ejecuta data) {
      return await this.Mediator.Send (data);
    }

    [HttpPost ("/foro")]
    public async Task<ActionResult<Unit>> AgregarForo (AgregarForo.Ejecuta data) {
      return await this.Mediator.Send (data);
    }

    [HttpPost ("/actividad")]
    public async Task<ActionResult<Unit>> AgregarActividad (AgregarActividad.Ejecuta data) {
      return await this.Mediator.Send (data);
    }

    [HttpPost ("/material")]
    public async Task<ActionResult<Unit>> AgregarMaterial (AgregarMaterial.Ejecuta data) {
      return await this.Mediator.Send (data);
    }

    [HttpGet]
    public async Task<ActionResult<List<CursoSeccion>>> GetCursosSeccion () {
      return await this.Mediator.Send (new Consulta.Ejecuta ());
    }

    [HttpGet ("{id}")]
    public async Task<ActionResult<CursoSeccion>> GetCursoSeccion (Guid Id) {
      return await this.Mediator.Send (new ConsultaById.Ejecuta { CursoSeccionId = Id });
    }

    [HttpGet ("{id}")]
    public async Task<ActionResult<CursoSeccion>> GetCursoSeccionByCurso (Guid Id) {
      return await this.Mediator.Send (new ConsultaByCurso.Ejecuta { CursoId = Id });
    }

    [HttpGet ("{id}")]
    public async Task<ActionResult<CursoSeccion>> GetCursoSeccionBySeccion (Guid Id) {
      return await this.Mediator.Send (new ConsultaBySeccion.Ejecuta { SeccionId = Id });
    }

    [HttpPut ("{id}")]
    public async Task<ActionResult<Unit>> ModificarCurso (Guid Id, Editar.Ejecuta data) {
      data.CursoId = Id;
      return await this.Mediator.Send (data);
    }

    [HttpDelete ("{id}")]
    public async Task<ActionResult<Unit>> Eliminar (Guid Id) {
      return await this.Mediator.Send (new Eliminar.Ejecuta { CursoSeccionId = Id });
    }

  }

}