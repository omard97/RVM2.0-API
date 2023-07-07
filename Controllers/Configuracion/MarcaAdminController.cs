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
    public class MarcaAdminController : ControllerBase
    {
        private readonly AppDbContext context;

        public MarcaAdminController(AppDbContext context)
        {
            this.context = context;
        }
        // GET: api/<MarcaAdminController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<MarcaAdminController>/5
        [HttpGet("{idMarca}")]
        public IActionResult getMarca(int idMarca)
        {
            var dato = from MarcaVehiculo in context.MarcaVehiculo
                       where MarcaVehiculo.IDMarca == idMarca
                       select new
                       {
                           idMarca = MarcaVehiculo.IDMarca,
                           nombre = MarcaVehiculo.Nombre,
                       };

            if (dato == null)
            {
                return NotFound();
            }

            return Ok(dato);
        }

        // POST api/<MarcaAdminController>
        [HttpPost]
        public ActionResult Post([FromBody] MarcaVehiculo marcaVehiculo)
        {
            try
            {
                context.MarcaVehiculo.Add(marcaVehiculo);
                context.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // PUT api/<MarcaAdminController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<MarcaAdminController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
