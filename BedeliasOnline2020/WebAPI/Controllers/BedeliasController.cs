using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Business.Bedelias;


namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BedeliasController : MiControllerBase
    {
    
    [HttpPost("aprobar-inscripcion-evaluacion")]
    public async Task<ActionResult<bool>> AprobarInscripcionEvaluacion(AprobacionInscripcionEvaluaciones.Ejecuta data) =>  await this.Mediator.Send(data);
    
    [HttpPost("aprobar-inscripcion-curso")]
    public async Task<ActionResult<bool>> AprobarInscripcionCurso(AprobarInscripcionCurso.Ejecuta data) =>  await this.Mediator.Send(data);
    
    [HttpPost("cerrar-acta")]
    public async Task<ActionResult<bool>> CerrarActa(CerrarActa.Ejecuta data) =>  await this.Mediator.Send(data);


    }
}