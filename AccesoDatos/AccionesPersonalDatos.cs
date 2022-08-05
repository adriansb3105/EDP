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
    /// Clase para administrar el CRUD para las acciones personales
    /// </summary>
    public class AccionesPersonalDatos
    {
        private ConexionDatos conexion = new ConexionDatos();

        /// <summary>
        /// Obtiene todas las acciones de personal de la base de datos según el número de identificación dado
        /// </summary>
        /// <param name="numeroIdentificacionFuncionario">Número de identificación para buscar sus acciones de personal</param>
        /// <exception cref="Exception">Lanza una excepción si ocurre un error durante la lectura</exception>
        /// <returns>Retorna una lista <code>List<AccionPersonal></code> que contiene las acciones de personal para el funcionario dado</returns>
        public List<AccionPersonal> ObtenerPorId(string numeroIdentificacionFuncionario)
        {
            SqlConnection sqlConnection = conexion.conexionEDP();
            List<AccionPersonal> accionesPersonal = new List<AccionPersonal>();
            
            string consulta = @"SELECT id_accion_de_personal, nombre,
            periodo, descripcion, ruta_documento,nombre_documento, numero_identificacion_funcionario, 
            id_tipo_accion_de_personal FROM acciones_de_personal
            WHERE numero_identificacion_funcionario = '" + numeroIdentificacionFuncionario + "' order by nombre;";

            SqlCommand sqlCommand = new SqlCommand(consulta, sqlConnection);

            SqlDataReader reader;

            try
            {
                sqlConnection.Open();
                reader = sqlCommand.ExecuteReader();

                while (reader.Read())
                {
                    AccionPersonal accionPersonal = new AccionPersonal();

                    accionPersonal.IdAccionPersonal = Convert.ToInt32(reader["id_accion_de_personal"].ToString());
                    accionPersonal.Nombre = Convert.ToString(reader["nombre"].ToString());
                    accionPersonal.Periodo = Convert.ToDateTime(reader["periodo"].ToString());
                    accionPersonal.Descripcion = Convert.ToString(reader["descripcion"].ToString());
                    accionPersonal.RutaDocumento = Convert.ToString(reader["ruta_documento"].ToString());
                    accionPersonal.NombreDocumento = Convert.ToString(reader["nombre_documento"].ToString());
                    string numeroIdentificacion = Convert.ToString(reader["numero_identificacion_funcionario"].ToString());
                    accionPersonal.Funcionario = new Funcionario(numeroIdentificacion);
                    accionPersonal.TipoAccionPersonal = new TipoAccionPersonal(Convert.ToInt32(reader["id_tipo_accion_de_personal"].ToString()));

                    accionesPersonal.Add(accionPersonal);
                }

                sqlConnection.Close();
            }
            catch (Exception exception)
            {
                Estado.ErrorBitacora(exception.Message, "AccionesPersonalDatos:ObtenerPorId()");
            }

            return accionesPersonal;
        }

        /// <summary>
        /// Inserta la entidad AccionPersonal en la base de datos
        /// </summary>
        /// <param name="accionPersonal">Elemento de tipo <code>AccionPersonal</code> que va a ser insertado</param>
        /// <exception cref="Exception">Lanza una excepción si ocurre un error durante la escrita a la base de datos</exception>
        /// <returns>Retorna un entero con el código según sea el resultado</returns>
        public int Insertar(AccionPersonal accionPersonal)
        {
            SqlConnection sqlConnection = conexion.conexionEDP();
            int resultado = 0;

            SqlCommand sqlCommand = new SqlCommand("insert into acciones_de_personal(nombre, periodo, descripcion, nombre_documento, " +
                "ruta_documento,numero_identificacion_funcionario, id_tipo_accion_de_personal)" +
                "output INSERTED.id_accion_de_personal values(@nombre, @periodo, @descripcion, @nombre_documento, @ruta_documento," +
                "@numero_identificacion_funcionario, @id_tipo_accion_de_personal);", sqlConnection);

            sqlCommand.Parameters.AddWithValue("@nombre", accionPersonal.Nombre);
            sqlCommand.Parameters.AddWithValue("@periodo", accionPersonal.Periodo);
            sqlCommand.Parameters.AddWithValue("@descripcion", accionPersonal.Descripcion);
            sqlCommand.Parameters.AddWithValue("@nombre_documento", accionPersonal.NombreDocumento);
            sqlCommand.Parameters.AddWithValue("@ruta_documento", accionPersonal.RutaDocumento);
            sqlCommand.Parameters.AddWithValue("@numero_identificacion_funcionario", accionPersonal.Funcionario.NumeroIdentificacion);
            sqlCommand.Parameters.AddWithValue("@id_tipo_accion_de_personal", accionPersonal.TipoAccionPersonal.IdAccionPersonal);
            
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
                Estado.ErrorBitacora(exception.Message, "AccionesPersonalDatos:Insertar()");
            }

            sqlConnection.Close();

            return resultado;
        }

        /// <summary>
        /// Actualiza la entidad AccionPersonal en la base de datos
        /// </summary>
        /// <param name="accionPersonal">Elemento de tipo <code>AccionPersonal</code> que va a ser actualizado</param>
        /// <exception cref="Exception">Lanza una excepción si ocurre un error durante la escrita a la base de datos</exception>
        /// <returns>Retorna un entero con el código según sea el resultado</returns>
        public int Actualizar(AccionPersonal accionPersonal)
        {
            SqlConnection sqlConnection = conexion.conexionEDP();
            int resultado = 0;

            SqlCommand sqlCommand = new SqlCommand("update acciones_de_personal set nombre=@nombre, periodo=@periodo, descripcion=@descripcion, " +
                "nombre_documento=@nombre_documento, ruta_documento=@ruta_documento, numero_identificacion_funcionario=@numero_identificacion_funcionario, " +
                "id_tipo_accion_de_personal=@id_tipo_accion_de_personal output INSERTED.id_accion_de_personal where id_accion_de_personal=@id_accion_de_personal;", sqlConnection);

            sqlCommand.Parameters.AddWithValue("@nombre", accionPersonal.Nombre);
            sqlCommand.Parameters.AddWithValue("@periodo", accionPersonal.Periodo);
            sqlCommand.Parameters.AddWithValue("@descripcion", accionPersonal.Descripcion);
            sqlCommand.Parameters.AddWithValue("@nombre_documento", accionPersonal.NombreDocumento);
            sqlCommand.Parameters.AddWithValue("@ruta_documento", accionPersonal.RutaDocumento);
            sqlCommand.Parameters.AddWithValue("@numero_identificacion_funcionario", accionPersonal.Funcionario.NumeroIdentificacion);
            sqlCommand.Parameters.AddWithValue("@id_tipo_accion_de_personal", accionPersonal.TipoAccionPersonal.IdAccionPersonal);
            sqlCommand.Parameters.AddWithValue("@id_accion_de_personal", accionPersonal.IdAccionPersonal);
            
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
                Estado.ErrorBitacora(exception.Message, "AccionesPersonalDatos:Actualizar()");
            }

            sqlConnection.Close();

            return resultado;
        }

        /// <summary>
        /// Elimina la entidad AccionPersonal en la base de datos
        /// </summary>
        /// <param name="accionPersonal">Elemento de tipo <code>AccionPersonal</code> que va a ser eliminado</param>
        /// <exception cref="Exception">Lanza una excepción si ocurre un error durante la escrita a la base de datos</exception>
        /// <returns>Retorna un entero con el código según sea el resultado</returns>
        public int Eliminar(int idAccionPersonal)
        {
            SqlConnection sqlConnection = conexion.conexionEDP();
            int resultado = 0;

            SqlCommand sqlCommand = new SqlCommand("delete from acciones_de_personal where id_accion_de_personal=@id_accion_de_personal;", sqlConnection);
            sqlCommand.Parameters.AddWithValue("@id_accion_de_personal", idAccionPersonal);

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
                Estado.ErrorBitacora(exception.Message, "AccionesPersonalDatos:Eliminar()");
            }

            sqlConnection.Close();

            return resultado;
        }
    }
}
