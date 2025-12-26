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
        public string Nome { get; set; } = null!;
        public string? Descricao { get; set; }
        public decimal Preco { get; set; }
        public int Stock { get; set; }
        public int NrDeVendas { get; set; }
        public string EstadoProduto { get; set; } = null!;
        public string Tipo { get; set; } = null!;
        public string URLImagem { get; set; } = null!;
        public int SubcategoriaId { get; set; }
        public string ApplicationUserId { get; set; } = null!;
    }
}
