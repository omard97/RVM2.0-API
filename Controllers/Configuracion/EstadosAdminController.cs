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
    public class EstadosAdminController : ControllerBase
    {
        private readonly AppDbContext context;

        public EstadosAdminController(AppDbContext context)
        {
            this.context = context;
        }
        // GET: api/<EstadosAdminController>
        [HttpGet]
        public IActionResult GetEstados(int idTipoEstado)
        {
            var data = from Estado in context.Estado
                       join TipoEstado in context.TipoEstado on Estado.ID_TipoEstado equals TipoEstado.IDTipoEstado
                       where Estado.ID_TipoEstado == idTipoEstado
                       select new
                       {
                           idEstado = Estado.IDEstado,
                           nombre = Estado.Nombre,
                           idTipoEstado = Estado.ID_TipoEstado,
                           nombreTipoEstado = TipoEstado.nombre

                       };
            if (data == null)
            {
                return NotFound();
            }

            return Ok(data);
        }

        // GET api/<EstadosAdminController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var _estado = from Estado in context.Estado
                          join TipoEstado in context.TipoEstado on Estado.ID_TipoEstado equals TipoEstado.IDTipoEstado
                          where TipoEstado.IDTipoEstado == id && (Estado.IDEstado != 14 && Estado.IDEstado != 13)
                          select new
                          {
                              idEstado = Estado.IDEstado,
                              estadoNombre = Estado.Nombre,
                              idTipoEstado = TipoEstado.IDTipoEstado,
                              tipoEstadoNombre = TipoEstado.nombre,

                          };
            if (_estado.Count() == 0)
            {
                var mensajeError = "No se encontró ningún estado de reclamo";
                return NotFound(mensajeError);
            }

            return Ok(_estado);
        }

        // POST api/<EstadosAdminController>
        [HttpPost]
        public ActionResult PostEstado([FromBody] Estado objEstado)
        {
            Estado est = new Estado();

            string [] listaEstados = new string[] { "Pendiente", "En Revisión", "Solucionado", "Descartado" };
            try
            {
                
                for (int i = 0; i < 4; i++)
                {
                    objEstado.IDEstado = 0;
                    objEstado.Nombre = listaEstados[i];
                    var PEstado = context.Estado.Add(objEstado);
                    context.SaveChanges();
                }

                //reclamo.IDReclamo = recl.Entity.IDReclamo;

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // PUT api/<EstadosAdminController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<EstadosAdminController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
