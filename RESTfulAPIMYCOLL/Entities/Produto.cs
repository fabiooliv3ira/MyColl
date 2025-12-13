using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

using System;

namespace RESTfulAPIMYCOLL.Entities
{
	public class Produto
	{
		public int Id { get; set; }
        [Required(ErrorMessage = "Mete nome nisso senão não deixo!")]
        public string Nome { get; set; } = null!;
		public string? Descricao { get; set; }
		public decimal Preco { get; set; }
		public int Stock { get; set; }
		public int NrDeVendas { get; set; }
		public string EstadoProduto { get; set; } = null!; // e.g. "Ativo", "Pendente", "Inativo"
		public string Tipo { get; set; } = null!; // e.g. "Venda", "Listagem"

		public string URLImagem { get; set; } = null!;

		// FKs
        public int SubcategoriaId { get; set; }
		public SubCategoria? SubCategoria { get; set; }
		
		public string ApplicationUserId { get; set; } = null!;

        [MaxLength(5242880)]
        public byte[]? Imagem { get; set; }

        [NotMapped] // Diz à entityCore para não mapear esta propriedade na base de dados
        public IFormFile? ImageFile { get; set; }

    }
}