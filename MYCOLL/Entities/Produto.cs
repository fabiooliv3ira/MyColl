using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

using System;

namespace MYCOLL.Entities
{
	public class Produto
	{
		public int IdProduto { get; set; }
		public string Nome { get; set; } = null!;
		public string? Descricao { get; set; }
		public decimal PrecoBase { get; set; }
		public decimal PrecoFinal { get; set; }
		public int Stock { get; set; }
		public string EstadoProduto { get; set; } = null!; // e.g. "Ativo", "Pendente", "Inativo"
		public string Tipo { get; set; } = null!; // e.g. "Venda", "Listagem"

		// FKs
		public int IdFornecedor { get; set; }
		public int IdCategoria { get; set; }
		public int IdSubcategoria { get; set; }

		// Navigation
		public virtual Users.Fornecedor? Fornecedor { get; set; }
		public virtual Categoria? Categoria { get; set; }
		public virtual SubCategoria? SubCategoria { get; set; }
	}
}