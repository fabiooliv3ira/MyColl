using Microsoft.EntityFrameworkCore;
using RESTfulAPIMYCOLL.Data;
using RESTfulAPIMYCOLL.Entities;
using System.Threading.Tasks;

namespace RESTfulAPIMYCOLL.Repositories
{
	public class ItemCarrinhoRepository : IItemCarrinhoRepository
	{
		private readonly ApplicationDbContext _context;

		public ItemCarrinhoRepository(ApplicationDbContext context)
		{
			_context = context;
		}

		public async Task<ItemCarrinho?> GetItemCarrinhoAsync(int id)
		{
			return await _context.ItensCarrinho
				.Include(i => i.Produto) // Incluímos o produto para ver detalhes se necessário
				.FirstOrDefaultAsync(i => i.Id == id);
		}

		public async Task<ItemCarrinho> AddOrUpdateItemCarrinhoAsync(ItemCarrinho item)
		{
			// 1. Verificar se este produto já existe nesta encomenda específica
			var itemExistente = await _context.ItensCarrinho
				.FirstOrDefaultAsync(i => i.ApplicationUserId == item.ApplicationUserId && i.ProdutoId == item.ProdutoId);

			// Vamos buscar o preço do produto para garantir que o Subtotal fica correto
			var produto = await _context.Produtos.FindAsync(item.ProdutoId);
			if (produto == null) throw new Exception("Produto não encontrado.");

			if (itemExistente != null)
			{
				// JÁ EXISTE: Aumentar a quantidade
				itemExistente.Quantidade += item.Quantidade;

				// Recalcular Subtotal (Quantidade Atualizada * Preço do Produto)
				itemExistente.Subtotal = itemExistente.Quantidade * produto.Preco;

				// Atualizar no contexto
				_context.ItensCarrinho.Update(itemExistente);
				await _context.SaveChangesAsync();
				return itemExistente;
			}
			else
			{
				_context.ItensCarrinho.Add(item);
				await _context.SaveChangesAsync();
				return item;
			}
		}

		public async Task<bool> RemoveItemCarrinhoAsync(int id)
		{
			var item = await _context.ItensCarrinho.FindAsync(id);
			if (item == null) return false;

			_context.ItensCarrinho.Remove(item);
			await _context.SaveChangesAsync();
			return true;
		}

		// Auxiliar para segurança: Ajudar o controller a verificar se o user é dono do item
		public async Task<string?> GetUserIdByItemCarrinhoIdAsync(int itemCarrinhoId)
		{
			var item = await _context.ItensCarrinho
				.FirstOrDefaultAsync(i => i.Id == itemCarrinhoId);

			return item?.ApplicationUserId;
		}

		public async Task<IEnumerable<ItemCarrinho>> GetItensCarrinhoByUserIdAsync(string userId)
		{
			return await _context.ItensCarrinho
				.Include(i => i.Produto) 
				.Where(i => i.ApplicationUserId == userId)
				.ToListAsync();
		}
	}
}