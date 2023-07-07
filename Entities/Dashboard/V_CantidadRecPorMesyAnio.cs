using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ApiRVM2019.Entities.Dashboard
{
    public class V_CantidadRecPorMesyAnio
    {
        public int Cantidad { get; set; }
        public string Anio { get; set; }
        public string Mes { get; set; }
        public string Nick { get; set; }
        [Key]
        public int IDUsuario { get; set; }
    }
}
