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

namespace ApiRVM2019.Controllers.FiltrosHistorialController
{
    [EnableCors("All")]
    [Route("[controller]")]
    [ApiController]
    public class FiltroNombreReclamosController : ControllerBase
    {
        private readonly AppDbContext context;

        public FiltroNombreReclamosController(AppDbContext context)
        {
            this.context = context;
        }
        // GET: api/<FiltroNombreReclamosController>
        [HttpGet]
        public IActionResult Get(string nombreUsuario) //buscar reclamos por nombre de usuario siendo admin
        {
            //https://localhost:44363/FiltroNombreReclamos?nombreUsuario=Omar


            var _DetReclamo = (from DetalleReclamo in context.DetalleReclamo
                               join reclamo in context.Reclamo on DetalleReclamo.ID_Reclamo equals reclamo.IDReclamo
                               join estado in context.Estado on reclamo.ID_Estado equals estado.IDEstado
                               join TipoReclamo in context.TipoReclamo on reclamo.ID_TipoReclamo equals TipoReclamo.IDTipoReclamo
                               join ReclamoAmbiental in context.ReclamoAmbiental on DetalleReclamo.ID_ReclamoAmbiental equals ReclamoAmbiental.IDReclamoAmbiental
                               join sesion in context.Sesion on reclamo.ID_Sesion equals sesion.IDSesion
                               join usuario in context.Usuario on sesion.ID_Usuario equals usuario.IDUsuario
                               where usuario.Nick.Contains(nombreUsuario)
                               select new
                               {
                                   IDDetalleReclamo = DetalleReclamo.IDDetalleReclamo,
                                   Descripcion = DetalleReclamo.Descripcion,
                                   Altura = DetalleReclamo.Altura,
                                   Longitud = DetalleReclamo.longitud,
                                   Latitud = DetalleReclamo.latitud,
                                   Direccion = DetalleReclamo.Direccion,
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
                                   dominio = DetalleReclamo.Dominio,
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


        // GET api/<FiltroNombreReclamosController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<FiltroNombreReclamosController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<FiltroNombreReclamosController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<FiltroNombreReclamosController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
