using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiRVM2019.Entities.VistaReclamo
{
    public class v_estadoPendiente
    {
        [Key]
        public int IDEstado { get; set; }

        public string Nombre { get; set; }
        public int ID_TipoEstado { get; set; }
        public int IDTipoEstado { get; set; }

        public string nombreTipoEstado { get; set; }
    }
}
