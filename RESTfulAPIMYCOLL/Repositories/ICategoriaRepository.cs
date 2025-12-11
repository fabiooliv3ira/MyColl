using RESTfulAPIMYCOLL.Entities;

namespace RESTfulAPIWeb.Repositories
{
	public interface ICategoriaRepository
	{
		Task<IEnumerable<Categoria>> GetCategorias();
	}
}
