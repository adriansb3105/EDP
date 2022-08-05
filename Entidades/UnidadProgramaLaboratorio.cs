using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    /// <summary>
    /// Adrián Serrano
    /// 16/10/2021
    /// Clase para administrar la entidad Unidad, Programa, Laboratorio
    /// </summary>
    public class UnidadProgramaLaboratorio
    {
        // Atributos, Getters y Setters
        public int IdUnidadProgramaLaboratorio { get; set; }
        public string Sigla { get; set; }
        public string Nombre { get; set; }

        // Constructor para crear una entidad UnidadProgramaLaboratorio
        public UnidadProgramaLaboratorio(int idUnidadProgramaLaboratorio, string sigla, string nombre)
        {
            this.IdUnidadProgramaLaboratorio = idUnidadProgramaLaboratorio;
            this.Sigla = sigla ?? throw new ArgumentNullException(nameof(sigla));
            this.Nombre = nombre ?? throw new ArgumentNullException(nameof(nombre));
        }

        // Constructor para hacer referencia a una Categoria Laboral
        public UnidadProgramaLaboratorio(int idUnidadProgramaLaboratorio)
        {
            this.IdUnidadProgramaLaboratorio = idUnidadProgramaLaboratorio;
        }
        // Constructor por defecto
        public UnidadProgramaLaboratorio()
        {
        }
    }
}
