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
    /// Clase para administrar el CRUD para las experiencias laborales
    /// </summary>
    public class ExperienciaLaboralDatos
    {
        private ConexionDatos conexion = new ConexionDatos();

        /// <summary>
        /// Obtiene todos las experiencias laborales de la base de datos según el número de identificación dado
        /// </summary>
        /// <param name="numeroIdentificacionFuncionario">Número de identificación para buscar sus experiencias laborales</param>
        /// <exception cref="Exception">Lanza una excepción si ocurre un error durante la lectura</exception>
        /// <returns>Retorna una lista <code>List<ExperienciaLaboral></code> para el funcionario dado</returns>
        public List<ExperienciaLaboral> ObtenerPorId(string numeroIdentificacionFuncionario)
        {
            SqlConnection sqlConnection = conexion.conexionEDP();
            List<ExperienciaLaboral> experienciasLaborales = new List<ExperienciaLaboral>();

            string consulta = @"SELECT id_experiencia_laboral,nombre_empresa,fecha_inicio,fecha_finalizacion,descripcion_puesto,numero_identificacion_funcionario 
            FROM experiencias_laborales WHERE numero_identificacion_funcionario = '" + numeroIdentificacionFuncionario+ "' order by nombre_empresa;";

            SqlCommand sqlCommand = new SqlCommand(consulta, sqlConnection);

            SqlDataReader reader;

            try
            {
                sqlConnection.Open();
                reader = sqlCommand.ExecuteReader();

                while (reader.Read())
                {
                    ExperienciaLaboral experienciaLaboral = new ExperienciaLaboral();

                    experienciaLaboral.IdExperienciaLaboral = Convert.ToInt32(reader["id_experiencia_laboral"].ToString());
                    experienciaLaboral.NombreEmpresa = Convert.ToString(reader["nombre_empresa"].ToString());
                    experienciaLaboral.FechaInicio = Convert.ToDateTime(reader["fecha_inicio"].ToString());
                    experienciaLaboral.FechaFinalizacion = Convert.ToDateTime(reader["fecha_finalizacion"].ToString());
                    experienciaLaboral.DescripcionPuesto = Convert.ToString(reader["descripcion_puesto"].ToString());
                    string numeroIdentificacion = Convert.ToString(reader["numero_identificacion_funcionario"].ToString());
                    experienciaLaboral.Funcionario = new Funcionario(numeroIdentificacion);

                    experienciasLaborales.Add(experienciaLaboral);
                }

                sqlConnection.Close();
            }
            catch (Exception exception)
            {
                Estado.ErrorBitacora(exception.Message, "ExperienciaLaboralDatos:ObtenerPorId()");
            }

            return experienciasLaborales;
        }

        /// <summary>
        /// Inserta la entidad Experiencia Laboral en la base de datos
        /// </summary>
        /// <param name="experienciaLaboral">Elemento de tipo <code>ExperienciaLaboral</code> que va a ser insertado</param>
        /// <exception cref="Exception">Lanza una excepción si ocurre un error durante la escrita a la base de datos</exception>
        /// <returns>Retorna un entero con el código según sea el resultado</returns>
        public int Insertar(ExperienciaLaboral experienciaLaboral)
        {
            SqlConnection sqlConnection = conexion.conexionEDP();
            int resultado = 0;

            SqlCommand sqlCommand = new SqlCommand("insert into experiencias_laborales(nombre_empresa,fecha_inicio," +
                "fecha_finalizacion, descripcion_puesto, numero_identificacion_funcionario)" +
                "output INSERTED.id_experiencia_laboral values(@nombre_empresa,@fecha_inicio, @fecha_finalizacion, " +
                "@descripcion_puesto, @numero_identificacion_funcionario);", sqlConnection);

            sqlCommand.Parameters.AddWithValue("@nombre_empresa", experienciaLaboral.NombreEmpresa);
            sqlCommand.Parameters.AddWithValue("@fecha_inicio", experienciaLaboral.FechaInicio);
            sqlCommand.Parameters.AddWithValue("@fecha_finalizacion", experienciaLaboral.FechaFinalizacion);
            sqlCommand.Parameters.AddWithValue("@descripcion_puesto", experienciaLaboral.DescripcionPuesto);
            sqlCommand.Parameters.AddWithValue("@numero_identificacion_funcionario", experienciaLaboral.Funcionario.NumeroIdentificacion);
            
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
                Estado.ErrorBitacora(exception.Message, "ExperienciaLaboralDatos:Insertar()");
            }

            sqlConnection.Close();

            return resultado;
        }

        /// <summary>
        /// Actualiza la entidad Experiencia Laboral en la base de datos
        /// </summary>
        /// <param name="experienciaLaboral">Elemento de tipo <code>ExperienciaLaboral</code> que va a ser actualizado</param>
        /// <exception cref="Exception">Lanza una excepción si ocurre un error durante la escrita a la base de datos</exception>
        /// <returns>Retorna un entero con el código según sea el resultado</returns>
        public int Actualizar(ExperienciaLaboral experienciaLaboral)
        {
            SqlConnection sqlConnection = conexion.conexionEDP();
            int resultado = 0;

            SqlCommand sqlCommand = new SqlCommand("update experiencias_laborales set nombre_empresa=@nombre_empresa,fecha_inicio=@fecha_inicio," +
                "fecha_finalizacion=@fecha_finalizacion, descripcion_puesto=@descripcion_puesto, numero_identificacion_funcionario=@numero_identificacion_funcionario " +
                "output INSERTED.id_experiencia_laboral where id_experiencia_laboral=@id_experiencia_laboral;", sqlConnection); 

            sqlCommand.Parameters.AddWithValue("@nombre_empresa", experienciaLaboral.NombreEmpresa);
            sqlCommand.Parameters.AddWithValue("@fecha_inicio", experienciaLaboral.FechaInicio);
            sqlCommand.Parameters.AddWithValue("@fecha_finalizacion", experienciaLaboral.FechaFinalizacion);
            sqlCommand.Parameters.AddWithValue("@descripcion_puesto", experienciaLaboral.DescripcionPuesto);
            sqlCommand.Parameters.AddWithValue("@numero_identificacion_funcionario", experienciaLaboral.Funcionario.NumeroIdentificacion);
            sqlCommand.Parameters.AddWithValue("@id_experiencia_laboral", experienciaLaboral.IdExperienciaLaboral);

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
                Estado.ErrorBitacora(exception.Message, "ExperienciaLaboralDatos:Actualizar()");
            }

            sqlConnection.Close();

            return resultado;
        }

        /// <summary>
        /// Elimina la entidad ExperienciaLaboral en la base de datos
        /// </summary>
        /// <param name="experienciaLaboral">Elemento de tipo <code>ExperienciaLaboral</code> que va a ser eliminado</param>
        /// <exception cref="Exception">Lanza una excepción si ocurre un error durante la escrita a la base de datos</exception>
        /// <returns>Retorna un entero con el código según sea el resultado</returns>
        public int Eliminar(int idExperienciaLaboral)
        {
            SqlConnection sqlConnection = conexion.conexionEDP();
            int resultado = 0;

            SqlCommand sqlCommand = new SqlCommand("delete from experiencias_laborales where id_experiencia_laboral=@id_experiencia_laboral;", sqlConnection);
            sqlCommand.Parameters.AddWithValue("@id_experiencia_laboral", idExperienciaLaboral);

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
                Estado.ErrorBitacora(exception.Message, "ExperienciaLaboralDatos:Eliminar()");
            }

            sqlConnection.Close();

            return resultado;
        }
    }
}
