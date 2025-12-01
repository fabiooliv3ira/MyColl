using System.Collections.Generic;

namespace MYCOLL.Entities.Users
{
	public class Fornecedor
	{
		public int Id { get; set; }
		public string NomeEmpresa { get; set; } = null!;
		public string Email { get; set; } = null!;
		public string PasswordHash { get; set; } = null!;


	}
}