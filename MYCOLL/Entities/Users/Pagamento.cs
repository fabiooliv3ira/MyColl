using System;

namespace MYCOLL.Entities.Users
{
	public class Pagamento
	{
		public int Id { get; set; }
		public string EstadoPagamento { get; set; } = null!; // "Aprovado", "Recusado", "Pendente"
		public DateTime DataPagamento { get; set; }

		// FK (unique in DBML)
		public int EncomendaId { get; set; }


	}
}