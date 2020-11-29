using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Business.Foros;
using System.Collections.Generic;
using Models;
using System;
using Microsoft.AspNetCore.Authorization;

namespace WebAPI.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class ForoController : MiControllerBase
    {
        [HttpPost]
        public async Task<ActionResult<Unit>> AltaForo(Nuevo.Ejecuta data)
        {
            return await this.Mediator.Send(data);
        }

        [HttpGet]
        public async Task<ActionResult<List<Foro>>> GetForos()
        {
            return await this.Mediator.Send(new Consulta.Ejecuta());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Foro>> GetForo(Guid id)
        {
            return await this.Mediator.Send(new ConsultaById.Ejecuta { ForoId = id });
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Unit>> ModificarForo(Guid id, Editar.Ejecuta data)
        {
            data.ForoId = id;
            return await this.Mediator.Send(data);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Unit>> EliminarForo(Guid Id)
        {
            return await this.Mediator.Send(new Eliminar.Ejecuta { ForoId = Id });
        }

    }
}