using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ApiRVM2019.Contexts;
using ApiRVM2019.Entities;
using System.Linq;
using System.Net.Mail;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ApiRVM2019.Controllers.Login.Correo
{

    [EnableCors("All")]
    [Route("[controller]")]
    [ApiController]
    public class formularioController : ControllerBase
    {
        // GET: api/<formularioController>
        [HttpGet]
        public IActionResult Get(string correo, string contrasenia, string nombre, string usuario)
        {
            // ejemplo URL: https://localhost:44363/formulario?correo=omarf.dandrea@gmail.com&&contrasenia=omar123&&nombre=Omar&&usuario=omard97


            try
            {
                
                formulario.Email(correo, contrasenia, nombre, usuario);
                return Ok();



            } catch (Exception ex)
            {
                return BadRequest();
            }




            //try
            //{
            //    string EmailOrigen = "villamariarvm@gmail.com";
            //    string EmailDestino = correo;
            //    string Contrasenia = "Amodil987.,";

            //    MailMessage oMailMessage = new MailMessage(EmailOrigen, EmailDestino,
            //        "Hola",
            //        "Usted se registro en RVM");

            //    oMailMessage.IsBodyHtml = true;

            //    SmtpClient oSmtpClient = new SmtpClient("smtp.gmail.com");
            //    oSmtpClient.EnableSsl = true;
            //    oSmtpClient.UseDefaultCredentials = false;
            //    //oSmtpClient.Host = "smtp.gmail.com";
            //    oSmtpClient.Port = 587;
            //    oSmtpClient.Credentials = new System.Net.NetworkCredential(EmailOrigen, Contrasenia);

            //    oSmtpClient.Send(oMailMessage);

            //    oSmtpClient.Dispose();
            //    return Ok();
            //}
            //catch (Exception ex)
            //{
            //    return BadRequest(ex);
            //}
            
        }

        // GET api/<formularioController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<formularioController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<formularioController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<formularioController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
