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
	public class TipoReclamoController : ControllerBase
	{
		private readonly AppDbContext context;
		public TipoReclamoController(AppDbContext context)
		{
			this.context = context;
		}

		// GET: api/<TipoReclamoController>

		[HttpGet]
		public IActionResult Get()
		{
			//utilizado para traer los tipos de reclamo cuando se quiere cambiar el estado del reclamo - administrador
			var _TipoR = from TipoReclamo in context.TipoReclamo
						   select TipoReclamo;

			if (_TipoR == null)
			{
				return NotFound();
			}
			return Ok(_TipoR);
			
		}

		// GET api/<TipoReclamoController>/5
		[HttpGet("{id}")]
		public async Task<ActionResult<TipoReclamo>> GetTipoReclamo(int id)
		{
			var _TipoR = await context.TipoReclamo.FindAsync(id);

			if (_TipoR == null)
			{
				return NotFound();
			}

			return _TipoR;
		}

		// POST api/<TipoReclamoController>
		[HttpPost]
		public ActionResult Post([FromBody] TipoReclamo TipoR)
		{
			try
			{
				context.TipoReclamo.Add(TipoR);
				context.SaveChanges();
				return Ok();
			}
			catch (Exception ex)
			{
				return BadRequest();
			}
		}
		// PUT api/<TipoReclamoController>/5
		[HttpPut("{id}")]
		public ActionResult Put(int id, [FromBody] TipoReclamo TipoR)
		{
			if (TipoR.IDTipoReclamo == id)
			{
				context.Entry(TipoR).State = EntityState.Modified;
				context.SaveChanges();
				return Ok();
			}
			else
			{
				return BadRequest();
			}
		}


		// DELETE api/<TipoReclamoController>/5
		[HttpDelete("{id}")]
		public void Delete(int id)
		{
		}
	}
}
