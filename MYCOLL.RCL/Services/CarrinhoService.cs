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
        private readonly ILocalStorageService _localStorage;

        public CarrinhoService(HttpClient http, ILocalStorageService localStorage)
        {
            _http = http;
            _localStorage = localStorage;
        }

        private async Task EnsureAuthHeaderAsync()
        {
            var token = await _localStorage.GetItemAsync<string>("authToken");
            if (!string.IsNullOrWhiteSpace(token))
            {
                _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
            else
            {
                _http.DefaultRequestHeaders.Authorization = null;
            }
        }

        public async Task<IEnumerable<ItemCarrinhoDTO>> GetMyCarrinhoAsync()
        {
            await EnsureAuthHeaderAsync();
            var resp = await _http.GetFromJsonAsync<IEnumerable<ItemCarrinhoDTO>>("api/ItemCarrinho/GetMyCarrinho");
            return resp ?? new List<ItemCarrinhoDTO>();
        }

        public async Task<ItemCarrinhoDTO?> AddItemAsync(ItemCarrinhoDTO item)
        {
            await EnsureAuthHeaderAsync();
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
            await EnsureAuthHeaderAsync();
            var response = await _http.DeleteAsync($"api/ItemCarrinho/{itemId}");
            return response.IsSuccessStatusCode;
        }

        public async Task<EncomendaDTO?> CreateEncomendaAsync(EncomendaDTO encomenda)
        {
            await EnsureAuthHeaderAsync();
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