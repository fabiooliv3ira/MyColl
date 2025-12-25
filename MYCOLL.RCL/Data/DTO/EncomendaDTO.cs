using System.Collections.Generic;

namespace MYCOLL.RCL.Data.DTO
{
    public record EncomendaDTO
    {
        public int Id { get; init; }
        public string Estado { get; init; } = string.Empty;
        public decimal ValorTotal { get; init; }
        public List<ItemCarrinhoDTO>? ItensCarrinho { get; init; }
    }
}