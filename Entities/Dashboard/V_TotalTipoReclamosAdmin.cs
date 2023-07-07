using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ApiRVM2019.Entities.Dashboard
{
    public class V_TotalTipoReclamosAdmin
    {
        public string Nombre { get; set; }
        [Key]
        public int Cantidad { get; set; }
    }
}
