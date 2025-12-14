namespace MYCOLL.Services
{
    public class AuthenticationService
    {
        private readonly HttpClient http;
        public AuthenticationService(IHttpClientFactory factory)
        {
            http = factory.CreateClient("api");
        }
        public async Task<HttpResponseMessage> Login(string email, string password)
        {
            var loginData = new
            {
                Email = email,
                Password = password
            };
            var response = await http.PostAsJsonAsync("Identity/Login", loginData);
            return response;
        }
    }
}
