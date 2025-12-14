using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RESTfulAPIMYCOLL.Data;
using RESTfulAPIMYCOLL.Entities;
using RESTfulAPIMYCOLL.Repositories;

namespace RESTfulAPIMYCOLL.Controllers
{
	[Route("api/[controller]")]
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
		public async Task<IEnumerable<SubCategoria>> GetSubCategorias()
		{
			return await _subCategoriasRepository.GetAllSubCategoriasAsync();
		}
		// GET: api/SubCategorias/PorCategoria/1 (Filtra por categoria mãe)
		[HttpGet("PorCategoria/{categoriaId}")]
		public async Task<IEnumerable<SubCategoria>> GetPorCategoria(int categoriaId)
		{
			return await _subCategoriasRepository.GetSubCategoriasByCategoriaIdAsync(categoriaId);
        }
		[HttpPost]
		public async Task<SubCategoria> PostSubCategoria([FromBody]SubCategoria subCategoria)
		{
;			return await _subCategoriasRepository.AddSubCategoriaAsync(subCategoria);
        }
    }
}