using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using MYCOLL.RCL.Data.Interfaces;
using MYCOLL.RCL.Data.DTO;
using Blazored.LocalStorage;

namespace MYCOLL.RCL.Services
{
    public class CarrinhoService : ICarrinhoService
    {
        private readonly HttpClient _http;

        public CarrinhoService(HttpClient http)
        {
            _http = http;
        }

        public async Task<IEnumerable<ItemCarrinhoDTO>> GetMyCarrinhoAsync()
        {
 
            var resp = await _http.GetFromJsonAsync<IEnumerable<ItemCarrinhoDTO>>("api/ItemCarrinho/GetMyCarrinho");
            return resp ?? new List<ItemCarrinhoDTO>();
        }

        public async Task<ItemCarrinhoDTO?> AddItemAsync(ItemCarrinhoDTO item)
        {
            var response = await _http.PostAsJsonAsync("api/ItemCarrinho", item);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<ItemCarrinhoDTO>();
            }
            // opcional: lançar erro para UI ver
            var text = await response.Content.ReadAsStringAsync();
            throw new HttpRequestException($"AddItem failed: {response.StatusCode} - {text}");
        }

        public async Task<bool> RemoveItemAsync(int itemId)
        {
            var response = await _http.DeleteAsync($"api/ItemCarrinho/{itemId}");
            return response.IsSuccessStatusCode;
        }

        public async Task<EncomendaDTO?> CreateEncomendaAsync(EncomendaDTO encomenda)
        {
            var response = await _http.PostAsJsonAsync("api/Encomenda", encomenda);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<EncomendaDTO>();
            }
            var text = await response.Content.ReadAsStringAsync();
            throw new HttpRequestException($"CreateEncomenda failed: {response.StatusCode} - {text}");
        }
    }
}