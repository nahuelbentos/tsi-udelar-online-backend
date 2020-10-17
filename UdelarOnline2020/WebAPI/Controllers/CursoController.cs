
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using Microsoft.Extensions.Logging;
using Business.Cursos;

namespace WebAPI.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class CursoController : MiControllerBase
  {
    private readonly ILogger<CursoController> logger;

    public CursoController(ILogger<CursoController> logger)
    {
      this.logger = logger;
    }

    [HttpPost]

    public async Task<ActionResult<Unit>> AltaCurso(Nuevo.Ejecuta data)
    {
      this.logger.LogInformation("La data al crear es, Nombre: " + data.Nombre);
      // this.logger.LogInformation("La data al crear es, Descripcion: " + data.Descripcion);
      // this.logger.LogInformation("La data al crear es, ModalidadCurso: " + data.ModalidadCurso);
      // this.logger.LogInformation("La data al crear es, TemplateCurso: " + data.TemplateCursoId);
      return await this.Mediator.Send(data);

    }

  }

}