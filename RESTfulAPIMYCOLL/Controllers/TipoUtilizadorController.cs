using Microsoft.AspNetCore.Mvc;
using RESTfulAPIMYCOLL.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RESTfulAPIMYCOLL.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class TipoUtilizadorController : ControllerBase
	{
		private readonly ITipoUtilizadorRepository _repository;

		public TipoUtilizadorController(ITipoUtilizadorRepository repository)
		{
			_repository = repository;
		}

		// GET: api/TipoUtilizador/All
		[HttpGet("All")]
		public async Task<ActionResult<List<string>>> GetAllTipos()
		{
			var tipos = await _repository.GetAllTiposAsync();
			return Ok(tipos);
		}

		// GET: api/TipoUtilizador/Ativos
		[HttpGet("Ativos")]
		public async Task<ActionResult<List<string>>> GetTiposAtivos()
		{
			var tipos = await _repository.GetTiposAtivosAsync();
			return Ok(tipos);
		}
	}
}