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

namespace ApiRVM2019.Controllers.Dashboard
{
    [EnableCors("All")]
    [Route("[controller]")]
    [ApiController]
    public class V_CantidadRecAmbientalUsuarioController : ControllerBase
    {
        private readonly AppDbContext context;

        public V_CantidadRecAmbientalUsuarioController(AppDbContext context)
        {
            this.context = context;
        }
            
        // GET: api/<V_CantidadRecAmbientalUsuarioController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<V_CantidadRecAmbientalUsuarioController>/4
        [HttpGet("{idUsuario}")]
        public IActionResult GetReclamosUsuario(int idUsuario)
        {

            var data = from V_CantidadRecAmbientalUsuarioController in context.V_CantidadRecAmbientalUsuario
                       where V_CantidadRecAmbientalUsuarioController.ID_ReclamoAmbiental != 12 &&
                       V_CantidadRecAmbientalUsuarioController.IDusuario == idUsuario
                       select new
                       {
                           name = V_CantidadRecAmbientalUsuarioController.Nombre,
                           value = V_CantidadRecAmbientalUsuarioController.Cantidad
                       };
            if (data == null)
            {
                return NotFound();
            }
            return Ok(data);
        }

        // POST api/<V_CantidadRecAmbientalUsuarioController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<V_CantidadRecAmbientalUsuarioController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<V_CantidadRecAmbientalUsuarioController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
