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
    /// Clase para administrar el CRUD para los antecedentes
    /// </summary>
    public class AntecedenteDatos
    {
        private ConexionDatos conexion = new ConexionDatos();

        /// <summary>
        /// Obtiene todos los antecedentes de la base de datos según el número de identificación dado
        /// </summary>
        /// <param name="numeroIdentificacionFuncionario">Número de identificación para buscar sus antecedentes</param>
        /// <exception cref="Exception">Lanza una excepción si ocurre un error durante la lectura</exception>
        /// <returns>Retorna una lista <code>List<Antecedente></code> para el funcionario dado</returns>
        public List<Antecedente> ObtenerPorId(string numeroIdentificacionFuncionario)
        {
            SqlConnection sqlConnection = conexion.conexionEDP();
            List<Antecedente> antecedentes = new List<Antecedente>();

            string consulta = @"SELECT id_antecedente,nombre,descripcion,fecha,ruta_documento,nombre_documento,
            numero_identificacion_funcionario,id_tipo_antecedente 
            FROM antecedentes WHERE numero_identificacion_funcionario = '" + numeroIdentificacionFuncionario+ "' order by nombre;";

            SqlCommand sqlCommand = new SqlCommand(consulta, sqlConnection);

            SqlDataReader reader;

            try
            {
                sqlConnection.Open();
                reader = sqlCommand.ExecuteReader();

                while (reader.Read())
                {
                    Antecedente antecedente = new Antecedente();

                    antecedente.IdAntecedente = Convert.ToInt32(reader["id_antecedente"].ToString());
                    antecedente.Nombre = Convert.ToString(reader["nombre"].ToString());
                    antecedente.Descripcion = Convert.ToString(reader["descripcion"].ToString());

                    string fechaString = reader["fecha"].ToString();
                    DateTime fecha = new DateTime(1900, 01, 01);

                    if (fechaString.Trim() != "")
                    {
                        fecha = Convert.ToDateTime(fechaString);
                    }
                    antecedente.Fecha = fecha;
                    antecedente.RutaDocumento = Convert.ToString(reader["ruta_documento"].ToString());
                    antecedente.NombreDocumento = Convert.ToString(reader["nombre_documento"].ToString());
                    string numeroIdentificacion = Convert.ToString(reader["numero_identificacion_funcionario"].ToString());
                    antecedente.Funcionario = new Funcionario(numeroIdentificacion);
                    antecedente.TipoAntecedente = new TipoAntecedente(Convert.ToInt32(reader["id_tipo_antecedente"].ToString()));

                    antecedentes.Add(antecedente);
                }

                sqlConnection.Close();
            }
            catch (Exception exception)
            {
                Estado.ErrorBitacora(exception.Message, "AntecedenteDatos:ObtenerPorId()");
            }

            return antecedentes;
        }

        /// <summary>
        /// Inserta la entidad Antecedente en la base de datos
        /// </summary>
        /// <param name="antecedente">Elemento de tipo <code>Antecedente</code> que va a ser insertado</param>
        /// <exception cref="Exception">Lanza una excepción si ocurre un error durante la escrita a la base de datos</exception>
        /// <returns>Retorna un entero con el código según sea el resultado</returns>
        public int Insertar(Antecedente antecedente)
        {
            SqlConnection sqlConnection = conexion.conexionEDP();
            int resultado = 0;

            SqlCommand sqlCommand = new SqlCommand("insert into antecedentes(nombre, fecha, descripcion, nombre_documento, " +
                "ruta_documento,numero_identificacion_funcionario, id_tipo_antecedente)" +
                "output INSERTED.id_antecedente values(@nombre, @fecha, @descripcion, @nombre_documento, @ruta_documento," +
                "@numero_identificacion_funcionario, @id_tipo_antecedente);", sqlConnection);

            sqlCommand.Parameters.AddWithValue("@nombre", antecedente.Nombre);
            sqlCommand.Parameters.AddWithValue("@fecha", antecedente.Fecha);
            sqlCommand.Parameters.AddWithValue("@descripcion", antecedente.Descripcion);
            sqlCommand.Parameters.AddWithValue("@nombre_documento", antecedente.NombreDocumento);
            sqlCommand.Parameters.AddWithValue("@ruta_documento", antecedente.RutaDocumento);
            sqlCommand.Parameters.AddWithValue("@numero_identificacion_funcionario", antecedente.Funcionario.NumeroIdentificacion);
            sqlCommand.Parameters.AddWithValue("@id_tipo_antecedente", antecedente.TipoAntecedente.IdAntecedente);

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
                Estado.ErrorBitacora(exception.Message, "AntecedenteDatos:Insertar()");
            }

            sqlConnection.Close();

            return resultado;
        }

        /// <summary>
        /// Actualiza la entidad Antecedente en la base de datos
        /// </summary>
        /// <param name="antecedente">Elemento de tipo <code>Antecedente</code> que va a ser actualizado</param>
        /// <exception cref="Exception">Lanza una excepción si ocurre un error durante la escrita a la base de datos</exception>
        /// <returns>Retorna un entero con el código según sea el resultado</returns>
        public int Actualizar(Antecedente antecedente)
        {
            SqlConnection sqlConnection = conexion.conexionEDP();
            int resultado = 0;

            SqlCommand sqlCommand = new SqlCommand("update antecedentes set nombre=@nombre, fecha=@fecha, descripcion=@descripcion, " +
                "nombre_documento=@nombre_documento, ruta_documento=@ruta_documento, numero_identificacion_funcionario=@numero_identificacion_funcionario," +
                "id_tipo_antecedente=@id_tipo_antecedente output INSERTED.id_antecedente where id_antecedente=@id_antecedente;", sqlConnection);

            sqlCommand.Parameters.AddWithValue("@nombre", antecedente.Nombre);
            sqlCommand.Parameters.AddWithValue("@fecha", antecedente.Fecha);
            sqlCommand.Parameters.AddWithValue("@descripcion", antecedente.Descripcion);
            sqlCommand.Parameters.AddWithValue("@nombre_documento", antecedente.NombreDocumento);
            sqlCommand.Parameters.AddWithValue("@ruta_documento", antecedente.RutaDocumento);
            sqlCommand.Parameters.AddWithValue("@numero_identificacion_funcionario", antecedente.Funcionario.NumeroIdentificacion);
            sqlCommand.Parameters.AddWithValue("@id_tipo_antecedente", antecedente.TipoAntecedente.IdAntecedente);
            sqlCommand.Parameters.AddWithValue("@id_antecedente", antecedente.IdAntecedente);

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
                Estado.ErrorBitacora(exception.Message, "AntecedenteDatos:Actualizar()");
            }

            sqlConnection.Close();

            return resultado;
        }

        /// <summary>
        /// Elimina la entidad Antecedente en la base de datos
        /// </summary>
        /// <param name="idAntecedente">Elemento de tipo <code>Antecedente</code> que va a ser eliminado</param>
        /// <exception cref="Exception">Lanza una excepción si ocurre un error durante la escrita a la base de datos</exception>
        /// <returns>Retorna un entero con el código según sea el resultado</returns>
        public int Eliminar(int idAntecedente)
        {
            SqlConnection sqlConnection = conexion.conexionEDP();
            int resultado = 0;

            SqlCommand sqlCommand = new SqlCommand("delete from antecedentes where id_antecedente=@id_antecedente;", sqlConnection);
            sqlCommand.Parameters.AddWithValue("@id_antecedente", idAntecedente);

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
                Estado.ErrorBitacora(exception.Message, "AntecedenteDatos:Eliminar()");
            }

            sqlConnection.Close();

            return resultado;
        }
    }
}
