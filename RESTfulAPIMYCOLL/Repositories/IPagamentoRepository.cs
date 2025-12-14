using RESTfulAPIMYCOLL.Entities;
using System.Threading.Tasks;

namespace RESTfulAPIMYCOLL.Repositories
{
	public interface IPagamentoRepository
	{
		// Cria pagamento (Estado inicial: Pendente)
		Task<Pagamento> CreatePagamentoAsync(Pagamento pagamento);

		Task<Pagamento?> GetPagamentoByIdAsync(int id);

		Task<Pagamento?> GetPagamentoByEncomendaIdAsync(int encomendaId);

		Task<bool> SimularSucessoPagamentoAsync(int pagamentoId);

		Task<IEnumerable<Pagamento>> GetPagamentosByUserIdAsync(string userId);
	}
}