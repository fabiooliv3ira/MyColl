using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using MYCOLL.RCL.Data.DTO;
using MYCOLL.RCL.Data.Interfaces;

namespace MYCOLL.RCL.Services
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
				var result = await _http.GetFromJsonAsync<List<ProdutoDTO>>("api/Produtos");
				return result ?? new List<ProdutoDTO>();
			}
			catch
			{
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

		public async Task<ProdutoDTO?> UpdateProdutoAsync(ProdutoDTO produto)
		{
			var response = await _http.PutAsJsonAsync($"api/Produtos/{produto.Id}", produto);
			if (response.IsSuccessStatusCode)
			{
				return await response.Content.ReadFromJsonAsync<ProdutoDTO>(); // A API retorna 204 NoContent ou o objeto?
			}
			return null;
		}

		public async Task<bool> DeleteProdutoAsync(int id)
        {
            var resp = await _http.DeleteAsync($"api/Produtos/{id}");
            return resp.IsSuccessStatusCode;
        }

        // Implemented: returns DTO list for a given category
        public async Task<List<ProdutoDTO>> GetProdutosByCategoria(int categoriaId)
        {
            try
            {
                var resp = await _http.GetFromJsonAsync<List<ProdutoDTO>>($"api/Produtos/Por-categoria/{categoriaId}");
                return resp ?? new List<ProdutoDTO>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao obter produtos por categoria: {ex.Message}");
                return new List<ProdutoDTO>();
            }
        }

		public async Task<List<ProdutoDTO>> GetMeusProdutosAsync()
		{
			try
			{
				var result = await _http.GetFromJsonAsync<List<ProdutoDTO>>("api/Produtos/Meus");
				return result ?? new List<ProdutoDTO>();
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Erro ao obter meus produtos: {ex.Message}");
				return new List<ProdutoDTO>();
			}
		}
	}
}
