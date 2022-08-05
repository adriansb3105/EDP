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
    /// Clase para administrar el CRUD para los comprobantes
    /// </summary>
    public class ComprobantesDatos
    {
        private ConexionDatos conexion = new ConexionDatos();

        /// <summary>
        /// Obtiene todos los comprobantes de la base de datos según el número de identificación dado
        /// </summary>
        /// <param name="numeroIdentificacionFuncionario">Número de identificación para buscar sus comprobantes</param>
        /// <exception cref="Exception">Lanza una excepción si ocurre un error durante la lectura</exception>
        /// <returns>Retorna una lista <code>List<Comprobante></code> que contiene los comprobantes para el funcionario dado</returns>
        public List<Comprobante> ObtenerPorId(string numeroIdentificacionFuncionario)
        {
            SqlConnection sqlConnection = conexion.conexionEDP();
            List<Comprobante> comprobantes = new List<Comprobante>();

            string consulta = @"SELECT id_comprobante, fecha, descripcion, ruta_documento, nombre_documento, 
            numero_identificacion_funcionario, id_tipo_comprobante FROM comprobante
            WHERE numero_identificacion_funcionario = '" + numeroIdentificacionFuncionario + "' order by fecha;";

            SqlCommand sqlCommand = new SqlCommand(consulta, sqlConnection);

            SqlDataReader reader;

            try
            {
                sqlConnection.Open();
                reader = sqlCommand.ExecuteReader();

                while (reader.Read())
                {
                    Comprobante comprobante = new Comprobante();

                    comprobante.IdComprobante = Convert.ToInt32(reader["id_comprobante"].ToString());
                    comprobante.Fecha = Convert.ToDateTime(reader["fecha"].ToString());
                    comprobante.Descripcion = Convert.ToString(reader["descripcion"].ToString());
                    comprobante.RutaDocumento = Convert.ToString(reader["ruta_documento"].ToString());
                    comprobante.NombreDocumento = Convert.ToString(reader["nombre_documento"].ToString());
                    string numeroIdentificacion = Convert.ToString(reader["numero_identificacion_funcionario"].ToString());
                    comprobante.Funcionario = new Funcionario(numeroIdentificacion);
                    comprobante.TipoComprobante = new TipoComprobante(Convert.ToInt32(reader["id_tipo_comprobante"].ToString()));

                    comprobantes.Add(comprobante);
                }

                sqlConnection.Close();
            }
            catch (Exception exception)
            {
                Estado.ErrorBitacora(exception.Message, "ComprobantesDatos:ObtenerPorId()");
            }

            return comprobantes;
        }

        /// <summary>
        /// Inserta la entidad Comprobante en la base de datos
        /// </summary>
        /// <param name="comprobante">Elemento de tipo <code>Comprobante</code> que va a ser insertado</param>
        /// <exception cref="Exception">Lanza una excepción si ocurre un error durante la escrita a la base de datos</exception>
        /// <returns>Retorna un entero con el código según sea el resultado</returns>
        public int Insertar(Comprobante comprobante)
        {
            SqlConnection sqlConnection = conexion.conexionEDP();
            int resultado = 0;

            SqlCommand sqlCommand = new SqlCommand("insert into comprobante(fecha, descripcion, nombre_documento, " +
                "ruta_documento,numero_identificacion_funcionario, id_tipo_comprobante)" +
                "output INSERTED.id_comprobante values(@fecha, @descripcion, @nombre_documento, @ruta_documento," +
                "@numero_identificacion_funcionario, @id_tipo_comprobante);", sqlConnection);

            sqlCommand.Parameters.AddWithValue("@fecha", comprobante.Fecha);
            sqlCommand.Parameters.AddWithValue("@descripcion", comprobante.Descripcion);
            sqlCommand.Parameters.AddWithValue("@nombre_documento", comprobante.NombreDocumento);
            sqlCommand.Parameters.AddWithValue("@ruta_documento", comprobante.RutaDocumento);
            sqlCommand.Parameters.AddWithValue("@numero_identificacion_funcionario", comprobante.Funcionario.NumeroIdentificacion);
            sqlCommand.Parameters.AddWithValue("@id_tipo_comprobante", comprobante.TipoComprobante.IdComprobante);

            sqlConnection.Open();

            try
            {
                /// Retorna el identificador con el cuál fue insertado
                resultado = Convert.ToInt32(sqlCommand.ExecuteScalar());
            }
            catch (Exception exception)
            {
                /// Ocurre un error durante la escrita a la base de datos
                resultado = Estado.ERROR_INESPERADO;
                Estado.ErrorBitacora(exception.Message, "ComprobantesDatos:Insertar()");
            }

            sqlConnection.Close();

            return resultado;
        }

        /// <summary>
        /// Actualiza la entidad Comprobante en la base de datos
        /// </summary>
        /// <param name="comprobante">Elemento de tipo <code>Comprobante</code> que va a ser actualizado</param>
        /// <exception cref="Exception">Lanza una excepción si ocurre un error durante la escrita a la base de datos</exception>
        /// <returns>Retorna un entero con el código según sea el resultado</returns>
        public int Actualizar(Comprobante comprobante)
        {
            SqlConnection sqlConnection = conexion.conexionEDP();
            int resultado = 0;

            SqlCommand sqlCommand = new SqlCommand("update comprobante set fecha=@fecha, descripcion=@descripcion, nombre_documento=@nombre_documento, " +
                "ruta_documento=@ruta_documento, numero_identificacion_funcionario=@numero_identificacion_funcionario, id_tipo_comprobante=@id_tipo_comprobante " +
                "output INSERTED.id_comprobante where id_comprobante=@id_comprobante;", sqlConnection);

            sqlCommand.Parameters.AddWithValue("@fecha", comprobante.Fecha);
            sqlCommand.Parameters.AddWithValue("@descripcion", comprobante.Descripcion);
            sqlCommand.Parameters.AddWithValue("@nombre_documento", comprobante.NombreDocumento);
            sqlCommand.Parameters.AddWithValue("@ruta_documento", comprobante.RutaDocumento);
            sqlCommand.Parameters.AddWithValue("@numero_identificacion_funcionario", comprobante.Funcionario.NumeroIdentificacion);
            sqlCommand.Parameters.AddWithValue("@id_tipo_comprobante", comprobante.TipoComprobante.IdComprobante);
            sqlCommand.Parameters.AddWithValue("@id_comprobante", comprobante.IdComprobante);

            sqlConnection.Open();

            try
            {
                /// Retorna el identificador con el cuál fue actualizado
                resultado = Convert.ToInt32(sqlCommand.ExecuteScalar());
            }
            catch (Exception exception)
            {
                /// Ocurre un error durante la escrita a la base de datos
                resultado = Estado.ERROR_INESPERADO;
                Estado.ErrorBitacora(exception.Message, "ComprobantesDatos:Actualizar()");
            }

            sqlConnection.Close();

            return resultado;
        }

        /// <summary>
        /// Elimina la entidad Comprobante en la base de datos
        /// </summary>
        /// <param name="comprobante">Elemento de tipo <code>Comprobante</code> que va a ser eliminado</param>
        /// <exception cref="Exception">Lanza una excepción si ocurre un error durante la escrita a la base de datos</exception>
        /// <returns>Retorna un entero con el código según sea el resultado</returns>
        public int Eliminar(int idComprobante)
        {
            SqlConnection sqlConnection = conexion.conexionEDP();
            int resultado = 0;

            SqlCommand sqlCommand = new SqlCommand("delete from comprobante where id_comprobante=@id_comprobante;", sqlConnection);
            sqlCommand.Parameters.AddWithValue("@id_comprobante", idComprobante);

            sqlConnection.Open();

            try
            {
                /// Retorna el identificador con el cuál fue eliminado
                resultado = Convert.ToInt32(sqlCommand.ExecuteScalar());
            }
            catch (Exception exception)
            {
                /// Ocurre un error durante la escrita a la base de datos
                resultado = Estado.ERROR_INESPERADO;
                Estado.ErrorBitacora(exception.Message, "ComprobantesDatos:Eliminar()");
            }

            sqlConnection.Close();

            return resultado;
        }
    }
}
