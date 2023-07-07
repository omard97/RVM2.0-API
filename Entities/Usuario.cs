using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiRVM2019.Entities
{
	public class Usuario
	{
		[Key]
		public int IDUsuario { get; set; }
		public string Nombre { get; set; }
		public string Apellido { get; set; }
		public string Celular { get; set; }
		public string Contrasenia { get; set; }
		public string DNI { get; set; }
		public string Correo { get; set; }
		public int ID_Perfil { get; set; }
		public int ID_Estado { get; set; }
		public string Nick { get; set; }

		public string Foto { get; set; }

    }
}
