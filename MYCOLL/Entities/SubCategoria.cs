namespace MYCOLL.Entities
{
	public class SubCategoria
	{
		public int IdSubcategoria { get; set; }
		public string Nome { get; set; } = null!;
		public bool Ativo { get; set; }

		// FK
		public int IdCategoria { get; set; }

		// Navigation
		public virtual Categoria? Categoria { get; set; }
		public virtual ICollection<Produto> Produtos { get; set; } = new List<Produto>();
	}
}