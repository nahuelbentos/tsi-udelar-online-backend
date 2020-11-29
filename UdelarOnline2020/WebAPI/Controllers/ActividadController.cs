using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Business.Actividades;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace WebAPI.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class ActividadController : MiControllerBase
    {
        [HttpPost]
        public async Task<ActionResult<Unit>> AltaActividad(Nuevo.Ejecuta data)
        {
            return await this.Mediator.Send(data);
        }

        [HttpPost("/encuesta")]
        public async Task<ActionResult<Unit>> AltaEncuesta(NuevaEncuesta.Ejecuta data)
        {
            return await this.Mediator.Send(data);
        }

        [HttpPost("/pruebaonline")]
        public async Task<ActionResult<Unit>> AltaPruebaOnline(NuevaPruebaOnline.Ejecuta data)
        {
            return await this.Mediator.Send(data);
        }

        [HttpGet]
        public async Task<ActionResult<List<Actividad>>> GetActividades()
        {
            return await this.Mediator.Send(new Consulta.Ejecuta());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Actividad>> GetActividad(Guid id)
        {
            return await this.Mediator.Send(new ConsultaById.Ejecuta { ActividadId = id});
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Unit>> ModificarActividad(Guid id, Editar.Ejecuta data)
        {
            data.ActividadId = id;
            return await this.Mediator.Send(data);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Unit>> EliminarActividad(Guid id)
        {
            return await this.Mediator.Send(new Eliminar.Ejecuta { ActividadId = id});
        }
    }
}