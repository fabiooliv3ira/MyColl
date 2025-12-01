using System;

namespace MYCOLL.Entities.Users
{
	public class Pagamento
	{
		public int IdPagamento { get; set; }
		public string EstadoPagamento { get; set; } = null!; // "Aprovado", "Recusado", "Pendente"
		public DateTime DataPagamento { get; set; }

		// FK (unique in DBML)
		public int IdEncomenda { get; set; }

		// Navigation
		public virtual MYCOLL.Entities.Encomenda? Encomenda { get; set; }
	}
}