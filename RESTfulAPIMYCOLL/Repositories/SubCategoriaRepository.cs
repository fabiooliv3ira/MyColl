using Microsoft.EntityFrameworkCore;
using RESTfulAPIMYCOLL.Data;
using RESTfulAPIMYCOLL.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RESTfulAPIMYCOLL.Repositories
{
	public class SubCategoriasRepository : ISubCategoriasRepository
	{
		private readonly ApplicationDbContext _context;

		public SubCategoriasRepository(ApplicationDbContext context)
		{
			_context = context;
		}

		public async Task<IEnumerable<SubCategoria>> GetAllSubCategoriasAsync()
		{
			return await _context.SubCategorias.ToListAsync();
		}

		public async Task<SubCategoria?> GetSubCategoriaByIdAsync(int id)
		{
			return await _context.SubCategorias.FindAsync(id);
        }

        public async Task<IEnumerable<SubCategoria>> GetSubCategoriasByCategoriaIdAsync(int categoriaId)
		{
			return await _context.SubCategorias
				.Where(s => s.CategoriaId == categoriaId)
				.ToListAsync();
		}
		public async Task<SubCategoria> AddSubCategoriaAsync(SubCategoria subCategoria)
		{
			_context.SubCategorias.Add(subCategoria);
			await _context.SaveChangesAsync();
			return subCategoria;
		}
		public async Task<SubCategoria?> UpdateSubCategoriaAsync(int id, SubCategoria subCategoria)
		{
			var existingSubCategoria = await _context.SubCategorias.FindAsync(id);
			if (existingSubCategoria == null)
			{
				return null;

			}
			existingSubCategoria.Nome = subCategoria.Nome;
			existingSubCategoria.Ativo = subCategoria.Ativo;
			existingSubCategoria.CategoriaId = subCategoria.CategoriaId;
			await _context.SaveChangesAsync();
			return existingSubCategoria;
		}
		public async Task<bool> DeleteSubCategoriaAsync(int id)
		{
			var existingSubCategoria = await _context.SubCategorias.FindAsync(id);
			if (existingSubCategoria == null)
			{
				return false;
			}
			_context.SubCategorias.Remove(existingSubCategoria);
			await _context.SaveChangesAsync();
			return true;
        }
    }
}