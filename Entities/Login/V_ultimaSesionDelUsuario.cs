using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiRVM2019.Entities.Login
{
    public class V_ultimaSesionDelUsuario
    {
        [Key]
        public int IDSesion { get; set; }

        public int ID_Usuario { get; set; }
        public int IDUsuario { get; set; }
        public string Nombre { get; set; }
        public string Nick { get; set; }
        public string Correo { get; set; }
        public int ID_Perfil { get; set; }
    }
}
