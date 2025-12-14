namespace RESTfulAPIMYCOLL.Entities
{
    public class Pagamento
    {
        public int Id { get; set; }
        public string MetodoPagamento { get; set; } = null!; //"Cartão de Crédito", "PayPal", "MBWAY"
        public decimal Montante { get; set; }
        public DateTime DataPagamento { get; set; }
        public string EstadoPagamento { get; set; } = null!; //"Pendente", "Concluído"
        // FK
        public int EncomendaId { get; set; }
        public Encomenda? Encomenda { get; set; }
    }
}
