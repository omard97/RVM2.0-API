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

namespace ApiRVM2019.Controllers.Estadistica
{
    [EnableCors("All")]
    [Route("[controller]")]
    [ApiController]
    public class VE_CallesXlocalidad2Controller : ControllerBase
    {
        private readonly AppDbContext context;
        public VE_CallesXlocalidad2Controller(AppDbContext context)
        {
            this.context = context;
        }
        // GET: api/<VE_CallesXlocalidad2Controller>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<VE_CallesXlocalidad2Controller>/5
        [HttpGet("{idUsuario}/{idLocalidad}/{idRol}")]
        public IActionResult GetDatos(int idUsuario, int idLocalidad, int idRol)
        {
            //https://localhost:44363/VE_CallesXlocalidad2/2/1
            //uUtilizado para rellenar el segundo grafico de torta
            // visualiza todas las calles de esa localidad y saca un porsentaje de reclamos realizados en esa calle y localidad
            if (idRol==1)
            {
                var _datos = (from grafico2 in context.VE_CallesXlocalidad2
                             where grafico2.IDLocalidad == idLocalidad
                             group grafico2 by new {grafico2.direccion, grafico2.Cantidad} into d
                             select new
                             {

                                 name = d.Key.direccion,
                                 value = d.Sum(x => x.Cantidad)

                             }).OrderByDescending(x => x.value);
                if (_datos == null)
                {
                    return NotFound();
                }
                return Ok(_datos);
            }
            else
            {
                var _datos = (from grafico2 in context.VE_CallesXlocalidad2
                             where grafico2.IDUsuario == idUsuario && grafico2.IDLocalidad == idLocalidad
                             group grafico2 by new { grafico2.direccion, grafico2.Cantidad } into d
                             select new
                             {

                                 name = d.Key.direccion,
                                 value = d.Sum(x => x.Cantidad)

                             }).OrderByDescending(x => x.value);
                if (_datos == null)
                {
                    return NotFound();
                }
                return Ok(_datos);
            }
            
        }

        // POST api/<VE_CallesXlocalidad2Controller>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<VE_CallesXlocalidad2Controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<VE_CallesXlocalidad2Controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
