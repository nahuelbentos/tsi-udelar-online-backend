using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CursoController : ControllerBase
    {
        private readonly CursoBusiness cb;

        public CursoController()
        {
            cb = new CursoBusiness();
        }


        // GET: api/Usuarios/5
        [HttpGet("{id}")]
        public Curso GetCurso(int id)
        {
            var curso =  cb.GetCurso(id);

            if (curso == null)
            {
                return null;
            }

            return curso;
        }

        //public async Task<ActionResult<Curso>> GetCurso(int id)
        //{
        //    var curso = await cb.GetCurso(id);;

        //    if (curso == null)
        //    {
        //        return NotFound();
        //    }

        //    return curso;
        //}
    }

}
