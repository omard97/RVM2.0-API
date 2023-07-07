using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ApiRVM2019.Entities.Dashboard
{
    public class V_CantidadxEstadoUsuario
    {
        [Key]
        public int IDusuario { get; set; }

        public string Nombre { get; set; }

        public int Cantidad { get; set; }

    }
}
