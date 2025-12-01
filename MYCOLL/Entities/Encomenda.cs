using System;
using System.Collections.Generic;
using MYCOLL.Entities.Users;

namespace MYCOLL.Entities
{
	public class Encomenda
	{
		
		public int Id { get; set; }
		public DateTime Data { get; set; }
		public string Estado { get; set; } = null!; // Pendente, Confirmada, Rejeitada, Expedida
		public decimal ValorTotal { get; set; }

		// FKs
		public int ClienteId { get; set; }
		public int? FuncionarioId { get; set; }

	
	}
}