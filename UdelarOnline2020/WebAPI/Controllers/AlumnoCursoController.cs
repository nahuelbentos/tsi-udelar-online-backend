using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Business.AlumnoCursos;
using Business.Datatypes;
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
        public async Task<ActionResult<Unit>> AltaAlumnoCurso(Nuevo.Ejecuta data) => await this.Mediator.Send(data);

        [HttpGet]
        public async Task<ActionResult<List<DtAlumnoCurso>>> GetAlumnoCursos() => await this.Mediator.Send(new Consulta.Ejecuta());

        [HttpGet("id/{alumnoId}/{cursoId}")]
        public async Task<ActionResult<DtAlumnoCurso>> GetById(string alumnoId, Guid cursoId) => await this.Mediator.Send(new ConsultaById.Ejecuta { AlumnoId = alumnoId, CursoId = cursoId});
        [HttpGet("byalumno/{id}")]
        public async Task<ActionResult<List<DtAlumnoCurso>>> GetAlumnoCursoByAlumno(Guid id) => await this.Mediator.Send(new ConsultaByAlumnoId.Ejecuta { AlumnoId = id});

        [HttpGet("exportarPdf/{cursoId}")]
        public async Task<ActionResult<Stream>>ExportarPDF(Guid cursoId) =>  await Mediator.Send(new ExportarPDF.Ejecuta{CursoId = cursoId});

        [HttpGet("bycurso/{id}")]
        public async Task<ActionResult<List<DtAlumnoCurso>>> GetAlumnoCursoByCurso(Guid id) => await this.Mediator.Send(new ConsultaByCursoId.Ejecuta { CursoId = id});

        [HttpPut]
        public async Task<ActionResult<Unit>> ModificarAlumnoCurso(  Editar.Ejecuta data) => await this.Mediator.Send(data);

        [HttpDelete("{alumnoId}/{cursoId}")]
        public async Task<ActionResult<Unit>> EliminarAlumnoCurso(Guid alumnoId, Guid cursoId) => await this.Mediator.Send(new Eliminar.Ejecuta { AlumnoId = alumnoId, CursoId = cursoId });
    }
}