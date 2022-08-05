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
    /// Clase para administrar la entidad Antecedentes
    /// </summary>
    public class Antecedente
    {
        // Atributos, Getters y Setters
        public int IdAntecedente { get; set; }
        public string Nombre { get; set; }
        public DateTime Fecha { get; set; }
        public string Descripcion { get; set; }
        public string RutaDocumento { get; set; }
        public string NombreDocumento { get; set; }
        public Funcionario Funcionario { get; set; }
        public TipoAntecedente TipoAntecedente { get; set; }
    }
}
