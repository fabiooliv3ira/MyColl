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
	[Authorize]
	public class PagamentoController : ControllerBase
	{
		private readonly IPagamentoRepository _pagamentoRepository;
		private readonly IEncomendaRepository _encomendaRepository;

		public PagamentoController(IPagamentoRepository pagamentoRepository, IEncomendaRepository encomendaRepository)
		{
			_pagamentoRepository = pagamentoRepository;
			_encomendaRepository = encomendaRepository;
		}

		// POST: api/Pagamento
		// Cria um registo de pagamento "Pendente"
		[HttpPost]
		public async Task<ActionResult<Pagamento>> CriarIntencaoPagamento(Pagamento pagamento)
		{
			// 1. Verificar se a encomenda existe e pertence ao user
			var encomenda = await _encomendaRepository.GetEncomendaAsync(pagamento.EncomendaId);

			if (encomenda == null) return NotFound("Encomenda não encontrada.");

			var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
			if (encomenda.ApplicationUserId != currentUserId)
			{
				return BadRequest("Não pode pagar uma encomenda que não é sua.");
			}

			if (encomenda.Estado != "NaoPaga")
			{
				return BadRequest("Esta encomenda já foi paga ou processada.");
			}

			// 2. Definir dados automáticos (segurança contra injeção de dados falsos)
			pagamento.Montante = encomenda.ValorTotal; // O valor vem da BD, não do input do user
			pagamento.EstadoPagamento = "Pendente";
			pagamento.DataPagamento = DateTime.Now; // Data de criação da intenção

			var novoPagamento = await _pagamentoRepository.CreatePagamentoAsync(pagamento);

			return Ok(novoPagamento);
		}

		// POST: api/Pagamento/Simular/5
		// Botão mágico para concluir o pagamento
		[HttpPost("Simular/{id}")]
		public async Task<IActionResult> SimularPagamento(int id)
		{
			// 1. Buscar pagamento para validar dono
			var pagamento = await _pagamentoRepository.GetPagamentoByIdAsync(id);
			if (pagamento == null) return NotFound();

			// 2. Verificar permissões
			var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
			// Nota: Precisamos do Include na Encomenda feito no Repository para aceder ao UserId
			if (pagamento.Encomenda != null && pagamento.Encomenda.ApplicationUserId != currentUserId)
			{
				return Forbid();
			}

			// 3. Executar simulação
			var sucesso = await _pagamentoRepository.SimularSucessoPagamentoAsync(id);

			if (!sucesso) return BadRequest("Erro ao processar pagamento.");

			return Ok(new { message = "Pagamento simulado com sucesso! Encomenda agora está Pendente." });
		}

		// GET: api/Pagamento/PorEncomenda/5
		[HttpGet("PorEncomenda/{encomendaId}")]
		public async Task<ActionResult<Pagamento>> GetPagamentoPorEncomenda(int encomendaId)
		{
			// Validar dono da encomenda antes de mostrar dados
			var encomenda = await _encomendaRepository.GetEncomendaAsync(encomendaId);
			if (encomenda == null) return NotFound();

			var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
			if (encomenda.ApplicationUserId != currentUserId && !User.IsInRole(TipoUtilizador.admin))
			{
				return Forbid();
			}

			var pagamento = await _pagamentoRepository.GetPagamentoByEncomendaIdAsync(encomendaId);
			if (pagamento == null) return NotFound("Nenhum pagamento encontrado para esta encomenda.");

			return Ok(pagamento);
		}


		// GET: api/Pagamento/MeusPagamentos
		[HttpGet("MeusPagamentos")]
		public async Task<ActionResult<IEnumerable<Pagamento>>> GetMeusPagamentos()
		{
			// Obtém o ID do utilizador logado através do Token
			var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

			if (string.IsNullOrEmpty(userId))
			{
				return Unauthorized();
			}

			var pagamentos = await _pagamentoRepository.GetPagamentosByUserIdAsync(userId);

			// Retorna a lista (mesmo que vazia)
			return Ok(pagamentos);
		}
	}
}