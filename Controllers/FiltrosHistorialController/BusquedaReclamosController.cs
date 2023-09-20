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

namespace ApiRVM2019.Controllers.FiltrosHistorialController
{
    [EnableCors("All")]
    [Route("[controller]")]
    [ApiController]
    public class FiltrosReclamosController : ControllerBase
    {
        private readonly AppDbContext context;

        public FiltrosReclamosController (AppDbContext context)
        {
            this.context = context;
        }
        // GET: api/<FiltrosReclamosController>
        [HttpGet]
        public IActionResult Get(int idTipoR, int idEstado, string nombreUsuario, int idRol,int idUsuario)
        {//se obtienen los reclamos filtrados por tipo y estado y nombre de usuario siendo administrador
            if (idRol==1 || idRol==2)
            {
                if ((idEstado != 15 && idEstado != 14) && nombreUsuario == null)
                {//cuando el admin solicita los reclamos del tipo de reclamo, su estado pero NO el usuario y no selecciona TODOS
                    var _DetReclamo = (from DetalleReclamo in context.DetalleReclamo
                                       join reclamo in context.Reclamo on DetalleReclamo.ID_Reclamo equals reclamo.IDReclamo
                                       join estado in context.Estado on reclamo.ID_Estado equals estado.IDEstado
                                       join TipoReclamo in context.TipoReclamo on reclamo.ID_TipoReclamo equals TipoReclamo.IDTipoReclamo
                                       join ReclamoAmbiental in context.ReclamoAmbiental on DetalleReclamo.ID_ReclamoAmbiental equals ReclamoAmbiental.IDReclamoAmbiental
                                       join sesion in context.Sesion on reclamo.ID_Sesion equals sesion.IDSesion
                                       join usuario in context.Usuario on sesion.ID_Usuario equals usuario.IDUsuario
                                       where reclamo.ID_TipoReclamo == idTipoR && reclamo.ID_Estado == idEstado
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
                                           dominio = DetalleReclamo.Dominio
                                       }).OrderByDescending(ID => ID.ID_Reclamo);

                    if (_DetReclamo == null)
                    {
                        return NotFound();
                    }
                    return Ok(_DetReclamo);
                }
                else if ((idEstado == 15 || idEstado == 14) && nombreUsuario == null)
                {//cuando el admin solicita los reclamos del tipo de reclamo, su estado pero NO el usuario y SI selecciona TODOS
                    var _DetReclamo = (from DetalleReclamo in context.DetalleReclamo
                                       join reclamo in context.Reclamo on DetalleReclamo.ID_Reclamo equals reclamo.IDReclamo
                                       join estado in context.Estado on reclamo.ID_Estado equals estado.IDEstado
                                       join TipoReclamo in context.TipoReclamo on reclamo.ID_TipoReclamo equals TipoReclamo.IDTipoReclamo
                                       join ReclamoAmbiental in context.ReclamoAmbiental on DetalleReclamo.ID_ReclamoAmbiental equals ReclamoAmbiental.IDReclamoAmbiental
                                       join sesion in context.Sesion on reclamo.ID_Sesion equals sesion.IDSesion
                                       join usuario in context.Usuario on sesion.ID_Usuario equals usuario.IDUsuario
                                       where reclamo.ID_TipoReclamo == idTipoR
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
                                           dominio = DetalleReclamo.Dominio
                                       }).OrderByDescending(ID => ID.ID_Reclamo);


                    if (_DetReclamo == null)
                    {
                        return NotFound();
                    }
                    return Ok(_DetReclamo);


                }
                else if ((idEstado != 15 && idEstado != 14) && nombreUsuario != null)
                {//cuando el admin solicita los reclamos del tipo de reclamo, su estado pero SI el usuario y NO selecciona TODOS
                    var _DetReclamo = (from DetalleReclamo in context.DetalleReclamo
                                       join reclamo in context.Reclamo on DetalleReclamo.ID_Reclamo equals reclamo.IDReclamo
                                       join estado in context.Estado on reclamo.ID_Estado equals estado.IDEstado
                                       join TipoReclamo in context.TipoReclamo on reclamo.ID_TipoReclamo equals TipoReclamo.IDTipoReclamo
                                       join ReclamoAmbiental in context.ReclamoAmbiental on DetalleReclamo.ID_ReclamoAmbiental equals ReclamoAmbiental.IDReclamoAmbiental
                                       join sesion in context.Sesion on reclamo.ID_Sesion equals sesion.IDSesion
                                       join usuario in context.Usuario on sesion.ID_Usuario equals usuario.IDUsuario
                                       where reclamo.ID_TipoReclamo == idTipoR && reclamo.ID_Estado == idEstado && usuario.Nick == nombreUsuario
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
                                           dominio = DetalleReclamo.Dominio
                                       }).OrderByDescending(ID => ID.ID_Reclamo);


