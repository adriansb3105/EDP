using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    /// <summary>
    /// Adrián Serrano
    /// 03/10/2021
    /// Clase para administrar la entidad Comprobante
    /// </summary>
    public class Comprobante
    {
        // Atributos, Getters y Setters
        public int IdComprobante { get; set; }
        public DateTime Fecha { get; set; }
        public string Descripcion { get; set; }
        public string RutaDocumento { get; set; }
        public string NombreDocumento { get; set; }
        public Funcionario Funcionario { get; set; }
        public TipoComprobante TipoComprobante { get; set; }
    }
}
