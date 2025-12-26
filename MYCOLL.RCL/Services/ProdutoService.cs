using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using MYCOLL.RCL.Data.DTO;
using MYCOLL.RCL.Data.Interfaces;
using MYCOLL.RCL.Entities;

namespace MYCOLL.RCL.Services
{
	public class ProdutoService : IProdutoService
	{
		private readonly HttpClient _http;

		public ProdutoService(HttpClient http)
		{
			_http = http;
		}

		public async Task<List<Produto>> GetProdutosAsync()
		{
			try
			{
				// ATENÇÃO: Confirma se a rota na tua API é "api/Produtos"
				var resultado = await _http.GetFromJsonAsync<List<Produto>>("api/Produtos");
				return resultado ?? new List<Produto>();
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Erro: {ex.Message}");
				return new List<Produto>();
			}
		}

		public async Task<Produto?> GetProdutoByIdAsync(int id)
		{
			try
			{
				return await _http.GetFromJsonAsync<Produto>($"api/Produtos/{id}");
			}
			catch
			{
				return null;
			}
		}

		// Create
		public async Task<Produto?> CreateProdutoAsync(Produto produto)
		{
			var resp = await _http.PostAsJsonAsync("api/Produtos", produto);
			if (resp.IsSuccessStatusCode)
			{
				return await resp.Content.ReadFromJsonAsync<Produto>();
			}
			var text = await resp.Content.ReadAsStringAsync();
			throw new HttpRequestException($"CreateProduto failed: {resp.StatusCode} - {text}");
		}

		// Update
		public async Task<Produto?> UpdateProdutoAsync(Produto produto)
		{
			var resp = await _http.PutAsJsonAsync($"api/Produtos/{produto.Id}", produto);
			if (resp.IsSuccessStatusCode)
			{
				return await resp.Content.ReadFromJsonAsync<Produto>();
			}
			var text = await resp.Content.ReadAsStringAsync();
			throw new HttpRequestException($"UpdateProduto failed: {resp.StatusCode} - {text}");
		}

		public async Task<IEnumerable<Produto>> GetProdutosByCategoria(int catId)
		{
			var resp = await _http.GetFromJsonAsync<IEnumerable<Produto>>($"api/Produtos/Por-categoria/{catId}");
			return resp ?? Enumerable.Empty<Produto>();
        }

		// Delete
		public async Task<bool> DeleteProdutoAsync(int id)
		{
			var resp = await _http.DeleteAsync($"api/Produtos/{id}");
			return resp.IsSuccessStatusCode;
		}
	}
}
