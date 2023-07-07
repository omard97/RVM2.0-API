using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


namespace ApiRVM2019.Entities.Dashboard
{
    public class V_TotalRecAmbientalAdmin
    {
        [Key]
        public int Cantidad { get; set; }
        public string Nombre { get; set; }
    }
}
