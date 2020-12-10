using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Business.AlumnoPruebaOnlines;
using Business.Datatypes;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlumnoPruebaOnlineController : MiControllerBase
    {
        [HttpPost]
        public async Task<ActionResult<Unit>> AltaAlumnoPruebaOnline(Nuevo.Ejecuta data) => await this.Mediator.Send(data);

        [HttpGet]
        public async Task<ActionResult<List<AlumnoPruebaOnline>>> GetAlumnoPruebaOnlines() => await this.Mediator.Send(new Consulta.Ejecuta());

        [HttpGet("byalumno/{id}")]
        public async Task<ActionResult<List<DtEvaluacion>>> GetAlumnoPruebaOnlineByAlumno(Guid id) => await this.Mediator.Send(new ConsultaByAlumnoId.Ejecuta { AlumnoId = id});

        [HttpGet("byprueba/{id}")]
        public async Task<ActionResult<List<AlumnoPruebaOnline>>> GetAlumnoPruebaOnlineByPruebaOnline(Guid id) => await this.Mediator.Send(new ConsultaByPruebaOnlineId.Ejecuta { PruebaOnlineId = id});

        [HttpPut("{alumnoId}/{pruebaId}")]
        public async Task<ActionResult<Unit>> ModificarAlumnoPruebaOnline(Guid alumnoId, Guid pruebaId, Editar.Ejecuta data)
        {
            data.AlumnoId = alumnoId;
            data.PruebaOnlineId = pruebaId;
            return await this.Mediator.Send(data);
        }

        [HttpDelete("{alumnoId}/{pruebaId}")]
        public async Task<ActionResult<Unit>> EliminarAlumnoPruebaOnline(Guid alumnoId, Guid pruebaId) => await this.Mediator.Send(new Eliminar.Ejecuta { AlumnoId = alumnoId, PruebaOnlineId = pruebaId });
    }
}