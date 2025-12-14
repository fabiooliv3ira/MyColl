using RESTfulAPIMYCOLL.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RESTfulAPIMYCOLL.Repositories
{
	public class TipoUtilizadorRepository : ITipoUtilizadorRepository
	{
		public Task<List<string>> GetAllTiposAsync()
		{
			return Task.FromResult(TipoUtilizador.GetAllTipos());
		}

		public Task<List<string>> GetTiposAtivosAsync()
		{
			return Task.FromResult(TipoUtilizador.GetTiposAtivos());
		}
	}
}