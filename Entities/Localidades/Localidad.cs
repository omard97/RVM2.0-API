using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiRVM2019.Entities.Localidades
{
    public class Localidad
    {
        [Key]
        public int IDLocalidad { get; set; }
        public string Nombre { get; set; }

        public string Provincia { get; set; }
        public int ID_Pais { get; set; }
        public int ID_EstadoLocalidad { get; set; }
        
    }
}
