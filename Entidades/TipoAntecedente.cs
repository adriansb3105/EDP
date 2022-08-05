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
    /// Clase para administrar los tipos a los que pertenece la entidad Antecedente
    /// </summary>
    public class TipoAntecedente
    {
        // Atributos, Getters y Setters
        public int IdAntecedente { get; set; }
        public string Nombre { get; set; }

        // Constructor para referenciar un Tipo de Antecedente
        public TipoAntecedente(int idAntecedente)
        {
            IdAntecedente = idAntecedente;
        }

        // Constructor para crear un Tipo de Antecedente
        public TipoAntecedente(int idAntecedente, string nombre)
        {
            IdAntecedente = idAntecedente;
            Nombre = nombre ?? throw new ArgumentNullException(nameof(nombre));
        }
    }
}
