using ApiRVM2019.Contexts;
using ApiRVM2019.Entities;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ApiRVM2019.Controllers
{
	[EnableCors("All")]
	[Route("[controller]")]
	[ApiController]
	public class PerfilController : ControllerBase
	{
		private readonly AppDbContext context;
		public PerfilController(AppDbContext context)
		{
			this.context = context;
		}

		// GET: api/<PerfilController>
		[HttpGet]
		public IActionResult Get()
		{
			var _Perfil = from Perfil in context.Perfil
						 select Perfil;

			if (_Perfil == null)
			{
				return NotFound();
			}
			return Ok(_Perfil);
		}

		// GET api/<PerfilController>/5
		[HttpGet("{id}")]
		public async Task<ActionResult<Perfil>> GetPerfil(int id)
		{
			var _Perfil = await context.Perfil.FindAsync(id);

			if (_Perfil == null)
			{
				return NotFound();
			}

			return _Perfil;
		}

		// POST api/<PerfilController>
		[HttpPost]
		public ActionResult Post([FromBody] Perfil perfil)
		{
			try
			{
				context.Perfil.Add(perfil);
				context.SaveChanges();
				return Ok();
			}
			catch (Exception ex)
			{
				return BadRequest();
			}
		}

		// PUT api/<PerfilController>/5
		[HttpPut("{id}")]
		public ActionResult Put(int id, [FromBody] Perfil perfil)
		{
			if (perfil.IDPerfil == id)
			{
				context.Entry(perfil).State = EntityState.Modified;
				context.SaveChanges();
				return Ok();
			}
			else
			{
				return BadRequest();
			}
		}

		// DELETE api/<PerfilController>/5
		[HttpDelete("{id}")]
		public void Delete(int id)
		{
		}
	}
}
