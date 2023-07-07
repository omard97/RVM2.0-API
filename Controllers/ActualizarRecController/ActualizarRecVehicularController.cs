using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ApiRVM2019.Contexts;
using ApiRVM2019.Entities;
using Microsoft.AspNetCore.Cors;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ApiRVM2019.Controllers.ActualizarRecController
{
    [EnableCors("All")]
    [Route("[controller]")]
    [ApiController]
    public class ActualizarRecVehicularController : ControllerBase
    {
        private readonly AppDbContext context;

        public ActualizarRecVehicularController(AppDbContext context)
        {
            this.context = context;
        }

        // GET: api/<ActualizarRecVehicularController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<ActualizarRecVehicularController>/5
        [HttpGet("{idDetalle}")]
    public IActionResult GetReclamosVehicular(int idDetalle)
    {
            var _VehiculoXDetalleR = (from VehiculoXDetalleReclamo in context.VehiculoXDetalleReclamo
                                      join DetalleReclamo in context.DetalleReclamo on VehiculoXDetalleReclamo.ID_DetalleReclamo equals DetalleReclamo.IDDetalleReclamo
                                      join ReclamoAmbiental in context.ReclamoAmbiental on DetalleReclamo.ID_ReclamoAmbiental equals ReclamoAmbiental.IDReclamoAmbiental
                                      join Vehiculo in context.Vehiculo on VehiculoXDetalleReclamo.ID_Vehiculo equals Vehiculo.IDVehiculo
                                      join MarcaVehiculo in context.MarcaVehiculo on Vehiculo.ID_MarcaVehiculo equals MarcaVehiculo.IDMarca
                                      join TipoVehiculo in context.TipoVehiculo on Vehiculo.ID_TipoVehiculo equals TipoVehiculo.IDTipoVehiculo
                                      join estado in context.Estado on Vehiculo.ID_Estado equals estado.IDEstado
                                      join reclamo in context.Reclamo on DetalleReclamo.ID_Reclamo equals reclamo.IDReclamo
                                      join sesion in context.Sesion on reclamo.ID_Sesion equals sesion.IDSesion
                                      join usuario in context.Usuario on sesion.ID_Usuario equals usuario.IDUsuario
                                      join TipoReclamo in context.TipoReclamo on reclamo.ID_TipoReclamo equals TipoReclamo.IDTipoReclamo
                                      join modelo in context.ModeloVehiculo on Vehiculo.ID_Modelo equals modelo.IDModelo

                                      where DetalleReclamo.IDDetalleReclamo == idDetalle
                                      //superconsulta como la del reclamo
                                      select new
                                      {

                                          IDDetalleReclamo = DetalleReclamo.IDDetalleReclamo,
                                          Descripcion = DetalleReclamo.Descripcion,
                                          Altura = DetalleReclamo.Altura,
                                          Direccion = DetalleReclamo.Direccion,
                                          ID_Reclamo = DetalleReclamo.ID_Reclamo,
                                          Fecha = reclamo.Fecha,
                                          Hora = reclamo.Hora,
                                          IDSesion = reclamo.ID_Sesion,
                                          ID_estadoRec = reclamo.ID_Estado,
                                          ID_Estado = reclamo.ID_Estado,

                                          NombreMarca = MarcaVehiculo.Nombre,

                                          ID_Vehiculo = VehiculoXDetalleReclamo.ID_Vehiculo,
                                          colorAuto = Vehiculo.Color,
                                          numeroChasis = Vehiculo.NumeroChasis,
                                          numeroMotor = Vehiculo.NumeroMotor,
                                          ID_marca = Vehiculo.ID_MarcaVehiculo,
                                          ID_EstadoVehiculo = estado.IDEstado,
                                          ID_Tipovehiculo = Vehiculo.ID_TipoVehiculo,
                                          ID_Modelo = Vehiculo.ID_Modelo,
                                          nombreModelo = modelo.Nombre,
                                          

                                          EstadoVehiculo = estado.Nombre,

                                          NombreTRec = TipoReclamo.Nombre,
                                          IDTipoRec = TipoReclamo.IDTipoReclamo,
                                          IDRecAmb = ReclamoAmbiental.IDReclamoAmbiental,

                                          NombreRecAmbiental = ReclamoAmbiental.Nombre, //quema de arboles, unundaciones, etc
                                          Dominio = DetalleReclamo.Dominio,
                                          Nick = usuario.Nick,
                                          Foto = reclamo.Foto

                                          //idVehiculoxDetalle = VehiculoXDetalleReclamo.IDVehiculoXDetalle,
                                          //id_vehiculo = VehiculoXDetalleReclamo.ID_Vehiculo,
                                          //id_detalle = VehiculoXDetalleReclamo.ID_DetalleReclamo,
                                          //vehiculo
                                          //IDVehiculo = Vehiculo.IDVehiculo,
                                          //dominio = Vehiculo.Dominio,
                                          //color = Vehiculo.Color,
                                          //marca = MarcaVehiculo.Nombre,
                                          //tipVehiculo = TipoVehiculo.Nombre,
                                          //estadoVehiculo = Estado.Nombre,
                                          //DetalleReclamo
                                          //IDDetalleReclamo = DetalleReclamo.IDDetalleReclamo,
                                          //ReclamoDescr = DetalleReclamo.Descripcion,
                                          //Direccion = DetalleReclamo.Direccion,
                                          //Altura = DetalleReclamo.Altura,
                                          // Reclamo
                                          //IDReclamo = Reclamo.IDReclamo,
                                          //fechaInicio = Reclamo.Fecha,
                                          //hora = Reclamo.Hora,
                                          //tipoRecla = TipoReclamo.Nombre,

                                      }).OrderBy(ID => ID.ID_Reclamo);

            if (_VehiculoXDetalleR == null)
            {
                return NotFound();
            }
            return Ok(_VehiculoXDetalleR);
        }

        // POST api/<ActualizarRecVehicularController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ActualizarRecVehicularController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<Vehiculo>> actualizarVehiculo(int id, [FromBody] Vehiculo item)
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

        // DELETE api/<ActualizarRecVehicularController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
