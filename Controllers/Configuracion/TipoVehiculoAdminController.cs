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
    public class TipoVehiculoAdminController : ControllerBase
    {
        private readonly AppDbContext context;

        public TipoVehiculoAdminController(AppDbContext context)
        {
            this.context = context;
        }
        // GET: api/<TipoVehiculoAdminController>
        [HttpGet]
        public IActionResult GetTipoVehiculos()
        {
            //utiliza a la hora de mostrar los tipos de vehiculos dentro del select
            var data = from TipoVehiculo in context.TipoVehiculo
                       select new
                       {
                           idTipoVehiculo = TipoVehiculo.IDTipoVehiculo,
                           nombre = TipoVehiculo.Nombre,
                       };
            if (data == null)
            {
                return NotFound();
            }

            return Ok(data);
        }

        // GET api/<TipoVehiculoAdminController>/5
        [HttpGet("{idTipoVehiculo}")]
        public IActionResult Get(int idTipoVehiculo)
        {
            //se buscan los tipos de vehiculos segun el que se haya seleccionado
            var dato = from Vehiculo in context.Vehiculo
                       join TipoVehiculo in context.TipoVehiculo on Vehiculo.ID_TipoVehiculo equals TipoVehiculo.IDTipoVehiculo
                       join MarcaVehiculo in context.MarcaVehiculo on Vehiculo.ID_MarcaVehiculo equals MarcaVehiculo.IDMarca
                       
                       join ModeloVehiculo in context.ModeloVehiculo on Vehiculo.ID_Modelo equals ModeloVehiculo.IDModelo
                       join Estado in context.Estado on Vehiculo.ID_Estado equals Estado.IDEstado
                       where Vehiculo.ID_TipoVehiculo==idTipoVehiculo
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

        // POST api/<TipoVehiculoAdminController>
        [HttpPost]
        public ActionResult PostTipoVehiculo([FromBody] TipoVehiculo objTipoVehiculo)
        {
            try
            {
                var tipVehiculo = context.TipoVehiculo.Add(objTipoVehiculo);
                context.SaveChanges();

                //reclamo.IDReclamo = recl.Entity.IDReclamo;

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // PUT api/<TipoVehiculoAdminController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<TipoVehiculoAdminController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
