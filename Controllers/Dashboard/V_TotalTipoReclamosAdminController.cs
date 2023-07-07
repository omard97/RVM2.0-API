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
    public class V_TotalTipoReclamosAdminController : ControllerBase
    {
        private readonly AppDbContext context;

        public V_TotalTipoReclamosAdminController (AppDbContext context)
        {
            this.context = context;
        }
        // GET: api/<V_TotalTipoReclamosAdminController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<V_TotalTipoReclamosAdminController>/5
        [HttpGet("{idRol}")]
        public IActionResult Get(int idUsuario, int idRol)
        {
            var data = from V_TotalTipoReclamosAdminController in context.V_TotalTipoReclamosAdmin
                       select new
                       {
                           name = V_TotalTipoReclamosAdminController.Nombre,
                           value = V_TotalTipoReclamosAdminController.Cantidad
                       };

            if (data == null)
            {
                return NotFound();
            }
            return Ok(data);
        }

        // POST api/<V_TotalTipoReclamosAdminController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<V_TotalTipoReclamosAdminController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<V_TotalTipoReclamosAdminController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
