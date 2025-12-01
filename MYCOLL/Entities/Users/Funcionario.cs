namespace MYCOLL.Entities.Users
{
	public class Funcionario
	{
		public int Id { get; set; }
		public string Nome { get; set; } = null!;
		public string Email { get; set; } = null!;
		public string PasswordHash { get; set; } = null!;
		public string Role { get; set; } = null!; // "Admin", "Funcionario"

	}
}