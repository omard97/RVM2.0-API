using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiRVM2019.Entities.Estadistica
{
    public class VE_ReclamosLocalidadXCalle
    {
        [Key]
        public int IDLocalidad { get; set; }
        public string direccion { get; set; }
        public string altura { get; set; }
        public string Localidad { get; set; }
        public int Cantidad { get; set; }
        public int IDUsuario { get; set; }
        public string nick { get; set; }
      
    }
}
