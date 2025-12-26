using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MYCOLL.RCL.Data.DTO;
using MYCOLL.RCL.Entities;

namespace MYCOLL.RCL.Data.Interfaces
{
	public interface IProdutoService
	{
		Task<List<Produto>> GetProdutosAsync();
		Task<Produto?> GetProdutoByIdAsync(int id);

		// Fornecedor:
		Task<Produto?> CreateProdutoAsync(Produto produto);
		Task<Produto?> UpdateProdutoAsync(Produto produto);
		Task<bool> DeleteProdutoAsync(int id);
	}
}
