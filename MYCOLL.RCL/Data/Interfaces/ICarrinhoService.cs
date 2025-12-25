using System.Collections.Generic;
using System.Threading.Tasks;
using MYCOLL.RCL.Data.DTO;

namespace MYCOLL.RCL.Data.Interfaces
{
    public interface ICarrinhoService
    {
        Task<IEnumerable<ItemCarrinhoDTO>> GetMyCarrinhoAsync();
        Task<ItemCarrinhoDTO?> AddItemAsync(ItemCarrinhoDTO item); // usa POST api/ItemCarrinho
        Task<bool> RemoveItemAsync(int itemId); // DELETE api/ItemCarrinho/{id}
        Task<EncomendaDTO?> CreateEncomendaAsync(EncomendaDTO encomenda); // POST api/Encomenda
    }
}