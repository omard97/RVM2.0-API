using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiRVM2019.Entities
{
    public class Estado
    {
        [Key]
        public int IDEstado { get; set; }
        public string Nombre { get; set; }
        public int ID_TipoEstado { get; set; }
        
    }
}
