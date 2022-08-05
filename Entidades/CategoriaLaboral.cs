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
    /// Clase para administrar las categorias laborales
    /// </summary>
    public class CategoriaLaboral
    {
        // Atributos, Getters y Setters
        public int IdCategoriaLaboral { get; set; }
        public string Nombre { get; set; }

        // Constructor para crear una Categoria Laboral
        public CategoriaLaboral(int idCategoriaLaboral, string nombre)
        {
            IdCategoriaLaboral = idCategoriaLaboral;
            Nombre = nombre ?? throw new ArgumentNullException(nameof(nombre));
        }

        // Constructor para hacer referencia a una Categoria Laboral
        public CategoriaLaboral(int idCategoriaLaboral)
        {
            IdCategoriaLaboral = idCategoriaLaboral;
        }

        // Constructor por defecto
        public CategoriaLaboral()
        {
        }
    }
}
