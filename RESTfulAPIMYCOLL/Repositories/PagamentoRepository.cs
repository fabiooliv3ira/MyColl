using Microsoft.EntityFrameworkCore;
using RESTfulAPIMYCOLL.Data;
using RESTfulAPIMYCOLL.Entities;
using System;
using System.Threading.Tasks;

namespace RESTfulAPIMYCOLL.Repositories
{
	public class PagamentoRepository : IPagamentoRepository
	{
		private readonly ApplicationDbContext _context;

		public PagamentoRepository(ApplicationDbContext context)
		{
			_context = context;
		}

		public async Task<Pagamento> CreatePagamentoAsync(Pagamento pagamento)
		{
			_context.Pagamentos.Add(pagamento);
			await _context.SaveChangesAsync();
			return pagamento;
		}

		public async Task<Pagamento?> GetPagamentoByIdAsync(int id)
		{
			return await _context.Pagamentos
				.Include(p => p.Encomenda) // Incluir encomenda para verificar owner no controller
				.FirstOrDefaultAsync(p => p.Id == id);
		}

		public async Task<Pagamento?> GetPagamentoByEncomendaIdAsync(int encomendaId)
		{
			return await _context.Pagamentos
				.FirstOrDefaultAsync(p => p.EncomendaId == encomendaId);
		}

		public async Task<bool> SimularSucessoPagamentoAsync(int pagamentoId)
		{
			var pagamento = await _context.Pagamentos
				.Include(p => p.Encomenda)
				.FirstOrDefaultAsync(p => p.Id == pagamentoId);

			if (pagamento == null) return false;

			pagamento.EstadoPagamento = "Concluído";
			pagamento.DataPagamento = DateTime.Now;

			// Atualiza a Encomenda associada para "Pendente"
			if (pagamento.Encomenda != null)
			{
				pagamento.Encomenda.Estado = "Pendente";
			}

			await _context.SaveChangesAsync();
			return true;
		}

		public async Task<IEnumerable<Pagamento>> GetPagamentosByUserIdAsync(string userId)
		{
			return await _context.Pagamentos
				.Include(p => p.Encomenda)
				.Where(p => p.Encomenda.ApplicationUserId == userId)
				.OrderByDescending(p => p.DataPagamento)
				.ToListAsync();
		}
	}
}