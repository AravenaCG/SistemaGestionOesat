using System.Security.Claims;

namespace SistemaGestionOrquesta.Models
{
    public class Jwt
    {
        public Jwt() { }
        public string Key { get; set; } = null!;
        public string Issuer { get; set; } = null!;
        public string Audience { get; set; } = null!;
        public string Subject { get; set; } = null!;


        public static dynamic ValidarToken(ClaimsIdentity identity)
        {
            try
            {
                if (identity.Claims.Count() == 0)
                {
                    return new
                    {
                        succes = false,
                        message = "Verificar Token",
                        result = ""
                    };
                }

                var id = identity.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier || x.Type == "id")?.Value;
                Usuario usuario = Usuario.DB().FirstOrDefault(x => x.IdUsuario == id);

                return new
                {
                    succes = true,
                    message = "EXITO",
                    result = usuario
                };
            }
            catch (Exception ex)
            {
                return new
                {
                    succes = false,
                    message = "Catch:" + ex.Message,
                    result = ""
                };
            }
        }

    }
}
