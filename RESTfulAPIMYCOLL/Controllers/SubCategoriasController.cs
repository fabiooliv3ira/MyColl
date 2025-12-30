using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RESTfulAPIMYCOLL.Data;
using RESTfulAPIMYCOLL.Entities;
using RESTfulAPIMYCOLL.Repositories;

namespace RESTfulAPIMYCOLL.Controllers
{
	[Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    [ApiController]
	public class SubCategoriasController : ControllerBase
	{
		private readonly ApplicationDbContext _context;
		ISubCategoriasRepository _subCategoriasRepository;

		public SubCategoriasController(ApplicationDbContext context, ISubCategoriasRepository subCategoriasRepository)
		{
			_context = context;
			_subCategoriasRepository = subCategoriasRepository;
		}
		// GET: api/SubCategorias
		[HttpGet]
		[AllowAnonymous]
		public async Task<IEnumerable<SubCategoria>> GetSubCategorias()
		{
			return await _subCategoriasRepository.GetAllSubCategoriasAsync();
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<SubCategoria>> GetSubCategoria(int id)
		{
			var subCategoria = await _subCategoriasRepository.GetSubCategoriaByIdAsync(id);
			if (subCategoria == null)
			{
				return NotFound();
			}
			return subCategoria;
		}

		// GET: api/SubCategorias/PorCategoria/1 (Filtra por categoria mãe)
		[HttpGet("PorCategoria/{categoriaId}")]
		public async Task<IEnumerable<SubCategoria>> GetPorCategoria(int categoriaId)
		{
			return await _subCategoriasRepository.GetSubCategoriasByCategoriaIdAsync(categoriaId);
		}
		[HttpPost]
		public async Task<SubCategoria> PostSubCategoria([FromBody] SubCategoria subCategoria)
		{
			return await _subCategoriasRepository.AddSubCategoriaAsync(subCategoria);
		}
		[HttpPut("{id}")]
		public async Task<SubCategoria?> PutSubCategoria(int id, [FromBody] SubCategoria subCategoria)
		{

			return await _subCategoriasRepository.UpdateSubCategoriaAsync(id, subCategoria);
		}
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteSubCategoria(int id)
		{
			bool deleted = await _subCategoriasRepository.DeleteSubCategoriaAsync(id);
			if (!deleted)
			{
				return NotFound();

			}
			return NoContent();
		}
	}
}