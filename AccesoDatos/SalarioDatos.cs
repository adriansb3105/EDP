//using Entidades;
//using System;
//using System.Collections.Generic;
//using System.Data.SqlClient;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace AccesoDatos
//{
//    /// <summary>
//    /// Adrián Serrano
//    /// 26/09/2021
//    /// Clase para administrar el CRUD para los salarios
//    /// </summary>
//    public class SalarioDatos
//    {
//        private ConexionDatos conexion = new ConexionDatos();

//        /// <summary>
//        /// Obtiene todos los salarios de la base de datos según el número de identificación dado
//        /// </summary>
//        /// <param name="numeroIdentificacionFuncionario">Número de identificación para buscar sus salarios</param>
//        /// <exception cref="Exception">Lanza una excepción si ocurre un error durante la lectura</exception>
//        /// <returns>Retorna una lista <code>List<Salario></code> que contiene los salarios para el funcionario dado</returns>
//        public List<Salario> ObtenerPorId(string numeroIdentificacionFuncionario)
//        {
//            SqlConnection sqlConnection = conexion.conexionEDP();
//            List<Salario> salarios = new List<Salario>();

//            string consulta = @"SELECT id_salario, fecha, ruta_documento, nombre_documento, descripcion, entregado,
//                            numero_identificacion_funcionario FROM salarios WHERE 
//                            numero_identificacion_funcionario = '" + numeroIdentificacionFuncionario+ "' order by fecha;";

//            SqlCommand sqlCommand = new SqlCommand(consulta, sqlConnection);

//            SqlDataReader reader;

//            try
//            {
//                sqlConnection.Open();
//                reader = sqlCommand.ExecuteReader();

//                while (reader.Read())
//                {
//                    Salario salario = new Salario();

//                    salario.IdSalario = Convert.ToInt32(reader["id_salario"].ToString());
//                    salario.NombreDocumento = Convert.ToString(reader["nombre_documento"].ToString());
//                    salario.RutaDocumento = Convert.ToString(reader["ruta_documento"].ToString());
//                    salario.Fecha = Convert.ToDateTime(reader["fecha"].ToString());
//                    salario.Descripcion = Convert.ToString(reader["descripcion"].ToString());
//                    salario.Entregado = Convert.ToBoolean(reader["entregado"].ToString());
//                    string numeroIdentificacion = Convert.ToString(reader["numero_identificacion_funcionario"].ToString());
//                    salario.Funcionario = new Funcionario(numeroIdentificacion);

//                    salarios.Add(salario);
//                }

//                sqlConnection.Close();
//            }
//            catch (Exception exception)
//            {
//                Estado.ErrorBitacora(exception.Message, "SalarioDatos:ObtenerPorId()");
//            }

//            return salarios;
//        }

//        /// <summary>
//        /// Inserta la entidad Salario en la base de datos
//        /// </summary>
//        /// <param name="salario">Elemento de tipo <code>Salario</code> que va a ser insertado</param>
//        /// <exception cref="Exception">Lanza una excepción si ocurre un error durante la escrita a la base de datos</exception>
//        /// <returns>Retorna un entero con el código según sea el resultado</returns>
//        public int Insertar(Salario salario)
//        {
//            SqlConnection sqlConnection = conexion.conexionEDP();
//            int resultado = 0;

//            SqlCommand sqlCommand = new SqlCommand("insert into salarios(fecha, nombre_documento, ruta_documento," +
//                "descripcion, entregado, numero_identificacion_funcionario)" +
//                "output INSERTED.id_salario values(@fecha, @nombre_documento, @ruta_documento," +
//                "@descripcion, @entregado, @numero_identificacion_funcionario);", sqlConnection);

//            sqlCommand.Parameters.AddWithValue("@nombre_documento", salario.NombreDocumento);
//            sqlCommand.Parameters.AddWithValue("@ruta_documento", salario.RutaDocumento);
//            sqlCommand.Parameters.AddWithValue("@fecha", salario.Fecha);
//            sqlCommand.Parameters.AddWithValue("@descripcion", salario.Descripcion);
//            sqlCommand.Parameters.AddWithValue("@entregado", salario.Entregado);
//            sqlCommand.Parameters.AddWithValue("@numero_identificacion_funcionario", salario.Funcionario.NumeroIdentificacion);
            
//            sqlConnection.Open();

//            try
//            {
//                /// Retorna el identificador con el cuál fue insertado
//                resultado = Convert.ToInt32(sqlCommand.ExecuteScalar());
//            }
//            catch (Exception exception)
//            {
//                /// Ocurre un error durante la escrita a la base de datos
//                resultado = Estado.ERROR_INESPERADO;
//                Estado.ErrorBitacora(exception.Message, "SalarioDatos:Insertar()");
//            }

//            sqlConnection.Close();

//            return resultado;
//        }

//        /// <summary>
//        /// Actualiza la entidad Salario en la base de datos
//        /// </summary>
//        /// <param name="salario">Elemento de tipo <code>Salario</code> que va a ser actualizado</param>
//        /// <exception cref="Exception">Lanza una excepción si ocurre un error durante la escrita a la base de datos</exception>
//        /// <returns>Retorna un entero con el código según sea el resultado</returns>
//        public int Actualizar(Salario salario)
//        {
//            SqlConnection sqlConnection = conexion.conexionEDP();
//            int resultado = 0;

//            SqlCommand sqlCommand = new SqlCommand("update salarios set fecha=@fecha, nombre_documento=@nombre_documento, ruta_documento=@ruta_documento," +
//                "descripcion=@descripcion, entregado=@entregado, numero_identificacion_funcionario=@numero_identificacion_funcionario" +
//                " output INSERTED.id_salario where id_salario=@id_salario;", sqlConnection);

//            sqlCommand.Parameters.AddWithValue("@nombre_documento", salario.NombreDocumento);
//            sqlCommand.Parameters.AddWithValue("@ruta_documento", salario.RutaDocumento);
//            sqlCommand.Parameters.AddWithValue("@fecha", salario.Fecha);
//            sqlCommand.Parameters.AddWithValue("@descripcion", salario.Descripcion);
//            sqlCommand.Parameters.AddWithValue("@entregado", salario.Entregado);
//            sqlCommand.Parameters.AddWithValue("@numero_identificacion_funcionario", salario.Funcionario.NumeroIdentificacion);
//            sqlCommand.Parameters.AddWithValue("@id_salario", salario.IdSalario);

//            sqlConnection.Open();

//            try
//            {
//                /// Retorna el identificador con el cuál fue actualizado
//                resultado = Convert.ToInt32(sqlCommand.ExecuteScalar());
//            }
//            catch (Exception exception)
//            {
//                /// Ocurre un error durante la escrita a la base de datos
//                resultado = Estado.ERROR_INESPERADO;
//                Estado.ErrorBitacora(exception.Message, "SalarioDatos:Actualizar()");
//            }

//            sqlConnection.Close();

//            return resultado;
//        }
//    }
//}
