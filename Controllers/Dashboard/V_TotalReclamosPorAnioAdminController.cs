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
    public class V_TotalReclamosPorAnioAdminController : ControllerBase
    {
        private readonly AppDbContext context;

        public V_TotalReclamosPorAnioAdminController(AppDbContext context)
        {
            this.context = context;
        }
        // GET: api/<V_TotalReclamosPorAnioAdminController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<V_TotalReclamosPorAnioAdminController>/5
        [HttpGet("{idRol}/{anio}")]
        public IActionResult Get( int idRol, string anio)
        {
            var data = from V_TotalReclamosPorAnioAdminController in context.V_TotalReclamosPorAnioAdmin
                       where V_TotalReclamosPorAnioAdminController.Anio== anio
                       select new
                       {
                           name = V_TotalReclamosPorAnioAdminController.Mes,
                           value = V_TotalReclamosPorAnioAdminController.Cantidad
                       };

            if (data == null)
            {
                return NotFound();
            }
            return Ok(data);
        }

        // POST api/<V_TotalReclamosPorAnioAdminController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<V_TotalReclamosPorAnioAdminController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<V_TotalReclamosPorAnioAdminController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
