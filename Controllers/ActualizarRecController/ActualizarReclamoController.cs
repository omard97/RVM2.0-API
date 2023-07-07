using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ApiRVM2019.Contexts;
using ApiRVM2019.Entities;
using Microsoft.AspNetCore.Cors;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ApiRVM2019.Controllers.ActualizarReclamoController
{
    [EnableCors("All")]
    [Route("[controller]")]
    [ApiController]
    public class ActualizarReclamoController : ControllerBase
    {
        private readonly AppDbContext context;
        public object _DetReclamo;

        public ActualizarReclamoController(AppDbContext context)
        {
            this.context = context;
        }

        // GET: api/<ActualizarReclamoController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<ActualizarReclamoController>/5
        [HttpGet("{idDetalle}")]
        public IActionResult GetReclamosUsuario( int idDetalle)
        {

           
                _DetReclamo = (from DetalleReclamo in context.DetalleReclamo
                               join reclamo in context.Reclamo on DetalleReclamo.ID_Reclamo equals reclamo.IDReclamo
                               join estado in context.Estado on reclamo.ID_Estado equals estado.IDEstado
                               join TipoReclamo in context.TipoReclamo on reclamo.ID_TipoReclamo equals TipoReclamo.IDTipoReclamo
                               join ReclamoAmbiental in context.ReclamoAmbiental on DetalleReclamo.ID_ReclamoAmbiental equals ReclamoAmbiental.IDReclamoAmbiental
                               join sesion in context.Sesion on reclamo.ID_Sesion equals sesion.IDSesion
                               join usuario in context.Usuario on sesion.ID_Usuario equals usuario.IDUsuario
                             
                               where DetalleReclamo.IDDetalleReclamo == idDetalle
                               select new
                               {
                                   IDDetalleReclamo = DetalleReclamo.IDDetalleReclamo,
                                   Descripcion = DetalleReclamo.Descripcion,
                                   Altura = DetalleReclamo.Altura,
                                   Direccion = DetalleReclamo.Direccion,
                                   ID_Reclamo = DetalleReclamo.ID_Reclamo,
                                   Longitud = DetalleReclamo.longitud,
                                   Latitud = DetalleReclamo.latitud,
                                   Fecha = reclamo.Fecha,
                                   Hora = reclamo.Hora,
                                   IDSesion = reclamo.ID_Sesion,

                                   Nombre = estado.Nombre,
                                   IDEstado = estado.IDEstado,

                                   NombreTRec = TipoReclamo.Nombre,
                                   IDTipoRec = TipoReclamo.IDTipoReclamo,
                                   IDRecAmb = ReclamoAmbiental.IDReclamoAmbiental,

                                   NombreRecAmbiental = ReclamoAmbiental.Nombre, //quema de arboles, unundaciones, etc
                                   Dominio = DetalleReclamo.Dominio,
                                   Nick = usuario.Nick,
                                   Foto = reclamo.Foto


                               }).OrderByDescending(ID => ID.ID_Reclamo);
            


            if (_DetReclamo == null)
            {
                return NotFound();
            }
            return Ok(_DetReclamo);

        }

        // POST api/<ActualizarReclamoController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ActualizarReclamoController>/5
        [HttpPut("{idreclamo}/{idDetalleR}")]
        public async Task<ActionResult<Reclamo>> actualizarReclamo(int idreclamo, [FromBody] Reclamo reclamo , int idDetalleR, [FromBody] DetalleReclamo DetRecla)
        {
            if (reclamo.IDReclamo == idreclamo)
            {
                context.Entry(reclamo).State = EntityState.Modified;
                context.SaveChanges();
            }
            else if (idreclamo != reclamo.IDReclamo)
            {
                return BadRequest();
            }

            var result = await context.Reclamo.FindAsync(idreclamo);

            if (result == null)
            {
                return NotFound();
            }

            return NoContent();
        }
        //public ActionResult Put(int IDRecla, Reclamo Reclamo, int IDDetalle, DetalleReclamo DetRecla)
        //{
        //    try
        //    {
        //        if (Reclamo.IDReclamo == IDRecla && DetRecla.IDDetalleReclamo == IDDetalle)
        //        {
        //            context.Entry(DetRecla).State = EntityState.Modified;
        //            context.Entry(Reclamo).State = EntityState.Modified;

        //            context.SaveChanges();
        //            return Ok();
        //        }
        //        else if (IDRecla != Reclamo.IDReclamo || IDRecla != Reclamo.IDReclamo)
        //        {
        //            return BadRequest();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }



        //    if (IDRecla == 0 || IDDetalle == 0)
        //    {
        //        return NotFound();
        //    }

        //    return NoContent();


        //}


        // DELETE api/<ActualizarReclamoController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
