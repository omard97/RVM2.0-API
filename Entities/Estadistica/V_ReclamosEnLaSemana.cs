using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiRVM2019.Entities.Estadistica
{
    public class V_ReclamosEnLaSemana
    {
        [Key]
        public int CantidadReclamos { get; set; }

        public string DiaDeLaSemana { get; set; }
        public string NombreMes { get; set; }
        public int Mes { get; set; }
        public int anio { get; set; }
        public string numeroDia { get; set; }
        public int idusuario { get; set; }
    }
}
