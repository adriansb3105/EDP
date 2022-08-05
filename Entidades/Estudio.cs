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
    /// Clase para administrar la entidad Estudio
    /// </summary>
    public class Estudio
    {
        // Atributos, Getters y Setters
        public int IdEstudio { get; set; }
        public string Nombre { get; set; }
        public string RutaDocumento { get; set; }
        public string NombreDocumento { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFinalizacion { get; set; }
        public string Observacion { get; set; }
        public bool Entregado { get; set; }
        public Funcionario Funcionario { get; set; }
        public TipoEstudio TipoEstudio { get; set; }
    }
}
