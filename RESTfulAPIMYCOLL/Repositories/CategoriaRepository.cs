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
		public async Task<Categoria?> GetCategoriaById(int id)
		{
			return await dbContext.Categorias
				.FirstOrDefaultAsync(c => c.Id == id && c.Ativo);
		}
		public async Task<Categoria> CreateCategoria(Categoria categoria)
		{
			Categoria novaCategoria = (await dbContext.Categorias.AddAsync(categoria)).Entity;
			await dbContext.SaveChangesAsync();
			return novaCategoria;
		}
		public async Task<Categoria?> UpdateCategoria(int id, Categoria categoria)
		{
			Categoria? categoriaExistente = await GetCategoriaById(id);
			if (categoriaExistente == null)
			{
				return null;

			}
			categoriaExistente.Nome = categoria.Nome;
			categoriaExistente.Ativo = categoria.Ativo;
			await dbContext.SaveChangesAsync();
			return categoriaExistente;
		}
		public async Task<bool> DeleteCategoria(int id)
		{
			Categoria? categoriaExistente = await GetCategoriaById(id);
			if (categoriaExistente == null)
			{
				return false;
			}
			dbContext.Categorias.Remove(categoriaExistente);
            await dbContext.SaveChangesAsync();
			return true;
        }
    }
}
