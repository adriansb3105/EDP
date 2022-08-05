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
    /// Clase para administrar los tipos a los que pertenece la entidad Comprobante
    /// </summary>
    public class TipoComprobante
    {
        // Atributos, Getters y Setters
        public int IdComprobante { get; set; }
        public string Nombre { get; set; }

        // Constructor para referenciar un Tipo de Comprobante
        public TipoComprobante(int idComprobante)
        {
            IdComprobante = idComprobante;
        }

        // Constructor para crear un Tipo de Comprobante
        public TipoComprobante(int idComprobante, string nombre)
        {
            IdComprobante = idComprobante;
            Nombre = nombre ?? throw new ArgumentNullException(nameof(nombre));
        }
    }
}
