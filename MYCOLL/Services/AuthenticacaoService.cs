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
                var response = await http.PostAsJsonAsync("Identity/Login", loginData);

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var authResponse = JsonSerializer.Deserialize<AuthResponse>(
                        json,
                        new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                    if (authResponse != null && !string.IsNullOrEmpty(authResponse.Token))
                    {
                        tokenStorageService.SetToken(authResponse.Token, authResponse.ExpirationTime);

                        Console.WriteLine("Token stored successfully.");
                        Console.WriteLine($"Token: {authResponse.Token.Substring(0, 20)}...");
                        Console.WriteLine("Tipo de Token: " + authResponse.Token.GetType().Name);
                        Console.WriteLine("Expiration Time: " + authResponse.ExpirationTime);
                        Console.WriteLine("Email: " + authResponse.Email);
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
    public class AuthResponse
    {
        public string? Token { get; set; }
        public string? TokenType { get; set; }
        public int ExpirationTime { get; set; }
        public string? Email { get; set; }


    }

    public class loginResult
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
       
    }
}