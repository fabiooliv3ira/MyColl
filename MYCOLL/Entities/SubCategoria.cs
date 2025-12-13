using System.Text.Json.Serialization;

namespace MYCOLL.Entities
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