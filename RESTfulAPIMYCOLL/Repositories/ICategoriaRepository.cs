using RESTfulAPIMYCOLL.Entities;

namespace RESTfulAPIMYCOLL.Repositories
{
	public interface ICategoriaRepository
	{
		Task<IEnumerable<Categoria>> GetCategorias();
		Task<Categoria?> GetCategoriaById(int id);
		Task<Categoria> CreateCategoria(Categoria categoria);
    }

}
