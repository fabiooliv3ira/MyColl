using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices;
using System.Collections.Generic;

namespace MYCOLL.Entities
{
	public class Categoria
	{
		public int Id { get; set; }
		public string Nome { get; set; } = null!;
		public bool Ativo { get; set; }

    }
}