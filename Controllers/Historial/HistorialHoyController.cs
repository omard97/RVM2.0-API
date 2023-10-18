using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiRVM2019.Contexts;
using ApiRVM2019.Entities;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ApiRVM2019.Controllers.Historial
{
    [EnableCors("All")]
    [Route("[controller]")]
    [ApiController]
    public class HistorialHoyController : ControllerBase
    {
        private readonly AppDbContext context;

        public HistorialHoyController(AppDbContext context)
        {
            this.context = context;
        }
        
        
        // GET: api/<HistorialHoyController>
        [HttpGet]
        public IActionResult Get(string fechaHoy, int idUsuario, int idEstadoA, int idEstadoV, int idRol)
        {
            //reclamos del dia de hoy
            //ejemplo
            //https://localhost:44363/HistorialHoy?fechaHoy=2021-10-22&idUsuario=1&idEstadoA=1&idEstadoV=5&idRol=1  Postman o Google
            if (idRol==1 || idRol==2) //administrador o empleado devuelve todos los reclamos del dia
            {
                var _DetReclamo = (from DetalleReclamo in context.DetalleReclamo
                                   join reclamo in context.Reclamo on DetalleReclamo.ID_Reclamo equals reclamo.IDReclamo
                                   join estado in context.Estado on reclamo.ID_Estado equals estado.IDEstado
                                   join TipoReclamo in context.TipoReclamo on reclamo.ID_TipoReclamo equals TipoReclamo.IDTipoReclamo
                                   join ReclamoAmbiental in context.ReclamoAmbiental on DetalleReclamo.ID_ReclamoAmbiental equals ReclamoAmbiental.IDReclamoAmbiental
                                   join sesion in context.Sesion on reclamo.ID_Sesion equals sesion.IDSesion
                                   join usuario in context.Usuario on sesion.ID_Usuario equals usuario.IDUsuario
                                  
                                   where reclamo.Fecha == fechaHoy && (reclamo.ID_Estado == idEstadoA || reclamo.ID_Estado == idEstadoV) // 1 pendiente ambiental y 5 pendiente vial
                                   select new
                                   {
                                       IDDetalleReclamo = DetalleReclamo.IDDetalleReclamo,
                                       Descripcion = DetalleReclamo.Descripcion,
                                       Altura = DetalleReclamo.Altura,
                                       Direccion = DetalleReclamo.Direccion,
                                       Dominio = DetalleReclamo.Dominio,
                                       Longitud = DetalleReclamo.longitud,
                                       Latitud = DetalleReclamo.latitud,
                                       ID_Reclamo = DetalleReclamo.ID_Reclamo,
                                       idReclamo = reclamo.IDReclamo,
                                       id_sesion = reclamo.ID_Sesion,
                                       Fecha = reclamo.Fecha,
                                       Hora = reclamo.Hora,
                                       Nombre = estado.Nombre,
                                       IDEstado = estado.IDEstado,
                                       NombreTRec = TipoReclamo.Nombre,
                                       IDTipoRec = TipoReclamo.IDTipoReclamo,
                                       IDRecAmb = ReclamoAmbiental.IDReclamoAmbiental,
                                       NombreRecAmbiental = ReclamoAmbiental.Nombre, //quema de arboles, unundaciones, etc
                                       Nick = usuario.Nick,
                                       Foto = reclamo.Foto,
                                       marca = "",
                                       color = "",
                                       modelo = ""
                                   }).OrderByDescending(ID => ID.ID_Reclamo);

                if (_DetReclamo == null)
                {
                    return NotFound();
                }
                return Ok(_DetReclamo);

            }
            else //siendo usuario solamente devuelve los reclamos del usuario del día de hoy
            {
                var _DetReclamo = (from DetalleReclamo in context.DetalleReclamo
                                   join reclamo in context.Reclamo on DetalleReclamo.ID_Reclamo equals reclamo.IDReclamo
                                   join estado in context.Estado on reclamo.ID_Estado equals estado.IDEstado
                                   join TipoReclamo in context.TipoReclamo on reclamo.ID_TipoReclamo equals TipoReclamo.IDTipoReclamo
                                   join ReclamoAmbiental in context.ReclamoAmbiental on DetalleReclamo.ID_ReclamoAmbiental equals ReclamoAmbiental.IDReclamoAmbiental
                                   join sesion in context.Sesion on reclamo.ID_Sesion equals sesion.IDSesion
                                   join usuario in context.Usuario on sesion.ID_Usuario equals usuario.IDUsuario
                                   where reclamo.Fecha == fechaHoy && sesion.ID_Usuario==idUsuario &&
                                   (reclamo.ID_Estado == idEstadoA || reclamo.ID_Estado == idEstadoV)  // 1 pendiente ambiental y 5 pendiente vial
                                   select new
                                   {
                                       IDDetalleReclamo = DetalleReclamo.IDDetalleReclamo,
                                       Descripcion = DetalleReclamo.Descripcion,
                                       Altura = DetalleReclamo.Altura,
                                       Direccion = DetalleReclamo.Direccion,
                                       Dominio = DetalleReclamo.Dominio,
                                       Longitud = DetalleReclamo.longitud,
                                       Latitud = DetalleReclamo.latitud,
                                       ID_Reclamo = DetalleReclamo.ID_Reclamo,
                                       idReclamo = reclamo.IDReclamo,
                                       id_sesion = reclamo.ID_Sesion,
                                       Fecha = reclamo.Fecha,
                                       Hora = reclamo.Hora,
                                       Nombre = estado.Nombre,
                                       IDEstado = estado.IDEstado,
                                       NombreTRec = TipoReclamo.Nombre,
                                       IDTipoRec = TipoReclamo.IDTipoReclamo,
                                       IDRecAmb = ReclamoAmbiental.IDReclamoAmbiental,
                                       NombreRecAmbiental = ReclamoAmbiental.Nombre, //quema de arboles, unundaciones, etc
                                       Nick = usuario.Nick,
                                       Foto = reclamo.Foto,
                                       marca = "",
                                       color = "",
                                       modelo = ""
                                   }).OrderByDescending(ID => ID.ID_Reclamo);


                if (_DetReclamo == null)
                {
                    return NotFound();
                }
                return Ok(_DetReclamo);
            }

           
        }

        // GET api/<HistorialHoyController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<HistorialHoyController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<HistorialHoyController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<HistorialHoyController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
