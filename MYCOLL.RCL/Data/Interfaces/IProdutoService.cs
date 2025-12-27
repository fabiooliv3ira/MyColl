using System.Collections.Generic;
using System.Threading.Tasks;
using MYCOLL.RCL.Data.DTO;

namespace MYCOLL.RCL.Data.Interfaces
{
    public interface IProdutoService
    {
		Task<List<ProdutoDTO>> GetProdutosAsync();
		Task<ProdutoDTO?> GetProdutoByIdAsync(int id);
        Task<ProdutoDTO?> CreateProdutoAsync(ProdutoDTO produto);
        Task<ProdutoDTO?> UpdateProdutoAsync(ProdutoDTO produto);
        Task<bool> DeleteProdutoAsync(int id);
        Task<List<ProdutoDTO>> GetProdutosByCategoria(int categoriaId);
    }
}
