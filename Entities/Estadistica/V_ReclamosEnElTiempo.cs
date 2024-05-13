using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiRVM2019.Entities.Estadistica
{
    public class V_ReclamosEnElTiempo
    {
        [Key]
        public int CantidadReclamo { get; set; }

        public string Hora { get; set; }
        public string TipoHora { get; set; }
        public string NombreMes {get; set;}
        public int Anio { get; set; }
        public int numeroDIa { get; set; }
        public int idUsuario { get; set; }
        public int ID_Localidad { get; set; } //Se agrego para el filtro

    }
}
