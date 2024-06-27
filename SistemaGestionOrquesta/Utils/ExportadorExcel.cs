using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaGestionOrquesta.Utils
{
    using ClosedXML.Excel;
    using System;
    using System.Collections.Generic;
    using System.IO;

    public class ExportadorExcel
    {
        public static void ExportarAExcel<T>(List<T> lista, string nombreArchivo)
        {
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Datos");

                // Obtener las propiedades de la clase T
                var propiedades = typeof(T).GetProperties();

                // Agregar encabezados de columna
                for (int i = 0; i < propiedades.Length; i++)
                {
                    worksheet.Cell(1, i + 1).Value = propiedades[i].Name;
                }

                // Agregar datos a las celdas
                for (int fila = 0; fila < lista.Count; fila++)
                {
                    for (int columna = 0; columna < propiedades.Length; columna++)
                    {
                        worksheet.Cell(fila + 2, columna + 1).Value = (XLCellValue)propiedades[columna].GetValue(lista[fila]);
                    }
                }

                // Guardar el archivo Excel
                workbook.SaveAs(nombreArchivo);
            }

            Console.WriteLine($"Exportación a Excel completada. Archivo guardado en: {Path.GetFullPath(nombreArchivo)}");
        }
    }

  /*  class Program
    {
        static void Main()
        {
            // Ejemplo de uso
            List<Estudiante> listaEstudiantes = ObtenerListaEstudiantes(); // Reemplaza con tu propia lógica para obtener la lista
            ExportadorExcel.ExportarAExcel(listaEstudiantes, "Estudiantes.xlsx");
        }

        // Ejemplo de método para obtener una lista de estudiantes (reemplaza con tu propia lógica)
        static List<Estudiante> ObtenerListaEstudiantes()
        {
            // Lógica para obtener la lista de estudiantes desde tu base de datos u otro origen
            // Solo es un ejemplo, reemplázalo con tu propia implementación
            return new List<Estudiante>
        {
            new Estudiante { Nombre = "Juan", Apellido = "Pérez", Edad = 20 },
            new Estudiante { Nombre = "María", Apellido = "López", Edad = 22 },
            // Agrega más estudiantes según sea necesario
        };
        }
    }

    public class Estudiante
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public int Edad { get; set; }
        // Agrega más propiedades según sea necesario
    }
  */
}
