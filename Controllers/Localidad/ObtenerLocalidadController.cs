using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ApiRVM2019.Contexts;
using ApiRVM2019.Entities;
using Microsoft.AspNetCore.Cors;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ApiRVM2019.Controllers.Localidad
{
    [EnableCors("All")]
    [Route("[controller]")]
    [ApiController]
    public class ObtenerLocalidadController : ControllerBase
    {
        public readonly AppDbContext contextDB;
        public ObtenerLocalidadController(AppDbContext context)
        {
            this.contextDB = context;
        }
        // GET: api/<ObtenerLocalidadController>
        [HttpGet]
        public IActionResult GetLocalidad(string nombreLoc)
        {
            //https://localhost:44363/obtenerLocalidad?nombreLoc=Laborde
            var _datoLocalidad = from Localidad in contextDB.Localidad
                                 where Localidad.Nombre == nombreLoc
                                 select new
                                 {
                                     idLocalidad = Localidad.IDLocalidad,
                                     Nombre = Localidad.Nombre,
                                     Provincia = Localidad.Provincia
                                     //pais = Pais.NombrePais,
                                     //IDPais = Localidad.ID_Pais,
                                     //ID_EstadoLocalidad = Localidad.ID_EstadoLocalidad
                                 };
            if (_datoLocalidad == null)
            {
                return NotFound();
            }

            return Ok(_datoLocalidad);
        }


            // GET api/<ObtenerLocalidadController>/5
            [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ObtenerLocalidadController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ObtenerLocalidadController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ObtenerLocalidadController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
