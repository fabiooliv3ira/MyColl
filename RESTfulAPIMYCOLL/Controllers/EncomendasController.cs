using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RESTfulAPIMYCOLL.Data;
using RESTfulAPIMYCOLL.Entities;
using RESTfulAPIMYCOLL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RESTfulAPIMYCOLL.Controllers
{
	[Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    [ApiController]
	public class EncomendaController : ControllerBase
	{
		private readonly IEncomendaRepository _encomendaRepository;
		private readonly UserManager<ApplicationUser> _userManager;

		public EncomendaController(IEncomendaRepository encomendaRepository, UserManager<ApplicationUser> userManager)
		{
			_encomendaRepository = encomendaRepository;
			_userManager = userManager;
		}

		// Método auxiliar para obter o ID do utilizador autenticado
		private string GetCurrentUserId()
		{
			return User.FindFirstValue(ClaimTypes.NameIdentifier);
		}

		// POST: api/Encomenda
		[HttpPost]
		[Authorize(Roles = TipoUtilizador.cliente)]
		public async Task<ActionResult<Encomenda>> CreateEncomenda([FromBody] Encomenda encomenda)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			// Dados automáticos do sistema
			encomenda.UserId = GetCurrentUserId();
			encomenda.Data = DateTime.UtcNow;
			encomenda.Estado = "NaoPaga";

			if (encomenda.ItensCarrinho == null || !encomenda.ItensCarrinho.Any())
			{
				return BadRequest("A encomenda deve conter pelo menos um item.");
			}

			try
			{
				// O Repository agora trata de buscar os preços aos produtos e calcular totais
				var createdEncomenda = await _encomendaRepository.CreateEncomendaAsync(encomenda);

				return CreatedAtAction(nameof(GetEncomenda), new { id = createdEncomenda.Id }, createdEncomenda);
			}
			catch (Exception ex)
			{
				// Apanha erros como "Produto não encontrado" ou "Stock insuficiente" lançados pelo repo
				return BadRequest(ex.Message);
			}
		}

		// GET: api/Encomenda/{id}
		// Obtém uma encomenda específica pelo seu Id.
		[HttpGet("{id}")]
		[Authorize(Roles = TipoUtilizador.cliente + "," + TipoUtilizador.admin + "," + TipoUtilizador.funcionario)]
		public async Task<ActionResult<Encomenda>> GetEncomenda(int id)
		{
			var encomenda = await _encomendaRepository.GetEncomendaAsync(id);

			if (encomenda == null)
			{
				return NotFound($"Encomenda com o Id {id} não encontrada.");
			}

			var currentUserId = GetCurrentUserId();
			var currentUser = await _userManager.FindByIdAsync(currentUserId);
			var roles = await _userManager.GetRolesAsync(currentUser!); // Assuming user will not be null if authenticated

			// Permite acesso se for o proprietário da encomenda OU um Admin/Funcionario
			if (encomenda.UserId == currentUserId || roles.Contains(TipoUtilizador.admin) || roles.Contains(TipoUtilizador.funcionario))
			{
				return Ok(encomenda);
			}
			else
			{
				return Forbid(); // Utilizador autenticado mas sem permissão para ver esta encomenda
			}
		}

		// GET: api/Encomenda/user
		// Obtém todas as encomendas do utilizador autenticado
		[HttpGet("user")]
		public async Task<ActionResult<ICollection<Encomenda>>> GetUserEncomendas()
		{
			var userId = GetCurrentUserId();
			var encomendas = await _encomendaRepository.GetUserEncomendasAsync(userId);

			if (encomendas == null || !encomendas.Any())
			{
				return NotFound($"Nenhuma encomenda encontrada para o utilizador atual.");
			}

			return Ok(encomendas);
		}

		// GET: api/Encomenda/all
		// Obtém todas as encomendas registadas no sistema (apenas para Administradores e Funcionários).
		[HttpGet("all")]
		public async Task<ActionResult<ICollection<Encomenda>>> GetAllEncomendas()
		{
			var encomendas = await _encomendaRepository.GetAllEncomendasAsync();

			if (encomendas == null || !encomendas.Any())
			{
				return NotFound("Nenhuma encomenda encontrada.");
			}

			return Ok(encomendas);
		}
	}
}