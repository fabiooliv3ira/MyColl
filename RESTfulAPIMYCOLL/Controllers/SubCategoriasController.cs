using GestaoMyMedia.Data;
using GestaoMyMedia.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RESTfulAPIMYCOLL.Data;
using RESTfulAPIMYCOLL.Entities;

namespace MyMedia.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class SubCategoriasController : ControllerBase
	{
		private readonly ApplicationDbContext _context;

		public SubCategoriasController(ApplicationDbContext context)
		{
			_context = context;
		}

		// GET: api/SubCategorias
		[HttpGet]
		public async Task<ActionResult<IEnumerable<SubCategoria>>> GetSubCategorias()
		{
			return await _context.SubCategorias.ToListAsync();
		}

		// GET: api/SubCategorias/PorCategoria/1 (Filtra por categoria mãe)
		[HttpGet("PorCategoria/{categoriaId}")]
		public async Task<ActionResult<IEnumerable<SubCategoria>>> GetPorCategoria(int categoriaId)
		{
			return await _context.SubCategorias
				.Where(s => s.CategoriaId == categoriaId)
				.ToListAsync();
		}
	}
}