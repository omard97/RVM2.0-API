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
    public class FiltroRangoFechasController : ControllerBase
    {
        private readonly AppDbContext context;

        public FiltroRangoFechasController(AppDbContext context) {
            this.context = context;
        }

       



        // GET: api/<FiltroRangoFechasController>
        [HttpGet]
        public IActionResult Get(int idTipoReclamo, int idEstado, string fechaDesde, string fechaHasta, int idRol, string nombreUsuario, int idUsuario)
        {

            if (idRol == 1 || idRol == 2)
            {
                //reclamos del dia de hoy
                //ejemplo
                //https://localhost:44363/FiltroRangoFechas?idTipoReclamo=1&idEstado=1&fechaDesde=2021-10-13&fechaHasta=2021-10-22&idRol=1&nombreUsuario=-   Postman o Google

                if (idTipoReclamo != 0 && (idEstado != 14 && idEstado != 13) && fechaDesde != string.Empty && fechaHasta == null && nombreUsuario is null)
                { //camino correcto - cuando se solicita todo en un una fecha en espeficio (de cualquier usuario siendo admin)

                    if (idTipoReclamo == 2)
                    {
                        var _DetReclamo = (from DetalleReclamo in context.DetalleReclamo
                                           join reclamo in context.Reclamo on DetalleReclamo.ID_Reclamo equals reclamo.IDReclamo
                                           join estado in context.Estado on reclamo.ID_Estado equals estado.IDEstado
                                           join TipoReclamo in context.TipoReclamo on reclamo.ID_TipoReclamo equals TipoReclamo.IDTipoReclamo
                                           join ReclamoAmbiental in context.ReclamoAmbiental on DetalleReclamo.ID_ReclamoAmbiental equals ReclamoAmbiental.IDReclamoAmbiental
                                           join sesion in context.Sesion on reclamo.ID_Sesion equals sesion.IDSesion
                                           join usuario in context.Usuario on sesion.ID_Usuario equals usuario.IDUsuario
                                           join VehiculoXDetalleReclamo in context.VehiculoXDetalleReclamo on DetalleReclamo.IDDetalleReclamo equals VehiculoXDetalleReclamo.ID_DetalleReclamo
                                           join Vehiculo in context.Vehiculo on VehiculoXDetalleReclamo.ID_Vehiculo equals Vehiculo.IDVehiculo
                                           join ModeloVehiculo in context.ModeloVehiculo on Vehiculo.ID_Modelo equals ModeloVehiculo.IDModelo
                                           join MarcaVehiculo in context.MarcaVehiculo on Vehiculo.ID_MarcaVehiculo equals MarcaVehiculo.IDMarca

                                           where reclamo.ID_TipoReclamo == idTipoReclamo && reclamo.ID_Estado == idEstado && reclamo.Fecha == fechaDesde

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

                                               color = Vehiculo.Color,
                                               Modelo = ModeloVehiculo.Nombre,
                                               Marca = MarcaVehiculo.Nombre,


                                           }).OrderByDescending(ID => ID.ID_Reclamo);

                        if (_DetReclamo == null)
                        {
                            return NotFound();
                        }
                        return Ok(_DetReclamo);
                    }
                    else
                    {
                        //ambiental u otro tipo de reclamo
                        var _DetReclamo = (from DetalleReclamo in context.DetalleReclamo
                                           join reclamo in context.Reclamo on DetalleReclamo.ID_Reclamo equals reclamo.IDReclamo
                                           join estado in context.Estado on reclamo.ID_Estado equals estado.IDEstado
                                           join TipoReclamo in context.TipoReclamo on reclamo.ID_TipoReclamo equals TipoReclamo.IDTipoReclamo
                                           join ReclamoAmbiental in context.ReclamoAmbiental on DetalleReclamo.ID_ReclamoAmbiental equals ReclamoAmbiental.IDReclamoAmbiental
                                           join sesion in context.Sesion on reclamo.ID_Sesion equals sesion.IDSesion
                                           join usuario in context.Usuario on sesion.ID_Usuario equals usuario.IDUsuario
                                           
                                           where reclamo.ID_TipoReclamo == idTipoReclamo && reclamo.ID_Estado == idEstado && reclamo.Fecha == fechaDesde

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
                                              
                                           }).OrderByDescending(ID => ID.ID_Reclamo);

                        if (_DetReclamo == null)
                        {
                            return NotFound();
                        }
                        return Ok(_DetReclamo);
                    }

                }
                //Cuando el administrador busque reclamos por tiporeclamo, el estado, una fecha y el nombre de usuario
                else if (idTipoReclamo != 0 && (idEstado != 14 && idEstado != 13) && fechaDesde != string.Empty && fechaHasta == null && nombreUsuario != null)
                {
                    if (idTipoReclamo==2) {
                        var _DetReclamo = (from DetalleReclamo in context.DetalleReclamo
                                          join reclamo in context.Reclamo on DetalleReclamo.ID_Reclamo equals reclamo.IDReclamo
                                          join estado in context.Estado on reclamo.ID_Estado equals estado.IDEstado
                                          join TipoReclamo in context.TipoReclamo on reclamo.ID_TipoReclamo equals TipoReclamo.IDTipoReclamo
                                          join ReclamoAmbiental in context.ReclamoAmbiental on DetalleReclamo.ID_ReclamoAmbiental equals ReclamoAmbiental.IDReclamoAmbiental
                                          join sesion in context.Sesion on reclamo.ID_Sesion equals sesion.IDSesion
                                          join usuario in context.Usuario on sesion.ID_Usuario equals usuario.IDUsuario
                                           join VehiculoXDetalleReclamo in context.VehiculoXDetalleReclamo on DetalleReclamo.IDDetalleReclamo equals VehiculoXDetalleReclamo.ID_DetalleReclamo
                                           join Vehiculo in context.Vehiculo on VehiculoXDetalleReclamo.ID_Vehiculo equals Vehiculo.IDVehiculo
                                           join ModeloVehiculo in context.ModeloVehiculo on Vehiculo.ID_Modelo equals ModeloVehiculo.IDModelo
                                           join MarcaVehiculo in context.MarcaVehiculo on Vehiculo.ID_MarcaVehiculo equals MarcaVehiculo.IDMarca
                                           where reclamo.ID_TipoReclamo == idTipoReclamo && reclamo.ID_Estado == idEstado && reclamo.Fecha == fechaDesde &&
                                          usuario.Nick == nombreUsuario

                                          select new
                                          {
                                              IDDetalleReclamo = DetalleReclamo.IDDetalleReclamo,
                                              Descripcion = DetalleReclamo.Descripcion,
                                              Altura = DetalleReclamo.Altura,
                                              Longitud = DetalleReclamo.longitud,
                                              Latitud = DetalleReclamo.latitud,
                                              Direccion = DetalleReclamo.Direccion,
                                              ID_Reclamo = DetalleReclamo.ID_Reclamo,
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

                                              color = Vehiculo.Color,
                                              Modelo = ModeloVehiculo.Nombre,
                                              Marca = MarcaVehiculo.Nombre,
                                          }).OrderByDescending(ID => ID.ID_Reclamo);
                        if (_DetReclamo == null)
                        {
                            return NotFound();
                        }
                        return Ok(_DetReclamo);


                    }
                    else
                    {
                        //ambiental
                        var _DetReclamo = (from DetalleReclamo in context.DetalleReclamo
                                           join reclamo in context.Reclamo on DetalleReclamo.ID_Reclamo equals reclamo.IDReclamo
                                           join estado in context.Estado on reclamo.ID_Estado equals estado.IDEstado
                                           join TipoReclamo in context.TipoReclamo on reclamo.ID_TipoReclamo equals TipoReclamo.IDTipoReclamo
                                           join ReclamoAmbiental in context.ReclamoAmbiental on DetalleReclamo.ID_ReclamoAmbiental equals ReclamoAmbiental.IDReclamoAmbiental
                                           join sesion in context.Sesion on reclamo.ID_Sesion equals sesion.IDSesion
                                           join usuario in context.Usuario on sesion.ID_Usuario equals usuario.IDUsuario
                                           where reclamo.ID_TipoReclamo == idTipoReclamo && reclamo.ID_Estado == idEstado && reclamo.Fecha == fechaDesde &&
                                           usuario.Nick == nombreUsuario

                                           select new
                                           {
                                               IDDetalleReclamo = DetalleReclamo.IDDetalleReclamo,
                                               Descripcion = DetalleReclamo.Descripcion,
                                               Altura = DetalleReclamo.Altura,
                                               Longitud = DetalleReclamo.longitud,
                                               Latitud = DetalleReclamo.latitud,
                                               Direccion = DetalleReclamo.Direccion,
                                               ID_Reclamo = DetalleReclamo.ID_Reclamo,
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
                                               color= "",
                                               modelo=""
                                           }).OrderByDescending(ID => ID.ID_Reclamo);
                        if (_DetReclamo == null)
                        {
                            return NotFound();
                        }
                        return Ok(_DetReclamo);
                    }
                    
                }
                else if (idTipoReclamo != 0 && (idEstado == 14 || idEstado == 13) && fechaDesde != string.Empty && fechaHasta == null && nombreUsuario == null)
                {//cuando el administrador solicite tipo reclamo, estado (todos), fecha pero no el nombre de usuario

                    if (idTipoReclamo==2)
                    {

                        var _DetReclamo = (from DetalleReclamo in context.DetalleReclamo
                                           join reclamo in context.Reclamo on DetalleReclamo.ID_Reclamo equals reclamo.IDReclamo
                                           join estado in context.Estado on reclamo.ID_Estado equals estado.IDEstado
                                           join TipoReclamo in context.TipoReclamo on reclamo.ID_TipoReclamo equals TipoReclamo.IDTipoReclamo
                                           join ReclamoAmbiental in context.ReclamoAmbiental on DetalleReclamo.ID_ReclamoAmbiental equals ReclamoAmbiental.IDReclamoAmbiental
                                           join sesion in context.Sesion on reclamo.ID_Sesion equals sesion.IDSesion
                                           join usuario in context.Usuario on sesion.ID_Usuario equals usuario.IDUsuario
                                           join VehiculoXDetalleReclamo in context.VehiculoXDetalleReclamo on DetalleReclamo.IDDetalleReclamo equals VehiculoXDetalleReclamo.ID_DetalleReclamo
                                           join Vehiculo in context.Vehiculo on VehiculoXDetalleReclamo.ID_Vehiculo equals Vehiculo.IDVehiculo
                                           join ModeloVehiculo in context.ModeloVehiculo on Vehiculo.ID_Modelo equals ModeloVehiculo.IDModelo
                                           join MarcaVehiculo in context.MarcaVehiculo on Vehiculo.ID_MarcaVehiculo equals MarcaVehiculo.IDMarca
                                           where reclamo.ID_TipoReclamo == idTipoReclamo && reclamo.Fecha == fechaDesde

                                           select new
                                           {
                                               IDDetalleReclamo = DetalleReclamo.IDDetalleReclamo,
                                               Descripcion = DetalleReclamo.Descripcion,
                                               Altura = DetalleReclamo.Altura,
                                               Longitud = DetalleReclamo.longitud,
                                               Latitud = DetalleReclamo.latitud,
                                               Direccion = DetalleReclamo.Direccion,
                                               ID_Reclamo = DetalleReclamo.ID_Reclamo,
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
                                               color = Vehiculo.Color,
                                               Modelo = ModeloVehiculo.Nombre,
                                               Marca = MarcaVehiculo.Nombre,
                                           }).OrderByDescending(ID => ID.ID_Reclamo);

                        if (_DetReclamo == null)
                        {
                            return NotFound();
                        }
                        return Ok(_DetReclamo);
                    }
                    else
                    {
                        var _DetReclamo = (from DetalleReclamo in context.DetalleReclamo
                                           join reclamo in context.Reclamo on DetalleReclamo.ID_Reclamo equals reclamo.IDReclamo
                                           join estado in context.Estado on reclamo.ID_Estado equals estado.IDEstado
                                           join TipoReclamo in context.TipoReclamo on reclamo.ID_TipoReclamo equals TipoReclamo.IDTipoReclamo
                                           join ReclamoAmbiental in context.ReclamoAmbiental on DetalleReclamo.ID_ReclamoAmbiental equals ReclamoAmbiental.IDReclamoAmbiental
                                           join sesion in context.Sesion on reclamo.ID_Sesion equals sesion.IDSesion
                                           join usuario in context.Usuario on sesion.ID_Usuario equals usuario.IDUsuario

                                           where reclamo.ID_TipoReclamo == idTipoReclamo && reclamo.Fecha == fechaDesde

                                           select new
                                           {
                                               IDDetalleReclamo = DetalleReclamo.IDDetalleReclamo,
                                               Descripcion = DetalleReclamo.Descripcion,
                                               Altura = DetalleReclamo.Altura,
                                               Longitud = DetalleReclamo.longitud,
                                               Latitud = DetalleReclamo.latitud,
                                               Direccion = DetalleReclamo.Direccion,
                                               ID_Reclamo = DetalleReclamo.ID_Reclamo,
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


                    

                } else if (idTipoReclamo != 0 && (idEstado == 14 || idEstado == 13) && fechaDesde != string.Empty && fechaHasta == null && nombreUsuario != null)
                {//cuando el administrador solicite tipo reclamo, estado (todos), fecha y el nombre de usuario

                    if (idTipoReclamo == 2)
                    {
                        var _DetReclamo = (from DetalleReclamo in context.DetalleReclamo
                                           join reclamo in context.Reclamo on DetalleReclamo.ID_Reclamo equals reclamo.IDReclamo
                                           join estado in context.Estado on reclamo.ID_Estado equals estado.IDEstado
                                           join TipoReclamo in context.TipoReclamo on reclamo.ID_TipoReclamo equals TipoReclamo.IDTipoReclamo
                                           join ReclamoAmbiental in context.ReclamoAmbiental on DetalleReclamo.ID_ReclamoAmbiental equals ReclamoAmbiental.IDReclamoAmbiental
                                           join sesion in context.Sesion on reclamo.ID_Sesion equals sesion.IDSesion
                                           join usuario in context.Usuario on sesion.ID_Usuario equals usuario.IDUsuario
                                           join VehiculoXDetalleReclamo in context.VehiculoXDetalleReclamo on DetalleReclamo.IDDetalleReclamo equals VehiculoXDetalleReclamo.ID_DetalleReclamo
                                           join Vehiculo in context.Vehiculo on VehiculoXDetalleReclamo.ID_Vehiculo equals Vehiculo.IDVehiculo
                                           join ModeloVehiculo in context.ModeloVehiculo on Vehiculo.ID_Modelo equals ModeloVehiculo.IDModelo
                                           join MarcaVehiculo in context.MarcaVehiculo on Vehiculo.ID_MarcaVehiculo equals MarcaVehiculo.IDMarca
                                           where reclamo.ID_TipoReclamo == idTipoReclamo && reclamo.Fecha == fechaDesde && usuario.Nick == nombreUsuario

                                           select new
                                           {
                                               IDDetalleReclamo = DetalleReclamo.IDDetalleReclamo,
                                               Descripcion = DetalleReclamo.Descripcion,
                                               Altura = DetalleReclamo.Altura,
                                               Longitud = DetalleReclamo.longitud,
                                               Latitud = DetalleReclamo.latitud,
                                               Direccion = DetalleReclamo.Direccion,
                                               ID_Reclamo = DetalleReclamo.ID_Reclamo,
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
                                               color = Vehiculo.Color,
                                               Modelo = ModeloVehiculo.Nombre,
                                               Marca = MarcaVehiculo.Nombre,
                                           }).OrderByDescending(ID => ID.ID_Reclamo);

                        if (_DetReclamo == null)
                        {
                            return NotFound();
                        }
                        return Ok(_DetReclamo);
                    }
                    else
                    {
                        var _DetReclamo = (from DetalleReclamo in context.DetalleReclamo
                                           join reclamo in context.Reclamo on DetalleReclamo.ID_Reclamo equals reclamo.IDReclamo
                                           join estado in context.Estado on reclamo.ID_Estado equals estado.IDEstado
                                           join TipoReclamo in context.TipoReclamo on reclamo.ID_TipoReclamo equals TipoReclamo.IDTipoReclamo
                                           join ReclamoAmbiental in context.ReclamoAmbiental on DetalleReclamo.ID_ReclamoAmbiental equals ReclamoAmbiental.IDReclamoAmbiental
                                           join sesion in context.Sesion on reclamo.ID_Sesion equals sesion.IDSesion
                                           join usuario in context.Usuario on sesion.ID_Usuario equals usuario.IDUsuario
                                           where reclamo.ID_TipoReclamo == idTipoReclamo && reclamo.Fecha == fechaDesde && usuario.Nick == nombreUsuario

                                           select new
                                           {
                                               IDDetalleReclamo = DetalleReclamo.IDDetalleReclamo,
                                               Descripcion = DetalleReclamo.Descripcion,
                                               Altura = DetalleReclamo.Altura,
                                               Longitud = DetalleReclamo.longitud,
                                               Latitud = DetalleReclamo.latitud,
                                               Direccion = DetalleReclamo.Direccion,
                                               ID_Reclamo = DetalleReclamo.ID_Reclamo,
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
                    
                }
                else
                {
                    return NotFound();
                }
            }
            else //siendo usuario Idrol==3
            {
                if (idTipoReclamo != 0 && fechaDesde != null && idRol == 3 && idUsuario != 0 && (idEstado != 14 && idEstado != 13))
                {//siendo usuario traigo todos los reclamos con respecto a la fecha dada con su tipo de reclamo y el estado (pendiente, en revision, etc?
                 //ej: tiporeclamo: ambiental/vial estado:pendiente fecha:22-10-11

                    if (idTipoReclamo == 2)
                    {
                        //vehicular
                        var _DetReclamo = (from DetalleReclamo in context.DetalleReclamo
                                           join reclamo in context.Reclamo on DetalleReclamo.ID_Reclamo equals reclamo.IDReclamo
                                           join estado in context.Estado on reclamo.ID_Estado equals estado.IDEstado
                                           join TipoReclamo in context.TipoReclamo on reclamo.ID_TipoReclamo equals TipoReclamo.IDTipoReclamo
                                           join ReclamoAmbiental in context.ReclamoAmbiental on DetalleReclamo.ID_ReclamoAmbiental equals ReclamoAmbiental.IDReclamoAmbiental
                                           join sesion in context.Sesion on reclamo.ID_Sesion equals sesion.IDSesion
                                           join usuario in context.Usuario on sesion.ID_Usuario equals usuario.IDUsuario
                                           join VehiculoXDetalleReclamo in context.VehiculoXDetalleReclamo on DetalleReclamo.IDDetalleReclamo equals VehiculoXDetalleReclamo.ID_DetalleReclamo
                                           join Vehiculo in context.Vehiculo on VehiculoXDetalleReclamo.ID_Vehiculo equals Vehiculo.IDVehiculo
                                           join ModeloVehiculo in context.ModeloVehiculo on Vehiculo.ID_Modelo equals ModeloVehiculo.IDModelo
                                           join MarcaVehiculo in context.MarcaVehiculo on Vehiculo.ID_MarcaVehiculo equals MarcaVehiculo.IDMarca
                                           where reclamo.Fecha == fechaDesde && sesion.ID_Usuario == idUsuario && reclamo.ID_Estado == idEstado
                                           && reclamo.ID_TipoReclamo == idTipoReclamo

                                           select new
                                           {
                                               IDDetalleReclamo = DetalleReclamo.IDDetalleReclamo,
                                               Descripcion = DetalleReclamo.Descripcion,
                                               Altura = DetalleReclamo.Altura,
                                               Longitud = DetalleReclamo.longitud,
                                               Latitud = DetalleReclamo.latitud,
                                               Direccion = DetalleReclamo.Direccion,
                                               ID_Reclamo = DetalleReclamo.ID_Reclamo,
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
                                               color = Vehiculo.Color,
                                               Modelo = ModeloVehiculo.Nombre,
                                               Marca = MarcaVehiculo.Nombre,
                                           }).OrderByDescending(ID => ID.ID_Reclamo);

                        if (_DetReclamo == null)
                        {
                            return NotFound();
                        }
                        return Ok(_DetReclamo);
                    }
                    else
                    {
                        //ambiental
                        var _DetReclamo = (from DetalleReclamo in context.DetalleReclamo
                                           join reclamo in context.Reclamo on DetalleReclamo.ID_Reclamo equals reclamo.IDReclamo
                                           join estado in context.Estado on reclamo.ID_Estado equals estado.IDEstado
                                           join TipoReclamo in context.TipoReclamo on reclamo.ID_TipoReclamo equals TipoReclamo.IDTipoReclamo
                                           join ReclamoAmbiental in context.ReclamoAmbiental on DetalleReclamo.ID_ReclamoAmbiental equals ReclamoAmbiental.IDReclamoAmbiental
                                           join sesion in context.Sesion on reclamo.ID_Sesion equals sesion.IDSesion
                                           join usuario in context.Usuario on sesion.ID_Usuario equals usuario.IDUsuario
                                           where reclamo.Fecha == fechaDesde && sesion.ID_Usuario == idUsuario && reclamo.ID_Estado == idEstado
                                           && reclamo.ID_TipoReclamo == idTipoReclamo

                                           select new
                                           {
                                               IDDetalleReclamo = DetalleReclamo.IDDetalleReclamo,
                                               Descripcion = DetalleReclamo.Descripcion,
                                               Altura = DetalleReclamo.Altura,
                                               Longitud = DetalleReclamo.longitud,
                                               Latitud = DetalleReclamo.latitud,
                                               Direccion = DetalleReclamo.Direccion,
                                               ID_Reclamo = DetalleReclamo.ID_Reclamo,
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
                   

                } else if (idTipoReclamo != 0 && fechaDesde != null && idRol == 3 && idUsuario != 0 && (idEstado == 14 || idEstado == 13))
                {//siendo usuario traigo todos los reclamos con respecto a la fecha dada con su tipo de reclamo y el estado (todos -> todos los estados)
                 //ej: tiporeclamo: ambiental/vial estado:Todos fecha:2022-10-11

                    if (idTipoReclamo ==2)
                    {
                        var _DetReclamo = (from DetalleReclamo in context.DetalleReclamo
                                           join reclamo in context.Reclamo on DetalleReclamo.ID_Reclamo equals reclamo.IDReclamo
                                           join estado in context.Estado on reclamo.ID_Estado equals estado.IDEstado
                                           join TipoReclamo in context.TipoReclamo on reclamo.ID_TipoReclamo equals TipoReclamo.IDTipoReclamo
                                           join ReclamoAmbiental in context.ReclamoAmbiental on DetalleReclamo.ID_ReclamoAmbiental equals ReclamoAmbiental.IDReclamoAmbiental
                                           join sesion in context.Sesion on reclamo.ID_Sesion equals sesion.IDSesion
                                           join usuario in context.Usuario on sesion.ID_Usuario equals usuario.IDUsuario
                                           join VehiculoXDetalleReclamo in context.VehiculoXDetalleReclamo on DetalleReclamo.IDDetalleReclamo equals VehiculoXDetalleReclamo.ID_DetalleReclamo
                                           join Vehiculo in context.Vehiculo on VehiculoXDetalleReclamo.ID_Vehiculo equals Vehiculo.IDVehiculo
                                           join ModeloVehiculo in context.ModeloVehiculo on Vehiculo.ID_Modelo equals ModeloVehiculo.IDModelo
                                           join MarcaVehiculo in context.MarcaVehiculo on Vehiculo.ID_MarcaVehiculo equals MarcaVehiculo.IDMarca
                                           where reclamo.Fecha == fechaDesde && sesion.ID_Usuario == idUsuario

                                           select new
                                           {
                                               IDDetalleReclamo = DetalleReclamo.IDDetalleReclamo,
                                               Descripcion = DetalleReclamo.Descripcion,
                                               Altura = DetalleReclamo.Altura,
                                               Longitud = DetalleReclamo.longitud,
                                               Latitud = DetalleReclamo.latitud,
                                               Direccion = DetalleReclamo.Direccion,
                                               ID_Reclamo = DetalleReclamo.ID_Reclamo,
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
                                               color = Vehiculo.Color,
                                               Modelo = ModeloVehiculo.Nombre,
                                               Marca = MarcaVehiculo.Nombre,
                                           }).OrderByDescending(ID => ID.ID_Reclamo);

                        if (_DetReclamo == null)
                        {
                            return NotFound();
                        }
                        return Ok(_DetReclamo);
                    }
                    else
                    {
                        var _DetReclamo = (from DetalleReclamo in context.DetalleReclamo
                                           join reclamo in context.Reclamo on DetalleReclamo.ID_Reclamo equals reclamo.IDReclamo
                                           join estado in context.Estado on reclamo.ID_Estado equals estado.IDEstado
                                           join TipoReclamo in context.TipoReclamo on reclamo.ID_TipoReclamo equals TipoReclamo.IDTipoReclamo
                                           join ReclamoAmbiental in context.ReclamoAmbiental on DetalleReclamo.ID_ReclamoAmbiental equals ReclamoAmbiental.IDReclamoAmbiental
                                           join sesion in context.Sesion on reclamo.ID_Sesion equals sesion.IDSesion
                                           join usuario in context.Usuario on sesion.ID_Usuario equals usuario.IDUsuario
                                           where reclamo.Fecha == fechaDesde && sesion.ID_Usuario == idUsuario

                                           select new
                                           {
                                               IDDetalleReclamo = DetalleReclamo.IDDetalleReclamo,
                                               Descripcion = DetalleReclamo.Descripcion,
                                               Altura = DetalleReclamo.Altura,
                                               Longitud = DetalleReclamo.longitud,
                                               Latitud = DetalleReclamo.latitud,
                                               Direccion = DetalleReclamo.Direccion,
                                               ID_Reclamo = DetalleReclamo.ID_Reclamo,
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

                   

                }
                else
                {
                    return NotFound();
                }



            }

        }

        // GET api/<FiltroRangoFechasController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<FiltroRangoFechasController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<FiltroRangoFechasController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<FiltroRangoFechasController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }

   
    
}


