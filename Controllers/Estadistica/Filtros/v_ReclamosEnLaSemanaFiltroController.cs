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
    public class v_ReclamosEnLaSemanaFiltroController : ControllerBase
    {
        private readonly AppDbContext context;
        public v_ReclamosEnLaSemanaFiltroController(AppDbContext context)
        {
            this.context = context;
        }
        // GET: api/<v_ReclamosEnLaSemanaFiltroController>
        [HttpGet]       
        public IActionResult ReclamosDelMes(int idRol, int idUsuario, string nombreMes, int anio, int idLocalidad)
        {

            //cuando se selecciona el mes en el grafico buscara los reclamos de ese mes, de ese año y de ese usuario por el nombre del mes
            // ejemplo URL: https://localhost:44363/v_ReclamosEnLaSemanaFiltro?idRol=3&idUsuario=2&nombreMes=Marzo&anio=2024&idLocalidad=1



            if (idRol == 1)
            {
                var info = (from recSemana in context.v_ReclamosEnLaSemana
                            where recSemana.NombreMes.Contains(nombreMes) && recSemana.anio == anio

                            group recSemana by new { recSemana.DiaDeLaSemana, recSemana.numeroDia } into g
                            select new
                            {
                                name = g.Key.DiaDeLaSemana,
                                value = g.Sum(x => x.CantidadReclamos),
                                numero = g.Key.numeroDia
                            }).OrderBy(numeroDia => numeroDia.numero);

                if (info == null)
                {
                    return NotFound();
                }
                return Ok(info);
            }
            else
            {
                if (idRol == 3)
                {
                    var info = (from recSemana in context.v_ReclamosEnLaSemana
                                where recSemana.NombreMes.Contains(nombreMes) && recSemana.anio == anio
                                && recSemana.idusuario == idUsuario && recSemana.ID_Localidad == idLocalidad
                                group recSemana by new { recSemana.DiaDeLaSemana, recSemana.numeroDia } into g
                                select new
                                {
                                    name = g.Key.DiaDeLaSemana,
                                    value = g.Sum(x => x.CantidadReclamos),
                                    numero = g.Key.numeroDia
                                }).OrderBy(numeroDia => numeroDia.numero);

                    if (info == null)
                    {
                        return NotFound();
                    }
                    return Ok(info);
                }
                return NotFound();
            }



        }

        // GET api/<v_ReclamosEnLaSemanaFiltroController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<v_ReclamosEnLaSemanaFiltroController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<v_ReclamosEnLaSemanaFiltroController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<v_ReclamosEnLaSemanaFiltroController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
