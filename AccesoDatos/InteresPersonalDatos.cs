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
    /// Clase para administrar el CRUD para los intereses personales
    /// </summary>
    public class InteresPersonalDatos
    {
        private ConexionDatos conexion = new ConexionDatos();

        /// <summary>
        /// Obtiene todos los intereses personal de la base de datos según el número de identificación dado
        /// </summary>
        /// <param name="numeroIdentificacionFuncionario">Número de identificación para buscar sus intereses personales</param>
        /// <exception cref="Exception">Lanza una excepción si ocurre un error durante la lectura</exception>
        /// <returns>Retorna una lista <code>List<InteresPersonal></code> para el funcionario dado</returns>
        public List<InteresPersonal> ObtenerPorId(string numeroIdentificacionFuncionario)
        {
            SqlConnection sqlConnection = conexion.conexionEDP();
            List<InteresPersonal> interesesPersonales = new List<InteresPersonal>();

            string consulta = @"SELECT id_interes_personal,descripcion,numero_identificacion_funcionario 
            FROM intereses_personales WHERE numero_identificacion_funcionario = '" + numeroIdentificacionFuncionario+ "' order by descripcion;";

            SqlCommand sqlCommand = new SqlCommand(consulta, sqlConnection);

            SqlDataReader reader;

            try
            {
                sqlConnection.Open();
                reader = sqlCommand.ExecuteReader();

                while (reader.Read())
                {
                    InteresPersonal interesPersonal = new InteresPersonal();

                    interesPersonal.IdInteresPersonal = Convert.ToInt32(reader["id_interes_personal"].ToString());
                    interesPersonal.Descripcion = Convert.ToString(reader["descripcion"].ToString());
                    string numeroIdentificacion = Convert.ToString(reader["numero_identificacion_funcionario"].ToString());
                    interesPersonal.Funcionario = new Funcionario(numeroIdentificacion);

                    interesesPersonales.Add(interesPersonal);
                }

                sqlConnection.Close();
            }
            catch (Exception exception)
            {
                Estado.ErrorBitacora(exception.Message, "InteresPersonalDatos:ObtenerPorId()");
            }

            return interesesPersonales;
        }

        /// <summary>
        /// Inserta la entidad Interes Personal en la base de datos
        /// </summary>
        /// <param name="interesPersonal">Elemento de tipo <code>InteresPersonal</code> que va a ser insertado</param>
        /// <exception cref="Exception">Lanza una excepción si ocurre un error durante la escrita a la base de datos</exception>
        /// <returns>Retorna un entero con el código según sea el resultado</returns>
        public int Insertar(InteresPersonal interesPersonal)
        {
            SqlConnection sqlConnection = conexion.conexionEDP();
            int resultado = 0;

            SqlCommand sqlCommand = new SqlCommand("insert into intereses_personales(descripcion, numero_identificacion_funcionario) " +
                "output INSERTED.id_interes_personal values(@descripcion,@numero_identificacion_funcionario);", sqlConnection);

            sqlCommand.Parameters.AddWithValue("@descripcion", interesPersonal.Descripcion);
            sqlCommand.Parameters.AddWithValue("@numero_identificacion_funcionario", interesPersonal.Funcionario.NumeroIdentificacion);
            
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
                Estado.ErrorBitacora(exception.Message, "InteresPersonalDatos:Insertar()");
            }

            sqlConnection.Close();

            return resultado;
        }

        /// <summary>
        /// Actualiza la entidad Interes Personal en la base de datos
        /// </summary>
        /// <param name="interesPersonal">Elemento de tipo <code>InteresPersonal</code> que va a ser actualizado</param>
        /// <exception cref="Exception">Lanza una excepción si ocurre un error durante la escrita a la base de datos</exception>
        /// <returns>Retorna un entero con el código según sea el resultado</returns>
        public int Actualizar(InteresPersonal interesPersonal)
        {
            SqlConnection sqlConnection = conexion.conexionEDP();
            int resultado = 0;

            SqlCommand sqlCommand = new SqlCommand("update intereses_personales set descripcion=@descripcion, numero_identificacion_funcionario=@numero_identificacion_funcionario " +
                "output INSERTED.id_interes_personal where id_interes_personal=@id_interes_personal;", sqlConnection);

            sqlCommand.Parameters.AddWithValue("@descripcion", interesPersonal.Descripcion);
            sqlCommand.Parameters.AddWithValue("@numero_identificacion_funcionario", interesPersonal.Funcionario.NumeroIdentificacion);
            sqlCommand.Parameters.AddWithValue("@id_interes_personal", interesPersonal.IdInteresPersonal);

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
                Estado.ErrorBitacora(exception.Message, "InteresPersonalDatos:Actualizar()");
            }

            sqlConnection.Close();

            return resultado;
        }

        /// <summary>
        /// Elimina la entidad InteresPersonal en la base de datos
        /// </summary>
        /// <param name="interesPersonal">Elemento de tipo <code>InteresPersonal</code> que va a ser eliminado</param>
        /// <exception cref="Exception">Lanza una excepción si ocurre un error durante la escrita a la base de datos</exception>
        /// <returns>Retorna un entero con el código según sea el resultado</returns>
        public int Eliminar(int idInteresPersonal)
        {
            SqlConnection sqlConnection = conexion.conexionEDP();
            int resultado = 0;

            SqlCommand sqlCommand = new SqlCommand("delete from intereses_personales where id_interes_personal=@id_interes_personal;", sqlConnection);
            sqlCommand.Parameters.AddWithValue("@id_interes_personal", idInteresPersonal);

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
                Estado.ErrorBitacora(exception.Message, "InteresPersonalDatos:Eliminar()");
            }

            sqlConnection.Close();

            return resultado;
        }
    }
}
