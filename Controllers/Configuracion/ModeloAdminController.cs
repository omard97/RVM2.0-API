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
    public class ModeloAdminController : ControllerBase
    {
        private readonly AppDbContext context;

        public ModeloAdminController (AppDbContext context)
        {
            this.context = context;
        }

        // GET: api/<ModeloAdminController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<ModeloAdminController>/5
        [HttpGet("{idModelo}")]
        public IActionResult getModelo(int idModelo)
        {
            var dato = (from ModeloVehiculo in context.ModeloVehiculo
                       where ModeloVehiculo.IDModelo == idModelo
                       select new
                       {
                           idModelo = ModeloVehiculo.IDModelo,
                           nombre = ModeloVehiculo.Nombre,
                       }).OrderByDescending(ID => ID.idModelo);

            if (dato == null)
            {
                return NotFound();
            }

            return Ok(dato);
        }

        // POST api/<ModeloAdminController>
        [HttpPost]
        public ActionResult PostModelo([FromBody] ModeloVehiculo objModelo)
        {
            try
            {
                var dato = context.ModeloVehiculo.Add(objModelo);
                context.SaveChanges();

                //reclamo.IDReclamo = recl.Entity.IDReclamo;

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // PUT api/<ModeloAdminController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ModeloAdminController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
