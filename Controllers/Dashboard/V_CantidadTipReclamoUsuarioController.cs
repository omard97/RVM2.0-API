using ApiRVM2019.Contexts;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiRVM2019.Entities;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ApiRVM2019.Controllers.Dashboard
{
    [EnableCors("All")]
    [Route("[controller]")]
    [ApiController]
    public class V_CantidadTipReclamoUsuarioController : ControllerBase
    {
        private readonly AppDbContext context;

        public V_CantidadTipReclamoUsuarioController(AppDbContext context)
        {
            this.context = context;
        }

        // GET: api/<V_CantidadTipReclamoUsuarioController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<V_CantidadTipReclamoUsuarioController>/5
        [HttpGet("{idUsuario}")]
        public IActionResult GetReclamosUsuario(int idUsuario)
        {
            var data = from V_CantidadTipReclamoUsuarioController in context.V_CantidadTipReclamoUsuario
                       where V_CantidadTipReclamoUsuarioController.IDUsuario == idUsuario
                       select new
                       {
                           name = V_CantidadTipReclamoUsuarioController.Nombre,
                           value = V_CantidadTipReclamoUsuarioController.Cantidad
                       };

            if (data == null)
            {
                return NotFound();
            }
            return Ok(data);
        }

        // POST api/<V_CantidadTipReclamoUsuarioController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<V_CantidadTipReclamoUsuarioController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<V_CantidadTipReclamoUsuarioController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
