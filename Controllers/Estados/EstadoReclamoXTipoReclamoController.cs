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
    public class EstadoReclamoXTipoReclamoController : ControllerBase
    {
        private readonly AppDbContext context;

        public EstadoReclamoXTipoReclamoController(AppDbContext context)
        {
            this.context = context;
        }
        // GET: api/<EstadoReclamoXTipoReclamoController>
        [HttpGet]
        //https://localhost:44363/EstadoReclamoXTipoReclamo?nombreTipoReclamo=Seguridad
        public IActionResult GetEstados(string nombreTipoReclamo)
        {
            var _estado = from Estado in context.Estado
                          join TipoEstado in context.TipoEstado on Estado.ID_TipoEstado equals TipoEstado.IDTipoEstado
                          where TipoEstado.nombre.Equals(nombreTipoReclamo) && (Estado.IDEstado != 14 && Estado.IDEstado != 13)
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

            // GET api/<EstadoReclamoXTipoReclamoController>/5
            [HttpGet("{nombreTipoReclamo}")]
        public IActionResult EstadoReclamo(string nombreTipoReclamo)
        {
            var _estado = from Estado in context.Estado
                          join TipoEstado in context.TipoEstado on Estado.ID_TipoEstado equals TipoEstado.IDTipoEstado
                          where TipoEstado.nombre.Equals(nombreTipoReclamo) && (Estado.IDEstado != 14 && Estado.IDEstado != 13)
                          select new
                          {
                              idEstado = Estado.IDEstado,
                              estadoNombre = Estado.Nombre,
                              idTipoEstado = TipoEstado.IDTipoEstado,
                              tipoEstadoNombre = TipoEstado.nombre,

                          };
            if (_estado.Count() == 0)
            {
                
                return NotFound();
            }

            return Ok(_estado);
        }

        // POST api/<EstadoReclamoXTipoReclamoController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<EstadoReclamoXTipoReclamoController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<EstadoReclamoXTipoReclamoController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
