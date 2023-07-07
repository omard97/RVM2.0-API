using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiRVM2019.Contexts;
using ApiRVM2019.Entities;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ApiRVM2019.Controllers
{
    [EnableCors("All")]
    [Route("[controller]")]
    [ApiController]
    public class VehiculoXDetalleReclamoController : ControllerBase
    {
        private readonly AppDbContext context;

        public VehiculoXDetalleReclamoController(AppDbContext context)
        {
            this.context = context;
        }
        // GET: api/<VehiculoXDetalleReclamoController>
        [HttpGet]
        public IActionResult Get()
        {

            var _VehiculoXDetalleR = (from VehiculoXDetalleReclamo in context.VehiculoXDetalleReclamo
                                     join DetalleReclamo in context.DetalleReclamo on VehiculoXDetalleReclamo.ID_DetalleReclamo equals DetalleReclamo.IDDetalleReclamo
                                     join Vehiculo in context.Vehiculo on VehiculoXDetalleReclamo.ID_Vehiculo equals Vehiculo.IDVehiculo
                                     join MarcaVehiculo in context.MarcaVehiculo on Vehiculo.ID_MarcaVehiculo equals MarcaVehiculo.IDMarca
                                     join TipoVehiculo in context.TipoVehiculo on Vehiculo.ID_TipoVehiculo equals TipoVehiculo.IDTipoVehiculo
                                     join Estado in context.Estado on Vehiculo.ID_Estado equals Estado.IDEstado                                   
                                     join Reclamo in context.Reclamo on DetalleReclamo.ID_Reclamo equals Reclamo.IDReclamo
                                     join TipoReclamo in context.TipoReclamo on Reclamo.ID_TipoReclamo equals TipoReclamo.IDTipoReclamo
                                     //superconsulta como la del reclamo
                                     select new
                              {
                                         idVehiculoxDetalle = VehiculoXDetalleReclamo.IDVehiculoXDetalle,
                                         id_vehiculo = VehiculoXDetalleReclamo.ID_Vehiculo,
                                         id_detalle = VehiculoXDetalleReclamo.ID_DetalleReclamo,
                                         //vehiculo
                                         IDVehiculo= Vehiculo.IDVehiculo,
                                         dominio = Vehiculo.Dominio,
                                         color = Vehiculo.Color,
                                         marca = MarcaVehiculo.Nombre,
                                         tipVehiculo = TipoVehiculo.Nombre,
                                         estadoVehiculo = Estado.Nombre,
                                         //DetalleReclamo
                                         IDDetalleReclamo = DetalleReclamo.IDDetalleReclamo,
                                         ReclamoDescr = DetalleReclamo.Descripcion,
                                         Direccion = DetalleReclamo.Direccion,
                                         Altura = DetalleReclamo.Altura,
                                         // Reclamo
                                         IDReclamo = Reclamo.IDReclamo,
                                         fechaInicio = Reclamo.Fecha,
                                         hora = Reclamo.Hora,
                                         tipoRecla = TipoReclamo.Nombre,
                                         
                                     }).OrderBy(ID => ID.idVehiculoxDetalle);

            if (_VehiculoXDetalleR == null)
            {
                return NotFound();
            }
            return Ok(_VehiculoXDetalleR);
        }

        // GET api/<VehiculoXDetalleReclamoController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<VehiculoXDetalleReclamo>> GetVehiculoXDetalleR(int id)
        {
            var _VehiculoXDetalle = await context.VehiculoXDetalleReclamo.FindAsync(id);

            if (_VehiculoXDetalle == null)
            {
                return NotFound();
            }

            return _VehiculoXDetalle;
        }

        // POST api/<VehiculoXDetalleReclamoController>
        [HttpPost]
        public ActionResult Post([FromBody] VehiculoXDetalleReclamo _VehiculoXDetalle)
        {
            try
            {
                context.VehiculoXDetalleReclamo.Add(_VehiculoXDetalle);
                context.SaveChanges();

                

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/<VehiculoXDetalleReclamoController>/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] VehiculoXDetalleReclamo _VehiculoXDetalle)
        {
            if (_VehiculoXDetalle.IDVehiculoXDetalle == id)
            {
                context.Entry(_VehiculoXDetalle).State = EntityState.Modified;
                context.SaveChanges();
                
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        // DELETE api/<VehiculoXDetalleRController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
