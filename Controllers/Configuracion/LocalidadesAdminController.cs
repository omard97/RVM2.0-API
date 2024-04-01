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

namespace ApiRVM2019.Controllers.Configuracion
{
    [EnableCors("All")]
    [Route("[controller]")]
    [ApiController]
    public class LocalidadesAdminController : ControllerBase
    {
        private readonly AppDbContext context;
        public LocalidadesAdminController(AppDbContext context)
        {
            this.context = context;
        }
        // GET: api/<LocalidadesAdminController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<LocalidadesAdminController>/5
        [HttpGet("{nombreLocalidad}")]
        public IActionResult getConfigLocalidades(string nombreLocalidad)
        {
            var _datoLocalidad = from Localidad in context.Localidad
                                 join Pais in context.Pais on Localidad.ID_Pais equals Pais.IDPais
                                 where Localidad.Nombre.Contains(nombreLocalidad)
                                 select new
                                 {
                                     idLocalidad = Localidad.IDLocalidad,
                                     Localidad = Localidad.Nombre,
                                     pais = Pais.NombrePais,
                                     IDPais = Localidad.ID_Pais,
                                 };
            if (_datoLocalidad == null)
            {
                return NotFound();
            }

            return Ok(_datoLocalidad);
        }

        // POST api/<LocalidadesAdminController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<LocalidadesAdminController>/5
        //utilizado para actualizar el nombre de la localidad en la pantalla de configuracion admin
        [HttpPut("{idLocalidad}")]
        public async Task<ActionResult<Entities.Localidades.Localidad>> actualizarLocalidad(int idLocalidad, [FromBody] Entities.Localidades.Localidad item)
        {
            if (idLocalidad != item.IDLocalidad)
            {
                return BadRequest();
            }

            var Localidad = await this.context.Localidad.FindAsync(idLocalidad);

            if (Localidad == null)
            {
                return NotFound();
            }

            // Actualizar solo los campos que han cambiado
            Localidad.Nombre = item.Nombre; 
            // Repite este bloque para cada campo que pueda ser actualizado

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LocalidadExists(idLocalidad))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();

        }
        private bool LocalidadExists(int idLoc)
        {
            return context.Localidad.Any(e => e.IDLocalidad == idLoc);
        }

        // DELETE api/<LocalidadesAdminController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
