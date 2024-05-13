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

namespace ApiRVM2019.Controllers.Estadistica.Filtros
{
    [EnableCors("All")]
    [Route("[controller]")]
    [ApiController]
    public class V_CantidadTipoReclamoDelMesFiltroController : ControllerBase
    {

        private readonly AppDbContext context;

        public V_CantidadTipoReclamoDelMesFiltroController( AppDbContext context)
        {
            this.context = context;
        }
        // GET: api/<V_CantidadTipoReclamoDelMesFiltroController>
        [HttpGet]
        public IActionResult ReclamosDelMes(int idRol, int idUsuario, string nombreMes, int anio, int idLocalidad)
        {
            if (idRol == 1)
            {
                //Administrador
                //cuando se selecciona el mes en el grafico buscara los reclamos de ese mes, de ese año y de ese usuario por el nombre del mes
                // ejemplo URL: https://localhost:44363/V_CantidadTipoReclamoDelMesFiltro?idRol=3&idUsuario=2&nombreMes=Febrero&anio=2024&idLocalidad=1

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
                                   && TRSemana.IDUsuario == idUsuario && TRSemana.ID_Localidad == idLocalidad
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

        // GET api/<V_CantidadTipoReclamoDelMesFiltroController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<V_CantidadTipoReclamoDelMesFiltroController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<V_CantidadTipoReclamoDelMesFiltroController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<V_CantidadTipoReclamoDelMesFiltroController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
