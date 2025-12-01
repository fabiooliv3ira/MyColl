using System;
using System.Collections.Generic;

namespace MYCOLL.Entities
{
	public class Encomenda
	{
		public int IdEncomenda { get; set; }
		public DateTime Data { get; set; }
		public string Estado { get; set; } = null!; // Pendente, Confirmada, Rejeitada, Expedida
		public decimal ValorTotal { get; set; }

		// FKs
		public int IdCliente { get; set; }
		public int? IdFuncionario { get; set; }

		// Navigation
		public virtual Users.Utilizador? Cliente { get; set; }    // or MYCOLL.Entities.Utilizador depending on design
		public virtual Users.Funcionario? Funcionario { get; set; }

		public virtual ICollection<ItemCarrinho> Itens { get; set; } = new List<ItemCarrinho>();
		public virtual Users.Pagamento? Pagamento { get; set; }
	}
}