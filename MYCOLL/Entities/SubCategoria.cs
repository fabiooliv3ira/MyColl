namespace MYCOLL.Entities
{
	public class SubCategoria
	{
		public int Id { get; set; }
		public string Nome { get; set; } = null!;
		public bool Ativo { get; set; }

		// FK
		public int CategoriaId { get; set; }
	}
}