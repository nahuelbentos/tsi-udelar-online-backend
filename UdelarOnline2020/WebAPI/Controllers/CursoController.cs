using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business;
using Models;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class CursoController : ControllerBase
  {
    private readonly CursoBusiness cb;

    public CursoController(CursoBusiness cursoBusiness)
    {
      this.cb = cursoBusiness;
    }


    // GET: api/Usuarios/5
    [HttpGet("{id}")]
    public Curso GetCurso(int id)
    {
      var curso = cb.GetCurso(id);

      if (curso == null)
      {
        return null;
      }

      return curso;

    }

  }
}
