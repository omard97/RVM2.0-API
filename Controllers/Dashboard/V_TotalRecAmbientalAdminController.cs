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

namespace ApiRVM2019.Controllers.Dashboard
{
    [EnableCors("All")]
    [Route("[controller]")]
    [ApiController]
    public class V_TotalRecAmbientalAdminController : ControllerBase
    {
        private readonly AppDbContext context;

        public V_TotalRecAmbientalAdminController (AppDbContext context)
        {
            this.context = context;
        }
        // GET: api/<V_TotalRecAmbientalAdminController>
        [HttpGet]
        public IActionResult Get()
        {
            var data = (from V_TotalRecAmbientalAdminController in context.V_TotalRecAmbientalAdmin
                       select new
                       {
                           name = V_TotalRecAmbientalAdminController.Nombre,
                           value = V_TotalRecAmbientalAdminController.Cantidad
                       }).OrderByDescending(cantidad => cantidad.value);
            if (data == null)
            {
                return NotFound();
            }
            return Ok(data);
        }

        // GET api/<V_TotalRecAmbientalAdminController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<V_TotalRecAmbientalAdminController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<V_TotalRecAmbientalAdminController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<V_TotalRecAmbientalAdminController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
