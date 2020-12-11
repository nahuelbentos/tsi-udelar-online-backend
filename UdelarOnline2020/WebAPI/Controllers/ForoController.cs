using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Business.Foros;
using System.Collections.Generic;
using Models;
using System;
using Microsoft.AspNetCore.Authorization;
using Business.Datatypes;

namespace WebAPI.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class ForoController : MiControllerBase
    {
        [HttpPost]
        public async Task<ActionResult<Unit>> AltaForo(Nuevo.Ejecuta data) =>  await this.Mediator.Send(data);

        [HttpGet]
        public async Task<ActionResult<List<DtForo>>> GetForos() =>  await this.Mediator.Send(new Consulta.Ejecuta());

        [HttpGet("{id}")]
        public async Task<ActionResult<DtForo>> GetForo(Guid id) =>  await this.Mediator.Send(new ConsultaById.Ejecuta { ForoId = id });

        [HttpPut("{id}")]
        public async Task<ActionResult<Unit>> ModificarForo(Guid id, Editar.Ejecuta data)
        {
            data.ForoId = id;
            return await this.Mediator.Send(data);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Unit>> EliminarForo(Guid Id) =>  await this.Mediator.Send(new Eliminar.Ejecuta { ForoId = Id });

    }
}