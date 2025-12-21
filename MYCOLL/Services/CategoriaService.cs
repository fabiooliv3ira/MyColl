using Microsoft.Rest;
using MYCOLL.Entities;
using System.Net.Http.Headers;

namespace MYCOLL.Services
{
    public class CategoriaService
    {
        private readonly HttpClient http;
        private readonly TokenStorageService _tokenStorage;

        public CategoriaService(IHttpClientFactory factory, TokenStorageService tokenStorage)
        {
            this.http = factory.CreateClient("api");
            _tokenStorage = tokenStorage;
        }

        public async Task<IEnumerable<Categoria>> GetCategorias()
        {
            
            var response = await http.GetFromJsonAsync<IEnumerable<Categoria>>("api/Categorias");
            return response ?? Enumerable.Empty<Categoria>();
        }
        public async Task<Categoria?> CreateCategoria(Categoria categoria)
        {
            var response = await http.PostAsJsonAsync("api/Categorias", categoria);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<Categoria>();
            }
            return null;
        }
        public async Task<Categoria?> UpdateCategoria(int id, Categoria categoria)
        {
            var response = await http.PutAsJsonAsync($"api/Categorias/{id}", categoria);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<Categoria>();
            }
            return null;
        }
        public async Task<bool> DeleteCategoria(int id)
        {
            var response = await http.DeleteAsync($"api/Categorias/{id}");
            return response.IsSuccessStatusCode;
        }
        public async Task<Categoria?> GetCategoriaById(int id)
        {
            var response = await http.GetFromJsonAsync<Categoria>($"api/Categorias/{id}");
            return response;
        }
    }
}

