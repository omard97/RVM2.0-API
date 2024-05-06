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

namespace ApiRVM2019.Controllers.Estadistica
{
    [EnableCors("All")]
    [Route("[controller]")]
    [ApiController]
    public class v_ReclamosEnLaSemanaController : ControllerBase
    {
        private readonly AppDbContext context;

        public v_ReclamosEnLaSemanaController(AppDbContext context)
        {
            this.context = context;
        }

        // GET: api/<v_ReclamosEnLaSemanaController>
        [HttpGet]
        public IActionResult ValidacionGet(int idRol, int idUsuario, string nombreMes, int anio)
        {

            //cuando se selecciona el mes en el grafico buscara los reclamos de ese mes, de ese año y de ese usuario por el nombre del mes
            // ejemplo URL: https://localhost:44363/v_ReclamosEnLaSemana?idRol=3&idUsuario=2&nombreMes=Mayo&anio=2024
            var info = (from recSemana in context.v_ReclamosEnLaSemana
                        where recSemana.NombreMes.Contains(nombreMes) && recSemana.anio == anio
                        && recSemana.idusuario == idUsuario
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

        // GET api/<v_ReclamosEnLaSemanaController>/5
        [HttpGet("{idRol}/{idUsuario}/{mes}/{anio}")]
        public IActionResult GetReclamosUsuario(int idRol, int idUsuario, int mes, int anio)
        {
            //Cuando se abre la pantalla de estadistica
            if (idRol==1)
            {
                //Administrador 
                //va a mostrar todos los reclamos del mes actual y a la misma consulta tambien el mes que se selecciono
                var info = (from recSemana in context.v_ReclamosEnLaSemana
                                   where recSemana.Mes == mes && recSemana.anio == anio
                                   select new
                                   {
                                       name = recSemana.DiaDeLaSemana,
                                       value = recSemana.CantidadReclamos,
                                       numero = recSemana.numeroDia
                                   }).OrderBy(numeroDia => numeroDia.numero);


                if (info == null)
                {
                    return NotFound();
                }
                return Ok(info);
            }
            else
            { //Usuario
                if (idRol ==3)
                {
                    //https://localhost:44363/v_ReclamosEnLaSemana/3/2/2/2024
                    var info = (from recSemana in context.v_ReclamosEnLaSemana
                                       where recSemana.Mes == mes && recSemana.anio == anio
                                             && recSemana.idusuario == idUsuario
                                       select new
                                       {
                                           name = recSemana.DiaDeLaSemana,
                                           value = recSemana.CantidadReclamos,
                                           numero = recSemana.numeroDia
                                       }).OrderBy(numeroDia => numeroDia.numero);


                    if (info == null)
                    {
                        return NotFound();
                    }
                    return Ok(info);
                }
                else
                {
                    return NotFound();
                }
            }
           
        }

        // POST api/<v_ReclamosEnLaSemanaController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<v_ReclamosEnLaSemanaController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<v_ReclamosEnLaSemanaController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
