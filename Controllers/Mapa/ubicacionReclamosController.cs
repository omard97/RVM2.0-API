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

namespace ApiRVM2019.Controllers.Mapa
{
    [EnableCors("All")]
    [Route("[controller]")]
    [ApiController]
    public class ubicacionReclamosController : ControllerBase
    {
        private readonly AppDbContext context;
        public ubicacionReclamosController(AppDbContext context)
        {
            this.context = context;
        }
        // GET: api/<ubicacionReclamosController>
        [HttpGet]
        public IActionResult getReclamos(int idUsuario, int idRol)
        {
            //Utilizado al cargar la pantalla de Mapas, se visualizan todos los reclamos que hay en el sistema siendo administrador 
            // o solamente del usuario (que no es administrador) logueado

            //Al ser administrador devuelve todos los reclamos 
            if (idRol==1)
            {
                var reclamos = from Reclamo in context.Reclamo
                               join DetalleReclamo in context.DetalleReclamo on Reclamo.IDReclamo equals DetalleReclamo.ID_Reclamo
                               join Sesion in context.Sesion on Reclamo.ID_Sesion equals Sesion.IDSesion
                               join Usuario in context.Usuario on Sesion.ID_Usuario equals Usuario.IDUsuario
                               join TipoReclamo in context.TipoReclamo on Reclamo.ID_TipoReclamo equals TipoReclamo.IDTipoReclamo
                               join ReclamoAmbiental in context.ReclamoAmbiental on DetalleReclamo.ID_ReclamoAmbiental equals ReclamoAmbiental.IDReclamoAmbiental
                               join Estado in context.Estado on Reclamo.ID_Estado equals Estado.IDEstado
                               
                               select new
                               {
                                   IDDetalleReclamo = DetalleReclamo.IDDetalleReclamo,
                                   Descripcion = DetalleReclamo.Descripcion,
                                   Altura = DetalleReclamo.Altura,
                                   Longitud = DetalleReclamo.longitud,
                                   Latitud = DetalleReclamo.latitud,
                                   Direccion = DetalleReclamo.Direccion,
                                   ID_Reclamo = DetalleReclamo.ID_Reclamo,
                                   idReclamo = Reclamo.IDReclamo,
                                   id_sesion = Reclamo.ID_Sesion,
                                   Fecha = Reclamo.Fecha,
                                   Hora = Reclamo.Hora,
                                   Nombre = Estado.Nombre,
                                   IDEstado = Estado.IDEstado,
                                   NombreTRec = TipoReclamo.Nombre,
                                   IDTipoRec = TipoReclamo.IDTipoReclamo,
                                   IDRecAmb = ReclamoAmbiental.IDReclamoAmbiental,
                                   NombreRecAmbiental = ReclamoAmbiental.Nombre,
                                   Nick = Usuario.Nick,
                                   Foto = Reclamo.Foto,
                                   dominio = DetalleReclamo.Dominio,

                               };

                if (reclamos == null)
                {
                    return NotFound();
                }

                return Ok(reclamos);
            }
            else if (idRol == 3 )
            {//Al ser Usuario devuelve todos los reclamos de ese usuario
                var reclamos = from Reclamo in context.Reclamo
                               join DetalleReclamo in context.DetalleReclamo on Reclamo.IDReclamo equals DetalleReclamo.ID_Reclamo
                               join Sesion in context.Sesion on Reclamo.ID_Sesion equals Sesion.IDSesion
                               join Usuario in context.Usuario on Sesion.ID_Usuario equals Usuario.IDUsuario
                               join TipoReclamo in context.TipoReclamo on Reclamo.ID_TipoReclamo equals TipoReclamo.IDTipoReclamo
                               join ReclamoAmbiental in context.ReclamoAmbiental on DetalleReclamo.ID_ReclamoAmbiental equals ReclamoAmbiental.IDReclamoAmbiental
                               join Estado in context.Estado on Reclamo.ID_Estado equals Estado.IDEstado
                               where Usuario.IDUsuario == idUsuario
                               select new
                               {
                                   IDDetalleReclamo = DetalleReclamo.IDDetalleReclamo,
                                   Descripcion = DetalleReclamo.Descripcion,
                                   Altura = DetalleReclamo.Altura,
                                   Longitud = DetalleReclamo.longitud,
                                   Latitud = DetalleReclamo.latitud,
                                   Direccion = DetalleReclamo.Direccion,
                                   ID_Reclamo = DetalleReclamo.ID_Reclamo,
                                   idReclamo = Reclamo.IDReclamo,
                                   id_sesion = Reclamo.ID_Sesion,
                                   Fecha = Reclamo.Fecha,
                                   Hora = Reclamo.Hora,
                                   Nombre = Estado.Nombre,
                                   IDEstado = Estado.IDEstado,
                                   NombreTRec = TipoReclamo.Nombre,
                                   IDTipoRec = TipoReclamo.IDTipoReclamo,
                                   IDRecAmb = ReclamoAmbiental.IDReclamoAmbiental,
                                   NombreRecAmbiental = ReclamoAmbiental.Nombre,
                                   Nick = Usuario.Nick,
                                   Foto = Reclamo.Foto,
                                   dominio = DetalleReclamo.Dominio,

                               };

                 if (reclamos == null)
                {
                    return NotFound();
                }


                return Ok(reclamos);
            }
            else
            {
                
                
                    return NotFound();
                
            }
            
        }

