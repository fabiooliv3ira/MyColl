namespace MYCOLL.Entities
{
	public class ItemCarrinho
	{
		public int IdLinha { get; set; }
		public int Quantidade { get; set; }
		public decimal PrecoUnitario { get; set; }
		public decimal Subtotal { get; set; }

		// FKs
		public int IdEncomenda { get; set; }
		public int IdProduto { get; set; }

		// Navigation
		public virtual Encomenda? Encomenda { get; set; }
		public virtual Produto? Produto { get; set; }
	}
}