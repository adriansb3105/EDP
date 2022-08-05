using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    /// <summary>
    /// Adrián Serrano
    /// 26/09/2021
    /// Clase para administrar la entidad Experiencia Laboral
    /// </summary>
    public class ExperienciaLaboral
    {
        // Atributos, Getters y Setters
        public int IdExperienciaLaboral { get; set; }
        public string NombreEmpresa { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFinalizacion { get; set; }
        public string DescripcionPuesto { get; set; }
        public Funcionario Funcionario { get; set; }
    }
}
