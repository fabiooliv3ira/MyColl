namespace MYCOLL.Entities
{
	public class ItemCarrinho
	{
		public int Id { get; set; }
		public int Quantidade { get; set; }
		public decimal PrecoUnitario { get; set; }
		public decimal Subtotal { get; set; }

		// FKs
		public int EncomendaId { get; set; }
		public Encomenda? Encomenda { get; set; }
        public int ProdutoId { get; set; }
		public Produto? Produto { get; set; }

    }
}