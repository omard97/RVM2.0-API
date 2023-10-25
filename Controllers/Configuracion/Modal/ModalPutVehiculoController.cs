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

namespace ApiRVM2019.Controllers.Configuracion.Modal
{
    [EnableCors("All")]
    [Route("[controller]")]
    [ApiController]
    public class ModalPutVehiculoController : ControllerBase
    {
        private readonly AppDbContext context;

        public ModalPutVehiculoController(AppDbContext context)
        {
            this.context = context;
        }

        // GET: api/<ModalPutVehiculoController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<ModalPutVehiculoController>/5
        [HttpGet("{idVehiculo}")]
        public IActionResult getVehiculoModal(int idVehiculo)
        {
            //configuracion - vehiculos
            // se utiliza para visualizar los datos en el modal cuando se seleciona un vehiculo en la lista de la tabla vehiculos
            var dato = from Vehiculo in context.Vehiculo
                       join TipoVehiculo in context.TipoVehiculo on Vehiculo.ID_TipoVehiculo equals TipoVehiculo.IDTipoVehiculo
                       join MarcaVehiculo in context.MarcaVehiculo on Vehiculo.ID_MarcaVehiculo equals MarcaVehiculo.IDMarca

                       join ModeloVehiculo in context.ModeloVehiculo on Vehiculo.ID_Modelo equals ModeloVehiculo.IDModelo
                       join Estado in context.Estado on Vehiculo.ID_Estado equals Estado.IDEstado
                       where Vehiculo.IDVehiculo==idVehiculo
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

        // POST api/<ModalPutVehiculoController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ModalPutVehiculoController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<Usuario>> actualizarUsuario(int id, [FromBody] Vehiculo item)
        {
            if (item.IDVehiculo == id)
            {
                context.Entry(item).State = EntityState.Modified;
                context.SaveChanges();
            }
            else if (id != item.IDVehiculo)
            {
                return BadRequest();
            }

            var result = await context.Vehiculo.FindAsync(id);

            if (result == null)
            {
                return NotFound();
            }

            return NoContent();
        }

        // DELETE api/<ModalPutVehiculoController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
