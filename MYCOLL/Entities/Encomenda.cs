using System;
using System.Collections.Generic;

namespace MYCOLL.Entities
{
	public class Encomenda
	{
		
		public int Id { get; set; }
		public DateTime Data { get; set; }
		public string Estado { get; set; } = null!; // NaoPaga, Pendente, Confirmada, Rejeitada, Expedida
		public decimal ValorTotal { get; set; }
		public string UserId { get; set; } = null!;
		public List<ItemCarrinho>? ItensCarrinho { get; set; }

    }
}