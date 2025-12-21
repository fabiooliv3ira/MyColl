// TokenStorageService.cs
using Microsoft.AspNetCore.Http;

namespace MYCOLL.Services
{
    public class TokenStorageService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        
        public TokenStorageService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor ?? 
                throw new ArgumentNullException(nameof(httpContextAccessor));
            Console.WriteLine("[TokenStorageService] Constructor called");
            Console.WriteLine($"[TokenStorageService] HttpContextAccessor is null: {httpContextAccessor == null}");
        }
        
        public string GetToken()
        {
            try
            {
                Console.WriteLine("[TokenStorageService.GetToken] Called");
                
                if (_httpContextAccessor?.HttpContext == null)
                {
                    Console.WriteLine("[TokenStorageService.GetToken] HttpContext is null");
                    return null;
                }
                
                var httpContext = _httpContextAccessor.HttpContext;
                
                // 1. Try Authorization header
                var authHeader = httpContext.Request.Headers["Authorization"];
                if (!string.IsNullOrEmpty(authHeader))
                {
                    var token = authHeader.ToString().Replace("Bearer ", "");
                    if (!string.IsNullOrEmpty(token))
                    {
                        Console.WriteLine($"[TokenStorageService.GetToken] Found token in header: {token.Substring(0, Math.Min(10, token.Length))}...");
                        return token;
                    }
                }
                
                // 2. Try cookies
                var cookieToken = httpContext.Request.Cookies["access_token"];
                if (!string.IsNullOrEmpty(cookieToken))
                {
                    Console.WriteLine($"[TokenStorageService.GetToken] Found token in cookie");
                    return cookieToken;
                }
                
                // 3. Try session
                var sessionToken = httpContext.Session.GetString("access_token");
                if (!string.IsNullOrEmpty(sessionToken))
                {
                    Console.WriteLine($"[TokenStorageService.GetToken] Found token in session");
                    return sessionToken;
                }
                
                Console.WriteLine("[TokenStorageService.GetToken] No token found");
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[TokenStorageService.GetToken] Error: {ex.Message}");
                return null;
            }
        }
        
        public void SetToken(string token, int expiresInSeconds = 3600)
        {
            try
            {
                Console.WriteLine($"[TokenStorageService.SetToken] Setting token: {token?.Substring(0, Math.Min(10, token?.Length ?? 0))}...");
                
                if (_httpContextAccessor?.HttpContext == null)
                {
                    Console.WriteLine("[TokenStorageService.SetToken] HttpContext is null - cannot store token");
                    return;
                }
                
                var httpContext = _httpContextAccessor.HttpContext;
                
                // Store in cookie
                httpContext.Response.Cookies.Append("access_token", token, new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.Strict,
                    Expires = DateTimeOffset.UtcNow.AddSeconds(expiresInSeconds),
                    Path = "/"
                });
                
                // Store in session (if session is enabled)
                httpContext.Session?.SetString("access_token", token);
                
                Console.WriteLine("[TokenStorageService.SetToken] Token stored successfully");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[TokenStorageService.SetToken] Error: {ex.Message}");
            }
        }
        
        public void ClearToken()
        {
            try
            {
                if (_httpContextAccessor?.HttpContext == null) return;
                
                var httpContext = _httpContextAccessor.HttpContext;
                
                httpContext.Response.Cookies.Delete("access_token");
                httpContext.Session?.Remove("access_token");
                
                Console.WriteLine("[TokenStorageService] Token cleared");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[TokenStorageService.ClearToken] Error: {ex.Message}");
            }
        }
    }
}