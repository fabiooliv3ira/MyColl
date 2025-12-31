using System;
using System.Collections.Generic;

namespace RESTfulAPIMYCOLL.Entities
{
	public class Encomenda
	{
		
		public int Id { get; set; }
		public DateTime Data { get; set; }
		public string Estado { get; set; } = null!; // NaoPaga, Pendente, Confirmada, Rejeitada, Expedida
		public decimal ValorTotal { get; set; }
		public string ApplicationUserId { get; set; } = null!;

    }
}