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
    public class V_listaUsuariosNickController : ControllerBase
    {
        private readonly AppDbContext context;
        public V_listaUsuariosNickController(AppDbContext context)
        {
            this.context = context;
        }



        // GET: api/<V_listaUsuariosNickController>
        [HttpGet]
        public IActionResult Get(string correo)
        {
            // Utilizado para buscar por correo
            // ejemplo URL: https://localhost:44363/V_listaUsuariosNick?correo=omarf.dandrea@gmail.com

            var _Usuario = from V_listaUsuariosNickController in context.V_listaUsuariosNick
                       where V_listaUsuariosNickController.Correo == correo
                       select new
                       {
                           idUsuario = V_listaUsuariosNickController.IDUsuario,
                           nick = V_listaUsuariosNickController.Nick,
                           correo = V_listaUsuariosNickController.Correo,
                           id_Estado = V_listaUsuariosNickController.ID_Estado

                       };
            if (_Usuario == null)
            {
                return NotFound();
            }
            return Ok(_Usuario);
        }

        // GET api/<V_listaUsuariosNickController>/5
        [HttpGet("{nickUsuario}")]
        public IActionResult GetNickUsuario(string nickUsuario)
        {
            // Utilizado para buscar por Usuario
            var data = from V_listaUsuariosNickController in context.V_listaUsuariosNick
                       where V_listaUsuariosNickController.Nick == nickUsuario
                       select new
                       {
                           idUsuario = V_listaUsuariosNickController.IDUsuario,
                           nick = V_listaUsuariosNickController.Nick,
                           correo = V_listaUsuariosNickController.Correo

                       };

            if (data == null)
            {
                return null;
            }
            return Ok(data);
        }

        // POST api/<V_listaUsuariosNickController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<V_listaUsuariosNickController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<V_listaUsuariosNickController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
