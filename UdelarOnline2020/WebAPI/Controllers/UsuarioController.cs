
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using Models;
using System;
using Business.Usuarios;
using Microsoft.AspNetCore.Authorization;
using Business.Datatypes;

namespace WebAPI.Controllers
{

  
  [Route("api/[controller]")]
  [ApiController]
  public class UsuarioController : MiControllerBase
  {

    [HttpPost]
    public async Task<ActionResult<Unit>> AltaUsuario(Nuevo.Ejecuta data) => await this.Mediator.Send(data);
  

    [HttpGet]
    public async Task<ActionResult<List<DtUsuario>>> GetUsuarios() => await this.Mediator.Send(new Consulta.Ejecuta());
  

    [HttpGet("id/{id}")]
    public async Task<ActionResult<DtUsuario>> GetUsuarioById(string id) => await this.Mediator.Send(new ConsultaById.Ejecuta { Id = id });
  

    [HttpGet("email/{email}")]
    public async Task<ActionResult<Usuario>> GetUsuarioByEmail(string email) => await this.Mediator.Send(new ConsultaByEmail.Ejecuta { Email = email });


    [HttpPut("{email}")]
    public async Task<ActionResult<Unit>> ModificarUsuario(string email, Editar.Ejecuta data) => await this.Mediator.Send(Editar.GetData(email, data));
  

    [HttpDelete("{email}")]
    public async Task<ActionResult<Unit>> Eliminar(string email) => await this.Mediator.Send(new Eliminar.Ejecuta { Email = email });
  

    [HttpPost("rol")]
    public async Task<ActionResult<Unit>> AgregarRol(AgregarRol.Ejecuta data) => await this.Mediator.Send(data);

    [HttpPatch("rol")]
    public async Task<ActionResult<Unit>> EliminarRol(QuitarRol.Ejecuta data) => await this.Mediator.Send(data);

    [HttpPost("tiene-role")]
    public async Task<ActionResult<Boolean>> TieneRole(TieneRole.Ejecuta data) => await Mediator.Send(data);
  }

}