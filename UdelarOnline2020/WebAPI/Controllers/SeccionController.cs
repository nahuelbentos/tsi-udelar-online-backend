using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Business.Secciones;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace WebAPI.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class SeccionController : MiControllerBase
    {
        [HttpPost]
        public async Task<ActionResult<Unit>> AltaSeccion(Nuevo.Ejecuta data)
        {
            return await this.Mediator.Send(data);
        }

        [HttpGet]
        public async Task<ActionResult<List<Seccion>>> GetSecciones()
        {
            return await this.Mediator.Send(new Consulta.Ejecuta());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Seccion>> GetSeccion(Guid id)
        {
            return await this.Mediator.Send(new ConsultaById.Ejecuta { SeccionId = id });
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Unit>> ModificarSeccion(Guid Id, Editar.Ejecuta data)
        {
            data.SeccionId = Id;
            return await this.Mediator.Send(data);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Unit>> EliminarSeccion(Guid Id)
        {
            return await this.Mediator.Send(new Eliminar.Ejecuta { SeccionId = Id });
        }
        
    }
}