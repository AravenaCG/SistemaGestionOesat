namespace BlazorAppOrquesta.Interfaces
{
    public interface ITokenManager
    {
        string AccessToken { get; set; }
        void SetToken(string token);
    }
}
