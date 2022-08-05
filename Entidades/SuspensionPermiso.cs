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
    /// Clase para administrar la entidad SuspensionPermiso
    /// </summary>
    public class SuspensionPermiso
    {
        // Atributos, Getters y Setters
        public int IdSuspensionPermiso { get; set; }
        public DateTime FechaSalida { get; set; }
        public DateTime FechaRegreso { get; set; }
        public int Tipo { get; set; }
        public string RutaDocumento { get; set; }
        public string NombreDocumento { get; set; }
        public string Descripcion { get; set; }
        public Funcionario Funcionario { get; set; }
    }
}
