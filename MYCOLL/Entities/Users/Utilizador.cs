using System.Collections.Generic;

namespace MYCOLL.Entities
{
	public class Utilizador
	{
		public int Id { get; set; }
		public string Nome { get; set; } = null!;
		public string Email { get; set; } = null!;
		public string PasswordHash { get; set; } = null!;
		public string? Morada { get; set; }
		public string? Telefone { get; set; }
		public string Estado { get; set; } = null!; // "Anonimo", "Logado"


	}
}