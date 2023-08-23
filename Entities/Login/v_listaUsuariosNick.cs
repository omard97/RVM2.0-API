using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiRVM2019.Entities.Login
{
    public class v_listaUsuariosNick
    {
        [Key]
        public int IDUsuario { get; set; }
        public string Nick { get; set; }
        public string Correo { get; set; }
    }
}
