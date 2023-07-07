using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ApiRVM2019.Contexts;
using ApiRVM2019.Entities;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ApiRVM2019.Controllers.Menu
{
    [EnableCors("All")]
    [Route("[controller]")]
    [ApiController]
    public class V_rolUsuario : ControllerBase
    {
        private readonly AppDbContext context;

        public V_rolUsuario(AppDbContext context)
        {
            this.context = context;
        }

        // GET: api/<V_rolUsuarioController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<V_rolUsuarioController>/5
        [HttpGet("{idUsuario}")]
        public IActionResult GetRolUsuario(int idUsuario)
        {
            var data = from V_rolUsuario in context.V_rolUsuario
                       where V_rolUsuario.IDUsuario==idUsuario
                       select new
                       {
                           idUsuario= V_rolUsuario.IDUsuario,
                           nick = V_rolUsuario.Nick,
                            idRol= V_rolUsuario.IDPerfil,
                            rol = V_rolUsuario.Rol,
                            foto = V_rolUsuario.Foto
                       };
            if (data == null)
            {
                return NotFound();
            }
            return Ok(data);
        }

        // POST api/<V_rolUsuarioController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<V_rolUsuarioController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<V_rolUsuarioController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
