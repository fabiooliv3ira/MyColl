using MYCOLL.Entities.Interfaces;

namespace MYCOLL.Entities.Users
{
    public class Fornecedores : IUtilizador
    {
        public int id { get; set; }
        public int tipoUtilizador => (int)IUtilizador.TipoUtilizador.fornecedor;

        public Produto[]? produtosVendidos { get; set; }
    }
}
