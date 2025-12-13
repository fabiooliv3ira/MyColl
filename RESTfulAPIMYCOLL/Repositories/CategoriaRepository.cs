using Microsoft.EntityFrameworkCore;
using RESTfulAPIMYCOLL.Data;
using RESTfulAPIMYCOLL.Entities;

namespace RESTfulAPIMYCOLL.Repositories
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
				.Where(c => c.Ativo)
				.OrderBy(c => c.Nome)
				.ToListAsync();
		}

	}
}
