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
    /// Clase para administrar la conexión al servidor de base de datos y al servidor del Active Directory
    /// </summary>
    public class ConexionDatos
    {
        public SqlConnection conexionLogin()
        {
            return new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["LOGINConnectionString"].ConnectionString);
        }

        public SqlConnection conexionEDP()
        {
            return new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["EDPconnectionString"].ConnectionString);
            
        }

        public object[] loguearse(String usuario)
        {
            object[] rolNombreCompleto = new object[2];
            SqlConnection sqlConnection = conexionLogin();
            SqlCommand sqlCommand = new SqlCommand("select R.id_rol, U.nombre_completo from " +
                "Rol R, Usuario U, Aplicacion A, Usuario_Rol_Aplicacion URA " +
                "where A.nombre_aplicacion='EDP' and U.usuario=@usuario and URA.id_aplicacion=A.id_aplicacion and " +
                "URA.id_usuario = u.id_usuario and R.id_rol = URA.id_rol ;", sqlConnection);
            sqlCommand.Parameters.AddWithValue("@usuario", usuario.ToLower());
            SqlDataReader reader;
            sqlConnection.Open();
            reader = sqlCommand.ExecuteReader();
            if (reader.Read())
            {

                int rol = Int32.Parse(reader.GetValue(0).ToString());
                String nombreCompleto = reader.GetValue(1).ToString();

                rolNombreCompleto[0] = rol;
                rolNombreCompleto[1] = nombreCompleto;
            }

            sqlConnection.Close();

            return rolNombreCompleto;
        }
    }
}
