using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RESTfulAPIMYCOLL.Data;
using RESTfulAPIMYCOLL.Repositories;
using RESTfulAPIMYCOLL.Entities;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RESTfulAPIMYCOLL.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProdutosController : ControllerBase
	{
		// GET: api/<ProdutosController>
		private readonly ApplicationDbContext _context;
		IProdutoRepository _produtoRepository;
		public ProdutosController(ApplicationDbContext context, IProdutoRepository produtoRepository)
		{
			_context = context;
			_produtoRepository = produtoRepository;
		}

		[HttpGet]
		public async Task<IEnumerable<Produto>> Get()
		{
			return await _produtoRepository.ObterTodosProdutosAsync();
		}

		// GET api/<ProdutosController>/5
		[HttpGet("{id}")]
		public async Task<Produto> Get(int id)
		{
			return await _produtoRepository.ObterDetalheProdutoAsync(id);
		}

		[HttpGet("Mais-vendidos")]
		public async Task<IEnumerable<Produto>> GetProdutosMaisVendidos()
		{
			return await _produtoRepository.ObterProdutosMaisVendidosAsync();
		}


		[HttpGet("Por-categoria/{categoriaId}")]
		public async Task<IEnumerable<Produto>> GetProdutosPorCategoria(int categoriaId)
		{
			return await _produtoRepository.ObterProdutosPorCategoriaAsync(categoriaId);
		}

		[HttpPost]
		public async Task<Produto> Post([FromBody] Produto produto)
		{
			return await _produtoRepository.AdicionarProdutoAsync(produto);
		}

		// PUT api/<ProdutosController>/5
		[HttpPut]
		public async Task<Produto> Put([FromBody] Produto produto)
		{
			return await _produtoRepository.AtualizarProdutoAsync(produto);
		}

		// DELETE api/<ProdutosController>/5
		[HttpDelete("{id}")]
		public async Task<bool> Delete(int id)
		{
			return await _produtoRepository.EliminarProdutoAsync(id);
		}
		[HttpGet("Por-funcionario/{id}")]
		public async Task<IEnumerable<Produto>> GetProdutoPorFuncionario(string id)
		{
			return await _produtoRepository.ObterProdutoPorFuncionario(id);
        }
    }
}
