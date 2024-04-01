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
    public class UsuarioFiltroadminController : ControllerBase
    {
        private readonly AppDbContext Dbcontext;

        public UsuarioFiltroadminController (AppDbContext context)
        {
            this.Dbcontext = context;
        }

        // GET: api/<usuarioFiltroadminController>
        [HttpGet]
        public IActionResult get(string tipoEstado)
        {
            if (tipoEstado == "Usuario")
            {
                //https://localhost:44363/Usuariofiltroadmin?tipoEstado=Usuario
                var data = from Estado in Dbcontext.Estado
                           join TipoEstado in Dbcontext.TipoEstado on Estado.ID_TipoEstado equals TipoEstado.IDTipoEstado
                           where Estado.ID_TipoEstado == 3
                           select new
                           {
                               idEstado = Estado.IDEstado,
                               nombre = Estado.Nombre,
                               idTipoEstado = Estado.ID_TipoEstado,
                               nombreTipoEstado = TipoEstado.nombre

                           };
                if (data.Count() == 0)
                {
                    var mensajeError = "No se encontró ningún Estado";
                    return NotFound(mensajeError);
                }

                return Ok(data);
            }
            else
            {
                var mensajeError = "No se encontró ningún Estado";
                return NotFound(mensajeError);
            }
            

            
        }


        // GET api/<usuarioFiltroadminController>/5
        //https://localhost:44363/usuarioFiltroadmin/omar/omard97/9
        [HttpGet("{nombreU}/{idEstadoU}")]
        public IActionResult Get(string nombreU, int idEstadoU)
        {
            //utilizado en la pantalla de configuracion, cuando el administrador quiere buscar un usuario para poder cambiar su estado
            if (nombreU!="" && idEstadoU!=0 && (nombreU != null))
            {
                var dataUsuario = (from Usuario in Dbcontext.Usuario
                                   join Estado in Dbcontext.Estado on Usuario.ID_Estado equals Estado.IDEstado
                                   join TipoEstado in Dbcontext.TipoEstado on Estado.ID_TipoEstado equals TipoEstado.IDTipoEstado
                                   where (Usuario.Nick.Contains(nombreU) || Usuario.Nombre.Contains(nombreU)) && Estado.IDEstado == idEstadoU
                                   //hacer validacion
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
                if (dataUsuario==null)
                {
                  
                    return NotFound();
                }

                return Ok(dataUsuario);
            }                 
            else
            {
               
                return NotFound();
            }
        }

        // POST api/<usuarioFiltroadminController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<usuarioFiltroadminController>/5
        [HttpPut("{idUsuario}")]
        public async Task<ActionResult<Usuario>> actualizarReclamo(int idUsuario, [FromBody] Usuario item)
        {
            if (idUsuario != item.IDUsuario)
            {
                return BadRequest();
            }

            var usu = await this.Dbcontext.Usuario.FindAsync(idUsuario);

            if (usu == null)
            {
                return NotFound();
            }

            // Actualizar solo los campos que han cambiado
            usu.ID_Estado = item.ID_Estado;
            // Repite este bloque para cada campo que pueda ser actualizado

            try
            {
                await Dbcontext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsuarioExists(idUsuario))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();

        }

        private bool UsuarioExists(int idUsu)
        {
            return Dbcontext.Usuario.Any(e => e.IDUsuario == idUsu);
        }

        // DELETE api/<usuarioFiltroadminController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

       
    }
}
