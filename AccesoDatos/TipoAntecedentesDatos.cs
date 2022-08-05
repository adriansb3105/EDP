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
    /// 03/10/2021
    /// Clase para administrar el CRUD para los tipos de antecedentes que existen
    /// </summary>
    public class TipoAntecedentesDatos
    {
        private ConexionDatos conexion = new ConexionDatos();

        /// <summary>
        /// Obtiene todos los tipos de antecedentes de la base de datos
        /// </summary>
        /// <exception cref="Exception">Lanza una excepción si ocurre un error durante la lectura</exception>
        /// <returns>Retorna una lista <code>List<TipoAntecedente></code> que contiene los tipos para los antecedentes</returns>
        public List<TipoAntecedente> ObtenerTodos()
        {
            SqlConnection sqlConnection = conexion.conexionEDP();
            List<TipoAntecedente> tipoAntecedentes = new List<TipoAntecedente>();

            string consulta = @"SELECT id_tipo_antecedente, nombre FROM tipos_antecedentes order by nombre;";
            SqlCommand sqlCommand = new SqlCommand(consulta, sqlConnection);

            SqlDataReader reader;

            try
            {
                sqlConnection.Open();
                reader = sqlCommand.ExecuteReader();

                while (reader.Read())
                {
                    int id = Convert.ToInt32(reader["id_tipo_antecedente"].ToString());
                    string nombre = Convert.ToString(reader["nombre"].ToString());

                    TipoAntecedente tipoAntecedente = new TipoAntecedente(id, nombre);

                    tipoAntecedentes.Add(tipoAntecedente);
                }

                sqlConnection.Close();
            }
            catch (Exception exception)
            {
                Estado.ErrorBitacora(exception.Message, "TipoAntecedentesDatos:ObtenerTodos()");
            }

            return tipoAntecedentes;
        }
    }
}
