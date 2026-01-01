using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RESTfulAPIMYCOLL.Data;
using RESTfulAPIMYCOLL.Entities;
using RESTfulAPIMYCOLL.Repositories;
using System.Security.Claims;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RESTfulAPIMYCOLL.Controllers
{
	[Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = "Bearer")]
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
        [AllowAnonymous]
        [HttpGet]
		public async Task<IEnumerable<Produto>> Get()
		{
			return await _produtoRepository.ObterTodosProdutosAsync();
		}

        // GET api/<ProdutosController>/5
        [AllowAnonymous]
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

        [AllowAnonymous]
        [HttpGet("Por-categoria/{categoriaId}")]
		public async Task<IEnumerable<Produto>> GetProdutosPorCategoria(int categoriaId)
		{
			return await _produtoRepository.ObterProdutosPorCategoriaAsync(categoriaId);
		}

		[HttpPost]
		[Authorize(Roles = "Fornecedor, Admin")]
		public async Task<ActionResult<Produto>> Post([FromBody] ProdutoInputModel input)
		{
			var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
			if (string.IsNullOrEmpty(userId))
			{
				return Unauthorized("Token inválido ou sem ID de utilizador.");
			}

			var produto = new Produto
			{
				Nome = input.Nome,
				Descricao = input.Descricao,
				Preco = input.Preco,
				Stock = input.Stock,
				SubcategoriaId = input.SubcategoriaId,
				Imagem = input.Imagem,

				// Campos gerados pela API
				ApplicationUserId = userId,
				EstadoProduto = "Ativo",
				Tipo = "Venda",
				URLImagem = "temp.png" // Placeholder
			};

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

		[HttpGet("Meus")]
		[Authorize(Roles = "Fornecedor")] // Só fornecedores podem chamar este
		public async Task<ActionResult<IEnumerable<Produto>>> GetMeusProdutos()
		{
			// Obtém o ID do utilizador logado a partir do Token
			var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

			if (string.IsNullOrEmpty(userId))
			{
				return Unauthorized();
			}

			var produtos = await _produtoRepository.ObterProdutoPorFuncionario(userId);
			

			return Ok(produtos);
		}

		// Classe auxiliar apenas para receber os dados do Frontend sem validar o User
		public class ProdutoInputModel
		{
			public string Nome { get; set; } = string.Empty;
			public string? Descricao { get; set; }
			public decimal Preco { get; set; }
			public int Stock { get; set; }
			public int SubcategoriaId { get; set; }
			public byte[]? Imagem { get; set; } // Não incluí ApplicationUserId nem URLImagem aqui porque a API gera-os
		}
	}
}
