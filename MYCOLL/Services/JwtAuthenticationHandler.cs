using System.Net.Http.Headers;
namespace MYCOLL.Services
{
    public class JwtAuthenticationHandler : DelegatingHandler
    {
        private readonly TokenStorageService _tokenStorageService;
        public JwtAuthenticationHandler(TokenStorageService tokenStorageService)
        {
            _tokenStorageService = tokenStorageService;
        }
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var token = _tokenStorageService.GetToken();
            if (!string.IsNullOrEmpty(token))
            {
                request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            }
            return await base.SendAsync(request, cancellationToken);
        }
    }
}
