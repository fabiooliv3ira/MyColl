using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net.Http.Json;
using MYCOLL.RCL.Data.DTO;
using MYCOLL.RCL.Data.Interfaces;

namespace MYCOLL.RCL.Data.Services
{
	public class ProdutoService : IProdutoService
	{
		private readonly HttpClient _http;

		public ProdutoService(HttpClient http)
		{
			_http = http;
		}

		public async Task<List<ProdutoDTO>> GetProdutosAsync()
		{
			try
			{
				// ATENÇÃO: Confirma se a rota na tua API é "api/Produtos"
				var resultado = await _http.GetFromJsonAsync<List<ProdutoDTO>>("api/Produtos");
				return resultado ?? new List<ProdutoDTO>();
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Erro: {ex.Message}");
				return new List<ProdutoDTO>();
			}
		}

		public async Task<ProdutoDTO?> GetProdutoByIdAsync(int id)
		{
			try
			{
				return await _http.GetFromJsonAsync<ProdutoDTO>($"api/Produtos/{id}");
			}
			catch
			{
				return null;
			}
		}
	}
}
