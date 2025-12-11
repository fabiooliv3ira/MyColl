using Microsoft.EntityFrameworkCore;
using RESTfulAPIMYCOLL.Data;
using RESTfulAPIMYCOLL.Repositories;

namespace RESTfulAPIMYCOLL.Repositories
{
	public class ProdutoRepository : IProdutoRepository
	{
		private readonly ApplicationDbContext _context;

		public ProdutoRepository(ApplicationDbContext context)
		{
			_context = context;
		}

		public Task<Produtos> ObterDetalhePradutoAsync(int id)
		{
			throw new NotImplementedException();
		}

		public async Task<Produtos> ObterDetalheProdutoAsync(int id)
		{
			var temp = await _context.Produtos
				.Include(p => p.categoria) // Fixed: Include related category
				.FirstOrDefaultAsync(p => p.Id == id);

			return temp != null ? temp : new Produtos();

		}

		public async Task<IEnumerable<Produtos>> ObterProdutosMaisVendidosAsync()
		{
			return await _context.Produtos
				.Where(p => p.MaisVendido) // Assuming you have a property to flag best sellers
				.ToListAsync();
		}

		public Task<IEnumerable<Produtos>> ObterProdutosmaisVendidosAsync()
		{
			throw new NotImplementedException();
		}

		public async Task<IEnumerable<Produtos>> ObterProdutosPorCategoriaAsync(int categoriaId)
		{
			return await _context.Produtos
				.Where(p => p.CategoriaId == categoriaId)
				.Where(x => x.Imagem != null && x.Imagem.Length > 0)
				.Include("modoentrega")
				.Include("categoria")
				.OrderBy(p => p.Nome)
				.ToListAsync();
		}

		public async Task<IEnumerable<Produtos>> ObterProdutosPromocaoAsync()
		{
			return await _context.Produtos
				.Where(p => p.Promocao)
				.OrderByDescending(p => p.Preco)
				.ToListAsync();
		}

		public async Task<IEnumerable<Produtos>> ObterTodosProdutosAsync()
		{
			return await _context.Produtos
				.Include(p => p.categoria)
				.OrderBy(p => p.Nome)
				.ToListAsync();
		}

		public async Task<Produtos> AdicionarProdutoAsync(Produtos produto)
		{
			_context.Produtos.Add(produto);
			await _context.SaveChangesAsync();
			return produto;
		}
		public async Task<Produtos> AtualizarProdutoAsync(Produtos produto)
		{
			_context.Produtos.Update(produto);

			await _context.SaveChangesAsync();

			return produto;
		}
		public async Task<bool> EliminarProdutoAsync(int id)
		{

			var produto = await _context.Produtos.FindAsync(id);
			if (produto == null)
			{
				return false;
			}

			_context.Produtos.Remove(produto);
			try
			{
				await _context.SaveChangesAsync();
				return true;
			}
			catch (Exception ex)
			{
				return false;
			}
		}
	}
}