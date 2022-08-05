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
    /// Clase para administrar los tipos a los que pertenece la entidad AccionPersonal
    /// </summary>
    public class TipoAccionPersonal
    {
        // Atributos, Getters y Setters
        public int IdAccionPersonal { get; set; }
        public string Nombre { get; set; }

        // Constructor para referenciar un Tipo de AccionPersonal
        public TipoAccionPersonal(int idAccionPersonal)
        {
            IdAccionPersonal = idAccionPersonal;
        }

        // Constructor para crear un Tipo de AccionPersonal
        public TipoAccionPersonal(int idAccionPersonal, string nombre)
        {
            IdAccionPersonal = idAccionPersonal;
            Nombre = nombre ?? throw new ArgumentNullException(nameof(nombre));
        }
    }
}
