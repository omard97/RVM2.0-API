using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiRVM2019.Entities
{
    public class TipoVehiculo
    {
        [Key]

        public int IDTipoVehiculo{ get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
    }
}
