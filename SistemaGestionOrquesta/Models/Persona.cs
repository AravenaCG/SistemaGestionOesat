using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaGestionOrquesta.Models
{
     public class Persona
    {
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public string? Documento { get; set; }
        public string? Telefono { get; set; }
        public string? Direccion { get; set; }
        public string? Email { get; set; }

        public Persona(
        )
        {
        }

        public override string ToString()
        {
            return $"\n Nombre: {Nombre},\n Apellido: {Apellido},\n ,\n Documento: {Documento},\n Telefono: {Telefono},\n Email: {Email}.\n";
        }
    }
}
