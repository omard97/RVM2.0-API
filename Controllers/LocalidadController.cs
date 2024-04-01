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

namespace ApiRVM2019.Controllers
{
    [EnableCors("All")]
    [Route("[controller]")]
    [ApiController]
    public class LocalidadController : ControllerBase
    {

        private readonly AppDbContext context;
        public LocalidadController(AppDbContext context)
        {
            this.context = context;
        }


        // GET: api/<LocalidadController>
        //Metodo utilizado en la pantalla de inicio de CONFIGURACION - Tabla LOCALIDADES
        [HttpGet]
        public IActionResult Get()
        {
            var _datoLocalidad = from Localidad in context.Localidad
                                 join Pais in context.Pais on Localidad.ID_Pais equals Pais.IDPais
                                 select new
                                 {
                                     idLocalidad = Localidad.IDLocalidad,
                                     Nombre = Localidad.Nombre,
                                     Provincia = Localidad.Provincia,
                                     pais = Pais.NombrePais,
                                     IDPais = Localidad.ID_Pais,
                                     ID_EstadoLocalidad = Localidad.ID_EstadoLocalidad
                                 };
            if (_datoLocalidad == null)
            {
                return NotFound();
            }

            return Ok(_datoLocalidad);

        }

        // GET api/<LocalidadController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<LocalidadController>
        [HttpPost]
        public ActionResult Post([FromBody] Entities.Localidades.Localidad Localid)
        {
            try
            {
                var local = context.Localidad.Add(Localid);
                context.SaveChanges();
                return Ok(Localid);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // PUT api/<LocalidadController>/5
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
            Localidad.ID_EstadoLocalidad = item.ID_EstadoLocalidad; // 2 = Inactivo / se dio de baja esta localidad
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
        // DELETE api/<LocalidadController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
