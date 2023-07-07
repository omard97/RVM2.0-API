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

namespace ApiRVM2019.Controllers.Configuracion
{
    [EnableCors("All")]
    [Route("[controller]")]
    [ApiController]
    public class TipoReclamoAdminController : ControllerBase
    {
        private readonly AppDbContext context;

        public TipoReclamoAdminController(AppDbContext context)
        {
            this.context = context;
        }

        // GET: api/<TipoReclamoAdminController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<TipoReclamoAdminController>/5
        [HttpGet("{idTipoReclamo}")]
        public IActionResult GetTipoR(int idTipoReclamo)
        {
            var data = from TipoReclamo in context.TipoReclamo
                       where TipoReclamo.IDTipoReclamo == idTipoReclamo
                       select new
                       {
                           idTipoR = TipoReclamo.IDTipoReclamo,
                           nombre = TipoReclamo.Nombre,
                           descripcion = TipoReclamo.Descripcion,
                       };
            if (data == null)
            {
                return NotFound();
            }

            return Ok(data);
        }

        // POST api/<TipoReclamoAdminController>
        [HttpPost]
        public ActionResult PostTipoReclamo([FromBody] TipoReclamo objTipoReclamo)
        {
            try
            {
                var tipReclamo = context.TipoReclamo.Add(objTipoReclamo);
                context.SaveChanges();

                //reclamo.IDReclamo = recl.Entity.IDReclamo;

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // PUT api/<TipoReclamoAdminController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<TipoReclamoAdminController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
