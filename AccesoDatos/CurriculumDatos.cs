using Entidades;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos
{
    public class CurriculumDatos
    {
        private ConexionDatos conexion = new ConexionDatos();

        public List<Curriculum> ObtenerPorId(string numeroIdentificacionFuncionario)
        {
            SqlConnection sqlConnection = conexion.conexionEDP();
            List<Curriculum> listaCurriculums = new List<Curriculum>();

            string consulta = @"SELECT * FROM curriculum
            WHERE numero_identificacion = @numeroIdentificacion";

            SqlCommand sqlCommand = new SqlCommand(consulta, sqlConnection);
            sqlCommand.Parameters.AddWithValue("@numeroIdentificacion", numeroIdentificacionFuncionario);

            SqlDataReader reader;

            try
            {
                sqlConnection.Open();
                reader = sqlCommand.ExecuteReader();

                while (reader.Read())
                {
                    Curriculum curriculum = new Curriculum();
                    Funcionario funcionario = new Funcionario();

                    curriculum.idCurriculum = Convert.ToInt32(reader["id_curriculum"].ToString());
                    curriculum.nombre = reader["nombre"].ToString();
                    curriculum.ruta = reader["ruta"].ToString();
                    curriculum.descripcion = reader["descripcion"].ToString();
                    funcionario.NumeroIdentificacion = reader["numero_identificacion"].ToString();
                    curriculum.funcionario = funcionario;

                    listaCurriculums.Add(curriculum);
                }

                sqlConnection.Close();
            }
            catch (Exception exception)
            {
                Estado.ErrorBitacora(exception.Message, "curriculumDatos:ObtenerPorId()");
            }

            return listaCurriculums;
        }


        public int Insertar(Curriculum curriculum)
        {
            SqlConnection sqlConnection = conexion.conexionEDP();
            int resultado = 0;

            SqlCommand sqlCommand = new SqlCommand(@"insert into curriculum(nombre,ruta,descripcion,numero_identificacion)
                values(@nombre,@ruta,@descripcion,@numeroIdentificacion);SELECT SCOPE_IDENTITY();", sqlConnection);

            sqlCommand.Parameters.AddWithValue("@nombre", curriculum.nombre);
            sqlCommand.Parameters.AddWithValue("@ruta", curriculum.ruta);
            sqlCommand.Parameters.AddWithValue("@descripcion", curriculum.descripcion);
            sqlCommand.Parameters.AddWithValue("@numeroIdentificacion", curriculum.funcionario.NumeroIdentificacion);

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
                Estado.ErrorBitacora(exception.Message, "CurriculumDatos:Insertar()");
            }

            sqlConnection.Close();

            return resultado;
        }

        public void Actualizar(Curriculum curriculum)
        {
            SqlConnection sqlConnection = conexion.conexionEDP();

            SqlCommand sqlCommand = new SqlCommand(@"update curriculum set nombre=@nombre, ruta=@ruta, descripcion=@descripcion, numero_identificacion=@numeroIdentificacion 
            where id_curriculum=@idCurriculum;", sqlConnection);

            sqlCommand.Parameters.AddWithValue("@idCurriculum", curriculum.idCurriculum);
            sqlCommand.Parameters.AddWithValue("@nombre", curriculum.nombre);
            sqlCommand.Parameters.AddWithValue("@ruta", curriculum.ruta);
            sqlCommand.Parameters.AddWithValue("@descripcion", curriculum.descripcion);
            sqlCommand.Parameters.AddWithValue("@numeroIdentificacion", curriculum.funcionario.NumeroIdentificacion);

            sqlConnection.Open();

            try
            {
                /// Retorna el identificador con el cuál fue actualizado
                sqlCommand.ExecuteReader();
            }
            catch (Exception exception)
            {

                Estado.ErrorBitacora(exception.Message, "CurriculumDatos:Actualizar()");
            }
            sqlConnection.Close();
        }

        public void Eliminar(int idCurriculum)
        {
            SqlConnection sqlConnection = conexion.conexionEDP();

            SqlCommand sqlCommand = new SqlCommand("delete from curriculum where id_curriculum=@idCurriculum;", sqlConnection);
            sqlCommand.Parameters.AddWithValue("@idCurriculum", idCurriculum);

            sqlConnection.Open();

            try
            {
                /// Retorna el identificador con el cuál fue eliminado
                sqlCommand.ExecuteReader();
            }
            catch (Exception exception)
            {
                /// Ocurre un error durante la escrita a la base de datos
                Estado.ErrorBitacora(exception.Message, "CurriculumDatos:Eliminar()");
            }

            sqlConnection.Close();
        }
    }
}
