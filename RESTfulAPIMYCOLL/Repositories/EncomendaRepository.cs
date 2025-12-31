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
			return (await _context.Encomendas.AddAsync(encomenda)).Entity;
        }

		public async Task<Encomenda?> GetEncomendaAsync(int encomendaId)
		{
			return await _context.Encomendas
								 .FirstOrDefaultAsync(e => e.Id == encomendaId);
		}

		public async Task<ICollection<Encomenda>> GetUserEncomendasAsync(string userId)
		{
			return await _context.Encomendas
								 .Where(e => e.ApplicationUserId == userId)
								 .ToListAsync();
		}

		public async Task<ICollection<Encomenda>> GetAllEncomendasAsync()
		{
			return await _context.Encomendas
								 .ToListAsync();
		}
	}
}