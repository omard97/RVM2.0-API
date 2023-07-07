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
    public class V_CantidadxEstadoUsuario : ControllerBase
    {
        private readonly AppDbContext context;

        public V_CantidadxEstadoUsuario(AppDbContext context)
        {
            this.context = context;
        }

        // GET: api/<V_CantidadxEstadoUsuario>
        [HttpGet]
        public IActionResult Get( int idUsuario)
        {
            var data = from V_CantidadxEstadoUsuario in context.V_CantidadxEstadoUsuario
                       where V_CantidadxEstadoUsuario.IDusuario==idUsuario
                       select new
                       {
                           name = "Reclamos: "+ V_CantidadxEstadoUsuario.Nombre,
                           value = V_CantidadxEstadoUsuario.Cantidad
                       };
            if (data == null)
            {
                return NotFound();
            }
            return Ok(data);
        }

        // GET api/<V_CantidadxEstadoUsuario>/5
        [HttpGet("{id}")]
        public string GetDato(int id)
        {
            return "value";
        }

        // POST api/<V_CantidadxEstadoUsuario>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<V_CantidadxEstadoUsuario>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<V_CantidadxEstadoUsuario>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
