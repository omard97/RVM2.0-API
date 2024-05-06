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
    public class V_CantidadTipoReclamoDelMesController : ControllerBase
    {
        private readonly AppDbContext context;

        public V_CantidadTipoReclamoDelMesController (AppDbContext context)
        {
            this.context = context;
        }

        // GET: api/<V_CantidadTipoReclamoDelMesController>
        [HttpGet]
        public IActionResult ValidacionGet(int idRol, int idUsuario, string nombreMes, int anio)
        {
            if (idRol == 1)
            {
                //Administrador
                //cuando se selecciona el mes en el grafico buscara los reclamos de ese mes, de ese año y de ese usuario por el nombre del mes
                // ejemplo URL: https://localhost:44363/V_CantidadTipoReclamoDelMes?idRol=3&idUsuario=2&nombreMes=Mayo&anio=2024

                var cantTipo = from TRSemana in context.V_CantidadTipoReclamoDelMes
                               where TRSemana.NombreMes.Contains(nombreMes) && TRSemana.anio == anio
                               group TRSemana by new { TRSemana.nombre } into g
                               select new
                               {
                                   name = g.Key.nombre,
                                   value = g.Sum(x => x.CantidadTiposReclamos)

                               };
                if (cantTipo == null)
                {
                    return NotFound();
                }
                return Ok(cantTipo);

            }
            else
            {
                if (idRol == 3)
                {
                    var cantTipo = from TRSemana in context.V_CantidadTipoReclamoDelMes
                                   where TRSemana.NombreMes.Contains(nombreMes) && TRSemana.anio == anio
                                   && TRSemana.IDUsuario == idUsuario
                                   group TRSemana by new { TRSemana.nombre } into g
                                   select new
                                   {
                                       name = g.Key.nombre,
                                       value = g.Sum(x => x.CantidadTiposReclamos)

                                   };
                    if (cantTipo == null)
                    {
                        return NotFound();
                    }
                    return Ok(cantTipo);
                }

                return NotFound();
            }
        }

        // GET api/<V_CantidadTipoReclamoDelMesController>/5
        [HttpGet("{idRol}/{idUsuario}/{mes}/{anio}")]
        public IActionResult GetReclamosUsuario(int idRol, int idUsuario, int mes, int anio)
        {
            // https://localhost:44363/V_CantidadTipoReclamoDelMes/1/2/2/2024
            if (idRol == 1 )
            {
                //Administrador
                //cuando se abre la pantalla se visualizan los reclamos totales del mes actual y año

                var cantTipo = from TRSemana in context.V_CantidadTipoReclamoDelMes
                               where TRSemana.Mes == mes && TRSemana.anio == anio
                               group TRSemana by new { TRSemana.nombre } into g
                               select new
                               {
                                   name = g.Key.nombre,
                                   value = g.Sum(x => x.CantidadTiposReclamos)

                               };
                if (cantTipo == null)
                {
                    return NotFound();
                }
                return Ok(cantTipo);

            }
            else
            {
                if (idRol == 3)
                {
                    var cantTipo = from TRSemana in context.V_CantidadTipoReclamoDelMes
                                   where TRSemana.Mes == mes && TRSemana.anio == anio
                                   && TRSemana.IDUsuario == idUsuario
                                   group TRSemana by new { TRSemana.nombre } into g
                                   select new
                                   {
                                       name = g.Key.nombre,
                                       value = g.Sum(x => x.CantidadTiposReclamos)

                                   };
                    if (cantTipo == null)
                    {
                        return NotFound();
                    }
                    return Ok(cantTipo);
                }

                return NotFound();
            }
        }

        // POST api/<V_CantidadTipoReclamoDelMesController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<V_CantidadTipoReclamoDelMesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<V_CantidadTipoReclamoDelMesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
