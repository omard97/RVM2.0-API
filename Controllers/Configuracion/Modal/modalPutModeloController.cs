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

namespace ApiRVM2019.Controllers.Configuracion.Modal
{
    [EnableCors("All")]
    [Route("[controller]")]
    [ApiController]
    public class modalPutModeloController : ControllerBase
    {
        private readonly AppDbContext context;
        public modalPutModeloController(AppDbContext context)
        {
            this.context = context;
        }
        // GET: api/<modalPutModeloController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<modalPutModeloController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<modalPutModeloController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<modalPutModeloController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<ModeloVehiculo>> PutModelo(int id, [FromBody] ModeloVehiculo item)
        {
            if (item.IDModelo == id)
            {
                context.Entry(item).State = EntityState.Modified;
                context.SaveChanges();
            }
            else if (id != item.IDModelo)
            {
                return BadRequest();
            }

            var result = await context.ModeloVehiculo.FindAsync(id);

            if (result == null)
            {
                return NotFound();
            }

            return NoContent();
        }
    



            // DELETE api/<modalPutModeloController>/5
            [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
