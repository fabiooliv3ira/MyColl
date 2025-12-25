using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MYCOLL.RCL.Data.DTO
{
    public class ProdutoDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string? Descricao { get; set; }
        public decimal Preco { get; set; }
        public int Stock { get; set; }
		public int NrDeVendas { get; set; }
		public string EstadoProduto { get; set; } = string.Empty;
        public string? URLImagem { get; set; }
        public byte[]? Imagem { get; set; } // Caso uses bytes
    }
}
