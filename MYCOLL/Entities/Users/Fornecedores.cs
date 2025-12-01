using System.Collections.Generic;

namespace MYCOLL.Entities.Users
{
	public class Fornecedor
	{
		public int IdFornecedor { get; set; }
		public string NomeEmpresa { get; set; } = null!;
		public string Email { get; set; } = null!;
		public string PasswordHash { get; set; } = null!;

		// Navigation
		public virtual ICollection<MYCOLL.Entities.Produto> Produtos { get; set; } = new List<MYCOLL.Entities.Produto>();
	}
}