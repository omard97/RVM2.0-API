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
    public class V_ReclamosEnElTiempoFiltroController : ControllerBase
    {

        private readonly AppDbContext context;
        public V_ReclamosEnElTiempoFiltroController(AppDbContext context)
        {
            this.context = context;
        }
        // GET: api/<V_ReclamosEnElTiempoFiltroController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<V_ReclamosEnElTiempoFiltroController>/5
        [HttpGet("{idRol}/{idUsuario}/{mes}/{anio}/{idLocalidad}")]
        public IActionResult GetreclamosTiempo(int idRol, int idUsuario, int mes, int anio, int idLocalidad)
        {
            //Cuando utilicen los filtros se ejecuta esto
            //https://localhost:44363/V_ReclamosEnElTiempoFiltro/3/2/2/2024/1
            if (idRol == 1)
            {
                if (idLocalidad != 0 && anio != 0)
                {
                    //busque por localidad y anio
                    var data = from vista in context.V_ReclamosEnElTiempo
                               where vista.ID_Localidad == idLocalidad && vista.Anio==anio
                               group vista by new { vista.Hora, vista.TipoHora } into g
                               select new
                               {
                                   name = g.Key.Hora,
                                   value = g.Sum(x => x.CantidadReclamo),
                               };
                    if (data == null)
                    {
                        return NotFound();
                    }
                    return Ok(data);

                }
                else if (idLocalidad == 0 && anio != 0)
                {
                    //busco por anio y de cualquier localidad
                    var data = from vista in context.V_ReclamosEnElTiempo
                               where vista.Anio == anio
                               group vista by new { vista.Hora, vista.TipoHora } into g
                               select new
                               {
                                   name = g.Key.Hora,
                                   value = g.Sum(x => x.CantidadReclamo),
                               };
                    if (data == null)
                    {
                        return NotFound();
                    }
                    return Ok(data);

                }
                else if (idLocalidad != 0 && anio == 0)
                {

                    //busco por localidad de cualquier anio
                    var data = from vista in context.V_ReclamosEnElTiempo
                               where vista.ID_Localidad == idLocalidad
                               group vista by new { vista.Hora, vista.TipoHora } into g
                               select new
                               {
                                   name = g.Key.Hora,
                                   value = g.Sum(x => x.CantidadReclamo),
                               };
                    if (data == null)
                    {
                        return NotFound();
                    }
                    return Ok(data);
                }
                else
                {
                    var data = from vista in context.V_ReclamosEnElTiempo
                               where  vista.Anio <= anio
                               group vista by new { vista.Hora, vista.TipoHora } into g
                               select new
                               {
                                   name = g.Key.Hora,
                                   value = g.Sum(x => x.CantidadReclamo),
                               };
                    if (data == null)
                    {
                        return NotFound();
                    }
                    return Ok(data);
                }


                //var data = from vista in context.V_ReclamosEnElTiempo
                //           group vista by new { vista.Hora, vista.TipoHora } into g
                //           select new
                //           {
                //               name = g.Key.Hora,
                //               value = g.Sum(x => x.CantidadReclamo),
                //           };
                //if (data == null)
                //{
                //    return NotFound();
                //}
                //return Ok(data);
            }
            else
            {
                if (idRol == 3)
                {

                    if (idLocalidad != 0 && anio != 0)
                    {
                        //busque por localidad y anio

                        var data = from vista in context.V_ReclamosEnElTiempo
                                   where vista.idUsuario == idUsuario && vista.Anio == anio
                                   && vista.ID_Localidad == idLocalidad
                                   group vista by new { vista.Hora, vista.TipoHora } into g
                                   select new
                                   {
                                       name = g.Key.Hora,
                                       value = g.Sum(x => x.CantidadReclamo),
                                   };
                        if (data == null)
                        {
                            return NotFound();
                        }
                        return Ok(data);


                    }
                    else if (idLocalidad == 0 && anio != 0)
                    {
                        //busco por anio y de cualquier localidad
                        var data = from vista in context.V_ReclamosEnElTiempo
                                   where vista.idUsuario == idUsuario && vista.Anio == anio
                                   group vista by new { vista.Hora, vista.TipoHora } into g
                                   select new
                                   {
                                       name = g.Key.Hora,
                                       value = g.Sum(x => x.CantidadReclamo),
                                   };
                        if (data == null)
                        {
                            return NotFound();
                        }
                        return Ok(data);

                    }
                    else if (idLocalidad != 0 && anio == 0)
                    {

                        //busco por localidad de cualquier anio
                        var data = from vista in context.V_ReclamosEnElTiempo
                                   where vista.idUsuario == idUsuario && vista.ID_Localidad == idLocalidad
                                   group vista by new { vista.Hora, vista.TipoHora } into g
                                   select new
                                   {
                                       name = g.Key.Hora,
                                       value = g.Sum(x => x.CantidadReclamo),
                                   };
                        if (data == null)
                        {
                            return NotFound();
                        }
                        return Ok(data);

                    }
                    else
                    {
                        //busqueda historica, busca todos reclamos de todas las localidades y todos los anios
                        var data = from vista in context.V_ReclamosEnElTiempo
                                   where vista.idUsuario == idUsuario && vista.Anio <= anio
                                   && vista.ID_Localidad == idLocalidad
                                   group vista by new { vista.Hora, vista.TipoHora } into g
                                   select new
                                   {
                                       name = g.Key.Hora,
                                       value = g.Sum(x => x.CantidadReclamo),
                                   };
                        if (data == null)
                        {
                            return NotFound();
                        }
                        return Ok(data);
                    }


                    //var data = from vista in context.V_ReclamosEnElTiempo
                    //           where vista.idUsuario == idUsuario && vista.Anio <= anio
                    //           && vista.ID_Localidad == idLocalidad
                    //           group vista by new { vista.Hora, vista.TipoHora } into g
                    //           select new
                    //           {
                    //               name = g.Key.Hora,
                    //               value = g.Sum(x => x.CantidadReclamo),
                    //           };
                    //if (data == null)
                    //{
                    //    return NotFound();
                    //}
                    //return Ok(data);
                }

                return NotFound();
            }
        }

        // POST api/<V_ReclamosEnElTiempoFiltroController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<V_ReclamosEnElTiempoFiltroController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<V_ReclamosEnElTiempoFiltroController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
