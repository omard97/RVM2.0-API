using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiRVM2019.Entities
{
    public class Vehiculo
    {
        [Key]
        public int IDVehiculo { get; set; }
        public string Dominio{ get; set; }
        public string Color{ get; set; }
        public string NumeroChasis{ get; set; }
        public string NumeroMotor { get; set; }
        public int ID_MarcaVehiculo { get; set; }
        public int ID_Estado { get; set; }
        public int ID_TipoVehiculo { get; set; }
        public int ID_Modelo { get; set; }
    }
}
