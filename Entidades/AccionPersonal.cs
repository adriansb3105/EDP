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
    /// Clase para administrar la entidad AccionPersonal
    /// </summary>
    public class AccionPersonal
    {
        // Atributos, Getters y Setters
        public int IdAccionPersonal { get; set; }
        public string Nombre { get; set; }
        public DateTime Periodo { get; set; }
        public string Descripcion { get; set; }
        public string RutaDocumento { get; set; }
        public string NombreDocumento { get; set; }
        public Funcionario Funcionario { get; set; }
        public TipoAccionPersonal TipoAccionPersonal { get; set; }
    }
}
