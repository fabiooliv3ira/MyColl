using RESTfulAPIMYCOLL.Entities;

namespace RESTfulAPIMYCOLL.Repositories
{
    public interface IProdutoRepository
    {
        Task<IEnumerable<Produto>> ObterProdutosPorCategoriaAsync(int categoriaId);

        Task<IEnumerable<Produto>> ObterProdutosMaisVendidosAsync();

        Task<Produto> ObterDetalheProdutoAsync(int id);

        Task<IEnumerable<Produto>> ObterTodosProdutosAsync();

        Task<bool> EliminarProdutoAsync(int id);

        Task<Produto> AdicionarProdutoAsync(Produto produto);

        Task<Produto> AtualizarProdutoAsync(Produto produto);


    }
}
