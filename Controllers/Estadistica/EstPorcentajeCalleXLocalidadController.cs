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
    public class EstPorcentajeCalleXLocalidadController : ControllerBase
    {
        private readonly AppDbContext context;

        public EstPorcentajeCalleXLocalidadController(AppDbContext context)
        {
            this.context = context;
        }

        // GET: api/<EstPorcentajeCalleXLocalidadController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<EstPorcentajeCalleXLocalidadController>/5
        [HttpGet("{idUsuario}/{idRol}")]
        public IActionResult GetDatos(int idUsuario, int idRol)
        {
            //https://localhost:44363/EstPorcentajeCalleXLocalidad/2
            //Utilizada para mostrar las tarjetas de las cantidades de reclamos dependiendo de cada localidad de cordoba
            if (idRol==1)
            {
                var _datos = (from vista in context.VE_ReclamosXLocalidad   
                             group vista by new { vista.IDLocalidad, vista.Localidad } into g
                             select new
                             {

                                 name = g.Key.Localidad,
                                 value = g.Sum(x=> x.Cantidad)

                             }).OrderByDescending(X => X.value);
                if (_datos == null)
                {
                    return NotFound();
                }
                return Ok(_datos);
            }
            else
            {
                var _datos = from VE_ReclamosXLocalidadesController in context.VE_ReclamosXLocalidad
                             where VE_ReclamosXLocalidadesController.IDUsuario == idUsuario
                             select new
                             {

                                 name = VE_ReclamosXLocalidadesController.Localidad,
                                 value = VE_ReclamosXLocalidadesController.Cantidad,

                             };
                if (_datos == null)
                {
                    return NotFound();
                }
                return Ok(_datos);
            }
            
        }

        // POST api/<EstPorcentajeCalleXLocalidadController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<EstPorcentajeCalleXLocalidadController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<EstPorcentajeCalleXLocalidadController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
