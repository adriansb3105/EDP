using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    /// <summary>
    /// Adrián Serrano
    /// 23/10/2021
    /// Clase para administrar las secciones
    /// </summary>
    public class Seccion
    {
        // Atributos, Getters y Setters
        public int IdSeccion { get; set; }
        public string Nombre { get; set; }

        // Constructor para crear una Seccion
        public Seccion(int idSeccion, string nombre)
        {
            IdSeccion = idSeccion;
            Nombre = nombre ?? throw new ArgumentNullException(nameof(nombre));
        }

        // Constructor para hacer referencia a una Seccion
        public Seccion(int idSeccion)
        {
            IdSeccion = idSeccion;
        }

        // Constructor por defecto
        public Seccion()
        {
        }
    }
}
