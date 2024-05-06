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
    public class VE_ReclamosXLocalidadesController : ControllerBase
    {
        private readonly AppDbContext context;

        public VE_ReclamosXLocalidadesController (AppDbContext context)
        {
            this.context = context;
        }

        // GET: api/<VE_ReclamosXLocalidadesController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<VE_ReclamosXLocalidadesController>/5
        [HttpGet("{idUsuario}")]
        public IActionResult Get(int idUsuario) 
        {
            
            
                //va a ser el usuario comun
                //Crear otro controlador para el administrador
                //https://localhost:44363/VE_ReclamosXLocalidades/2
                //Utilizada para mostrar las tarjetas de las cantidades de reclamos dependiendo de cada localidad de cordoba
                var _datos = from VE_ReclamosXLocalidadesController in context.VE_ReclamosXLocalidad
                             where VE_ReclamosXLocalidadesController.IDUsuario == idUsuario
                             select new
                             {
                                 IDLocalidad = VE_ReclamosXLocalidadesController.IDLocalidad,
                                 Localidad = VE_ReclamosXLocalidadesController.Localidad,
                                 Cantidad = VE_ReclamosXLocalidadesController.Cantidad,
                                 IDusuario = VE_ReclamosXLocalidadesController.IDUsuario
                             };
                if (_datos == null)
                {
                    return NotFound();
                }
                return Ok(_datos);
            
            
        }

        // POST api/<VE_ReclamosXLocalidadesController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<VE_ReclamosXLocalidadesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<VE_ReclamosXLocalidadesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
