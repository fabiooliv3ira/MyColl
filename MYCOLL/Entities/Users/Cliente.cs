using MYCOLL.Entities.Interfaces;

namespace MYCOLL.Entities.Users
{
    public class Cliente : IUtilizador
    {
        public int id { get; set; } = 0; // Um valor padrão é útil para anónimos
        public int tipoUtilizador => (int)IUtilizador.TipoUtilizador.cliente;

        public Dictionary<Produto, int>? Carrinho { get; set; } = new Dictionary<Produto, int>(); // Produto e quantidade
    }
}
