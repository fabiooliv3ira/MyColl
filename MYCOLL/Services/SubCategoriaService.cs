using MYCOLL.Entities;

namespace MYCOLL.Services
{
    public class SubCategoriaService
    {
        private readonly HttpClient http;
        public SubCategoriaService(IHttpClientFactory factory)
        {
            this.http = factory.CreateClient("api");
        }
        public async Task<IEnumerable<SubCategoria>> GetSubCategorias()
        {
            var response = await http.GetFromJsonAsync<IEnumerable<SubCategoria>>("api/SubCategorias");
            return response ?? Enumerable.Empty<SubCategoria>();
        }
        public async Task<SubCategoria?> CreateSubCategoria(SubCategoria subCategoria)
        {
            var response = await http.PostAsJsonAsync("api/SubCategorias", subCategoria);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<SubCategoria>();
            }
            return null;
        }
    }
}
