using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MYCOLL.RCL.Data.DTO
{
	public class LoginDTO
	{
		public string Email { get; set; } = string.Empty;
		public string Password { get; set; } = string.Empty;
	}

	public class RegisterDTO
	{
		public string Email { get; set; } = string.Empty;
		public string Password { get; set; } = string.Empty;
		public string Nome { get; set; } = string.Empty;
		public string NIF { get; set; } = string.Empty;
		public string TipoUtilizador { get; set; } = "Cliente"; // Cliente ou Fornecedor
	}
}
