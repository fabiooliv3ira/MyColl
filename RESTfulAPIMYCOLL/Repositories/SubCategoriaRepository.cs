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
    }
}