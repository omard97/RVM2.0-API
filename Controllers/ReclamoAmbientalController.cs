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

namespace ApiRVM2019.Controllers
{
    [EnableCors("All")]
    [Route("[controller]")]
    [ApiController]
    public class ReclamoAmbientalController : ControllerBase
    {
        private readonly AppDbContext context;

        public ReclamoAmbientalController(AppDbContext context)
        {
            this.context = context;
        }
        // GET: api/<ReclamoAmbientalController>
        [HttpGet]
        public IActionResult Get()
        {
            //Agregar a la consulta la parte del inner join cuando este lista la parte de Perfil y Estado
            var _ReclamoAmbiental = from ReclamoAmbiental in context.ReclamoAmbiental
                                    where ReclamoAmbiental.IDReclamoAmbiental !=12
                           select new
                           {
                               IDReclamoAmbiental = ReclamoAmbiental.IDReclamoAmbiental,
                               NombreRecAmbiental = ReclamoAmbiental.Nombre
                           };

            if (_ReclamoAmbiental == null)
            {
                return NotFound();
            }
            return Ok(_ReclamoAmbiental);
        }

        // GET api/<ReclamoAmbientalController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ReclamoAmbiental>> GetReclamoAmbiental(int id)
        {
            var _ReclamoAmbiental = await context.ReclamoAmbiental.FindAsync(id);

            if (_ReclamoAmbiental == null)
            {
                return NotFound();
            }

            return _ReclamoAmbiental;
        }

        // POST api/<ReclamoAmbientalController>
        [HttpPost]
        public ActionResult Post([FromBody] ReclamoAmbiental recAmbiental)
        {
            try
            {
                context.ReclamoAmbiental.Add(recAmbiental);
                context.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        // PUT api/<ReclamoAmbientalController>/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] ReclamoAmbiental reclAmbiental)
        {
            if (reclAmbiental.IDReclamoAmbiental == id)
            {
                context.Entry(reclAmbiental).State = EntityState.Modified;
                context.SaveChanges();
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        // DELETE api/<ReclamoAmbientalController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
