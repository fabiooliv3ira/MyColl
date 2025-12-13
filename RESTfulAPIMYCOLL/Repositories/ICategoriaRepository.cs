using RESTfulAPIMYCOLL.Entities;

namespace RESTfulAPIMYCOLL.Repositories
{
	public interface ICategoriaRepository
	{
		Task<IEnumerable<Categoria>> GetCategorias();
	}
}
