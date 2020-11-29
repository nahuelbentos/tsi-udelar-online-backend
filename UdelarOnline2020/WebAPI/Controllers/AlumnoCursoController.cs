using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Business.AlumnoCursos;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlumnoCursoController : MiControllerBase
    {
        [HttpPost]
        public async Task<ActionResult<Unit>> AltaAlumnoCurso(Nuevo.Ejecuta data)
        {
            return await this.Mediator.Send(data);
        }

        [HttpGet]
        public async Task<ActionResult<List<AlumnoCurso>>> GetAlumnoCursos()
        {
            return await this.Mediator.Send(new Consulta.Ejecuta());
        }

        [HttpGet("byalumno/{id}")]
        public async Task<ActionResult<List<AlumnoCurso>>> GetAlumnoCursoByAlumno(Guid id)
        {
            return await this.Mediator.Send(new ConsultaByAlumnoId.Ejecuta { AlumnoId = id});
        }

        [HttpGet("bycurso/{id}")]
        public async Task<ActionResult<List<AlumnoCurso>>> GetAlumnoCursoByCurso(Guid id)
        {
            return await this.Mediator.Send(new ConsultaByCursoId.Ejecuta { CursoId = id});
        }

        [HttpPut("{alumnoCursoId}")]
        public async Task<ActionResult<Unit>> ModificarAlumnoCurso(Guid alumnoCursoId, Editar.Ejecuta data)
        {
            data.AlumnoCursoId = alumnoCursoId;
            return await this.Mediator.Send(data);
        }

        [HttpDelete("{alumnoCursoId}")]
        public async Task<ActionResult<Unit>> EliminarAlumnoCurso(Guid alumnoCursoId)
        {
            return await this.Mediator.Send(new Eliminar.Ejecuta { AlumnoId = alumnoCursoId});
        }
    }
}