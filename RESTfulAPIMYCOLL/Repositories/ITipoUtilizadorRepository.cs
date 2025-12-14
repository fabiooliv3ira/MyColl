using System.Collections.Generic;
using System.Threading.Tasks;

namespace RESTfulAPIMYCOLL.Repositories
{
	public interface ITipoUtilizadorRepository
	{
		Task<List<string>> GetAllTiposAsync();
		Task<List<string>> GetTiposAtivosAsync();
	}
}