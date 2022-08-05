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
    /// Clase para administrar la entidad PensionOEmbargo
    /// </summary>
    public class PensionOEmbargo
    {
        // Atributos, Getters y Setters
        public int IdPensionOEmbargo { get; set; }
        public string RutaDocumento { get; set; }
        public string NombreDocumento { get; set; }
        public DateTime FechaIngreso { get; set; }
        public string Descripcion { get; set; }
        public Funcionario Funcionario { get; set; }
    }
}
