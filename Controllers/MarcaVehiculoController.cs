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

    public class MarcaVehiculoController : Controller
    {
        private readonly AppDbContext context;

        public MarcaVehiculoController(AppDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        // GET: MarcaVehiculoController
        public IActionResult Get()
        {
            var _marcavehiculo = from MarcaVehiculo in context.MarcaVehiculo
                                 
                            select new
                            {
                                IDMarca = MarcaVehiculo.IDMarca,
                                marcaVehiculo=MarcaVehiculo.Nombre
                               
                            };
            if (_marcavehiculo == null)
            {
                return NotFound();
            }

            return Ok(_marcavehiculo);
        }

        [HttpGet("{id}")]
        // GET: MarcaVehiculoController/Details/5
        public async Task<ActionResult<MarcaVehiculo>> GetMarcaVehiculo(int id)
        {
            var _marcavehiculo = await context.MarcaVehiculo.FindAsync(id);

            if (_marcavehiculo == null)
            {
                return NotFound();
            }

            return _marcavehiculo;
        }

        [HttpPost]
        // GET: VehiculoController/Create
        public ActionResult Post([FromBody] MarcaVehiculo marcaVehiculo)
        {
            try
            {
                context.MarcaVehiculo.Add(marcaVehiculo);
                context.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPut("{id}")]
        // GET: MarcaVehiculoController/Edit/5
        public ActionResult Put(int id, [FromBody] MarcaVehiculo marcaVehiculo)
        {
            if (marcaVehiculo.IDMarca == id)
            {
                context.Entry(marcaVehiculo).State = EntityState.Modified;
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
