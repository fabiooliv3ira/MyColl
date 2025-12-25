using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using MYCOLL.RCL.Data.DTO;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Text.Json;
using static System.Net.WebRequestMethods;

namespace MYCOLL.RCL.Services
{
    public class AuthenticacaoService : AuthenticationStateProvider
    {
        private readonly HttpClient http;
        private readonly ILocalStorageService _localStorage;

        public AuthenticacaoService(IHttpClientFactory factory, ILocalStorageService localStorage)
        {
            http = factory.CreateClient("api");
            _localStorage = localStorage;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            string? token = null;

            try
            {
                Console.WriteLine("🔐 GetAuthenticationStateAsync - Verificando token...");

                // Tenta ler o token do armazenamento local
                token = await _localStorage.GetItemAsync<string>("authToken");

                if (!string.IsNullOrEmpty(token))
                {
                    Console.WriteLine("✅ Token encontrado no localStorage");
                }
                else
                {
                    Console.WriteLine("⚠️ Nenhum token no localStorage");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"⚠️ Erro ao ler token do localStorage: {ex.Message}");
            }

            // Se não houver token, retorna utilizador Anónimo (não logado)
            if (string.IsNullOrEmpty(token))
            {
                var anonState = new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
                Console.WriteLine("👤 Utilizador anónimo");
                return anonState;
            }

            try
            {
                // Se houver token, configura o HTTP para o usar e cria a identidade do utilizador
                http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var claims = ParseClaimsFromJwt(token);
                var identity = new ClaimsIdentity(claims, "jwt");
                var user = new ClaimsPrincipal(identity);

                var userName = user.Identity?.Name ?? "Desconhecido";
                var roles = string.Join(", ", user.Claims.Where(c => c.Type.Contains("role")).Select(c => c.Value));

                Console.WriteLine($"✅ Utilizador autenticado: {userName}");
                Console.WriteLine($"🎭 Roles: {roles}");

                return new AuthenticationState(user);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Erro ao processar token: {ex.Message}");

                // Token inválido, remove e retorna anónimo
                try
                {
                    await _localStorage.RemoveItemAsync("authToken");
                }
                catch { }

                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
            }
        }
        public async Task<loginResult> Login(string email, string password)
        {
            Console.WriteLine("🔓 Login - Guardando token...");
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
                        await _localStorage.SetItemAsync("authToken", authResponse.AccessToken);
                        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
                        Console.WriteLine("✅ Token guardado, estado de autenticação notificado");
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

        public async Task<HttpResponseMessage> Register(RegisterDTO registerData)
        {
            Console.WriteLine("📝 Register - Enviando dados de registo...");
            try
            {
                var response = await http.PostAsJsonAsync("api/Auth2/register", registerData);
                Console.WriteLine($"[AuthService] Register response status: {response.StatusCode}");
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("✅ Registo bem-sucedido.");
                }
                else
                {
                    Console.WriteLine("❌ Falha no registo.");
                }
                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocorreu um erro durante o registo: " + ex.Message);
                return new HttpResponseMessage(System.Net.HttpStatusCode.InternalServerError);
            }
           
        }

        public async Task Logout()
        {
            Console.WriteLine("🔒 Logout - Removendo token...");
            await _localStorage.RemoveItemAsync("authToken");
            http.DefaultRequestHeaders.Authorization = null;
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
            Console.WriteLine("✅ Token removido, estado de autenticação notificado");
        }

        private IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
        {
            var handler = new JwtSecurityTokenHandler();
            var token = handler.ReadJwtToken(jwt);
            return token.Claims;
        }
    }

    public class loginResult
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
    }
}