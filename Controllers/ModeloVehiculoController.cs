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

    public class ModeloVehiculoController : Controller
    {
        private readonly AppDbContext context;

        public ModeloVehiculoController(AppDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        // GET: ModeloVehiculoController
        public IActionResult Get()
        {
            var _modelovehiculo = (from ModeloVehiculo in context.ModeloVehiculo
                                 select new
                                 {
                                     IDModelo =  ModeloVehiculo.IDModelo,
                                     modeloVehiculo =ModeloVehiculo.Nombre
                                 }).OrderByDescending(id => id.IDModelo);
            if (_modelovehiculo == null)
            {
                return NotFound();
            }

            return Ok(_modelovehiculo);
        }

        [HttpGet("{id}")]
        // GET: ModeloVehiculoController/Details/5
        public async Task<ActionResult<ModeloVehiculo>> GetModeloVehiculo(int id)
        {
            var _modelovehiculo = await context.ModeloVehiculo.FindAsync(id);

            if (_modelovehiculo == null)
            {
                return NotFound();
            }

            return _modelovehiculo;
        }

        [HttpPost]
        // GET: ModeloVehiculoController/Create
        public ActionResult Post([FromBody] ModeloVehiculo modeloVehiculo)
        {
            try
            {
                context.ModeloVehiculo.Add(modeloVehiculo);
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
        public ActionResult Put(int id, [FromBody] ModeloVehiculo modeloVehiculo)
        {
            if (modeloVehiculo.IDModelo == id)
            {
                context.Entry(modeloVehiculo).State = EntityState.Modified;
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
