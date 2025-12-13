using Microsoft.EntityFrameworkCore;
using RESTfulAPIMYCOLL.Data;
using RESTfulAPIMYCOLL.Repositories;
using RESTfulAPIMYCOLL.Entities;


namespace RESTfulAPIMYCOLL.Repositories
{
	public class ProdutoRepository : IProdutoRepository
	{
		private readonly ApplicationDbContext _context;

		public ProdutoRepository(ApplicationDbContext context)
		{
			_context = context;
		}

		public Task<Produto> ObterDetalhePradutoAsync(int id)
		{
			throw new NotImplementedException();
		}

		public async Task<Produto> ObterDetalheProdutoAsync(int id)
		{
			return await _context.Produtos
				.FirstOrDefaultAsync(p => p.Id == id)
				?? new Produto();
		}


		public async Task<IEnumerable<Produto>> ObterProdutosMaisVendidosAsync()
		{
			return await _context.Produtos
				.OrderByDescending(p => p.NrDeVendas)
				.ToListAsync();
		}



		public async Task<IEnumerable<Produto>> ObterProdutosPorCategoriaAsync(int categoriaId)
		{
			return await _context.Produtos
				.Where(p => p.SubCategoria != null &&
							p.SubCategoria.CategoriaId == categoriaId)
				.OrderBy(p => p.Nome)
				.ToListAsync();
		}




		public async Task<IEnumerable<Produto>> ObterTodosProdutosAsync()
		{
			return await _context.Produtos
				.OrderBy(p => p.Id)
				.ToListAsync();
		}

		public async Task<Produto> AdicionarProdutoAsync(Produto produto)
		{
			_context.Produtos.Add(produto);
			await _context.SaveChangesAsync();
			return produto;
		}
		public async Task<Produto> AtualizarProdutoAsync(Produto produto)
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