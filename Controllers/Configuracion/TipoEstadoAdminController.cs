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
    public class TipoEstadoAdminController : ControllerBase
    {
        private readonly AppDbContext context;

        public TipoEstadoAdminController(AppDbContext context)
        {
            this.context = context;
        }
        // GET: api/<TipoEstadoController>
        [HttpGet]
        public IActionResult GetTipoEstados()
        {
            
            
                var data = from TipoEstado in context.TipoEstado
                           select new
                           {
                               IdTipoEstado = TipoEstado.IDTipoEstado,
                               Nombre = TipoEstado.nombre
                           };
                if (data == null)
                {
                    return NotFound();
                }

                return Ok(data);
           
            
        }

        // GET api/<TipoEstadoController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<TipoEstadoController>
        [HttpPost]
        public ActionResult Post([FromBody] TipoEstado tipoEstado)
        {
            try
            {
                var TipEstado = context.TipoEstado.Add(tipoEstado);
                context.SaveChanges();

                //reclamo.IDReclamo = recl.Entity.IDReclamo;

                return Ok(TipEstado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // PUT api/<TipoEstadoController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<TipoEstadoController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