                    if (_DetReclamo == null)
                    {
                        return NotFound();
                    }
                    return Ok(_DetReclamo);
                }
                else if ((idEstado == 15 || idEstado == 14) && nombreUsuario != null)
                {//cuando el admin solicita los reclamos del tipo de reclamo, su estado pero SI el usuario y SI selecciona TODOS
                    var _DetReclamo = (from DetalleReclamo in context.DetalleReclamo
                                       join reclamo in context.Reclamo on DetalleReclamo.ID_Reclamo equals reclamo.IDReclamo
                                       join estado in context.Estado on reclamo.ID_Estado equals estado.IDEstado
                                       join TipoReclamo in context.TipoReclamo on reclamo.ID_TipoReclamo equals TipoReclamo.IDTipoReclamo
                                       join ReclamoAmbiental in context.ReclamoAmbiental on DetalleReclamo.ID_ReclamoAmbiental equals ReclamoAmbiental.IDReclamoAmbiental
                                       join sesion in context.Sesion on reclamo.ID_Sesion equals sesion.IDSesion
                                       join usuario in context.Usuario on sesion.ID_Usuario equals usuario.IDUsuario
                                       where reclamo.ID_TipoReclamo == idTipoR && usuario.Nick == nombreUsuario
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
                                           dominio = DetalleReclamo.Dominio
                                       }).OrderByDescending(ID => ID.ID_Reclamo);


                    if (_DetReclamo == null)
                    {
                        return NotFound();
                    }
                    return Ok(_DetReclamo);
                }
                else
                {
                    return NotFound();
                }

            }
            else if(idRol==3) //acciones del usuario
            {
                if((idEstado != 15 && idEstado != 14))
                {//cuando el usuario solicita los reclamos del tipo de reclamo, su estado y no selecciona TODOS
                    var _DetReclamo = (from DetalleReclamo in context.DetalleReclamo
                                       join reclamo in context.Reclamo on DetalleReclamo.ID_Reclamo equals reclamo.IDReclamo
                                       join estado in context.Estado on reclamo.ID_Estado equals estado.IDEstado
                                       join TipoReclamo in context.TipoReclamo on reclamo.ID_TipoReclamo equals TipoReclamo.IDTipoReclamo
                                       join ReclamoAmbiental in context.ReclamoAmbiental on DetalleReclamo.ID_ReclamoAmbiental equals ReclamoAmbiental.IDReclamoAmbiental
                                       join sesion in context.Sesion on reclamo.ID_Sesion equals sesion.IDSesion
                                       join usuario in context.Usuario on sesion.ID_Usuario equals usuario.IDUsuario
                                       where reclamo.ID_TipoReclamo == idTipoR && reclamo.ID_Estado == idEstado && usuario.IDUsuario==idUsuario
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
                                           id_sesion= reclamo.ID_Sesion,
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
                                       }).OrderByDescending(ID => ID.ID_Reclamo);

                    if (_DetReclamo == null)
                    {
                        return NotFound();
                    }
                    return Ok(_DetReclamo);
                }
                else if ((idEstado == 15 || idEstado == 14))
                { ////cuando el usuario solicita los reclamos del tipo de reclamo, su estado y SI selecciona TODOS
                    var _DetReclamo = (from DetalleReclamo in context.DetalleReclamo
                                       join reclamo in context.Reclamo on DetalleReclamo.ID_Reclamo equals reclamo.IDReclamo
                                       join estado in context.Estado on reclamo.ID_Estado equals estado.IDEstado
                                       join TipoReclamo in context.TipoReclamo on reclamo.ID_TipoReclamo equals TipoReclamo.IDTipoReclamo
                                       join ReclamoAmbiental in context.ReclamoAmbiental on DetalleReclamo.ID_ReclamoAmbiental equals ReclamoAmbiental.IDReclamoAmbiental
                                       join sesion in context.Sesion on reclamo.ID_Sesion equals sesion.IDSesion
                                       join usuario in context.Usuario on sesion.ID_Usuario equals usuario.IDUsuario
                                       where reclamo.ID_TipoReclamo == idTipoR && usuario.IDUsuario==idUsuario
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
                                           dominio = DetalleReclamo.Dominio
                                       }).OrderByDescending(ID => ID.ID_Reclamo);


                    if (_DetReclamo == null)
                    {
                        return NotFound();
                    }
                    return Ok(_DetReclamo);
                }
                else
                {
                    return NotFound();
                }
            }
            else
            {
                return NotFound();
            }
           
           
        }

        // GET api/<FiltrosReclamosController>/5
        [HttpGet("{idTipoR}/{idEstado}/{idUsuario}")]//+fechaInicio+'/'+fechaFin
        public IActionResult Get(int idTipoR, int idEstado, int idUsuario) //se obtienen los reclamos filtrados por tipo reclamo, estado por el usuario logueado
        {
            var _DetReclamo = (from DetalleReclamo in context.DetalleReclamo
                               join reclamo in context.Reclamo on DetalleReclamo.ID_Reclamo equals reclamo.IDReclamo
                               join estado in context.Estado on reclamo.ID_Estado equals estado.IDEstado
                               join TipoReclamo in context.TipoReclamo on reclamo.ID_TipoReclamo equals TipoReclamo.IDTipoReclamo
                               join ReclamoAmbiental in context.ReclamoAmbiental on DetalleReclamo.ID_ReclamoAmbiental equals ReclamoAmbiental.IDReclamoAmbiental
                               join sesion in context.Sesion on reclamo.ID_Sesion equals sesion.IDSesion
                               join usuario in context.Usuario on sesion.ID_Usuario equals usuario.IDUsuario
                               where reclamo.ID_TipoReclamo == idTipoR && reclamo.ID_Estado == idEstado && usuario.IDUsuario==idUsuario
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
                                   dominio = DetalleReclamo.Dominio
                               }).OrderByDescending(ID => ID.ID_Reclamo);


            if (_DetReclamo == null)
            {
                return NotFound();
            }
            return Ok(_DetReclamo);
        }

        // POST api/<FiltrosReclamosController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<FiltrosReclamosController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<FiltrosReclamosController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
