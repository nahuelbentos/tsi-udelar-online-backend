using  Business.Bedelias;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BedeliasController : MiControllerBase
    {
    
    [HttpPost("aprobar-inscripcion-evaluacion")]
    public async Task<ActionResult<Unit>> AprobarInscripcionEvaluacion(AprobacionInscripcionEvaluaciones.Ejecuta data) =>  await this.Mediator.Send(data);
    
    [HttpPost("aprobar-inscripcion-evaluacion")]
    public async Task<ActionResult<Unit>> AprobarInscripcionEvaluacion(AprobacionInscripcionEvaluaciones.Ejecuta data) =>  await this.Mediator.Send(data);
    }
}