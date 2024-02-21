using BlazorAppOrquesta.Interfaces;

namespace BlazorAppOrquesta.Model
{
    public class TokenManager : ITokenManager
    {
        public string AccessToken { get; set; }

        public void SetToken(string token)
        {
            AccessToken = token;

        }
    }
}
