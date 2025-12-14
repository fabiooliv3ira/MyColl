using Microsoft.EntityFrameworkCore;
using RESTfulAPIMYCOLL.Data;
using RESTfulAPIMYCOLL.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RESTfulAPIMYCOLL.Repositories
{
	public class EncomendaRepository : IEncomendaRepository
	{
		private readonly ApplicationDbContext _context;

		public EncomendaRepository(ApplicationDbContext context)
		{
			_context = context;
		}

		public async Task<Encomenda> CreateEncomendaAsync(Encomenda encomenda)
		{
			decimal totalEncomenda = 0;

			// Iterar sobre os itens que vêm do Frontend (que trazem ProdutoId e Quantidade)
			foreach (var item in encomenda.ItensCarrinho)
			{
				// 1. Ir buscar o Produto à BD para obter o PREÇO REAL
				var produto = await _context.Produtos.FindAsync(item.ProdutoId);

				if (produto == null)
				{
					throw new Exception($"Produto com ID {item.ProdutoId} não encontrado.");
				}

				// 2. Validar stock (Opcional, mas recomendado)
				if (produto.Stock < item.Quantidade)
				{
					throw new Exception($"Stock insuficiente para o produto {produto.Nome}.");
				}

				// 3. Calcular o Subtotal usando o preço do Produto
				item.Subtotal = item.Quantidade * produto.Preco;

				// 4. Somar ao total da Encomenda
				totalEncomenda += item.Subtotal;

				// Garantir que o EF entende que isto é um novo item e não uma atualização
				item.Id = 0;
			}

			// Definir o valor total calculado
			encomenda.ValorTotal = totalEncomenda;

			_context.Encomendas.Add(encomenda);
			await _context.SaveChangesAsync();

			return encomenda;
		}

		public async Task<Encomenda?> GetEncomendaAsync(int encomendaId)
		{
			return await _context.Encomendas
								 .Include(e => e.ItensCarrinho)! // Inclui os itens do carrinho
									 .ThenInclude(ic => ic.Produto) // E para cada item, inclui os detalhes do Produto
								 .FirstOrDefaultAsync(e => e.Id == encomendaId);
		}

		public async Task<ICollection<Encomenda>> GetUserEncomendasAsync(string userId)
		{
			return await _context.Encomendas
								 .Include(e => e.ItensCarrinho)!
									 .ThenInclude(ic => ic.Produto)
								 .Where(e => e.UserId == userId)
								 .ToListAsync();
		}

		public async Task<ICollection<Encomenda>> GetAllEncomendasAsync()
		{
			return await _context.Encomendas
								 .Include(e => e.ItensCarrinho)!
									 .ThenInclude(ic => ic.Produto)
								 .ToListAsync();
		}
	}
}