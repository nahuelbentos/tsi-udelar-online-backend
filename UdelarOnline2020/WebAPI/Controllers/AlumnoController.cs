using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Business.AlumnoPruebaOnlines;
using Business.Alumnos;
using Business.Datatypes;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace WebAPI.Controllers
{

    // [Authorize(Roles = "Alumno")]
    [Route("api/[controller]")]
    [ApiController]
    public class AlumnoController : MiControllerBase
    {

    [HttpGet("mis-cursos/{id}")]
    public async Task<ActionResult<List<Curso>>> GetMisCursos(Guid id) => await this.Mediator.Send(new GetMisCursos.Ejecuta{ AlumnoId = id });
    
    [HttpPost("inscribirse-a-curso")]
    public async Task<ActionResult<Unit>> InscribirseACurso(InscribirseACurso.Ejecuta data) => await this.Mediator.Send(data);
    
    [HttpPost("inscribirse-a-evaluacion")]
    public async Task<ActionResult<Unit>> InscribirseAEvaluacion(InscribirseAEvaluacion.Ejecuta data) => await this.Mediator.Send(data);

    [HttpPost("esta-inscripto-curso")]
    public async Task<ActionResult<bool>> EstaInscriptoCurso(EstaInscriptoCurso.Ejecuta data) => await this.Mediator.Send(data);
    
    [HttpPost("esta-inscripto-evaluacion")]
    public async Task<ActionResult<bool>> EstaInscriptoEvaluacion(EstaInscriptoEvaluacion.Ejecuta data) => await this.Mediator.Send(data);


    [HttpGet("prueba-online/{id}")]
    public async Task<ActionResult<List<DtEvaluacion>>> GetPruebasOnline(Guid id) => await this.Mediator.Send(new ConsultaByAlumnoId.Ejecuta { AlumnoId = id });

    



    }
}