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

namespace ApiRVM2019.Controllers.Configuracion
{
    [EnableCors("All")]
    [Route("[controller]")]
   
    [ApiController]
    public class UsuarioAdminConfigurationController : ControllerBase
    {
        private readonly AppDbContext DBContext;

        public UsuarioAdminConfigurationController(AppDbContext contex)
        {
            this.DBContext = contex;
        }


        // GET: api/<UsuarioAdminConfigurationController>
        [HttpGet]
        public IActionResult getUsuarios()
        {//al apretar en la configuración del usuario obtengo todos los usuarios registrados - configuración - usuarios

            var dataUsuario =( from Usuario in DBContext.Usuario
                              join Estado in DBContext.Estado on Usuario.ID_Estado equals Estado.IDEstado
                              join TipoEstado in DBContext.TipoEstado on Estado.ID_TipoEstado equals TipoEstado.IDTipoEstado
                              select new
                              {
                                  idUsuario = Usuario.IDUsuario,
                                  nombreUsuario = Usuario.Nombre,
                                  apellido = Usuario.Apellido,
                                  telefono = Usuario.Celular,
                                  dni = Usuario.DNI,
                                  email = Usuario.Correo,
                                  nick = Usuario.Nick,
                                  foto = Usuario.Foto,

                                  idEstado = Estado.IDEstado,
                                  nombreEstado = Estado.Nombre,
                                  idTipoEstado = TipoEstado.IDTipoEstado,
                                  nombreTipoEstado = TipoEstado.nombre,



                              }).OrderBy(nombre => nombre.nombreUsuario);
            if (dataUsuario.Count() == 0)
            {
                var mensajeError = "No se encontró ningún usuario";
                return NotFound(mensajeError);
            }

            return Ok(dataUsuario);
        }

        // GET api/<UsuarioAdminConfigurationController>/5
        [HttpGet("{idUsuario}")]
        public IActionResult getDatousuario(int idUsuario)
        {
            var dataUsuario = from Usuario in DBContext.Usuario
                              join Estado in DBContext.Estado on Usuario.ID_Estado equals Estado.IDEstado
                              join TipoEstado in DBContext.TipoEstado on Estado.ID_TipoEstado equals TipoEstado.IDTipoEstado
                              where idUsuario == Usuario.IDUsuario
                              select new
                              {
                                  idUsuario = Usuario.IDUsuario,
                                  nombreUsuario = Usuario.Nombre,
                                  apellido = Usuario.Apellido,
                                  telefono = Usuario.Celular,
                                  contrasenia = Usuario.Contrasenia,
                                  dni = Usuario.DNI,
                                  email = Usuario.Correo,
                                  id_Perfil = Usuario.ID_Perfil,
                                  id_estado = Usuario.ID_Estado,
                                  nick = Usuario.Nick,
                                  foto = Usuario.Foto,

                                  idEstado = Estado.IDEstado,
                                  nombreEstado = Estado.Nombre,
                                  id_tipoEstado = Estado.ID_TipoEstado,

                                  idTipoEstado = TipoEstado.IDTipoEstado,
                                  nombreTipoEstado = TipoEstado.nombre,

                              };
            if (dataUsuario.Count()==0)
            {
                var mensajeError = "No se encontró ningún usuario con el ID especificado.";
                return NotFound(mensajeError);
            }

            return Ok(dataUsuario);
        }

        // POST api/<UsuarioAdminConfigurationController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<UsuarioAdminConfigurationController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<UsuarioAdminConfigurationController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
