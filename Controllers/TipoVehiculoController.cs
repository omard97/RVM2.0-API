using ApiRVM2019.Contexts;
using ApiRVM2019.Entities;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiRVM2019.Controllers
{
    [EnableCors("All")]
    [Route("[controller]")]
    [ApiController]
    public class TipoVehiculoController : Controller
    {
        private readonly AppDbContext context;

        public TipoVehiculoController(AppDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        // GET: TipoVehiculoController
        public IActionResult Get()
        {
            var _tipovehiculo = from TipoVehiculo in context.TipoVehiculo
                                  select new
                                  {
                                     tipoVehiculo = TipoVehiculo.Nombre
                                  };
            if (_tipovehiculo == null)
            {
                return NotFound();
            }

            return Ok(_tipovehiculo);
        }

        [HttpGet("{id}")]
        // GET: ModeloVehiculoController/Details/5
        public async Task<ActionResult<TipoVehiculo>> GetTipoVehiculo(int id)
        {
            var _tipovehiculo = await context.TipoVehiculo.FindAsync(id);

            if (_tipovehiculo == null)
            {
                return NotFound();
            }

            return _tipovehiculo;
        }

        [HttpPost]
        // GET: TipoVehiculoController/Create
        public ActionResult Post([FromBody] TipoVehiculo tipoVehiculo)
        {
            try
            {
                context.TipoVehiculo.Add(tipoVehiculo);
                context.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpPut("{id}")]
        // GET: TipoVehiculoController/Edit/5
        public ActionResult Put(int id, [FromBody] TipoVehiculo tipoVehiculo)
        {
            if (tipoVehiculo.IDTipoVehiculo == id)
            {
                context.Entry(tipoVehiculo).State = EntityState.Modified;
                context.SaveChanges();
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
