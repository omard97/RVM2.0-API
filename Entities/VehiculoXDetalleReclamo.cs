using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiRVM2019.Entities
{
    public class VehiculoXDetalleReclamo
    {
        [Key]
        public int IDVehiculoXDetalle { get; set; }
        public int ID_Vehiculo { get; set; }
        public int ID_DetalleReclamo { get; set; }
    }
}