        // GET api/<ubicacionReclamosController>/5
        [HttpGet("{idUsuario}/{idRol}/{iDTipoEstado}/{iDEstado}/{fecha}")]
        public IActionResult getMarcadoresFiltro(int idUsuario, int idRol, int iDTipoEstado, int iDEstado, string fecha)
        {
            //Utilizado cuando en la pantalla de MAPAS el usuario o administrador quieren filtrar las ubicaciones de los reclamos

            if(idUsuario!=0 || idRol!=0|| iDTipoEstado!=0 ||iDEstado!=0 || fecha != "-")
            {
                //Adminitrador
                if (idRol == 1)
                {
                    //tipo de estado - estado = TODOS pero No ingresa fecha
                    if (iDTipoEstado != 0 && (iDEstado == 13 || iDEstado == 14) && fecha == "-") // obtengo todos los reclamos de todos los estados de cualquier fecha porque el usuario no ingreso una fecha
                    {

                        var reclamos = from Reclamo in context.Reclamo
                                       join DetalleReclamo in context.DetalleReclamo on Reclamo.IDReclamo equals DetalleReclamo.ID_Reclamo
                                       join Sesion in context.Sesion on Reclamo.ID_Sesion equals Sesion.IDSesion
                                       join Usuario in context.Usuario on Sesion.ID_Usuario equals Usuario.IDUsuario
                                       join TipoReclamo in context.TipoReclamo on Reclamo.ID_TipoReclamo equals TipoReclamo.IDTipoReclamo
                                       join ReclamoAmbiental in context.ReclamoAmbiental on DetalleReclamo.ID_ReclamoAmbiental equals ReclamoAmbiental.IDReclamoAmbiental
                                       join Estado in context.Estado on Reclamo.ID_Estado equals Estado.IDEstado
                                       join TipoEstado in context.TipoEstado on Estado.ID_TipoEstado equals TipoEstado.IDTipoEstado
                                       where (TipoEstado.IDTipoEstado == iDTipoEstado && Reclamo.ID_Estado == iDEstado)
                                       select new
                                       {
                                           IDDetalleReclamo = DetalleReclamo.IDDetalleReclamo,
                                           Descripcion = DetalleReclamo.Descripcion,
                                           Altura = DetalleReclamo.Altura,
                                           Longitud = DetalleReclamo.longitud,
                                           Latitud = DetalleReclamo.latitud,
                                           Direccion = DetalleReclamo.Direccion,
                                           ID_Reclamo = DetalleReclamo.ID_Reclamo,
                                           idReclamo = Reclamo.IDReclamo,
                                           id_sesion = Reclamo.ID_Sesion,
                                           Fecha = Reclamo.Fecha,
                                           Hora = Reclamo.Hora,
                                           Nombre = Estado.Nombre,
                                           IDEstado = Estado.IDEstado,
                                           NombreTRec = TipoReclamo.Nombre,
                                           IDTipoRec = TipoReclamo.IDTipoReclamo,
                                           IDRecAmb = ReclamoAmbiental.IDReclamoAmbiental,
                                           NombreRecAmbiental = ReclamoAmbiental.Nombre,
                                           Nick = Usuario.Nick,
                                           Foto = Reclamo.Foto,
                                           dominio = DetalleReclamo.Dominio,

                                       };

                        if (reclamos == null)
                        {
                            return NotFound();
                        }
                        return Ok(reclamos);
                    }
                    else if (iDTipoEstado != 0 && (iDEstado == 13 || iDEstado == 14) && fecha != "-")//Administrador - Todos los reclamos de todos los estados pero con fecha
                    {
                        var reclamos = from Reclamo in context.Reclamo
                                       join DetalleReclamo in context.DetalleReclamo on Reclamo.IDReclamo equals DetalleReclamo.ID_Reclamo
                                       join Sesion in context.Sesion on Reclamo.ID_Sesion equals Sesion.IDSesion
                                       join Usuario in context.Usuario on Sesion.ID_Usuario equals Usuario.IDUsuario
                                       join TipoReclamo in context.TipoReclamo on Reclamo.ID_TipoReclamo equals TipoReclamo.IDTipoReclamo
                                       join ReclamoAmbiental in context.ReclamoAmbiental on DetalleReclamo.ID_ReclamoAmbiental equals ReclamoAmbiental.IDReclamoAmbiental
                                       join Estado in context.Estado on Reclamo.ID_Estado equals Estado.IDEstado
                                       join TipoEstado in context.TipoEstado on Estado.ID_TipoEstado equals TipoEstado.IDTipoEstado
                                       where (TipoEstado.IDTipoEstado == iDTipoEstado && Reclamo.ID_Estado == iDEstado) && Reclamo.Fecha == fecha
                                       select new
                                       {
                                           IDDetalleReclamo = DetalleReclamo.IDDetalleReclamo,
                                           Descripcion = DetalleReclamo.Descripcion,
                                           Altura = DetalleReclamo.Altura,
                                           Longitud = DetalleReclamo.longitud,
                                           Latitud = DetalleReclamo.latitud,
                                           Direccion = DetalleReclamo.Direccion,
                                           ID_Reclamo = DetalleReclamo.ID_Reclamo,
                                           idReclamo = Reclamo.IDReclamo,
                                           id_sesion = Reclamo.ID_Sesion,
                                           Fecha = Reclamo.Fecha,
                                           Hora = Reclamo.Hora,
                                           Nombre = Estado.Nombre,
                                           IDEstado = Estado.IDEstado,
                                           NombreTRec = TipoReclamo.Nombre,
                                           IDTipoRec = TipoReclamo.IDTipoReclamo,
                                           IDRecAmb = ReclamoAmbiental.IDReclamoAmbiental,
                                           NombreRecAmbiental = ReclamoAmbiental.Nombre,
                                           Nick = Usuario.Nick,
                                           Foto = Reclamo.Foto,
                                           dominio = DetalleReclamo.Dominio,

                                       };

                        if (reclamos == null)
                        {
                            return NotFound();
                        }
                        return Ok(reclamos);
                    }

                    //Busqueda pero sin ingresar el estado de TODOS
                    //Ingresa el tipo de estado y su respectivo estado sin agregar la FECHA
                    if ((iDTipoEstado != 0 && iDEstado != 0) && fecha == "-") // - indica que no ingresó la fecha
                    {
                        var reclamos = from Reclamo in context.Reclamo
                                       join DetalleReclamo in context.DetalleReclamo on Reclamo.IDReclamo equals DetalleReclamo.ID_Reclamo
                                       join Sesion in context.Sesion on Reclamo.ID_Sesion equals Sesion.IDSesion
                                       join Usuario in context.Usuario on Sesion.ID_Usuario equals Usuario.IDUsuario
                                       join TipoReclamo in context.TipoReclamo on Reclamo.ID_TipoReclamo equals TipoReclamo.IDTipoReclamo
                                       join ReclamoAmbiental in context.ReclamoAmbiental on DetalleReclamo.ID_ReclamoAmbiental equals ReclamoAmbiental.IDReclamoAmbiental
                                       join Estado in context.Estado on Reclamo.ID_Estado equals Estado.IDEstado
                                       join TipoEstado in context.TipoEstado on Estado.ID_TipoEstado equals TipoEstado.IDTipoEstado
                                       where (TipoEstado.IDTipoEstado == iDTipoEstado && Reclamo.ID_Estado == iDEstado)
                                       select new
                                       {
                                           IDDetalleReclamo = DetalleReclamo.IDDetalleReclamo,
                                           Descripcion = DetalleReclamo.Descripcion,
                                           Altura = DetalleReclamo.Altura,
                                           Longitud = DetalleReclamo.longitud,
                                           Latitud = DetalleReclamo.latitud,
                                           Direccion = DetalleReclamo.Direccion,
                                           ID_Reclamo = DetalleReclamo.ID_Reclamo,
                                           idReclamo = Reclamo.IDReclamo,
                                           id_sesion = Reclamo.ID_Sesion,
                                           Fecha = Reclamo.Fecha,
                                           Hora = Reclamo.Hora,
                                           Nombre = Estado.Nombre,
                                           IDEstado = Estado.IDEstado,
                                           NombreTRec = TipoReclamo.Nombre,
                                           IDTipoRec = TipoReclamo.IDTipoReclamo,
                                           IDRecAmb = ReclamoAmbiental.IDReclamoAmbiental,
                                           NombreRecAmbiental = ReclamoAmbiental.Nombre,
                                           Nick = Usuario.Nick,
                                           Foto = Reclamo.Foto,
                                           dominio = DetalleReclamo.Dominio,

                                       };

                        if (reclamos == null)
                        {
                            return NotFound();
                        }
                        return Ok(reclamos);

                    }
                    else if ((iDTipoEstado != 0 && iDEstado != 0) && fecha != "-")// 2 En este caso el Administrador ingresa todo, tipo de reclamo, estado y la fecha en especifico
                    {
                        var reclamos = from Reclamo in context.Reclamo
                                       join DetalleReclamo in context.DetalleReclamo on Reclamo.IDReclamo equals DetalleReclamo.ID_Reclamo
                                       join Sesion in context.Sesion on Reclamo.ID_Sesion equals Sesion.IDSesion
                                       join Usuario in context.Usuario on Sesion.ID_Usuario equals Usuario.IDUsuario
                                       join TipoReclamo in context.TipoReclamo on Reclamo.ID_TipoReclamo equals TipoReclamo.IDTipoReclamo
                                       join ReclamoAmbiental in context.ReclamoAmbiental on DetalleReclamo.ID_ReclamoAmbiental equals ReclamoAmbiental.IDReclamoAmbiental
                                       join Estado in context.Estado on Reclamo.ID_Estado equals Estado.IDEstado
                                       join TipoEstado in context.TipoEstado on Estado.ID_TipoEstado equals TipoEstado.IDTipoEstado
                                       where (TipoEstado.IDTipoEstado == iDTipoEstado && Reclamo.ID_Estado == iDEstado) &&  Reclamo.Fecha == fecha
                                       select new
                                       {
                                           IDDetalleReclamo = DetalleReclamo.IDDetalleReclamo,
                                           Descripcion = DetalleReclamo.Descripcion,
                                           Altura = DetalleReclamo.Altura,
                                           Longitud = DetalleReclamo.longitud,
                                           Latitud = DetalleReclamo.latitud,
                                           Direccion = DetalleReclamo.Direccion,
                                           ID_Reclamo = DetalleReclamo.ID_Reclamo,
                                           idReclamo = Reclamo.IDReclamo,
                                           id_sesion = Reclamo.ID_Sesion,
                                           Fecha = Reclamo.Fecha,
                                           Hora = Reclamo.Hora,
                                           Nombre = Estado.Nombre,
                                           IDEstado = Estado.IDEstado,
                                           NombreTRec = TipoReclamo.Nombre,
                                           IDTipoRec = TipoReclamo.IDTipoReclamo,
                                           IDRecAmb = ReclamoAmbiental.IDReclamoAmbiental,
                                           NombreRecAmbiental = ReclamoAmbiental.Nombre,
                                           Nick = Usuario.Nick,
                                           Foto = Reclamo.Foto,
                                           dominio = DetalleReclamo.Dominio,

                                       };

                        if (reclamos == null)
                        {
                            return NotFound();
                        }
                        return Ok(reclamos);
                    }
                    else if ((iDTipoEstado == 0 && iDEstado == 0) && fecha != "-")// El Administrador trae todos los reclamos en una fecha en especifico
                    {
                        var reclamos = from Reclamo in context.Reclamo
                                       join DetalleReclamo in context.DetalleReclamo on Reclamo.IDReclamo equals DetalleReclamo.ID_Reclamo
                                       join Sesion in context.Sesion on Reclamo.ID_Sesion equals Sesion.IDSesion
                                       join Usuario in context.Usuario on Sesion.ID_Usuario equals Usuario.IDUsuario
                                       join TipoReclamo in context.TipoReclamo on Reclamo.ID_TipoReclamo equals TipoReclamo.IDTipoReclamo
                                       join ReclamoAmbiental in context.ReclamoAmbiental on DetalleReclamo.ID_ReclamoAmbiental equals ReclamoAmbiental.IDReclamoAmbiental
                                       join Estado in context.Estado on Reclamo.ID_Estado equals Estado.IDEstado
                                       join TipoEstado in context.TipoEstado on Estado.ID_TipoEstado equals TipoEstado.IDTipoEstado
                                       where Reclamo.Fecha == fecha
                                       select new
                                       {
                                           IDDetalleReclamo = DetalleReclamo.IDDetalleReclamo,
                                           Descripcion = DetalleReclamo.Descripcion,
                                           Altura = DetalleReclamo.Altura,
                                           Longitud = DetalleReclamo.longitud,
                                           Latitud = DetalleReclamo.latitud,
                                           Direccion = DetalleReclamo.Direccion,
                                           ID_Reclamo = DetalleReclamo.ID_Reclamo,
                                           idReclamo = Reclamo.IDReclamo,
                                           id_sesion = Reclamo.ID_Sesion,
                                           Fecha = Reclamo.Fecha,
                                           Hora = Reclamo.Hora,
                                           Nombre = Estado.Nombre,
                                           IDEstado = Estado.IDEstado,
                                           NombreTRec = TipoReclamo.Nombre,
                                           IDTipoRec = TipoReclamo.IDTipoReclamo,
                                           IDRecAmb = ReclamoAmbiental.IDReclamoAmbiental,
                                           NombreRecAmbiental = ReclamoAmbiental.Nombre,
                                           Nick = Usuario.Nick,
                                           Foto = Reclamo.Foto,
                                           dominio = DetalleReclamo.Dominio,

                                       };

                        if (reclamos == null)
                        {
                            return NotFound();
                        }
                        return Ok(reclamos);
                    }
                    else
                    {
                        return NotFound();
                    }

                }
                else if(idRol==3) //Usuario normal
                {
                    //Estado TODOS
                    if (iDTipoEstado!=0 && (iDEstado ==13 || iDEstado==14) && fecha=="-" ) // obtengo todos los reclamos de todos los estados de cualquier fecha porque el usuario no ingreso una fecha
                    {
                        var reclamos = from Reclamo in context.Reclamo
                                       join DetalleReclamo in context.DetalleReclamo on Reclamo.IDReclamo equals DetalleReclamo.ID_Reclamo
                                       join Sesion in context.Sesion on Reclamo.ID_Sesion equals Sesion.IDSesion
                                       join Usuario in context.Usuario on Sesion.ID_Usuario equals Usuario.IDUsuario
                                       join TipoReclamo in context.TipoReclamo on Reclamo.ID_TipoReclamo equals TipoReclamo.IDTipoReclamo
                                       join ReclamoAmbiental in context.ReclamoAmbiental on DetalleReclamo.ID_ReclamoAmbiental equals ReclamoAmbiental.IDReclamoAmbiental
                                       join Estado in context.Estado on Reclamo.ID_Estado equals Estado.IDEstado
                                       join TipoEstado in context.TipoEstado on Estado.ID_TipoEstado equals TipoEstado.IDTipoEstado
                                       where Usuario.IDUsuario == idUsuario && TipoEstado.IDTipoEstado == iDTipoEstado
                                       select new
                                       {
                                           IDDetalleReclamo = DetalleReclamo.IDDetalleReclamo,
                                           Descripcion = DetalleReclamo.Descripcion,
                                           Altura = DetalleReclamo.Altura,
                                           Longitud = DetalleReclamo.longitud,
                                           Latitud = DetalleReclamo.latitud,
                                           Direccion = DetalleReclamo.Direccion,
                                           ID_Reclamo = DetalleReclamo.ID_Reclamo,
                                           idReclamo = Reclamo.IDReclamo,
                                           id_sesion = Reclamo.ID_Sesion,
                                           Fecha = Reclamo.Fecha,
                                           Hora = Reclamo.Hora,
                                           Nombre = Estado.Nombre,
                                           IDEstado = Estado.IDEstado,
                                           NombreTRec = TipoReclamo.Nombre,
                                           IDTipoRec = TipoReclamo.IDTipoReclamo,
                                           IDRecAmb = ReclamoAmbiental.IDReclamoAmbiental,
                                           NombreRecAmbiental = ReclamoAmbiental.Nombre,
                                           Nick = Usuario.Nick,
                                           Foto = Reclamo.Foto,
                                           dominio = DetalleReclamo.Dominio,

                                       };

                        if (reclamos == null)
                        {
                            return NotFound();
                        }
                        return Ok(reclamos);
                    }
                    else if(iDTipoEstado != 0 && (iDEstado == 13 || iDEstado == 14) && fecha != "-")//Todos los reclamos de todos los estados pero con fecha
                    {
                        var reclamos = from Reclamo in context.Reclamo
                                       join DetalleReclamo in context.DetalleReclamo on Reclamo.IDReclamo equals DetalleReclamo.ID_Reclamo
                                       join Sesion in context.Sesion on Reclamo.ID_Sesion equals Sesion.IDSesion
                                       join Usuario in context.Usuario on Sesion.ID_Usuario equals Usuario.IDUsuario
                                       join TipoReclamo in context.TipoReclamo on Reclamo.ID_TipoReclamo equals TipoReclamo.IDTipoReclamo
                                       join ReclamoAmbiental in context.ReclamoAmbiental on DetalleReclamo.ID_ReclamoAmbiental equals ReclamoAmbiental.IDReclamoAmbiental
                                       join Estado in context.Estado on Reclamo.ID_Estado equals Estado.IDEstado
                                       join TipoEstado in context.TipoEstado on Estado.ID_TipoEstado equals TipoEstado.IDTipoEstado
                                       where Usuario.IDUsuario == idUsuario && TipoEstado.IDTipoEstado == iDTipoEstado && Reclamo.Fecha == fecha
                                       select new
                                       {
                                           IDDetalleReclamo = DetalleReclamo.IDDetalleReclamo,
                                           Descripcion = DetalleReclamo.Descripcion,
                                           Altura = DetalleReclamo.Altura,
                                           Longitud = DetalleReclamo.longitud,
                                           Latitud = DetalleReclamo.latitud,
                                           Direccion = DetalleReclamo.Direccion,
                                           ID_Reclamo = DetalleReclamo.ID_Reclamo,
                                           idReclamo = Reclamo.IDReclamo,
                                           id_sesion = Reclamo.ID_Sesion,
                                           Fecha = Reclamo.Fecha,
                                           Hora = Reclamo.Hora,
                                           Nombre = Estado.Nombre,
                                           IDEstado = Estado.IDEstado,
                                           NombreTRec = TipoReclamo.Nombre,
                                           IDTipoRec = TipoReclamo.IDTipoReclamo,
                                           IDRecAmb = ReclamoAmbiental.IDReclamoAmbiental,
                                           NombreRecAmbiental = ReclamoAmbiental.Nombre,
                                           Nick = Usuario.Nick,
                                           Foto = Reclamo.Foto,
                                           dominio = DetalleReclamo.Dominio,

                                       };

                        if (reclamos == null)
                        {
                            return NotFound();
                        }
                        return Ok(reclamos);
                    }


                    //1 - el usuario solamente ingresa el TIPO DE ESTADO y su ESTADO
                    if ((iDTipoEstado != 0 && iDEstado != 0) && fecha == "-")// - indica que no ingresó la fecha
                    {
                        var reclamos = from Reclamo in context.Reclamo
                                       join DetalleReclamo in context.DetalleReclamo on Reclamo.IDReclamo equals DetalleReclamo.ID_Reclamo
                                       join Sesion in context.Sesion on Reclamo.ID_Sesion equals Sesion.IDSesion
                                       join Usuario in context.Usuario on Sesion.ID_Usuario equals Usuario.IDUsuario
                                       join TipoReclamo in context.TipoReclamo on Reclamo.ID_TipoReclamo equals TipoReclamo.IDTipoReclamo
                                       join ReclamoAmbiental in context.ReclamoAmbiental on DetalleReclamo.ID_ReclamoAmbiental equals ReclamoAmbiental.IDReclamoAmbiental
                                       join Estado in context.Estado on Reclamo.ID_Estado equals Estado.IDEstado
                                       join TipoEstado in context.TipoEstado on Estado.ID_TipoEstado equals TipoEstado.IDTipoEstado
                                       where Usuario.IDUsuario == idUsuario && (TipoEstado.IDTipoEstado == iDTipoEstado && Reclamo.ID_Estado == iDEstado)
                                       select new
                                       {
                                           IDDetalleReclamo = DetalleReclamo.IDDetalleReclamo,
                                           Descripcion = DetalleReclamo.Descripcion,
                                           Altura = DetalleReclamo.Altura,
                                           Longitud = DetalleReclamo.longitud,
                                           Latitud = DetalleReclamo.latitud,
                                           Direccion = DetalleReclamo.Direccion,
                                           ID_Reclamo = DetalleReclamo.ID_Reclamo,
                                           idReclamo = Reclamo.IDReclamo,
                                           id_sesion = Reclamo.ID_Sesion,
                                           Fecha = Reclamo.Fecha,
                                           Hora = Reclamo.Hora,
                                           Nombre = Estado.Nombre,
                                           IDEstado = Estado.IDEstado,
                                           NombreTRec = TipoReclamo.Nombre,
                                           IDTipoRec = TipoReclamo.IDTipoReclamo,
                                           IDRecAmb = ReclamoAmbiental.IDReclamoAmbiental,
                                           NombreRecAmbiental = ReclamoAmbiental.Nombre,
                                           Nick = Usuario.Nick,
                                           Foto = Reclamo.Foto,
                                           dominio = DetalleReclamo.Dominio,

                                       };
                        if (reclamos == null)
                        {
                            return NotFound();
                        }
                        return Ok(reclamos);
                    }
                    else if ((iDTipoEstado != 0 && iDEstado != 0) && fecha != "-")// 2 En este caso el usuario ingresa todo, tipo de reclamo, estado y la fecha en especifico
                    {
                        var reclamos = from Reclamo in context.Reclamo
                                       join DetalleReclamo in context.DetalleReclamo on Reclamo.IDReclamo equals DetalleReclamo.ID_Reclamo
                                       join Sesion in context.Sesion on Reclamo.ID_Sesion equals Sesion.IDSesion
                                       join Usuario in context.Usuario on Sesion.ID_Usuario equals Usuario.IDUsuario
                                       join TipoReclamo in context.TipoReclamo on Reclamo.ID_TipoReclamo equals TipoReclamo.IDTipoReclamo
                                       join ReclamoAmbiental in context.ReclamoAmbiental on DetalleReclamo.ID_ReclamoAmbiental equals ReclamoAmbiental.IDReclamoAmbiental
                                       join Estado in context.Estado on Reclamo.ID_Estado equals Estado.IDEstado
                                       join TipoEstado in context.TipoEstado on Estado.ID_TipoEstado equals TipoEstado.IDTipoEstado
                                       where Usuario.IDUsuario == idUsuario && (TipoEstado.IDTipoEstado == iDTipoEstado && Reclamo.ID_Estado == iDEstado) &&
                                       Reclamo.Fecha == fecha
                                       select new
                                       {
                                           IDDetalleReclamo = DetalleReclamo.IDDetalleReclamo,
                                           Descripcion = DetalleReclamo.Descripcion,
                                           Altura = DetalleReclamo.Altura,
                                           Longitud = DetalleReclamo.longitud,
                                           Latitud = DetalleReclamo.latitud,
                                           Direccion = DetalleReclamo.Direccion,
                                           ID_Reclamo = DetalleReclamo.ID_Reclamo,
                                           idReclamo = Reclamo.IDReclamo,
                                           id_sesion = Reclamo.ID_Sesion,
                                           Fecha = Reclamo.Fecha,
                                           Hora = Reclamo.Hora,
                                           Nombre = Estado.Nombre,
                                           IDEstado = Estado.IDEstado,
                                           NombreTRec = TipoReclamo.Nombre,
                                           IDTipoRec = TipoReclamo.IDTipoReclamo,
                                           IDRecAmb = ReclamoAmbiental.IDReclamoAmbiental,
                                           NombreRecAmbiental = ReclamoAmbiental.Nombre,
                                           Nick = Usuario.Nick,
                                           Foto = Reclamo.Foto,
                                           dominio = DetalleReclamo.Dominio,

                                       };

                        if (reclamos == null)
                        {
                            return NotFound();
                        }
                        return Ok(reclamos);

                    } 
                    else if ((iDTipoEstado == 0 && iDEstado == 0) && fecha != "-") // 3 En este caso el usuario solamente ingresa la fecha, por ende trae todos los reclamos de esa fecha
                    {
                        var reclamos = from Reclamo in context.Reclamo
                                       join DetalleReclamo in context.DetalleReclamo on Reclamo.IDReclamo equals DetalleReclamo.ID_Reclamo
                                       join Sesion in context.Sesion on Reclamo.ID_Sesion equals Sesion.IDSesion
                                       join Usuario in context.Usuario on Sesion.ID_Usuario equals Usuario.IDUsuario
                                       join TipoReclamo in context.TipoReclamo on Reclamo.ID_TipoReclamo equals TipoReclamo.IDTipoReclamo
                                       join ReclamoAmbiental in context.ReclamoAmbiental on DetalleReclamo.ID_ReclamoAmbiental equals ReclamoAmbiental.IDReclamoAmbiental
                                       join Estado in context.Estado on Reclamo.ID_Estado equals Estado.IDEstado
                                       join TipoEstado in context.TipoEstado on Estado.ID_TipoEstado equals TipoEstado.IDTipoEstado
                                       where Usuario.IDUsuario == idUsuario && Reclamo.Fecha == fecha
                                       select new
                                       {
                                           IDDetalleReclamo = DetalleReclamo.IDDetalleReclamo,
                                           Descripcion = DetalleReclamo.Descripcion,
                                           Altura = DetalleReclamo.Altura,
                                           Longitud = DetalleReclamo.longitud,
                                           Latitud = DetalleReclamo.latitud,
                                           Direccion = DetalleReclamo.Direccion,
                                           ID_Reclamo = DetalleReclamo.ID_Reclamo,
                                           idReclamo = Reclamo.IDReclamo,
                                           id_sesion = Reclamo.ID_Sesion,
                                           Fecha = Reclamo.Fecha,
                                           Hora = Reclamo.Hora,
                                           Nombre = Estado.Nombre,
                                           IDEstado = Estado.IDEstado,
                                           NombreTRec = TipoReclamo.Nombre,
                                           IDTipoRec = TipoReclamo.IDTipoReclamo,
                                           IDRecAmb = ReclamoAmbiental.IDReclamoAmbiental,
                                           NombreRecAmbiental = ReclamoAmbiental.Nombre,
                                           Nick = Usuario.Nick,
                                           Foto = Reclamo.Foto,
                                           dominio = DetalleReclamo.Dominio,

                                       };

                        if (reclamos == null)
                        {
                            return NotFound();
                        }
                        return Ok(reclamos);


                    }
                    else

                    {

                        return NotFound();

                    }

                    
                }
                else
                {
                    return NotFound();
                }// if de roles

            }
            else
            {
                return NotFound();
            }// if para validar campos vacios
        }

        // POST api/<ubicacionReclamosController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ubicacionReclamosController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ubicacionReclamosController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
