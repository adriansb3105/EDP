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
    /// Clase para administrar la entidad Interes Personal
    /// </summary>
    public class InteresPersonal
    {
        // Atributos, Getters y Setters
        public int IdInteresPersonal { get; set; }
        public string Descripcion { get; set; }
        public Funcionario Funcionario { get; set; }
    }
}
