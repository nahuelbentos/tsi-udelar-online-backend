using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Business.Carreras;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace WebAPI.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class CarreraController : MiControllerBase
    {
        [HttpPost]
 
        public async Task<ActionResult<Unit>> AltaCarrera(Nuevo.Ejecuta data)
        {
            return await this.Mediator.Send(data);
        }

        [HttpGet]
        public async Task<ActionResult<List<Carrera>>> GetCarreras()
        {
            return await this.Mediator.Send(new Consulta.Ejecuta());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Carrera>> GetCarrera(Guid id)
        {
            return await this.Mediator.Send(new ConsultaById.Ejecuta { CarreraId = id });
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Unit>> ModificarCarrera(Guid Id, Editar.Ejecuta data)
        {
            data.CarreraId = Id;
            return await this.Mediator.Send(data);
        }

        [HttpPost("asignar-curso")]
        public async Task<ActionResult<Unit>> AsignarCurso(AgregarCurso.Ejecuta data)
        {
            return await this.Mediator.Send(data);
        }

        [HttpDelete("quitar-curso")]
        public async Task<ActionResult<Unit>> QuitarCurso(QuitarCurso.Ejecuta data)
        {
            return await this.Mediator.Send(data);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Unit>> EliminarCarrera(Guid Id)
        {
            return await this.Mediator.Send(new Eliminar.Ejecuta { CarreraId = Id });
        }
        
    }
}