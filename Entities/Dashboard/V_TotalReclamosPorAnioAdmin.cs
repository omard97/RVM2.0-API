using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ApiRVM2019.Entities.Dashboard
{
    public class V_TotalReclamosPorAnioAdmin
    {
        [Key]
        public int Cantidad { get; set; }
        public string Anio { get; set; }
        public string Mes { get; set; }
        
    }
}
