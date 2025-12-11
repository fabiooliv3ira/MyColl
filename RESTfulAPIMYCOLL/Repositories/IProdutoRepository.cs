using RESTfulAPIMYCOLL.Entities;

namespace RESTfulAPIMYCOLL.Repositories
{
    public interface IProdutoRepository
    {
        Task<IEnumerable<Produtos>> ObterProdutosPorCategoriaAsync(int categoriaId);

        Task<IEnumerable<Produtos>> ObterProdutosPromocaoAsync();

        Task<IEnumerable<Produtos>> ObterProdutosmaisVendidosAsync();

        Task<Produtos> ObterDetalheProdutoAsync(int id);

        Task<IEnumerable<Produtos>> ObterTodosProdutosAsync();

        Task<bool> EliminarProdutoAsync(int id);

        Task<Produtos> AdicionarProdutoAsync(Produtos produto);

        Task<Produtos> AtualizarProdutoAsync(Produtos produto);


    }
}
