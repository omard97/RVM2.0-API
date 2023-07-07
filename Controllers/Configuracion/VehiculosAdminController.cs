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
    public class VehiculosAdminController : ControllerBase
    {
        private readonly AppDbContext context;

        public VehiculosAdminController(AppDbContext context)
        {
            this.context = context;
        }
        // GET: api/<VehiculosAdminController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "hola", "value2" };
        }

        // GET api/<VehiculosAdminController>/5
        [HttpGet("{idMarca}/{idModelo}")]
        public IActionResult getVehiculos(int idMarca, int idModelo)
        { 
            //configuracion - vehiculos
            var dato = from Vehiculo in context.Vehiculo
                       join TipoVehiculo in context.TipoVehiculo on Vehiculo.ID_TipoVehiculo equals TipoVehiculo.IDTipoVehiculo
                       join MarcaVehiculo in context.MarcaVehiculo on Vehiculo.ID_MarcaVehiculo equals MarcaVehiculo.IDMarca
                       
                       join ModeloVehiculo in context.ModeloVehiculo on Vehiculo.ID_Modelo equals ModeloVehiculo.IDModelo
                       join Estado in context.Estado on Vehiculo.ID_Estado equals Estado.IDEstado
                       where Vehiculo.ID_MarcaVehiculo==idMarca && ModeloVehiculo.IDModelo ==idModelo
                       select new
                       {
                           idVehiculo = Vehiculo.IDVehiculo,
                           dominio = Vehiculo.Dominio,
                           color = Vehiculo.Color,
                           chasis = Vehiculo.NumeroChasis,
                           numMotor = Vehiculo.NumeroMotor,
                           id_TipoVehiculo = Vehiculo.ID_TipoVehiculo,
                           id_Estado = Vehiculo.ID_Estado,
                           id_MarcaVehiculo = Vehiculo.ID_MarcaVehiculo,


                           nombreTipoVehiculo = TipoVehiculo.Nombre,
                           marcaAuto = MarcaVehiculo.Nombre,
                           id_ModeloV = ModeloVehiculo.IDModelo,
                           nombreModelo = ModeloVehiculo.Nombre,
                           nombreEstado = Estado.Nombre,
                       };
            if (dato == null)
            {
                return NotFound();
            }

            return Ok(dato);
        }

        // POST api/<VehiculosAdminController>
        [HttpPost]
        public ActionResult PostVehiculo([FromBody] Vehiculo objVehiculo)
        {
            try
            {
                context.Vehiculo.Add(objVehiculo);
                context.SaveChanges();

                //reclamo.IDReclamo = recl.Entity.IDReclamo;

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // PUT api/<VehiculosAdminController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<VehiculosAdminController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
