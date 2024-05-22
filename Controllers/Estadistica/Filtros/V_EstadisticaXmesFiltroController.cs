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

namespace ApiRVM2019.Controllers.Estadistica.Filtros
{
    [EnableCors("All")]
    [Route("[controller]")]
    [ApiController]
    public class V_EstadisticaXmesFiltroController : ControllerBase
    {
        private readonly AppDbContext context;

        public V_EstadisticaXmesFiltroController(AppDbContext context)
        {
            this.context = context;
        }

        // GET: api/<V_EstadisticaXmesFiltroController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<V_EstadisticaXmesFiltroController>/5
        [HttpGet("{idUsuario}/{idRol}/{anio}/{idLocalidad}")]
        public IActionResult GetDatos(int idUsuario, int idRol, string anio, int idLocalidad)
        {
            //Cuando se filtra estos parametros
            //localidad 
            //https://localhost:44363/V_EstadisticaXmesFiltro/2/3/2024/1
            //Administrador
            if (idRol == 1)
            {

                //Obligatoriamente tiene que buscar por localidad y año ARREGLAR
                if ( anio == "0" && idLocalidad!=0)
                {
                    //todos los reclamos - historicos de esa localidad sin  año
                    var dato = (from reclamosXmes in context.V_EstadisticaXmes
                                where reclamosXmes.ID_Localidad == idLocalidad
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
                }else if (anio!="0" && idLocalidad!= 0)
                {
                    //cuando busca por localidad y anio
                    var dato = (from reclamosXmes in context.V_EstadisticaXmes
                                where reclamosXmes.ID_Localidad == idLocalidad && reclamosXmes.anio==anio
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
                }else if (anio != "0" && idLocalidad == 0)
                {
                    //cuando solo busca por anio sin localidad
                    // es decir todos lo de ese año
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
                else
                {
                    //no ingresa ningun dato de filtro, busca para todos
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
            }
            else
            {
                //Usuario
                //https://localhost:44363/V_EstadisticaXmesFiltro/2/3/2024/1

                if (idLocalidad != 0 && anio != "0")
                {
                    //busque por localidad y anio

                    var dato = (from reclamosXmes in context.V_EstadisticaXmes
                                where reclamosXmes.ID_Localidad == idLocalidad
                                && reclamosXmes.IDUsuario == idUsuario && reclamosXmes.anio == anio
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
                else if (idLocalidad == 0 && anio != "0")
                {
                    //busco por anio y de cualquier localidad
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
                else if (idLocalidad != 0 && anio == "0")
                {

                    //busco por localidad de cualquier anio
                    var dato = (from reclamosXmes in context.V_EstadisticaXmes
                                where reclamosXmes.ID_Localidad == idLocalidad
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
                    //busco por todo sin ingresar anio y localidad, es decir un historico de todas las localidades y de todos los años
                    var dato = (from reclamosXmes in context.V_EstadisticaXmes
                                where  reclamosXmes.IDUsuario == idUsuario 
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



                //if ((anio == null) || anio == "")
                //{
                //    //todos los reclamos del usuario para filtrar solamente por localidad
                //    // año vacio
                //    var dato = (from reclamosXmes in context.V_EstadisticaXmes
                //                where reclamosXmes.ID_Localidad == idLocalidad
                //                && reclamosXmes.IDUsuario == idUsuario
                //                group reclamosXmes by new { reclamosXmes.NombreMes, reclamosXmes.numeroMes } into m
                //                select new
                //                {
                //                    name = m.Key.NombreMes,
                //                    value = m.Sum(x => x.Cantidad),
                //                    numero = m.Key.numeroMes
                //                }).OrderBy(x => x.numero);
                //    if (dato == null)
                //    {
                //        return NotFound();
                //    }
                //    return Ok(dato);
                //}
                //else
                //{
                //    //cuando quiere filtrar por anio y localidad
                   
                //    var dato = (from reclamosXmes in context.V_EstadisticaXmes
                //                where reclamosXmes.anio == anio && reclamosXmes.IDUsuario == idUsuario
                //                && reclamosXmes.ID_Localidad == idLocalidad
                //                group reclamosXmes by new { reclamosXmes.NombreMes, reclamosXmes.numeroMes } into m
                //                select new
                //                {
                //                    name = m.Key.NombreMes,
                //                    value = m.Sum(x => x.Cantidad),
                //                    numero = m.Key.numeroMes
                //                }).OrderBy(x => x.numero);

                //    if (dato == null)
                //    {
                //        return NotFound();
                //    }
                //    return Ok(dato);
                //}
            }
            //https://localhost:44363/EstPorcentajeCalleXLocalidad/2


        }

        // POST api/<V_EstadisticaXmesFiltroController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<V_EstadisticaXmesFiltroController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<V_EstadisticaXmesFiltroController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
