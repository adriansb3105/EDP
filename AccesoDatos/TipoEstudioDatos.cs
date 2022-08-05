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
    /// Clase para administrar el CRUD para los tipos de estudios que existen
    /// </summary>
    public class TipoEstudioDatos
    {
        private ConexionDatos conexion = new ConexionDatos();

        /// <summary>
        /// Obtiene todos los tipos de estudios de la base de datos
        /// </summary>
        /// <exception cref="Exception">Lanza una excepción si ocurre un error durante la lectura</exception>
        /// <returns>Retorna una lista <code>List<TipoEstudio></code> que contiene los tipos para los estudios formales</returns>
        public List<TipoEstudio> ObtenerTodosEstudio()
        {
            SqlConnection sqlConnection = conexion.conexionEDP();
            List<TipoEstudio> tiposEstudio = new List<TipoEstudio>();

            string consulta = @"SELECT id_tipo_estudio, nombre FROM tipos_estudios where clasificacion='estudio';";
            SqlCommand sqlCommand = new SqlCommand(consulta, sqlConnection);

            SqlDataReader reader;

            try
            {
                sqlConnection.Open();
                reader = sqlCommand.ExecuteReader();

                while (reader.Read())
                {
                    int id = Convert.ToInt32(reader["id_tipo_estudio"].ToString());
                    string nombre = Convert.ToString(reader["nombre"].ToString());

                    TipoEstudio tipoEstudio = new TipoEstudio(id, nombre);

                    tiposEstudio.Add(tipoEstudio);
                }

                sqlConnection.Close();
            }
            catch (Exception exception)
            {
                Estado.ErrorBitacora(exception.Message, "TipoEstudioDatos:ObtenerTodosEstudio()");
            }

            return tiposEstudio;
        }
    }
}
