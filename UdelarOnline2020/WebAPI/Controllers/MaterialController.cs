using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Business.Materiales;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MaterialController : MiControllerBase
    {
        [HttpPost]
        public async Task<ActionResult<Unit>> AltaMaterial(Nuevo.Ejecuta data)
        {
        return await this.Mediator.Send(data);
        }

        [HttpGet]
        public async Task<ActionResult<List<Material>>> GetMensajes()
        {
        return await this.Mediator.Send(new Consulta.Ejecuta());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Material>> GetMensaje(Guid Id)
        {
        return await this.Mediator.Send(new ConsultaById.Ejecuta { MaterialId = Id });
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Unit>> ModificarMensaje(Guid Id, Editar.Ejecuta data)
        {
        data.MaterialId = Id;
        return await this.Mediator.Send(data);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Unit>> Eliminar(Guid Id)
        {
        return await this.Mediator.Send(new Eliminar.Ejecuta { MaterialId = Id });
        } 
    }
}