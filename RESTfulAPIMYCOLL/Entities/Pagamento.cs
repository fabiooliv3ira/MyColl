namespace RESTfulAPIMYCOLL.Entities
{
    public class Pagamento
    {
        public int Id { get; set; }
        public string MetodoPagamento { get; set; } = null!; // e.g. "Cartão de Crédito", "PayPal", "MBWAY"
        public decimal Montante { get; set; }
        public DateTime DataPagamento { get; set; }
        public string EstadoPagamento { get; set; } = null!; // e.g. "Pendente", "Concluído", "Falhado"
        // FK
        public int EncomendaId { get; set; }
    }
}
