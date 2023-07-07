using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiRVM2019.Entities
{
    public class Reclamo
    {
        [Key]
        public int IDReclamo { get; set; }
        public string Fecha { get; set; } //dateTime
        public string Foto { get; set; }
        public string Hora { get; set; } //time
        public int ID_Sesion { get; set; }
        public int ID_TipoReclamo { get; set; }
        public int ID_Estado { get; set; }

        
    }
}
