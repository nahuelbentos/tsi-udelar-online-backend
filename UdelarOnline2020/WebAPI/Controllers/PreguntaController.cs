using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Business.Preguntas;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace WebAPI.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class PreguntaController : MiControllerBase
    {
        [HttpPost]
        public async Task<ActionResult<Unit>> AltaCarrera(Nuevo.Ejecuta data)
        {
            return await this.Mediator.Send(data);
        }

        [HttpGet]
        public async Task<ActionResult<List<Pregunta>>> GetPreguntas()
        {
            return await this.Mediator.Send(new Consulta.Ejecuta());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Pregunta>> GetPregunta(Guid id)
        {
            return await this.Mediator.Send(new ConsultaById.Ejecuta { PreguntaId = id });
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Unit>> ModificarPregunta(Guid id, Editar.Ejecuta data)
        {
            data.PreguntaId = id;
            return await this.Mediator.Send(data);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Unit>> EliminarPregunta(Guid id)
        {
            return await this.Mediator.Send(new Eliminar.Ejecuta { PreguntaId = id });
        }
        
    }
}