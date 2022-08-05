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
    /// Clase para administrar los tipos a los que pertenece la entidad Estudio
    /// </summary>
    public class TipoEstudio
    {
        // Atributos, Getters y Setters
        public int IdTipoEstudio { get; set; }
        public string Nombre { get; set; }

        // Constructor para referenciar un Tipo de Estudio
        public TipoEstudio(int idTipoEstudio)
        {
            IdTipoEstudio = idTipoEstudio;
        }

        // Constructor para crear un Tipo de Estudio
        public TipoEstudio(int idTipoEstudio, string nombre)
        {
            IdTipoEstudio = idTipoEstudio;
            Nombre = nombre ?? throw new ArgumentNullException(nameof(nombre));
        }
    }
}
