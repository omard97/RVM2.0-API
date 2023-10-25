using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiRVM2019.Contexts;
using ApiRVM2019.Entities;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ApiRVM2019.Controllers
{
    [EnableCors("All")]
    [Route("[controller]")]
    [ApiController]
    public class SesionController : ControllerBase
    {
        private readonly AppDbContext context;

        Sesion objSesion = new Sesion();

        public SesionController(AppDbContext context)
        {
            this.context = context;
        }
       
        //metodo get para realizar la validacion de inicio sesion
        [HttpGet]
        public IActionResult ValidacionGet(string email, string password)
        {
            var _sesion = from Usuario in context.Usuario
                          join Estado in context.Estado on Usuario.ID_Estado equals Estado.IDEstado
                          join Perfil in context.Perfil on Usuario.ID_Perfil equals Perfil.IDPerfil
                          where Usuario.Correo == email && Usuario.Contrasenia == password && Usuario.ID_Estado ==9
                          //validar con el estado activo, si esta activo es que no quiso eliminar su cuenta
                          select new
                           {
                                   CorreoUsuario = Usuario.Correo,
                                   PassUsuario = Usuario.Contrasenia,
                                   IDUser = Usuario.IDUsuario,
                                   PerfilUsuario = Perfil.Nombre,
                                   EstadoUsuario = Estado.Nombre,
                                   NombreUsuario = Usuario.Nombre,
                                   ApellidoUsuario = Usuario.Apellido,
                                   DNIUsuario = Usuario.DNI,
                                   CelularUsuario = Usuario.Celular,
                                   NickUsuario = Usuario.Nick,
                                   idPerfil = Usuario.ID_Perfil,
                               };

            if (_sesion == null)
            {
                return NotFound();
            }

            return Ok(_sesion);
        }

        // GET api/<SesionController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Sesion>> GetSesion(int id)
        {
            var _Sesion = await context.Sesion.FindAsync(id);

            if (_Sesion == null)
            {
                return NotFound();
            }

            return _Sesion;
        }

        // POST api/<SesionController>
        [HttpPost]
        public ActionResult Post([FromBody] Sesion SesionPost)
        {
            try
            {
                var ses= context.Sesion.Add(SesionPost);
                context.SaveChanges();

                SesionPost.IDSesion = ses.Entity.IDSesion;




                return Ok(SesionPost);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        // PUT api/<SesionController>/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Sesion SesionPut)
        {
            if (SesionPut.IDSesion == id)
            {
                context.Entry(SesionPut).State = EntityState.Modified;
                context.SaveChanges();
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        // DELETE api/<SesionController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
