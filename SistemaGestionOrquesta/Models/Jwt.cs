﻿using System.Security.Claims;

namespace SistemaGestionOrquesta.Models
{
    public class Jwt
    {
        public Jwt() { }
        public string  Key { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string  Subject{ get; set; }

            
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

                var id = identity.Claims.FirstOrDefault(x => x.Type == "id").Value;

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