using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RESTfulAPIMYCOLL.Entities;
using RESTfulAPIMYCOLL.Repositories;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RESTfulAPIMYCOLL.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
    public class ItemCarrinhoController : ControllerBase
	{
		private readonly IItemCarrinhoRepository _itemRepository;
		private readonly IEncomendaRepository _encomendaRepository; // Necessário para verificar dono da encomenda no Add

		public ItemCarrinhoController(IItemCarrinhoRepository itemRepository, IEncomendaRepository encomendaRepository)
		{
			_itemRepository = itemRepository;
			_encomendaRepository = encomendaRepository;
		}

		// GET: api/ItemCarrinho/5
		[HttpGet("{id}")]
		public async Task<ActionResult<ItemCarrinho>> GetItemCarrinho(int id)
		{
			var item = await _itemRepository.GetItemCarrinhoAsync(id);
			if (item == null) return NotFound();

			var ownerId = await _itemRepository.GetUserIdByItemCarrinhoIdAsync(id);
			var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

			if (ownerId != currentUserId && !User.IsInRole(TipoUtilizador.admin))
			{
				return Forbid(); // Ou Unauthorized
			}

			return Ok(item);
		}

		// POST: api/ItemCarrinho
		// Adiciona um item ou incrementa a quantidade se já existir na encomenda
		[HttpPost]
		public async Task<ActionResult<ItemCarrinho>> AddItemCarrinho(ItemCarrinho item)
		{

			if (item.Quantidade <= 0) return BadRequest("A quantidade deve ser maior que zero.");
			try
			{
				item.ApplicationUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var itemAtualizado = await _itemRepository.AddOrUpdateItemCarrinhoAsync(item);
				return Ok(itemAtualizado);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		// DELETE: api/ItemCarrinho/5
		[HttpDelete("{id}")]
		public async Task<IActionResult> RemoveItemCarrinho(int id)
		{
			// 1. Segurança: Verificar se o item pertence ao utilizador antes de apagar
			var ownerId = await _itemRepository.GetUserIdByItemCarrinhoIdAsync(id);
			var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

			if (ownerId == null) return NotFound();

			if (ownerId != currentUserId)
			{
				return Forbid();
			}

			// 2. Apagar
			var sucesso = await _itemRepository.RemoveItemCarrinhoAsync(id);
			if (!sucesso) return NotFound();

			return NoContent();
		}

		// GET: api/ItemCarrinho/GetMyCarrinho
		[HttpGet("GetMyCarrinho")]
		public async Task<ActionResult<IEnumerable<ItemCarrinho>>> GetMyCarrinho()
		{
			// Obtém o ID do utilizador logado no token
			var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

			if (string.IsNullOrEmpty(userId))
			{
				return Unauthorized();
			}

			var itens = await _itemRepository.GetItensCarrinhoByUserIdAsync(userId);

			//Retorna lista vazia se não tiver itens
			return Ok(itens);
		}
	}
}