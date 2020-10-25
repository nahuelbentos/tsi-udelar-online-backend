
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using Models;
using System;
using Business.Usuarios;
using Microsoft.AspNetCore.Authorization;

namespace WebAPI.Controllers
{

  [AllowAnonymous]
  [Route("api/[controller]")]
  [ApiController]
  public class UsuarioController : MiControllerBase
  {

    [HttpPost]

    public async Task<ActionResult<Unit>> AltaUsuario(Nuevo.Ejecuta data)
    {
      return await this.Mediator.Send(data);
    }

    [HttpGet]
    public async Task<ActionResult<List<Usuario>>> GetUsuarios()
    {
      return await this.Mediator.Send(new Consulta.Ejecuta());
    }

    [HttpGet("id/{id}")]
    public async Task<ActionResult<Usuario>> GetUsuarioById(string id)
    {
      return await this.Mediator.Send(new ConsultaById.Ejecuta { Id = id });
    }

    [HttpGet("email/{email}")]
    public async Task<ActionResult<Usuario>> GetUsuarioByEmail(string email)
    {
      return await this.Mediator.Send(new ConsultaByEmail.Ejecuta { Email = email });
    }

    [HttpPut("{email}")]
    public async Task<ActionResult<Unit>> ModificarUsuario(string email, Editar.Ejecuta data)
    {
      data.Email = email;
      return await this.Mediator.Send(data);
    }

    [HttpDelete("{email}")]
    public async Task<ActionResult<Unit>> Eliminar(string email)
    {
      return await this.Mediator.Send(new Eliminar.Ejecuta { Email = email });
    }
  }

}