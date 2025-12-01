using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using MYCOLL.Entities.Users;

using System;

namespace MYCOLL.Entities
{
	public class Produto
	{
		public int Id { get; set; }
		public string Nome { get; set; } = null!;
		public string? Descricao { get; set; }
		public decimal PrecoBase { get; set; }
		public decimal PrecoFinal { get; set; }
		public int Stock { get; set; }
		public string EstadoProduto { get; set; } = null!; // e.g. "Ativo", "Pendente", "Inativo"
		public string Tipo { get; set; } = null!; // e.g. "Venda", "Listagem"

		public string URLImagem { get; set; } = null!;

		// FKs
		public int FornecedorId { get; set; }
		public int CategoriaId { get; set; }
		public int SubcategoriaId { get; set; }

	}
}