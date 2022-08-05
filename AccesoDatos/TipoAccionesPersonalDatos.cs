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
    /// 26/09/2021
    /// Clase para administrar el CRUD para los tipos de acciones de personal que existen
    /// </summary>
    public class TipoAccionesPersonalDatos
    {
        private ConexionDatos conexion = new ConexionDatos();

        /// <summary>
        /// Obtiene todos los tipos de acciones de personal de la base de datos
        /// </summary>
        /// <exception cref="Exception">Lanza una excepción si ocurre un error durante la lectura</exception>
        /// <returns>Retorna una lista <code>List<TipoAccionesPersonal></code> que contiene los tipos para las acciones de personal</returns>
        public List<TipoAccionPersonal> ObtenerTodos()
        {
            SqlConnection sqlConnection = conexion.conexionEDP();
            List<TipoAccionPersonal> tipoAccionesPersonal = new List<TipoAccionPersonal>();

            string consulta = @"SELECT id_tipo_accion_de_personal, nombre FROM tipo_acciones_de_personal order by nombre;";
            SqlCommand sqlCommand = new SqlCommand(consulta, sqlConnection);

            SqlDataReader reader;

            try
            {
                sqlConnection.Open();
                reader = sqlCommand.ExecuteReader();

                while (reader.Read())
                {
                    int id = Convert.ToInt32(reader["id_tipo_accion_de_personal"].ToString());
                    string nombre = Convert.ToString(reader["nombre"].ToString());

                    TipoAccionPersonal tipoAccionPersonal = new TipoAccionPersonal(id, nombre);

                    tipoAccionesPersonal.Add(tipoAccionPersonal);
                }

                sqlConnection.Close();
            }
            catch (Exception exception)
            {
                Estado.ErrorBitacora(exception.Message, "TipoAccionesPersonalDatos:ObtenerTodos()");
            }

            return tipoAccionesPersonal;
        }
    }
}
