namespace MYCOLL.Services
{
    public class TokenStorageService
    {
        private string? _token;
        private DateTime? _expirationTime;

        public void SetToken(string token, int expirationTime)
        {
            _token = token;
            _expirationTime = DateTime.UtcNow.AddSeconds(expirationTime);
        }
        public string? GetToken()
        {
            if (_token != null && _expirationTime.HasValue && DateTime.UtcNow < _expirationTime.Value)
            {
                return _token;
            }
            ClearToken();
            return null;
        }
        public void ClearToken()
        {
            _token = null;
            _expirationTime = null;
        }
    }
}
