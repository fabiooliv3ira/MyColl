using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MYCOLL.RCL.Data.DTO
{
	public class ItemCarrinhoDTO
	{
		public int Id { get; set; }
		public int ProdutoId { get; set; }
		public string ProdutoNome { get; set; } = string.Empty; // Útil para mostrar no carrinho
		public int Quantidade { get; set; }
		public decimal PrecoUnitario { get; set; } // Para mostrar ao user
		public decimal Subtotal { get; set; }
		public int EncomendaId { get; set; }
		public ProdutoDTO? Produto { get; init; }
	}
}
