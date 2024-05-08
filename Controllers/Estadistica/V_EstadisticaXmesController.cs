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
    public class V_EstadisticaXmesController : ControllerBase
    {
        private readonly AppDbContext context;
        public V_EstadisticaXmesController(AppDbContext context)
        {
            this.context = context;
        }

        // GET: api/<V_EstadisticaXmesController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<V_EstadisticaXmesController>/5
        [HttpGet("{idUsuario}/{idRol}/{anio}")]
        public IActionResult GetDatos(int idUsuario,int idRol, string anio)
        {
            //Administrador
            if (idRol == 1 )
            {
                if((anio == null)|| anio == "")
                {
                    //todos los reclamos - historicos
                    var dato = (from reclamosXmes in context.V_EstadisticaXmes
                         
                                group reclamosXmes by new { reclamosXmes.NombreMes, reclamosXmes.numeroMes } into m
                                select new
                                {
                                    name = m.Key.NombreMes,
                                    value = m.Sum(x => x.Cantidad),
                                    numero = m.Key.numeroMes
                                }).OrderBy(x => x.numero);
                    if (dato == null)
                    {
                        return NotFound();
                    }
                    return Ok(dato);
                }
                else
                {
                    var dato = (from reclamosXmes in context.V_EstadisticaXmes
                                where reclamosXmes.anio == anio
                              
                                group reclamosXmes by new { reclamosXmes.NombreMes, reclamosXmes.numeroMes } into m
                                select new
                                {
                                    name = m.Key.NombreMes,
                                    value = m.Sum(x => x.Cantidad),
                                    numero = m.Key.numeroMes
                                }).OrderBy(x => x.numero);

                    if (dato == null)
                    {
                        return NotFound();
                    }
                    return Ok(dato);
                }
            }
            else
            {
                //Usuario
                //https://localhost:44363/V_EstadisticaXmes/2/3/2024
                if ((anio == null) || anio == "")
                {
                    //todos los reclamos del usuario al ingreesar en pantalla
                    var dato = (from reclamosXmes in context.V_EstadisticaXmes
                               where reclamosXmes.anio == DateTime.Now.Year.ToString() 
                               && reclamosXmes.IDUsuario == idUsuario
                               group reclamosXmes by new { reclamosXmes.NombreMes, reclamosXmes.numeroMes } into m
                               select new
                               {
                                   name = m.Key.NombreMes,
                                   value = m.Sum(x => x.Cantidad),
                                   numero = m.Key.numeroMes
                               }).OrderBy(x => x.numero);
                    if (dato == null)
                    {
                        return NotFound();
                    }
                    return Ok(dato);
                }
                else
                {
                    //cuando quiere filtrar por anio y con su usuario
                    // por ahora esta cuando ingrea a la pantalla
                    var dato = (from reclamosXmes in context.V_EstadisticaXmes
                                where reclamosXmes.anio == anio && reclamosXmes.IDUsuario == idUsuario
                                group reclamosXmes by new { reclamosXmes.NombreMes, reclamosXmes.numeroMes } into m
                                select new
                                {
                                    name = m.Key.NombreMes,
                                    value = m.Sum(x => x.Cantidad),
                                    numero = m.Key.numeroMes
                                }).OrderBy(x => x.numero);
                              
                    if (dato == null)
                    {
                        return NotFound();
                    }
                    return Ok(dato);
                }
            }
            //https://localhost:44363/EstPorcentajeCalleXLocalidad/2
          
         
        }

        // POST api/<V_EstadisticaXmesController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<V_EstadisticaXmesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<V_EstadisticaXmesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
