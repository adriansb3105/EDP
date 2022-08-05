using Entidades;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos
{
    public class DocumentoTramiteDatos
    {
        private ConexionDatos conexion = new ConexionDatos();

        public List<DocumentoTramite> ObtenerPorId(string numeroIdentificacionFuncionario)
        {
            SqlConnection sqlConnection = conexion.conexionEDP();
            List<DocumentoTramite> documentosTramites = new List<DocumentoTramite>();

            string consulta = @"SELECT id_documento_tramite, numero,
            descripcion, ruta_documento,nombre_documento, numero_identificacion_funcionario
            FROM documento_tramite
            WHERE numero_identificacion_funcionario = @numeroIdentificacion order by numero;";

            SqlCommand sqlCommand = new SqlCommand(consulta, sqlConnection);
            sqlCommand.Parameters.AddWithValue("@numeroIdentificacion", numeroIdentificacionFuncionario);

            SqlDataReader reader;

            try
            {
                sqlConnection.Open();
                reader = sqlCommand.ExecuteReader();

                while (reader.Read())
                {
                    DocumentoTramite documentoTramite = new DocumentoTramite();
                    Funcionario funcionario = new Funcionario();

                    documentoTramite.idDocumentoTramite = Convert.ToInt32(reader["id_documento_tramite"].ToString());
                    documentoTramite.nombreDocumento = reader["nombre_documento"].ToString();
                    documentoTramite.rutaDocumento = reader["ruta_documento"].ToString();
                    documentoTramite.numero = reader["numero"].ToString();
                    documentoTramite.descripcion = reader["descripcion"].ToString();
                    funcionario.NumeroIdentificacion = reader["numero_identificacion_funcionario"].ToString();
                    documentoTramite.funcionario = funcionario;

                    documentosTramites.Add(documentoTramite);
                }

                sqlConnection.Close();
            }
            catch (Exception exception)
            {
                Estado.ErrorBitacora(exception.Message, "DocumnetoTramiteDatos:ObtenerPorId()");
            }

            return documentosTramites;
        }

       
        public int Insertar(DocumentoTramite documentoTramite)
        {
            SqlConnection sqlConnection = conexion.conexionEDP();
            int resultado = 0;

            SqlCommand sqlCommand = new SqlCommand(@"insert into documento_tramite(nombre_documento,ruta_documento,numero,descripcion,numero_identificacion_funcionario)
                values(@nombreDocumento,@rutaDocumento,@numero,@descripcion,@numeroIdentificacionFuncionario);SELECT SCOPE_IDENTITY();", sqlConnection);

            sqlCommand.Parameters.AddWithValue("@nombreDocumento", documentoTramite.nombreDocumento);
            sqlCommand.Parameters.AddWithValue("@rutaDocumento", documentoTramite.rutaDocumento);
            sqlCommand.Parameters.AddWithValue("@numero", documentoTramite.numero);
            sqlCommand.Parameters.AddWithValue("@descripcion", documentoTramite.descripcion);
            sqlCommand.Parameters.AddWithValue("@numeroIdentificacionFuncionario", documentoTramite.funcionario.NumeroIdentificacion);

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
                Estado.ErrorBitacora(exception.Message, "DocumnetoTramiteDatos:Insertar()");
            }

            sqlConnection.Close();

            return resultado;
        }

        public void Actualizar(DocumentoTramite documentoTramite)
        {
            SqlConnection sqlConnection = conexion.conexionEDP();

            SqlCommand sqlCommand = new SqlCommand(@"update documento_tramite set nombre_documento =@nombreDocumento,
            ruta_documento=@rutaDocumento,numero=@numero,descripcion=@descripcion,numero_identificacion_funcionario=@numeroIdentificacionFuncionario 
            where id_documento_tramite=@idDocumentoTramite;", sqlConnection);

            sqlCommand.Parameters.AddWithValue("@idDocumentoTramite", documentoTramite.idDocumentoTramite);
            sqlCommand.Parameters.AddWithValue("@nombreDocumento", documentoTramite.nombreDocumento);
            sqlCommand.Parameters.AddWithValue("@rutaDocumento", documentoTramite.rutaDocumento);
            sqlCommand.Parameters.AddWithValue("@numero", documentoTramite.numero);
            sqlCommand.Parameters.AddWithValue("@descripcion", documentoTramite.descripcion);
            sqlCommand.Parameters.AddWithValue("@numeroIdentificacionFuncionario", documentoTramite.funcionario.NumeroIdentificacion);

            sqlConnection.Open();

            try
            {
                /// Retorna el identificador con el cuál fue actualizado
                sqlCommand.ExecuteReader();
            }
            catch (Exception exception)
            {
               
                Estado.ErrorBitacora(exception.Message, "DocumnetoTramiteDatos:Actualizar()");
            }
            sqlConnection.Close();
        }

        public void Eliminar(int idDocumentoTramite)
        {
            SqlConnection sqlConnection = conexion.conexionEDP();

            SqlCommand sqlCommand = new SqlCommand("delete from documento_tramite where id_documento_tramite=@idDocumentoTramite;", sqlConnection);
            sqlCommand.Parameters.AddWithValue("@idDocumentoTramite", idDocumentoTramite);

            sqlConnection.Open();

            try
            {
                /// Retorna el identificador con el cuál fue eliminado
                sqlCommand.ExecuteReader();
            }
            catch (Exception exception)
            {
                /// Ocurre un error durante la escrita a la base de datos
                Estado.ErrorBitacora(exception.Message, "DocumnetoTramiteDatos:Eliminar()");
            }

            sqlConnection.Close();
        }
    }
}
