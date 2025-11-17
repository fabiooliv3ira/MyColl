using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace MYCOLL.Entities
{
    public class Produtos
    {
        public int Id { get; set; }
        [StringLength(100)]
        [Required]
        public string? Nome { get; set; }
        [StringLength(200)]
        [Required]
        public string? Detalhe { get; set; }
        [StringLength(200)]
        public string? URLImagem { get; set; }
        public byte[]? Imagem { get; set; }

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal Preco { get; set; }
        public bool Promocao { get; set; }
        public bool MaisVendido { get; set; }
        [Column(TypeName = "decimal(10,2)")]
        public decimal EmStock { get; set; }
        public bool Disponivel { get; set; }

        public string? Origem { get; set; }
        public int CategoriaId { get; set; }
        public Categoria categoria { get; set; }

        [JsonIgnore]
        public int? ModoEntregaId { get; set; }
        public ModoEntrega modoEntrega { get; set; }
        [NotMapped]
        public IFormFile? ImageFile { get; set; }
    }
}
