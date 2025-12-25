using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
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

		// Create
		public async Task<ProdutoDTO?> CreateProdutoAsync(ProdutoDTO produto)
		{
			var resp = await _http.PostAsJsonAsync("api/Produtos", produto);
			if (resp.IsSuccessStatusCode)
			{
				return await resp.Content.ReadFromJsonAsync<ProdutoDTO>();
			}
			var text = await resp.Content.ReadAsStringAsync();
			throw new HttpRequestException($"CreateProduto failed: {resp.StatusCode} - {text}");
		}

		// Update
		public async Task<ProdutoDTO?> UpdateProdutoAsync(ProdutoDTO produto)
		{
			var resp = await _http.PutAsJsonAsync($"api/Produtos/{produto.Id}", produto);
			if (resp.IsSuccessStatusCode)
			{
				return await resp.Content.ReadFromJsonAsync<ProdutoDTO>();
			}
			var text = await resp.Content.ReadAsStringAsync();
			throw new HttpRequestException($"UpdateProduto failed: {resp.StatusCode} - {text}");
		}

		// Delete
		public async Task<bool> DeleteProdutoAsync(int id)
		{
			var resp = await _http.DeleteAsync($"api/Produtos/{id}");
			return resp.IsSuccessStatusCode;
		}
	}
}
