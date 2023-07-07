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

    public class VehiculoController : Controller
    {
        private readonly AppDbContext context;

        public VehiculoController(AppDbContext context)
        {
            this.context = context;
        }
        [HttpGet]
        // GET: VehiculoController
        public IActionResult Get()
        {
            var _vehiculo = from Vehiculo in context.Vehiculo
                          join TipoVehiculo in context.TipoVehiculo on Vehiculo.ID_TipoVehiculo equals TipoVehiculo.IDTipoVehiculo
                          join MarcaVehiculo in context.MarcaVehiculo on Vehiculo.ID_MarcaVehiculo equals MarcaVehiculo.IDMarca
                          
                          join ModeloVehiculo in context.ModeloVehiculo on Vehiculo.ID_Modelo equals ModeloVehiculo.IDModelo
                          join Estado in context.Estado on Vehiculo.ID_Estado equals Estado.IDEstado
                          select new
                          {
                             tipoVehiculo=TipoVehiculo.Nombre,
                             marcaVehiculo=MarcaVehiculo.Nombre,
                             modeloVehiculo=ModeloVehiculo.Nombre,
                             patente=Vehiculo.Dominio,
                             numeroChasis=Vehiculo.NumeroChasis,
                             numeroMotor=Vehiculo.NumeroMotor,
                             color=Vehiculo.Color,
                             estadoVehiculo=Estado.Nombre
                          };
            if (_vehiculo == null)
            {
                return NotFound();
            }

            return Ok(_vehiculo);
        }

        [HttpGet("{id}")]
        // GET: VehiculoController/Details/5
        public async Task<ActionResult<Vehiculo>> GetVehiculo(int id)
        {
            var _vehiculo = await context.Vehiculo.FindAsync(id);

            if (_vehiculo == null)
            {
                return NotFound();
            }

            return _vehiculo;
        }

        [HttpPost]
        // GET: VehiculoController/Create
        public ActionResult Post([FromBody] Vehiculo vehiculo)
        {
            try
            {
                var VH = context.Vehiculo.Add(vehiculo);
                context.SaveChanges();
                vehiculo.IDVehiculo = VH.Entity.IDVehiculo;
                return Ok(vehiculo);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpPut("{id}")]
        // GET: VehiculoController/Edit/5
        public ActionResult Put(int id, [FromBody] Vehiculo vehiculo)
        {
            if (vehiculo.IDVehiculo == id)
            {
                context.Entry(vehiculo).State = EntityState.Modified;
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
