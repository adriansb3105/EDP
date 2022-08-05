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
    /// Clase para administrar el CRUD para los tipos de comprobantes que existen
    /// </summary>
    public class TipoComprobantesDatos
    {
        private ConexionDatos conexion = new ConexionDatos();

        /// <summary>
        /// Obtiene todos los tipos de comprobantes de la base de datos
        /// </summary>
        /// <exception cref="Exception">Lanza una excepción si ocurre un error durante la lectura</exception>
        /// <returns>Retorna una lista <code>List<TipoComprobante></code> que contiene los tipos para los comprobantes</returns>
        public List<TipoComprobante> ObtenerTodos()
        {
            SqlConnection sqlConnection = conexion.conexionEDP();
            List<TipoComprobante> tiposComprobante = new List<TipoComprobante>();

            string consulta = @"SELECT id_tipo_comprobante, nombre FROM tipos_comprobantes order by nombre;";
            SqlCommand sqlCommand = new SqlCommand(consulta, sqlConnection);

            SqlDataReader reader;

            try
            {
                sqlConnection.Open();
                reader = sqlCommand.ExecuteReader();

                while (reader.Read())
                {
                    int id = Convert.ToInt32(reader["id_tipo_comprobante"].ToString());
                    string nombre = Convert.ToString(reader["nombre"].ToString());

                    TipoComprobante tipoComprobante = new TipoComprobante(id, nombre);

                    tiposComprobante.Add(tipoComprobante);
                }

                sqlConnection.Close();
            }
            catch (Exception exception)
            {
                Estado.ErrorBitacora(exception.Message, "TipoComprobantesDatos:ObtenerTodos()");
            }

            return tiposComprobante;
        }
    }
}
