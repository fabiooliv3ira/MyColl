using Microsoft.EntityFrameworkCore;
using RESTfulAPIMYCOLL.Data;
using RESTfulAPIMYCOLL.Entities;

namespace RESTfulAPIWeb.Repositories
{
	public class CategoriaRepository : ICategoriaRepository
	{
		private readonly ApplicationDbContext dbContext;
		public CategoriaRepository(ApplicationDbContext dbContext)
		{
			this.dbContext = dbContext;
		}
		public async Task<IEnumerable<Categoria>> GetCategorias()
		{
			return await dbContext.Categorias
				.Where(c => c.Imagem != null && c.Imagem.Length > 0)
				.OrderBy(O => O.Ordem)
				.ThenBy(p => p.Nome)
				.ToListAsync();
		}
	}
}
