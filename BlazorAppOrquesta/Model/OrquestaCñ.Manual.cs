namespace BlazorAppOrquesta
{
    // Esto "completa" la clase que el generador intenta crear en obj
    public partial class OrquestaCñ
    {
        private string _accessToken = string.Empty;

        public void SetAccessToken(string token)
        {
            _accessToken = token;
            // Aquí podrías agregar la lógica para que el HttpClient 
            // use este token en el Header "Authorization"
        }
    }
}