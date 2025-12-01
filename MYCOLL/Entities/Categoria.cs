using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices;
using System.Collections.Generic;

namespace MYCOLL.Entities
{
	public class Categoria
	{
		public int IdCategoria { get; set; }
		public string Nome { get; set; } = null!;
		public bool Ativo { get; set; }

		// Navigation
		public virtual ICollection<SubCategoria> Subcategorias { get; set; } = new List<SubCategoria>();
		public virtual ICollection<Produto> Produtos { get; set; } = new List<Produto>();
	}
}