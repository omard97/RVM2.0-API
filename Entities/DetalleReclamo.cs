using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiRVM2019.Entities
{
    public class DetalleReclamo
    {
        [Key]
        public int IDDetalleReclamo { get; set; }
        public string Descripcion { get; set; }
        public string Direccion { get; set; }
        public int Altura { get; set; }
        public string Dominio { get; set; }
        public int ID_ReclamoAmbiental { get; set; }
        //public int ID_Vehiculo { get; set; }
        public int ID_Reclamo { get; set; }
        public string longitud { get;  set; }

        public string latitud { get; set; }
    }
}
