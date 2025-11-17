using MYCOLL.Entities.Interfaces;

namespace MYCOLL.Entities.Users
{
    public class Anonimo : IUtilizador
    {
        public int id { get; set; } = 0;
        public int tipoUtilizador => (int)IUtilizador.TipoUtilizador.anonimo;
        public Dictionary<Produto, int>? Carrinho { get; set; } =  new Dictionary<Produto, int>(); // Produto e quantidade
    }
}
