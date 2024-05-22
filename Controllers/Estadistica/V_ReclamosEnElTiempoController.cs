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

namespace ApiRVM2019.Controllers.Estadistica
{
    [EnableCors("All")]
    [Route("[controller]")]
    [ApiController]
    public class V_ReclamosEnElTiempoController : ControllerBase
    {

        private readonly AppDbContext context;

        public V_ReclamosEnElTiempoController(AppDbContext context)
        {
            this.context = context;
        }

        // GET: api/<V_ReclamosEnElTiempoController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<V_ReclamosEnElTiempoController>/5
        [HttpGet("{idRol}/{idUsuario}/{mes}/{anio}")]
        public IActionResult GetreclamosTiempo(int idRol, int idUsuario, int mes, int anio)
        {
            if (idRol == 1)
            {
                var data = from vista in context.V_ReclamosEnElTiempo
                           where vista.Anio <= anio
                           group vista by new { vista.Hora, vista.TipoHora } into g
                           select new
                           {
                               name = g.Key.Hora,
                               value = g.Sum(x => x.CantidadReclamo),
                           };
                if (data == null)
                {
                    return NotFound();
                }
                return Ok(data);
            }
            else
            {
                if (idRol == 3)
                {
                    var data = from vista in context.V_ReclamosEnElTiempo
                               where vista.idUsuario == idUsuario && vista.Anio <= anio
                               group vista by new { vista.Hora, vista.TipoHora} into g
                               select new
                               {
                                   name = g.Key.Hora,
                                   value = g.Sum( x => x.CantidadReclamo),
                               };
                    if (data == null)
                    {
                        return NotFound();
                    }
                    return Ok(data);
                }

                return NotFound();
            }
        }

        // POST api/<V_ReclamosEnElTiempoController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<V_ReclamosEnElTiempoController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<V_ReclamosEnElTiempoController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
