using ApiRVM2019.Contexts;
using ApiRVM2019.Entities;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ApiRVM2019.Controllers.Configuracion.Modal
{
    [EnableCors("All")]
    [Route("[controller]")]
    [ApiController]
    public class modalUsuarioController : ControllerBase
    {
        //Utilizado para actualizar el estado del usuario seleccionado desde la configuracion

        private readonly AppDbContext dbContext;

        public modalUsuarioController(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }



        // GET: api/<modalUsuarioController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<modalUsuarioController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<modalUsuarioController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<modalUsuarioController>/5
        [HttpPut("{id}")]
       

        public async Task<ActionResult<Usuario>> actualizarUsuario(int id, [FromBody] Usuario item)
        {
            if (item.IDUsuario == id)
            {
                dbContext.Entry(item).State = EntityState.Modified;
                dbContext.SaveChanges();
            }
            else if (id != item.IDUsuario)
            {
                return BadRequest();
            }

            var result = await dbContext.Usuario.FindAsync(id);

            if (result == null)
            {
                return NotFound();
            }

            return NoContent();
        }

        // DELETE api/<modalUsuarioController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
