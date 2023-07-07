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

namespace ApiRVM2019.Controllers.Estadistica
{
    [EnableCors("All")]
    [Route("[controller]")]
    [ApiController]
    public class CantidadXEstadosController : ControllerBase
    {
        private readonly AppDbContext context;

        public CantidadXEstadosController(AppDbContext context)
        {
            this.context = context;
        }

        // GET: api/<CantidadXEstadosController>
        [HttpGet]
        public IActionResult Get()
        {
            var data = from V_CantidadxEstado in context.V_CantidadxEstado
                       select new
            {
                           name ="Reclamos: "+ V_CantidadxEstado.Nombre,
                           value = V_CantidadxEstado.Cantidad
            };    
            if (data == null)
            {
                return NotFound();
            }
            return Ok(data);         
        }

        // GET api/<CantidadXEstadosController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<CantidadXEstadosController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<CantidadXEstadosController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<CantidadXEstadosController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
