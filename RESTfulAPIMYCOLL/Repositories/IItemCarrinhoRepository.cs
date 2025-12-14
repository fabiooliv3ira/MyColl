using RESTfulAPIMYCOLL.Entities;
using System.Threading.Tasks;

namespace RESTfulAPIMYCOLL.Repositories
{
	public interface IItemCarrinhoRepository
	{
		Task<ItemCarrinho?> GetItemCarrinhoAsync(int id);

		// Esta função vai tratar da lógica de "Adicionar novo" OU "Incrementar existente"
		Task<ItemCarrinho> AddOrUpdateItemCarrinhoAsync(ItemCarrinho item);

		Task<bool> RemoveItemCarrinhoAsync(int id);

		// Método auxiliar para verificar permissões no Controller (saber de quem é este item)
		Task<string?> GetUserIdByItemCarrinhoIdAsync(int itemCarrinhoId);

		Task<IEnumerable<ItemCarrinho>> GetItensCarrinhoByUserIdAsync(string userId);
	}
}