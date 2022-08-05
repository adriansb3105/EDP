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
    /// Clase para administrar el CRUD para los estudios formales
    /// </summary>
    public class EstudioDatos
    {
        private ConexionDatos conexion = new ConexionDatos();

        /// <summary>
        /// Obtiene todos los estudios de la base de datos según el número de identificación dado
        /// </summary>
        /// <param name="numeroIdentificacionFuncionario">Número de identificación para buscar sus estudios</param>
        /// <exception cref="Exception">Lanza una excepción si ocurre un error durante la lectura</exception>
        /// <returns>Retorna una lista <code>List<Estudio></code> que contiene los estudios para el funcionario dado</returns>
        public List<Estudio> ObtenerPorId(string clasificacion, string numeroIdentificacionFuncionario)
        {
            SqlConnection sqlConnection = conexion.conexionEDP();
            List<Estudio> estudios = new List<Estudio>();

            string consulta = @"SELECT id_estudio, estudios.nombre,nombre_documento,ruta_documento,fecha_inicio,
                                fecha_finalizacion,observacion,entregado,numero_identificacion_funcionario,
                                estudios.id_tipo_estudio FROM estudios
                                JOIN tipos_estudios ON estudios.id_tipo_estudio = tipos_estudios.id_tipo_estudio
                                WHERE estudios.numero_identificacion_funcionario = '" + numeroIdentificacionFuncionario + 
                                "' AND tipos_estudios.clasificacion = '"+clasificacion+ "' order by estudios.nombre;";

            SqlCommand sqlCommand = new SqlCommand(consulta, sqlConnection);

            SqlDataReader reader;

            try
            {
                sqlConnection.Open();
                reader = sqlCommand.ExecuteReader();

                while (reader.Read())
                {
                    Estudio estudio = new Estudio();

                    estudio.IdEstudio = Convert.ToInt32(reader["id_estudio"].ToString());
                    estudio.Nombre = Convert.ToString(reader["nombre"].ToString());
                    estudio.NombreDocumento = Convert.ToString(reader["nombre_documento"].ToString());
                    estudio.RutaDocumento = Convert.ToString(reader["ruta_documento"].ToString());
                    estudio.FechaInicio = Convert.ToDateTime(reader["fecha_inicio"].ToString());
                    estudio.FechaFinalizacion = Convert.ToDateTime(reader["fecha_finalizacion"].ToString());
                    estudio.Observacion = Convert.ToString(reader["observacion"].ToString());
                    estudio.Entregado = Convert.ToBoolean(reader["entregado"].ToString());
                    string numeroIdentificacion = Convert.ToString(reader["numero_identificacion_funcionario"].ToString());
                    estudio.Funcionario = new Funcionario(numeroIdentificacion);
                    estudio.TipoEstudio = new TipoEstudio(Convert.ToInt32(reader["id_tipo_estudio"].ToString()));
                    
                    estudios.Add(estudio);
                }

                sqlConnection.Close();
            }
            catch (Exception exception)
            {
                Estado.ErrorBitacora(exception.Message, "EstudioDatos:ObtenerPorId()");
            }

            return estudios;
        }

        /// <summary>
        /// Inserta la entidad Estudio en la base de datos
        /// </summary>
        /// <param name="estudio">Elemento de tipo <code>Estudio</code> que va a ser insertado</param>
        /// <exception cref="Exception">Lanza una excepción si ocurre un error durante la escrita a la base de datos</exception>
        /// <returns>Retorna un entero con el código según sea el resultado</returns>
        public int Insertar(Estudio estudio)
        {
            SqlConnection sqlConnection = conexion.conexionEDP();
            int resultado = 0;

            SqlCommand sqlCommand = new SqlCommand("insert into estudios(nombre, nombre_documento, ruta_documento,"+
                "fecha_inicio, fecha_finalizacion, observacion, entregado, numero_identificacion_funcionario, id_tipo_estudio)"+
                "output INSERTED.id_estudio values(@nombre, @nombre_documento, @ruta_documento," +
                "@fecha_inicio, @fecha_finalizacion, @observacion, @entregado, @numero_identificacion_funcionario," +
                "@id_tipo_estudio);", sqlConnection);

            sqlCommand.Parameters.AddWithValue("@nombre", estudio.Nombre);
            sqlCommand.Parameters.AddWithValue("@nombre_documento", estudio.NombreDocumento);
            sqlCommand.Parameters.AddWithValue("@ruta_documento", estudio.RutaDocumento);
            sqlCommand.Parameters.AddWithValue("@fecha_inicio", estudio.FechaInicio);
            sqlCommand.Parameters.AddWithValue("@fecha_finalizacion", estudio.FechaFinalizacion);
            sqlCommand.Parameters.AddWithValue("@observacion", estudio.Observacion);
            sqlCommand.Parameters.AddWithValue("@entregado", estudio.Entregado);
            sqlCommand.Parameters.AddWithValue("@numero_identificacion_funcionario", estudio.Funcionario.NumeroIdentificacion);
            sqlCommand.Parameters.AddWithValue("@id_tipo_estudio", estudio.TipoEstudio.IdTipoEstudio);
            
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
                Estado.ErrorBitacora(exception.Message, "EstudioDatos:Insertar()");
            }

            sqlConnection.Close();

            return resultado;
        }

        /// <summary>
        /// Actualiza la entidad Estudio en la base de datos
        /// </summary>
        /// <param name="estudio">Elemento de tipo <code>Estudio</code> que va a ser actualizado</param>
        /// <exception cref="Exception">Lanza una excepción si ocurre un error durante la escrita a la base de datos</exception>
        /// <returns>Retorna un entero con el código según sea el resultado</returns>
        public int Actualizar(Estudio estudio)
        {
            SqlConnection sqlConnection = conexion.conexionEDP();
            int resultado = 0;

            SqlCommand sqlCommand = new SqlCommand("update estudios set nombre=@nombre, nombre_documento=@nombre_documento," +
                "ruta_documento=@ruta_documento,fecha_inicio=@fecha_inicio, fecha_finalizacion=@fecha_finalizacion," +
                "observacion=@observacion, entregado=@entregado, numero_identificacion_funcionario=@numero_identificacion_funcionario," +
                "id_tipo_estudio=@id_tipo_estudio output INSERTED.id_estudio where id_estudio=@id_estudio;", sqlConnection);

            sqlCommand.Parameters.AddWithValue("@nombre", estudio.Nombre);
            sqlCommand.Parameters.AddWithValue("@nombre_documento", estudio.NombreDocumento);
            sqlCommand.Parameters.AddWithValue("@ruta_documento", estudio.RutaDocumento);
            sqlCommand.Parameters.AddWithValue("@fecha_inicio", estudio.FechaInicio);
            sqlCommand.Parameters.AddWithValue("@fecha_finalizacion", estudio.FechaFinalizacion);
            sqlCommand.Parameters.AddWithValue("@observacion", estudio.Observacion);
            sqlCommand.Parameters.AddWithValue("@entregado", estudio.Entregado);
            sqlCommand.Parameters.AddWithValue("@numero_identificacion_funcionario", estudio.Funcionario.NumeroIdentificacion);
            sqlCommand.Parameters.AddWithValue("@id_tipo_estudio", estudio.TipoEstudio.IdTipoEstudio);
            sqlCommand.Parameters.AddWithValue("@id_estudio", estudio.IdEstudio);

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
                Estado.ErrorBitacora(exception.Message, "EstudioDatos:Actualizar()");
            }

            sqlConnection.Close();

            return resultado;
        }

        /// <summary>
        /// Elimina la entidad Estudio en la base de datos
        /// </summary>
        /// <param name="estudio">Elemento de tipo <code>Estudio</code> que va a ser eliminado</param>
        /// <exception cref="Exception">Lanza una excepción si ocurre un error durante la escrita a la base de datos</exception>
        /// <returns>Retorna un entero con el código según sea el resultado</returns>
        public int Eliminar(int idEstudio)
        {
            SqlConnection sqlConnection = conexion.conexionEDP();
            int resultado = 0;

            SqlCommand sqlCommand = new SqlCommand("delete from estudios where id_estudio=@id_estudio;", sqlConnection);
            sqlCommand.Parameters.AddWithValue("@id_estudio", idEstudio);

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
                Estado.ErrorBitacora(exception.Message, "EstudioDatos:Eliminar()");
            }

            sqlConnection.Close();

            return resultado;
        }
    }
}
