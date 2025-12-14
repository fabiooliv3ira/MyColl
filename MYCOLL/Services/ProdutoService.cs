using MYCOLL.Entities;

namespace MYCOLL.Services
{
    public class ProdutoService
    {
        private readonly HttpClient http;
        public ProdutoService(IHttpClientFactory factory)
        {
            this.http = factory.CreateClient("api");
        }
        public async Task<IEnumerable<Produto>> GetProdutos()
        {
            var response = await http.GetFromJsonAsync<IEnumerable<Produto>>("api/Produtos");
            return response ?? Enumerable.Empty<Produto>();
        }
        public async Task<Produto?> GetProdutoById(int id)
        {
            var response = await http.GetFromJsonAsync<Produto>($"api/Produtos/{id}");
            return response;
        }
        public async Task<IEnumerable<Produto>> GetProdutosPorFuncionario(string id)
        {
            var response = await http.GetFromJsonAsync<IEnumerable<Produto>>(
                $"api/Produtos/Por-funcionario/{id}"
            );

            return response ?? Enumerable.Empty<Produto>();
        }
        public async Task<Produto?> CreateProduto(Produto produto)
        {
            var response = await http.PostAsJsonAsync("api/Produtos", produto);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<Produto>();
            }
            return null;
        }
        public async Task<Produto?> UpdateProduto(Produto produto)
        {
            var response = await http.PutAsJsonAsync("api/Produtos", produto);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<Produto>();
            }
            return null;
        }
        public async Task<bool> DeleteProduto(int id)
        {
            var response = await http.DeleteAsync($"api/Produtos/{id}");
            return response.IsSuccessStatusCode;
        }

        public async Task<Produto?> GetProdutosPorID(int id)
        {
            var response = await http.GetFromJsonAsync<Produto>($"api/Produtos/{id}");
            return response;
        }
    }
}
