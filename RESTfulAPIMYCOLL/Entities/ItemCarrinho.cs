namespace RESTfulAPIMYCOLL.Entities
{
	public class ItemCarrinho
	{
		public int Id { get; set; }
		public int Quantidade { get; set; }
		public decimal Subtotal { get; set; }

		// FKs
        public int ProdutoId { get; set; }
		public Produto? Produto { get; set; }

		public String? ApplicationUserId { get; set; }

    }
}