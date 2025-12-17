using Azure.Core;

namespace RESTfulAPIMYCOLL.Entities.Dto
{
    public class AuthResponseDto
    {
        public string? AccessToken {get;set;}
        public string? JsonTokenType {get;set;}

        public int ExpiresIn {get;set;}
        public string? EmailTokenProvider {get;set;}

    }
}
