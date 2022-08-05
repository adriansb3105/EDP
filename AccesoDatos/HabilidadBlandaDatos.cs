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
    /// Clase para administrar el CRUD para las habilidades blandas
    /// </summary>
    public class HabilidadBlandaDatos
    {
        private ConexionDatos conexion = new ConexionDatos();

        /// <summary>
        /// Obtiene todos las habilidades blandas de la base de datos según el número de identificación dado
        /// </summary>
        /// <param name="numeroIdentificacionFuncionario">Número de identificación para buscar sus habilidades blandas</param>
        /// <exception cref="Exception">Lanza una excepción si ocurre un error durante la lectura</exception>
        /// <returns>Retorna una lista <code>List<HabilidadBlanda></code> para el funcionario dado</returns>
        public List<HabilidadBlanda> ObtenerPorId(string numeroIdentificacionFuncionario)
        {
            SqlConnection sqlConnection = conexion.conexionEDP();
            List<HabilidadBlanda> habilidadesBlandas = new List<HabilidadBlanda>();

            string consulta = @"SELECT id_habilidad_blanda,descripcion,numero_identificacion_funcionario 
            FROM habilidades_blandas WHERE numero_identificacion_funcionario = '" + numeroIdentificacionFuncionario+ "' order by descripcion;";

            SqlCommand sqlCommand = new SqlCommand(consulta, sqlConnection);

            SqlDataReader reader;

            try
            {
                sqlConnection.Open();
                reader = sqlCommand.ExecuteReader();

                while (reader.Read())
                {
                    HabilidadBlanda habilidadBlanda = new HabilidadBlanda();

                    habilidadBlanda.IdHabilidadBlanda = Convert.ToInt32(reader["id_habilidad_blanda"].ToString());
                    habilidadBlanda.Descripcion = Convert.ToString(reader["descripcion"].ToString());
                    string numeroIdentificacion = Convert.ToString(reader["numero_identificacion_funcionario"].ToString());
                    habilidadBlanda.Funcionario = new Funcionario(numeroIdentificacion);

                    habilidadesBlandas.Add(habilidadBlanda);
                }

                sqlConnection.Close();
            }
            catch (Exception exception)
            {
                Estado.ErrorBitacora(exception.Message, "HabilidadBlandaDatos:ObtenerPorId()");
            }

            return habilidadesBlandas;
        }

        /// <summary>
        /// Inserta la entidad Habilidad Blanda en la base de datos
        /// </summary>
        /// <param name="habilidadBlanda">Elemento de tipo <code>HabilidadBlanda</code> que va a ser insertada</param>
        /// <exception cref="Exception">Lanza una excepción si ocurre un error durante la escrita a la base de datos</exception>
        /// <returns>Retorna un entero con el código según sea el resultado</returns>
        public int Insertar(HabilidadBlanda habilidadBlanda)
        {
            SqlConnection sqlConnection = conexion.conexionEDP();
            int resultado = 0;

            SqlCommand sqlCommand = new SqlCommand("insert into habilidades_blandas(descripcion, numero_identificacion_funcionario) " +
                "output INSERTED.id_habilidad_blanda values(@descripcion,@numero_identificacion_funcionario);", sqlConnection);

            sqlCommand.Parameters.AddWithValue("@descripcion", habilidadBlanda.Descripcion);
            sqlCommand.Parameters.AddWithValue("@numero_identificacion_funcionario", habilidadBlanda.Funcionario.NumeroIdentificacion);
            
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
                Estado.ErrorBitacora(exception.Message, "HabilidadBlandaDatos:Insertar()");
            }

            sqlConnection.Close();

            return resultado;
        }

        /// <summary>
        /// Actualiza la entidad Habilidad Blanda en la base de datos
        /// </summary>
        /// <param name="habilidadBlanda">Elemento de tipo <code>HabilidadBlanda</code> que va a ser actualizada</param>
        /// <exception cref="Exception">Lanza una excepción si ocurre un error durante la escrita a la base de datos</exception>
        /// <returns>Retorna un entero con el código según sea el resultado</returns>
        public int Actualizar(HabilidadBlanda habilidadBlanda)
        {
            SqlConnection sqlConnection = conexion.conexionEDP();
            int resultado = 0;

            SqlCommand sqlCommand = new SqlCommand("update habilidades_blandas set descripcion=@descripcion, numero_identificacion_funcionario=@numero_identificacion_funcionario" +
                " output INSERTED.id_habilidad_blanda where id_habilidad_blanda=@id_habilidad_blanda;", sqlConnection);

            sqlCommand.Parameters.AddWithValue("@descripcion", habilidadBlanda.Descripcion);
            sqlCommand.Parameters.AddWithValue("@numero_identificacion_funcionario", habilidadBlanda.Funcionario.NumeroIdentificacion);
            sqlCommand.Parameters.AddWithValue("@id_habilidad_blanda", habilidadBlanda.IdHabilidadBlanda);
            

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
                Estado.ErrorBitacora(exception.Message, "HabilidadBlandaDatos:Actualizar()");
            }

            sqlConnection.Close();

            return resultado;
        }

        /// <summary>
        /// Elimina la entidad HabilidadBlanda en la base de datos
        /// </summary>
        /// <param name="habilidadBlanda">Elemento de tipo <code>HabilidadBlanda</code> que va a ser eliminado</param>
        /// <exception cref="Exception">Lanza una excepción si ocurre un error durante la escrita a la base de datos</exception>
        /// <returns>Retorna un entero con el código según sea el resultado</returns>
        public int Eliminar(int idHabilidadBlanda)
        {
            SqlConnection sqlConnection = conexion.conexionEDP();
            int resultado = 0;

            SqlCommand sqlCommand = new SqlCommand("delete from habilidades_blandas where id_habilidad_blanda=@id_habilidad_blanda;", sqlConnection);
            sqlCommand.Parameters.AddWithValue("@id_habilidad_blanda", idHabilidadBlanda);

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
                Estado.ErrorBitacora(exception.Message, "HabilidadBlandaDatos:Eliminar()");
            }

            sqlConnection.Close();

            return resultado;
        }
    }
}
