using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiRVM2019.Entities.Estadistica
{
    public class VE_ReclamosXLocalidad
    {
        [Key]
        public int IDLocalidad { get; set; }
        public string Localidad { get; set; }
        public int IDUsuario { get; set; }
        public string Nick { get; set; }
        public int Cantidad { get; set; }
    }
}
