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
    /// Clase para administrar el CRUD para las pensiones o embargos
    /// </summary>
    public class PensionOEmbargoDatos
    {
        private ConexionDatos conexion = new ConexionDatos();

        /// <summary>
        /// Obtiene todos las pensiones o embargos de la base de datos según el número de identificación dado
        /// </summary>
        /// <param name="numeroIdentificacionFuncionario">Número de identificación para buscar sus pensiones o embargos</param>
        /// <exception cref="Exception">Lanza una excepción si ocurre un error durante la lectura</exception>
        /// <returns>Retorna una lista <code>List<PensionOEmbargo></code> que contiene las pensiones o embargos para el funcionario dado</returns>
        public List<PensionOEmbargo> ObtenerPorId(string numeroIdentificacionFuncionario)
        {
            SqlConnection sqlConnection = conexion.conexionEDP();
            List<PensionOEmbargo> pensionOEmbargos = new List<PensionOEmbargo>();

            string consulta = @"SELECT id_pension_o_embargo, ruta_documento, nombre_documento, fecha_ingreso, descripcion,
                            numero_identificacion_funcionario FROM pensiones_o_embargos WHERE 
                            numero_identificacion_funcionario = '" + numeroIdentificacionFuncionario+ "' order by fecha_ingreso DESC;";

            SqlCommand sqlCommand = new SqlCommand(consulta, sqlConnection);

            SqlDataReader reader;

            try
            {
                sqlConnection.Open();
                reader = sqlCommand.ExecuteReader();

                while (reader.Read())
                {
                    PensionOEmbargo pensionOEmbargo = new PensionOEmbargo();

                    pensionOEmbargo.IdPensionOEmbargo = Convert.ToInt32(reader["id_pension_o_embargo"].ToString());
                    pensionOEmbargo.NombreDocumento = Convert.ToString(reader["nombre_documento"].ToString());
                    pensionOEmbargo.RutaDocumento = Convert.ToString(reader["ruta_documento"].ToString());

                    string fechaIngresoString = reader["fecha_ingreso"].ToString();
                    DateTime fechaIngreso = new DateTime(1900, 01, 01);

                    if (fechaIngresoString.Trim() != "")
                    {
                        fechaIngreso = Convert.ToDateTime(fechaIngresoString);
                    }

                    pensionOEmbargo.FechaIngreso = fechaIngreso;
                    pensionOEmbargo.Descripcion = Convert.ToString(reader["descripcion"].ToString());
                    string numeroIdentificacion = Convert.ToString(reader["numero_identificacion_funcionario"].ToString());
                    pensionOEmbargo.Funcionario = new Funcionario(numeroIdentificacion);

                    pensionOEmbargos.Add(pensionOEmbargo);
                }

                sqlConnection.Close();
            }
            catch (Exception exception)
            {
                Estado.ErrorBitacora(exception.Message, "PensionOEmbargoDatos:ObtenerPorId()");
            }

            return pensionOEmbargos;
        }

        /// <summary>
        /// Inserta la entidad PensionOEmbargo en la base de datos
        /// </summary>
        /// <param name="pensionOEmbargo">Elemento de tipo <code>PensionOEmbargo</code> que va a ser insertado</param>
        /// <exception cref="Exception">Lanza una excepción si ocurre un error durante la escrita a la base de datos</exception>
        /// <returns>Retorna un entero con el código según sea el resultado</returns>
        public int Insertar(PensionOEmbargo pensionOEmbargo)
        {
            SqlConnection sqlConnection = conexion.conexionEDP();
            int resultado = 0;

            SqlCommand sqlCommand = new SqlCommand("insert into pensiones_o_embargos(nombre_documento, ruta_documento,fecha_ingreso," +
                "descripcion, numero_identificacion_funcionario)" +
                "output INSERTED.id_pension_o_embargo values(@nombre_documento, @ruta_documento,@fecha_ingreso," +
                "@descripcion, @numero_identificacion_funcionario);", sqlConnection);

            sqlCommand.Parameters.AddWithValue("@nombre_documento", pensionOEmbargo.NombreDocumento);
            sqlCommand.Parameters.AddWithValue("@ruta_documento", pensionOEmbargo.RutaDocumento);
            sqlCommand.Parameters.AddWithValue("@fecha_ingreso", pensionOEmbargo.FechaIngreso);
            sqlCommand.Parameters.AddWithValue("@descripcion", pensionOEmbargo.Descripcion);
            sqlCommand.Parameters.AddWithValue("@numero_identificacion_funcionario", pensionOEmbargo.Funcionario.NumeroIdentificacion);
            
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
                Estado.ErrorBitacora(exception.Message, "PensionOEmbargoDatos:Insertar()");
            }

            sqlConnection.Close();

            return resultado;
        }

        /// <summary>
        /// Actualiza la entidad PensionOEmbargo en la base de datos
        /// </summary>
        /// <param name="pensionOEmbargo">Elemento de tipo <code>PensionOEmbargo</code> que va a ser actualizado</param>
        /// <exception cref="Exception">Lanza una excepción si ocurre un error durante la escrita a la base de datos</exception>
        /// <returns>Retorna un entero con el código según sea el resultado</returns>
        public int Actualizar(PensionOEmbargo pensionOEmbargo)
        {
            SqlConnection sqlConnection = conexion.conexionEDP();
            int resultado = 0;

            SqlCommand sqlCommand = new SqlCommand("update pensiones_o_embargos set nombre_documento=@nombre_documento, ruta_documento=@ruta_documento," +
                "fecha_ingreso=@fecha_ingreso,descripcion=@descripcion, numero_identificacion_funcionario=@numero_identificacion_funcionario output INSERTED.id_pension_o_embargo " +
                "where id_pension_o_embargo=@id_pension_o_embargo;", sqlConnection);

            sqlCommand.Parameters.AddWithValue("@nombre_documento", pensionOEmbargo.NombreDocumento);
            sqlCommand.Parameters.AddWithValue("@ruta_documento", pensionOEmbargo.RutaDocumento);
            sqlCommand.Parameters.AddWithValue("@fecha_ingreso", pensionOEmbargo.FechaIngreso);
            sqlCommand.Parameters.AddWithValue("@descripcion", pensionOEmbargo.Descripcion);
            sqlCommand.Parameters.AddWithValue("@numero_identificacion_funcionario", pensionOEmbargo.Funcionario.NumeroIdentificacion);
            sqlCommand.Parameters.AddWithValue("@id_pension_o_embargo", pensionOEmbargo.IdPensionOEmbargo);

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
                Estado.ErrorBitacora(exception.Message, "PensionOEmbargoDatos:Actualizar()");
            }

            sqlConnection.Close();

            return resultado;
        }

        /// <summary>
        /// Elimina la entidad PensionOEmbargo en la base de datos
        /// </summary>
        /// <param name="pensionOEmbargo">Elemento de tipo <code>PensionOEmbargo</code> que va a ser eliminado</param>
        /// <exception cref="Exception">Lanza una excepción si ocurre un error durante la escrita a la base de datos</exception>
        /// <returns>Retorna un entero con el código según sea el resultado</returns>
        public int Eliminar(int idPensionOEmbargo)
        {
            SqlConnection sqlConnection = conexion.conexionEDP();
            int resultado = 0;

            SqlCommand sqlCommand = new SqlCommand("delete from pensiones_o_embargos where id_pension_o_embargo=@id_pension_o_embargo;", sqlConnection);
            sqlCommand.Parameters.AddWithValue("@id_pension_o_embargo", idPensionOEmbargo);

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
                Estado.ErrorBitacora(exception.Message, "PensionOEmbargoDatos:Eliminar()");
            }

            sqlConnection.Close();

            return resultado;
        }
    }
}
