using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiRVM2019.Entities.Dashboard
{
    public class V_TotalReclamosRealizados
    {
        public int Cantidad { get; set; }
        [Key]
        public int IDUsuario { get; set; }
        public string Nick { get; set; }
    }
}
