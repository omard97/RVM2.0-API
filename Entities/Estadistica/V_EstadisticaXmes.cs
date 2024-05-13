using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiRVM2019.Entities.Estadistica
{
    public class V_EstadisticaXmes
    {
        [Key]
        public int Cantidad { get; set; }
        public string anio { get; set; }
        public string NombreMes { get; set; }
        public int numeroMes { get; set; }

        public int IDUsuario { get; set; }

        public int ID_Localidad { get; set; } //Se agrego para el filtro
    }
}
