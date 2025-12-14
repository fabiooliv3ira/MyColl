using Microsoft.Rest;
using MYCOLL.Entities;

namespace MYCOLL.Services
{
    public class CategoriaService
    {
        private readonly HttpClient http;
        public CategoriaService(IHttpClientFactory factory)
        {
            this.http = factory.CreateClient("api"); 
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
    }
}

