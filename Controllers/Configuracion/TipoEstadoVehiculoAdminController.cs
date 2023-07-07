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
    public class TipoEstadoVehiculoAdminController : ControllerBase
    {
        private readonly AppDbContext context;

        public TipoEstadoVehiculoAdminController(AppDbContext context)
        {
            this.context = context;
        }
        // GET: api/<TipoEstadoVehiculoAdminController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<TipoEstadoVehiculoAdminController>/5
        [HttpGet("{idTipoEstado}")]
        public IActionResult getEstadosVehicular(int idTipoEstado)
        {//lo estaba para usar el modal de nuevo vehiculo pero no, despues se dara otro uso.
            var dato = from Estado in context.Estado
                       join TipoEstado in context.TipoEstado on Estado.ID_TipoEstado equals TipoEstado.IDTipoEstado
                       where TipoEstado.IDTipoEstado == idTipoEstado
                       select new
                       {
                           idEstado = Estado.IDEstado,
                           nombreEstado = Estado.Nombre,
                           id_TipoEstado = Estado.ID_TipoEstado,
                           nombreTipoEstado = TipoEstado.nombre,
                       };
            if (dato == null)
            {
                return NotFound();
            }

            return Ok(dato);
        }

        // POST api/<TipoEstadoVehiculoAdminController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<TipoEstadoVehiculoAdminController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<TipoEstadoVehiculoAdminController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
