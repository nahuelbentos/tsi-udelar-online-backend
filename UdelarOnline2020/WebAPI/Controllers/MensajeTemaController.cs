using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Business.MensajesTema;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MensajeTemaController : MiControllerBase
    {
        [HttpPost]
        public async Task<ActionResult<Unit>> AltaMensajeTema(Nuevo.Ejecuta data)
        {
        return await this.Mediator.Send(data);
        }

        [HttpGet]
        public async Task<ActionResult<List<MensajeTema>>> GetMensajesTema()
        {
        return await this.Mediator.Send(new Consulta.Ejecuta());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MensajeTema>> GetMensajeTema(Guid Id)
        {
        return await this.Mediator.Send(new ConsultaById.Ejecuta { MensajeId = Id });
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Unit>> ModificarMensajeTema(Guid Id, Editar.Ejecuta data)
        {
        data.MensajeId = Id;
        return await this.Mediator.Send(data);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Unit>> Eliminar(Guid Id)
        {
        return await this.Mediator.Send(new Eliminar.Ejecuta { MensajeId = Id });
        }
    }
}