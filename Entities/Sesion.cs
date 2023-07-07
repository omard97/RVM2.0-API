using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiRVM2019.Entities
{
    public class Sesion
    {
        [Key]
        public int IDSesion { get; set; }
        public string FechaInicio { get; set; } //DateTime
        public string FechaFin { get; set; } //DateTime
        public string HoraInicio { get; set; } //TimeSpan
        public string  HoraFin { get; set; } //TimeSpan

        public int ID_Usuario { get; set; }

    }
}
