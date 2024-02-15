namespace SistemaGestionOrquesta.Models
{
    public class Usuario
    {
        public string IdUsuario { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Rol { get; set; }

        public static List<Usuario> DB()
        {
            var list = new List<Usuario>();
            list.Add(
                new Usuario
                {
                    IdUsuario = "1",
                    User = "mateo",
                    Password = "123",
                    Email = "administrador@gmail.com",
                    Rol = "Admin"
                });
            list.Add(
                new Usuario
                {
                    IdUsuario = "2",
                    User = "Marcos",
                    Password = "098.",
                    Email = "administrador@gmail.com",
                    Rol = "empleado"
                });
            return list;
        }
    }
}
