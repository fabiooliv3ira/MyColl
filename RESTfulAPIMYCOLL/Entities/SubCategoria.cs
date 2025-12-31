using RESTfulAPIMYCOLL.Entities;
using System.Text.Json.Serialization;

namespace RESTfulAPIMYCOLL.Entities
{
	public class SubCategoria
	{
		public int Id { get; set; }
		public string Nome { get; set; } = null!;
		public bool Ativo { get; set; }
		public int CategoriaId { get; set; }
		[JsonIgnore]
        public Categoria? categoria { get; set; }
	}
}