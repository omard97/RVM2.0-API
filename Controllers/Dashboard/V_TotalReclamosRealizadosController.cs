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

namespace ApiRVM2019.Controllers.Dashboard
{
    [EnableCors("All")]
    [Route("[controller]")]
    [ApiController]
    public class V_TotalReclamosRealizados : ControllerBase
    {

        private readonly AppDbContext context;

        public V_TotalReclamosRealizados(AppDbContext context)
        {
            this.context = context;
        }

        // GET: api/<V_TotalReclamosRealizadosController>
        [HttpGet]
        public IActionResult Get()
        {
            //no se esta utilizando
            var data = from V_TotalReclamosRealizados in context.V_TotalReclamosRealizados
                       
                       select V_TotalReclamosRealizados;

            if (data == null)
            {
                return NotFound();
            }
            return Ok(data);
        }

        // GET api/<V_TotalReclamosRealizadosController>/5
        [HttpGet("{idUsuario}/{idRol}")]
        public IActionResult Get(int idUsuario,int idRol)
        {           
                var data = from V_TotalReclamosRealizados in context.V_TotalReclamosRealizados
                           where V_TotalReclamosRealizados.IDUsuario == idUsuario
                           select new
                           {
                               name = "Cantidad Total de Reclamos",
                               value = V_TotalReclamosRealizados.Cantidad
                           };

                if (data == null)
                {
                    return NotFound();
                }
                return Ok(data);
            
           
        }

        // POST api/<V_TotalReclamosRealizadosController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<V_TotalReclamosRealizadosController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<V_TotalReclamosRealizadosController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
