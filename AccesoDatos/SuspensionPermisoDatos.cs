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
    /// Clase para administrar el CRUD para las suspensiones o permisos
    /// </summary>
    public class SuspensionPermisoDatos
    {
        private ConexionDatos conexion = new ConexionDatos();

        /// <summary>
        /// Obtiene todos las suspensiones o permisos de la base de datos según el número de identificación dado
        /// </summary>
        /// <param name="numeroIdentificacionFuncionario">Número de identificación para buscar sus suspensiones o permisos</param>
        /// <exception cref="Exception">Lanza una excepción si ocurre un error durante la lectura</exception>
        /// <returns>Retorna una lista <code>List<SuspensionPermiso></code> para el funcionario dado</returns>
        public List<SuspensionPermiso> ObtenerPorId(string numeroIdentificacionFuncionario)
        {
            SqlConnection sqlConnection = conexion.conexionEDP();
            List<SuspensionPermiso> suspensionPermisos = new List<SuspensionPermiso>();

            string consulta = @"SELECT id_suspension_o_permiso,fecha_salida,fecha_regreso,tipo,descripcion,numero_identificacion_funcionario 
            FROM suspensiones_o_permisos WHERE numero_identificacion_funcionario = '" + numeroIdentificacionFuncionario+ "' order by tipo;";

            SqlCommand sqlCommand = new SqlCommand(consulta, sqlConnection);

            SqlDataReader reader;

            try
            {
                sqlConnection.Open();
                reader = sqlCommand.ExecuteReader();

                while (reader.Read())
                {
                    SuspensionPermiso suspensionPermiso = new SuspensionPermiso();

                    suspensionPermiso.IdSuspensionPermiso = Convert.ToInt32(reader["id_suspension_o_permiso"].ToString());
                    suspensionPermiso.FechaSalida = Convert.ToDateTime(reader["fecha_salida"].ToString());
                    suspensionPermiso.FechaRegreso = Convert.ToDateTime(reader["fecha_regreso"].ToString());
                    suspensionPermiso.Tipo = Convert.ToInt32(reader["tipo"].ToString());
                    suspensionPermiso.Descripcion = Convert.ToString(reader["descripcion"].ToString());
                    string numeroIdentificacion = Convert.ToString(reader["numero_identificacion_funcionario"].ToString());
                    suspensionPermiso.Funcionario = new Funcionario(numeroIdentificacion);

                    suspensionPermisos.Add(suspensionPermiso);
                }

                sqlConnection.Close();
            }
            catch (Exception exception)
            {
                Estado.ErrorBitacora(exception.Message, "SuspensionPermisoDatos:ObtenerPorId()");
            }

            return suspensionPermisos;
        }

        /// <summary>
        /// Inserta la entidad SuspensionPermiso en la base de datos
        /// </summary>
        /// <param name="suspensionPermiso">Elemento de tipo <code>SuspensionPermiso</code> que va a ser insertado</param>
        /// <exception cref="Exception">Lanza una excepción si ocurre un error durante la escrita a la base de datos</exception>
        /// <returns>Retorna un entero con el código según sea el resultado</returns>
        public int Insertar(SuspensionPermiso suspensionPermiso)
        {
            SqlConnection sqlConnection = conexion.conexionEDP();
            int resultado = 0;

            SqlCommand sqlCommand = new SqlCommand("insert into suspensiones_o_permisos(fecha_salida," +
                "fecha_regreso, tipo, descripcion, numero_identificacion_funcionario)" +
                "output INSERTED.id_suspension_o_permiso values(@fecha_salida, @fecha_regreso, @tipo, " +
                "@descripcion, @numero_identificacion_funcionario);", sqlConnection);

            sqlCommand.Parameters.AddWithValue("@fecha_salida", suspensionPermiso.FechaSalida);
            sqlCommand.Parameters.AddWithValue("@fecha_regreso", suspensionPermiso.FechaRegreso);
            sqlCommand.Parameters.AddWithValue("@tipo", suspensionPermiso.Tipo);
            sqlCommand.Parameters.AddWithValue("@descripcion", suspensionPermiso.Descripcion);
            sqlCommand.Parameters.AddWithValue("@numero_identificacion_funcionario", suspensionPermiso.Funcionario.NumeroIdentificacion);
            
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
                Estado.ErrorBitacora(exception.Message, "SuspensionPermisoDatos:Insertar()");
            }

            sqlConnection.Close();

            return resultado;
        }

        /// <summary>
        /// Actualiza la entidad SuspensionPermiso en la base de datos
        /// </summary>
        /// <param name="suspensionPermiso">Elemento de tipo <code>SuspensionPermiso</code> que va a ser actualizado</param>
        /// <exception cref="Exception">Lanza una excepción si ocurre un error durante la escrita a la base de datos</exception>
        /// <returns>Retorna un entero con el código según sea el resultado</returns>
        public int Actualizar(SuspensionPermiso suspensionPermiso)
        {
            SqlConnection sqlConnection = conexion.conexionEDP();
            int resultado = 0;

            SqlCommand sqlCommand = new SqlCommand("update suspensiones_o_permisos set fecha_salida=@fecha_salida," +
                "fecha_regreso=@fecha_regreso, tipo=@tipo, descripcion=@descripcion, numero_identificacion_funcionario=@numero_identificacion_funcionario " +
                "output INSERTED.id_suspension_o_permiso where id_suspension_o_permiso=@id_suspension_o_permiso;", sqlConnection);

            sqlCommand.Parameters.AddWithValue("@fecha_salida", suspensionPermiso.FechaSalida);
            sqlCommand.Parameters.AddWithValue("@fecha_regreso", suspensionPermiso.FechaRegreso);
            sqlCommand.Parameters.AddWithValue("@tipo", suspensionPermiso.Tipo);
            sqlCommand.Parameters.AddWithValue("@descripcion", suspensionPermiso.Descripcion);
            sqlCommand.Parameters.AddWithValue("@numero_identificacion_funcionario", suspensionPermiso.Funcionario.NumeroIdentificacion);
            sqlCommand.Parameters.AddWithValue("@id_suspension_o_permiso", suspensionPermiso.IdSuspensionPermiso);

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
                Estado.ErrorBitacora(exception.Message, "SuspensionPermisoDatos:Actualizar()");
            }

            sqlConnection.Close();

            return resultado;
        }

        /// <summary>
        /// Elimina la entidad SuspensionPermiso en la base de datos
        /// </summary>
        /// <param name="idSuspensionPermiso">Elemento de tipo <code>SuspensionPermiso</code> que va a ser eliminado</param>
        /// <exception cref="Exception">Lanza una excepción si ocurre un error durante la escrita a la base de datos</exception>
        /// <returns>Retorna un entero con el código según sea el resultado</returns>
        public int Eliminar(int idSuspensionPermiso)
        {
            SqlConnection sqlConnection = conexion.conexionEDP();
            int resultado = 0;

            SqlCommand sqlCommand = new SqlCommand("delete from suspensiones_o_permisos where id_suspension_o_permiso=@id_suspension_o_permiso;", sqlConnection);
            sqlCommand.Parameters.AddWithValue("@id_suspension_o_permiso", idSuspensionPermiso);

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
                Estado.ErrorBitacora(exception.Message, "SuspensionPermisoDatos:Eliminar()");
            }

            sqlConnection.Close();

            return resultado;
        }
    }
}
