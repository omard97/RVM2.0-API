using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiRVM2019.Entities
{
    public class TipoEstado
    {
        [Key]
        public int IDTipoEstado { get; set; }
        public  string nombre { get; set; }
    }
}
