using RESTfulAPIMYCOLL.Entities.Dto;
using System.Text.Json;

namespace MYCOLL.Services
{
    public class AuthenticacaoService
    {
        private readonly HttpClient http;
        private readonly TokenStorageService tokenStorageService;
        public AuthenticacaoService(IHttpClientFactory factory, TokenStorageService tokenStorageService)
        {
            http = factory.CreateClient("api");
            this.tokenStorageService = tokenStorageService;
        }
        public async Task<loginResult> Login(string email, string password)
        {
            var loginData = new
            {
                Email = email,
                Password = password
            };

            try
            {
                var response = await http.PostAsJsonAsync("api/Auth2", loginData);

                Console.WriteLine($"[AuthService] Login response status: {response.StatusCode}");

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var authResponse = JsonSerializer.Deserialize<AuthResponseDto>(
                        json,
                        new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                    Console.WriteLine($"[AuthService] Received token null? {string.IsNullOrEmpty(authResponse?.AccessToken)} ExpiresIn={authResponse?.ExpiresIn}");

                    if (authResponse != null && !string.IsNullOrEmpty(authResponse.AccessToken))
                    {
                        tokenStorageService.SetToken(authResponse.AccessToken, authResponse.ExpiresIn);
                        Console.WriteLine("[AuthService] TokenStorageService.SetToken called.");
                    }
                    else
                    {
                        Console.WriteLine("[AuthService] No token returned from API.");
                    }
                }
                return new loginResult
                {
                    Success = response.IsSuccessStatusCode,
                    Message = response.IsSuccessStatusCode ? "Login successful." : "Login failed."
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred during login: " + ex.Message);
            }
            return new loginResult
            {
                Success = false,
                Message = "An error occurred during login."
            };
        }
    }

    public class loginResult
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
    }
}