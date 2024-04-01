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
    public class VeReclamosLocalidadXCalleController : ControllerBase
    {
        private readonly AppDbContext context;

        public VeReclamosLocalidadXCalleController(AppDbContext context)
        {
            this.context = context;
        }
        // GET: api/<VeReclamosLocalidadXCalleController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }
        //https://localhost:44363/VeReclamosLocalidadXCalle/5
        //https://localhost:44363/VeReclamosLocalidadXCalle/1/2
        // GET api/<VeReclamosLocalidadXCalleController>/5
        [HttpGet("{idLocalidad}/{idUsuario}")]
        public IActionResult Get(int idLocalidad, int idUsuario)
        {
            var _datos = from VeReclamosLocalidadXCalleController in context.VE_ReclamosLocalidadXCalle
                         where VeReclamosLocalidadXCalleController.IDLocalidad == idLocalidad
                         && VeReclamosLocalidadXCalleController.IDUsuario == idUsuario
                         orderby VeReclamosLocalidadXCalleController.IDUsuario, VeReclamosLocalidadXCalleController.direccion
                         select new
                         {
                             direccion = VeReclamosLocalidadXCalleController.direccion,
                             altura = Convert.ToInt32(VeReclamosLocalidadXCalleController.altura),
                             cantidad = Convert.ToInt32(VeReclamosLocalidadXCalleController.Cantidad),

                         };
            if (_datos == null)
            {
                return NotFound();
            }
            return Ok(_datos);
        }



        // POST api/<VeReclamosLocalidadXCalleController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<VeReclamosLocalidadXCalleController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<VeReclamosLocalidadXCalleController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
