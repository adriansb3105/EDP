using Entidades;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos
{
    /// <summary>
    /// Adrián Serrano
    /// 13/10/2021
    /// Clase para administrar toda la información de los datos laborales
    /// </summary>
    public class InformacionLaboralDatos
    {
        private ConexionDatos conexion = new ConexionDatos();

        /// <summary>
        /// Obtiene todas las categorías laborales de la base de datos
        /// </summary>
        /// <exception cref="Exception">Lanza una excepción si ocurre un error durante la lectura</exception>
        /// <returns>Retorna un diccionario <code>Dictionary</code> que contiene las categorías laborales</returns>
        public Dictionary<int, string> ObtenerCategoriasLaborales()
        {
            SqlConnection sqlConnection = conexion.conexionEDP();

            Dictionary<int, string> categoriasLaborales = new Dictionary<int, string>();
            string consulta = @"SELECT id_categoria_laboral, nombre FROM categoria_laboral order by nombre;";

            SqlCommand sqlCommand = new SqlCommand(consulta, sqlConnection);
            SqlDataReader reader;

            try
            {
                sqlConnection.Open();
                reader = sqlCommand.ExecuteReader();

                while (reader.Read())
                {
                    int idCategoriaLaboral = Convert.ToInt32(reader["id_categoria_laboral"].ToString());
                    string nombre = Convert.ToString(reader["nombre"].ToString());

                    categoriasLaborales.Add(idCategoriaLaboral, nombre);
                }

                sqlConnection.Close();
            }
            catch (Exception exception)
            {
                Estado.ErrorBitacora(exception.Message, "InformacionLaboralDatos:ObtenerCategoriasLaborales()");
            }

            return categoriasLaborales;
        }

        /// <summary>
        /// Obtiene la categoría laboral de la base de datos basada en un Id
        /// </summary>
        /// <exception cref="Exception">Lanza una excepción si ocurre un error durante la lectura</exception>
        /// <returns>Retorna una <code>CategoriaLaboral</code></returns>
        public CategoriaLaboral ObtenerCategoriaLaboralPorId(int idCategoriaLaboral)
        {
            SqlConnection sqlConnection = conexion.conexionEDP();
            CategoriaLaboral categoriaLaboral = new CategoriaLaboral();

            string consulta = @"SELECT id_categoria_laboral, nombre FROM categoria_laboral where id_categoria_laboral = " + idCategoriaLaboral + " order by nombre;";

            SqlCommand sqlCommand = new SqlCommand(consulta, sqlConnection);
            SqlDataReader reader;

            try
            {
                sqlConnection.Open();
                reader = sqlCommand.ExecuteReader();

                if (reader.Read())
                {
                    int id = Convert.ToInt32(reader["id_categoria_laboral"].ToString());
                    string nombre = Convert.ToString(reader["nombre"].ToString());

                    categoriaLaboral = new CategoriaLaboral(id, nombre);
                }

                sqlConnection.Close();
            }
            catch (Exception exception)
            {
                Estado.ErrorBitacora(exception.Message, "InformacionLaboralDatos:ObtenerCategoriaLaboralPorId()");
            }

            return categoriaLaboral;
        }

        /// <summary>
        /// Obtiene todas las secciones de la base de datos
        /// </summary>
        /// <exception cref="Exception">Lanza una excepción si ocurre un error durante la lectura</exception>
        /// <returns>Retorna un diccionario <code>Dictionary</code> que contiene las seccion</returns>
        public Dictionary<int, string> ObtenerSecciones()
        {
            SqlConnection sqlConnection = conexion.conexionEDP();

            Dictionary<int, string> secciones = new Dictionary<int, string>();
            string consulta = @"SELECT id_seccion, nombre FROM seccion;";

            SqlCommand sqlCommand = new SqlCommand(consulta, sqlConnection);
            SqlDataReader reader;

            try
            {
                sqlConnection.Open();
                reader = sqlCommand.ExecuteReader();

                while (reader.Read())
                {
                    int idSeccion = Convert.ToInt32(reader["id_seccion"].ToString());
                    string nombre = Convert.ToString(reader["nombre"].ToString());

                    secciones.Add(idSeccion, nombre);
                }

                sqlConnection.Close();
            }
            catch (Exception exception)
            {
                Estado.ErrorBitacora(exception.Message, "InformacionLaboralDatos:ObtenerSecciones()");
            }

            return secciones;
        }

        /// <summary>
        /// Obtiene la sección de la base de datos basada en un Id
        /// </summary>
        /// <exception cref="Exception">Lanza una excepción si ocurre un error durante la lectura</exception>
        /// <returns>Retorna una <code>Seccion</code></returns>
        public Seccion ObtenerSeccionPorId(int idSeccion)
        {
            SqlConnection sqlConnection = conexion.conexionEDP();

            Seccion seccion = new Seccion();
            string consulta = @"SELECT id_seccion, nombre FROM seccion where id_seccion = " + idSeccion + " order by nombre;";

            SqlCommand sqlCommand = new SqlCommand(consulta, sqlConnection);
            SqlDataReader reader;

            try
            {
                sqlConnection.Open();
                reader = sqlCommand.ExecuteReader();

                if (reader.Read())
                {
                    int id = Convert.ToInt32(reader["id_seccion"].ToString());
                    string nombre = Convert.ToString(reader["nombre"].ToString());

                    seccion = new Seccion(id, nombre);
                }

                sqlConnection.Close();
            }
            catch (Exception exception)
            {
                Estado.ErrorBitacora(exception.Message, "InformacionLaboralDatos:ObtenerSeccionPorId()");
            }

            return seccion;
        }

        /// <summary>
        /// Obtiene todas las unidades, programas y laboratorios de la base de datos
        /// </summary>
        /// <exception cref="Exception">Lanza una excepción si ocurre un error durante la lectura</exception>
        /// <returns>Retorna una lista <code>List<UnidadProgramaLaboratorio></code> que contiene todas las UnidadProgramaLaboratorios</returns>
        public List<UnidadProgramaLaboratorio> ObtenerUnidadesProgramasLaboratorios()
        {
            SqlConnection sqlConnection = conexion.conexionEDP();
            List<UnidadProgramaLaboratorio> unidadProgramaLaboratorios = new List<UnidadProgramaLaboratorio>();

            string consulta = @"SELECT id_unidad_programa_laboratorio,sigla,nombre FROM unidad_programa_laboratorio order by nombre;";
            SqlCommand sqlCommand = new SqlCommand(consulta, sqlConnection);

            SqlDataReader reader;

            try
            {
                sqlConnection.Open();
                reader = sqlCommand.ExecuteReader();

                while (reader.Read())
                {
                    int id = Convert.ToInt32(reader["id_unidad_programa_laboratorio"].ToString());
                    string sigla = Convert.ToString(reader["sigla"].ToString());
                    string nombre = Convert.ToString(reader["nombre"].ToString());

                    UnidadProgramaLaboratorio unidadProgramaLaboratorio = new UnidadProgramaLaboratorio(id, sigla, nombre);
                    unidadProgramaLaboratorios.Add(unidadProgramaLaboratorio);
                }

                sqlConnection.Close();
            }
            catch (Exception exception)
            {
                Estado.ErrorBitacora(exception.Message, "InformacionLaboralDatos:ObtenerUnidadesProgramasLaboratorios()");
            }

            return unidadProgramaLaboratorios;
        }

        /// <summary>
        /// Obtiene la unidad, programa y laboratorio de la base de datos
        /// </summary>
        /// <exception cref="Exception">Lanza una excepción si ocurre un error durante la lectura</exception>
        /// <returns>Retorna una <code>UnidadProgramaLaboratorio</code></returns>
        public UnidadProgramaLaboratorio ObtenerUnidadProgramaLaboratorioPorId(int idUnidadProgramaLaboratorio)
        {
            SqlConnection sqlConnection = conexion.conexionEDP();
            UnidadProgramaLaboratorio unidadProgramaLaboratorio = new UnidadProgramaLaboratorio();

            string consulta = @"SELECT id_unidad_programa_laboratorio,sigla,nombre FROM unidad_programa_laboratorio where id_unidad_programa_laboratorio = " + idUnidadProgramaLaboratorio + " order by nombre;";
            SqlCommand sqlCommand = new SqlCommand(consulta, sqlConnection);

            SqlDataReader reader;

            try
            {
                sqlConnection.Open();
                reader = sqlCommand.ExecuteReader();

                if (reader.Read())
                {
                    int id = Convert.ToInt32(reader["id_unidad_programa_laboratorio"].ToString());
                    string sigla = Convert.ToString(reader["sigla"].ToString());
                    string nombre = Convert.ToString(reader["nombre"].ToString());

                    unidadProgramaLaboratorio = new UnidadProgramaLaboratorio(id, sigla, nombre);
                }

                sqlConnection.Close();
            }
            catch (Exception exception)
            {
                Estado.ErrorBitacora(exception.Message, "InformacionLaboralDatos:ObtenerUnidadProgramaLaboratorioPorId()");
            }

            return unidadProgramaLaboratorio;
        }
    }
}
