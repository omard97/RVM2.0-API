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

namespace ApiRVM2019.Controllers.Estados
{
    [EnableCors("All")]
    [Route("[controller]")]
    [ApiController]
    public class v_estadoPendienteReclamoController : ControllerBase
    {
        private readonly AppDbContext context;

        public v_estadoPendienteReclamoController(AppDbContext context)
        {
            this.context = context;
        }

        // GET: api/<v_estadoPendienteReclamoController>
        [HttpGet]
        public IActionResult GetEstados()
        {
            //se iba a utilizar para la creacion de los reclamos y que siempre seleccione el pendiente dependiendo del estado seleccionado
            //pero no se implmento porque a la hora de actualizar o dar de baja iba a ser mas complicado de resolver o tardaria mucho tiempo
            //por ahora se deja sin usar, a lo mejor en el futuro se implementa
            var estados = from v_estadoPendienteReclamoController in context.V_EstadoPendiente
                          select new
                          {
                              idEstado = v_estadoPendienteReclamoController.IDEstado,
                              nombreEstado = v_estadoPendienteReclamoController.Nombre,
                              id_tipoEstado = v_estadoPendienteReclamoController.ID_TipoEstado,
                              idTipoEstado = v_estadoPendienteReclamoController.IDTipoEstado,
                              nombreTipoEstado = v_estadoPendienteReclamoController.nombreTipoEstado,
                          };

            if (estados == null)
            {
                return NotFound();
            }
            return Ok(estados);

        }

        // GET api/<v_estadoPendienteReclamoController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<v_estadoPendienteReclamoController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<v_estadoPendienteReclamoController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<v_estadoPendienteReclamoController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
