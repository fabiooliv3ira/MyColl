using RESTfulAPIMYCOLL.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RESTfulAPIMYCOLL.Repositories
{
	public interface ISubCategoriasRepository
	{
		// Corresponde ao GetSubCategorias
		Task<IEnumerable<SubCategoria>> GetAllSubCategoriasAsync();

		// Corresponde ao GetPorCategoria
		Task<IEnumerable<SubCategoria>> GetSubCategoriasByCategoriaIdAsync(int categoriaId);

		Task<SubCategoria> AddSubCategoriaAsync(SubCategoria subCategoria);

    }
}