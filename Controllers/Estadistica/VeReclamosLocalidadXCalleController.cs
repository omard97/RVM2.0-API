using ApiRVM2019.Contexts;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiRVM2019.Entities;
using Microsoft.EntityFrameworkCore;
using ApiRVM2019.Entities.Localidades;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ApiRVM2019.Controllers.Estadistica
{
    [EnableCors("All")]
    [Route("[controller]")]
    [ApiController]
    public class VeReclamosLocalidadXCalleController : ControllerBase
    {
        private readonly AppDbContext context;

        public VeReclamosLocalidadXCalleController(AppDbContext context)
        {
            this.context = context;
        }
        // GET: api/<VeReclamosLocalidadXCalleController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        //NO SE VA A USAR

        //https://localhost:44363/VeReclamosLocalidadXCalle/5
        //https://localhost:44363/VeReclamosLocalidadXCalle/1/2
        // GET api/<VeReclamosLocalidadXCalleController>/5
        //[HttpGet("{idLocalidad}/{idUsuario}")]
        //public IActionResult Get(int idLocalidad, int idUsuario)
        //{
        //    var _datos = from VeReclamosLocalidadXCalleController in context.VE_ReclamosLocalidadXCalle
        //                 where VeReclamosLocalidadXCalleController.IDLocalidad == idLocalidad
        //                 && VeReclamosLocalidadXCalleController.IDUsuario == idUsuario
        //                 orderby VeReclamosLocalidadXCalleController.IDUsuario, VeReclamosLocalidadXCalleController.direccion
        //                 select new
        //                 {
        //                     direccion = VeReclamosLocalidadXCalleController.direccion,
        //                     altura = Convert.ToInt32(VeReclamosLocalidadXCalleController.altura),
        //                     cantidad = Convert.ToInt32(VeReclamosLocalidadXCalleController.Cantidad),

        //                 };
        //    if (_datos == null)
        //    {
        //        return NotFound();
        //    }
        //    return Ok(_datos);
        //}


        //utilizado cuando se selecciona una tarjeta y visualiza la tabla de calles Funciona
        //[HttpGet("{idLocalidad}/{idUsuario}")]
        //public IActionResult Get(int idLocalidad, int idUsuario)
        //{
        //    var datos = (from VeReclamosLocalidadXCalleController in context.VE_ReclamosLocalidadXCalle
        //                 where VeReclamosLocalidadXCalleController.IDLocalidad == idLocalidad
        //                 && VeReclamosLocalidadXCalleController.IDUsuario == idUsuario
        //                 orderby VeReclamosLocalidadXCalleController.IDUsuario, VeReclamosLocalidadXCalleController.direccion
        //                 select new DataPoint
        //                 {
        //                     Name = VeReclamosLocalidadXCalleController.direccion,
        //                     Series = new List<DataSeries>
        //                     {
        //                         new DataSeries
        //                         {
        //                             Name = Convert.ToString( VeReclamosLocalidadXCalleController.altura), // Aquí puedes asignar el valor que desees para Name
        //                             Value = VeReclamosLocalidadXCalleController.Cantidad,
        //                             Extra = new ExtraData
        //                             {
        //                                 Code = "de" // Puedes cambiar el valor según tus necesidades
        //                             }
        //                         },
        //                          Aquí puedes agregar más elementos a la lista Series según sea necesario
        //                     }
        //                 }).ToList();

        //    if (datos == null || datos.Count == 0)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(datos);
        //}


        //   [HttpGet("{idLocalidad}/{idUsuario}")]
        //   public IActionResult Get(int idLocalidad, int idUsuario)
        //   {
        //       var rawData = context.VE_ReclamosLocalidadXCalle
        //.Where(v => v.IDLocalidad == idLocalidad && v.IDUsuario == idUsuario)
        //.OrderBy(v => v.direccion)
        //.ToList();

        //       var groupedData = rawData.GroupBy(v => v.direccion);

        //       var datos = groupedData.Select(g => new DataPoint
        //       {
        //           Name = g.Key,
        //           Series = g.Select(x => new DataSeries
        //           {
        //               Name = x.altura.ToString(), // Convertir el entero a cadena aquí
        //               Value = x.Cantidad
        //           }).ToList()
        //       }).ToList();


        //       if (datos == null || datos.Count == 0)
        //       {
        //           return NotFound();
        //       }

        //       return Ok(datos);
        //   }


        //public class DataPoint
        //{
        //    public string Name { get; set; }
        //    public List<DataSeries> Series { get; set; }
        //}

        //public class DataSeries
        //{
        //    public string Name { get; set; }
        //    public int Value { get; set; }
        //    public ExtraData Extra { get; set; }
        //}

        //public class ExtraData
        //{
        //    public string Code { get; set; }
        //}

        //----------------------------------------------------------------------
        [HttpGet("{idLocalidad}/{idUsuario}/{idrol}")]
        public IActionResult Get(int idLocalidad, int idUsuario, int idrol)
        {

            if (idrol==1)
            {
                //administrador
                //muestra todas las calles de esa localidad pero la maxima primero
                var _datos = (from vista in context.VE_ReclamosLocalidadXCalle
                             where vista.IDLocalidad == idLocalidad
                             
                             orderby vista.IDUsuario, vista.direccion
                             select new
                             {
                                 direccion = vista.direccion,
                                 altura = Convert.ToInt32(vista.altura),
                                 cantidad = Convert.ToInt32(vista.Cantidad)
                             });

                if (_datos == null)
                {
                    return NotFound();
                }

                // Crear la estructura de datos deseada
                var response = new List<object>();
                foreach (var dato in _datos)
                {
                    var series = new List<object>
                {
                    new
                    {
                        name = dato.altura.ToString(),
                        value = dato.cantidad,
                        extra = new { code = "de" }
                    }
                };

                    response.Add(new
                    {
                        name = dato.direccion,
                        series
                    });
                }

                return Ok(response);
            }
            else
            {
                var _datos = from VeReclamosLocalidadXCalleController in context.VE_ReclamosLocalidadXCalle
                             where VeReclamosLocalidadXCalleController.IDLocalidad == idLocalidad
                             && VeReclamosLocalidadXCalleController.IDUsuario == idUsuario
                             orderby VeReclamosLocalidadXCalleController.IDUsuario, VeReclamosLocalidadXCalleController.direccion
                             select new
                             {
                                 direccion = VeReclamosLocalidadXCalleController.direccion,
                                 altura = Convert.ToInt32(VeReclamosLocalidadXCalleController.altura),
                                 cantidad = Convert.ToInt32(VeReclamosLocalidadXCalleController.Cantidad),
                             };

                if (_datos == null)
                {
                    return NotFound();
                }

                // Crear la estructura de datos deseada
                var response = new List<object>();
                foreach (var dato in _datos)
                {
                    var series = new List<object>
                {
                    new
                    {
                        name = dato.altura.ToString(),
                        value = dato.cantidad,
                        extra = new { code = "de" }
                    }
                };

                    response.Add(new
                    {
                        name = dato.direccion,
                        series
                    });
                }

                return Ok(response);
            }
           
        }


        // POST api/<VeReclamosLocalidadXCalleController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<VeReclamosLocalidadXCalleController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<VeReclamosLocalidadXCalleController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
