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

namespace ApiRVM2019.Controllers.Login
{
    [EnableCors("All")]
    [Route("[controller]")]
    [ApiController]
    public class V_ultimaSesionDelUsuarioController : ControllerBase
    {
        private readonly AppDbContext context;

        public V_ultimaSesionDelUsuarioController(AppDbContext context)
        {
            this.context = context;
        }

        // GET: api/<V_ultimaSesionDelUsuarioController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<V_ultimaSesionDelUsuarioController>/5
        [HttpGet("{idUsuario}")]
        public IActionResult GetReclamosUsuario(int idUsuario)
        {

            var data = from V_ultimaSesionDelUsuarioController in context.V_ultimaSesionDelUsuario
                       where V_ultimaSesionDelUsuarioController.ID_Usuario == idUsuario
                       select new
                       {
                           IDSesion = V_ultimaSesionDelUsuarioController.IDSesion,
                           ID_Usuario = V_ultimaSesionDelUsuarioController.ID_Usuario,
                           IDUsuario = V_ultimaSesionDelUsuarioController.IDUsuario,
                           nombre = V_ultimaSesionDelUsuarioController.Nombre,
                           nick = V_ultimaSesionDelUsuarioController.Nick,
                           correo = V_ultimaSesionDelUsuarioController.Correo,
                           ID_Perfil = V_ultimaSesionDelUsuarioController.ID_Perfil
                       };
            if (data == null)
            {
                return NotFound();
            }
            return Ok(data);
        }

        // POST api/<V_ultimaSesionDelUsuarioController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<V_ultimaSesionDelUsuarioController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<V_ultimaSesionDelUsuarioController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
