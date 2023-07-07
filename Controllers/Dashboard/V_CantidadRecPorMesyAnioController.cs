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

namespace ApiRVM2019.Controllers.Dashboard
{
    [EnableCors("All")]
    [Route("[controller]")]
    [ApiController]
    public class V_CantidadRecPorMesyAnioController : ControllerBase
    {
        private readonly AppDbContext context;

        public V_CantidadRecPorMesyAnioController(AppDbContext context)
        {
            this.context = context;
        }
        // GET: api/<V_CantidadRecPorMesyAnioController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<V_CantidadRecPorMesyAnioController>/5
        [HttpGet("{idUsuario}/{anio}")]
        public IActionResult GetCantidadReclamos(int idUsuario,string anio)
        {
            var data = from V_CantidadRecPorMesyAnioController in context.V_CantidadRecPorMesyAnio
                       where V_CantidadRecPorMesyAnioController.IDUsuario == idUsuario &&
                       V_CantidadRecPorMesyAnioController.Anio==anio
                       select new
                       {
                           name = V_CantidadRecPorMesyAnioController.Mes,
                           value = V_CantidadRecPorMesyAnioController.Cantidad
                       };
            if (data == null)
            {
                return NotFound();
            }
            return Ok(data);
        }

        // POST api/<V_CantidadRecPorMesyAnioController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<V_CantidadRecPorMesyAnioController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<V_CantidadRecPorMesyAnioController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
