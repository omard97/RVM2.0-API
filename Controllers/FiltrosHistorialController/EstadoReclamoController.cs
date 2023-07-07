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

namespace ApiRVM2019.Controllers.FiltrosHistorialController
{
    [EnableCors("All")]
    [Route("[controller]")]
    [ApiController]
    public class EstadoReclamoController : ControllerBase
    {
        private readonly AppDbContext context;

        public EstadoReclamoController(AppDbContext context)
        {
            this.context = context;
        }
        // GET: api/<EstadoReclamoController>
        [HttpGet]
        
        //public IActionResult GetEReclamo(int id)
        //{
        //    var _EstadoRec = (from estado in context.Estado
        //                      join TipoEstado in context.TipoEstado on estado.ID_TipoEstado equals TipoEstado.IDTipoEstado
        //                      where TipoEstado.IDTipoEstado == id
        //                      select new
        //                      {
        //                          IDEstado = estado.IDEstado,
        //                          Nombre = estado.Nombre,
        //                          ID_TipoEstado = estado.ID_TipoEstado
        //                      });



        //    if (_EstadoRec == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(_EstadoRec);
        //}

        // GET api/<EstadoReclamoController>/5
        [HttpGet("{id}")]
        public IActionResult Estadoreclamo(int id)
        {
            var _EstadoRec = (from estado in context.Estado
                              join TipoEstado in context.TipoEstado on estado.ID_TipoEstado equals TipoEstado.IDTipoEstado
                              where TipoEstado.IDTipoEstado == id
                              select new
                              {
                                  IDEstado = estado.IDEstado,
                                  Nombre = estado.Nombre,
                                  ID_TipoEstado = estado.ID_TipoEstado
                              });



            if (_EstadoRec == null)
            {
                return NotFound();
            }

            return Ok(_EstadoRec);
        }
        

        // POST api/<EstadoReclamoController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<EstadoReclamoController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<EstadoReclamoController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
