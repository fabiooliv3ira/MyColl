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

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var authResponse = JsonSerializer.Deserialize<AuthResponseDto>(
                        json,
                        new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                    if (authResponse != null && !string.IsNullOrEmpty(authResponse.AccessToken))
                    {
                        tokenStorageService.SetToken(authResponse.AccessToken, authResponse.ExpiresIn);

                        Console.WriteLine("---------------------------------");

                        Console.WriteLine("Token stored successfully.");
                        Console.WriteLine($"Token: {authResponse.AccessToken.Substring(0, 20)}...");
                        Console.WriteLine("Tipo de Token: " + authResponse.AccessToken.GetType().Name);
                        Console.WriteLine("Expiration Time: " + authResponse.ExpiresIn);
                        Console.WriteLine("Email: " + authResponse.EmailTokenProvider);
                        Console.WriteLine("---------------------------------");
                    }
                    else
                    {
                        Console.WriteLine("---------------------------------");
                        Console.WriteLine($"{string.IsNullOrEmpty(authResponse.AccessToken)} + {authResponse != null}");
                        Console.WriteLine("---------------------------------");
                    }
                }
                return new loginResult
                {
                    Success = true,
                    Message = "Login feito."

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