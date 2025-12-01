namespace MYCOLL.Entities.Users
{
	public class Funcionario
	{
		public int IdFuncionario { get; set; }
		public string Nome { get; set; } = null!;
		public string Email { get; set; } = null!;
		public string PasswordHash { get; set; } = null!;
		public string Role { get; set; } = null!; // "Admin", "Funcionario"

		// Navigation
		public virtual ICollection<MYCOLL.Entities.Encomenda> EncomendasProcessadas { get; set; } = new List<MYCOLL.Entities.Encomenda>();
	}
}