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

namespace ApiRVM2019.Controllers.Configuracion
{
    [EnableCors("All")]
    [Route("[controller]")]
    [ApiController]
    public class TipoPerfilAdminController : ControllerBase
    {
        private readonly AppDbContext context;

        public TipoPerfilAdminController(AppDbContext context)
        {
            this.context = context;
        }

        // GET: api/<TipoPerfilAdminController>
        [HttpGet]
        public IActionResult getPerfiles() 
        {

            var data = from Perfil in context.Perfil
                       select new
                       {
                           idPerfil = Perfil.IDPerfil,
                           nombre = Perfil.Nombre,
                       };
            if (data == null)
            {
                return NotFound();
            }

            return Ok(data);
        }

        // GET api/<TipoPerfilAdminController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<TipoPerfilAdminController>
        [HttpPost]
        public ActionResult PostTipoPerfil([FromBody] Perfil objPerfil)
        {
            try
            {
                var PPerfil = context.Perfil.Add(objPerfil);
                context.SaveChanges();

                //reclamo.IDReclamo = recl.Entity.IDReclamo;

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // PUT api/<TipoPerfilAdminController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<TipoPerfilAdminController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
