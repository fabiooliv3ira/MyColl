using RESTfulAPIMYCOLL.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RESTfulAPIMYCOLL.Repositories
{
	public interface IEncomendaRepository
	{
		Task<Encomenda> CreateEncomendaAsync(Encomenda encomenda);
		Task<Encomenda?> GetEncomendaAsync(int encomendaId);
		Task<ICollection<Encomenda>> GetUserEncomendasAsync(string userId);
		Task<ICollection<Encomenda>> GetAllEncomendasAsync(); // Adicionado para administradores/funcionários
	}
}