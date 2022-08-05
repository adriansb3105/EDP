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
    /// Clase para administrar la entidad Funcionario
    /// </summary>
    public class Funcionario
    {
        // Atributos, Getters y Setters
        public string NumeroIdentificacion { get; set; }
        public string RutaFotografia { get; set; }
        public string Nombre { get; set; }
        public string PrimerApellido { get; set; }
        public string SegundoApellido { get; set; }
        public string NumeroTelefono { get; set; }
        public DateTime FechaIngreso { get; set; }
        //public string TipoSangre { get; set; }
        //public string LugarResidencia { get; set; }
        public string TipoLicenciaConducir { get; set; }
        public string Puesto { get; set; }
        public string Correo { get; set; }
        public string Extension { get; set; }
        public string Observaciones { get; set; }
        public bool Estado { get; set; }
        public CategoriaLaboral CategoriaLaboral { get; set; }
        public Seccion Seccion { get; set; }
        public UnidadProgramaLaboratorio UnidadProgramaLaboratorio { get; set; }

        // Constructor para crear un funcionario la primera vez con los campos obligatorios
        public Funcionario(string numeroIdentificacion, string nombre, string primerApellido, string segundoApellido, bool estado)
        {
            this.NumeroIdentificacion = numeroIdentificacion ?? throw new ArgumentNullException(nameof(numeroIdentificacion));
            this.Nombre = nombre ?? throw new ArgumentNullException(nameof(nombre));
            this.PrimerApellido = primerApellido ?? throw new ArgumentNullException(nameof(primerApellido));
            this.SegundoApellido = segundoApellido ?? throw new ArgumentNullException(nameof(segundoApellido));
            this.Estado = estado;
        }

        // Constructor para obtener todos los datos de los funcionarios y para actualizarlo
        public Funcionario(string numeroIdentificacion, string rutaFotografia, string nombre, string primerApellido, string segundoApellido, string numeroTelefono, DateTime fechaIngreso, /*string tipoSangre, string lugarResidencia,*/ string tipoLicenciaConducir, string puesto, string correo, string extension, string observaciones, bool estado, CategoriaLaboral categoriaLaboral, Seccion seccion, UnidadProgramaLaboratorio unidadProgramaLaboratorio)
        {
            NumeroIdentificacion = numeroIdentificacion ?? throw new ArgumentNullException(nameof(numeroIdentificacion));
            RutaFotografia = rutaFotografia ?? throw new ArgumentNullException(nameof(rutaFotografia));
            Nombre = nombre ?? throw new ArgumentNullException(nameof(nombre));
            PrimerApellido = primerApellido ?? throw new ArgumentNullException(nameof(primerApellido));
            SegundoApellido = segundoApellido ?? throw new ArgumentNullException(nameof(segundoApellido));
            NumeroTelefono = numeroTelefono ?? throw new ArgumentNullException(nameof(numeroTelefono));
            FechaIngreso = fechaIngreso;
            //TipoSangre = tipoSangre ?? throw new ArgumentNullException(nameof(tipoSangre));
            //LugarResidencia = lugarResidencia ?? throw new ArgumentNullException(nameof(lugarResidencia));
            TipoLicenciaConducir = tipoLicenciaConducir ?? throw new ArgumentNullException(nameof(tipoLicenciaConducir));
            Puesto = puesto ?? throw new ArgumentNullException(nameof(puesto));
            Correo = correo ?? throw new ArgumentNullException(nameof(correo));
            Extension = extension ?? throw new ArgumentNullException(nameof(extension));
            Observaciones = observaciones ?? throw new ArgumentNullException(nameof(observaciones));
            Estado = estado;
            CategoriaLaboral = categoriaLaboral;
            Seccion = seccion;
            UnidadProgramaLaboratorio = unidadProgramaLaboratorio;
        }

        // Constructor para hacer referencia dentro de otros objetos
        public Funcionario(string numeroIdentificacion)
        {
            NumeroIdentificacion = numeroIdentificacion ?? throw new ArgumentNullException(nameof(numeroIdentificacion));
        }

        // Constructor por defecto
        public Funcionario()
        {
        }
    }
}
