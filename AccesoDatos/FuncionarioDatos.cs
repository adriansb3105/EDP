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
    /// Clase para administrar el CRUD para los funcionarios
    /// </summary>
    public class FuncionarioDatos
    {
        private ConexionDatos conexion = new ConexionDatos();
        private InformacionLaboralDatos informacionLaboralDatos = new InformacionLaboralDatos();

        /// <summary>
        /// Obtiene todos los funcionarios de la base de datos
        /// </summary>
        /// <exception cref="Exception">Lanza una excepción si ocurre un error durante la lectura</exception>
        /// <returns>Retorna una lista <code>List<Funcionario></code> que contiene todos los funcionarios</returns>
        public List<Funcionario> ObtenerTodos(bool habilitados)
        {
            SqlConnection sqlConnection = conexion.conexionEDP();
            List<Funcionario> funcionarios = new List<Funcionario>();
            
            string consulta = @"SELECT numero_identificacion,ruta_fotografia,nombre,
                                primer_apellido,segundo_apellido,numero_telefono,fecha_ingreso,tipo_sangre,
                                lugar_residencia,tipo_licencia_conducir,puesto,correo,extension,observaciones,estado,
                                id_categoria_laboral,id_seccion,id_unidad_programa_laboratorio 
                                FROM funcionarios where estado=@estado order by primer_apellido;";
            SqlCommand sqlCommand = new SqlCommand(consulta, sqlConnection);

            sqlCommand.Parameters.AddWithValue("@estado", habilitados);

            SqlDataReader reader;

            try
            {
                sqlConnection.Open();
                reader = sqlCommand.ExecuteReader();

                while (reader.Read())
                {
                    /**
                     * Se convierte la ruta de las fotografías en un arreglo de bytes en base 64
                     */
                    string filepath = @Convert.ToString(reader["ruta_fotografia"].ToString());

                    if (filepath.Trim() == "")
                    {
                        filepath = Estado.no_foto;
                    }

                    byte[] imageArray = System.IO.File.ReadAllBytes(filepath);
                    string base64ImageRepresentation = Convert.ToBase64String(imageArray);
                    string rutaFotografia = $"data: image/png; base64,{base64ImageRepresentation}";

                    string numeroIdentificacion = Convert.ToString(reader["numero_identificacion"].ToString());
                    string nombre = Convert.ToString(reader["nombre"].ToString());
                    string primerApellido = Convert.ToString(reader["primer_apellido"].ToString());
                    string segundoApellido = Convert.ToString(reader["segundo_apellido"].ToString());
                    string numeroTelefono = Convert.ToString(reader["numero_telefono"].ToString());

                    string fechaIngresoString = reader["fecha_ingreso"].ToString();
                    DateTime fechaIngreso = new DateTime(1900, 01, 01);

                    if (fechaIngresoString.Trim() != "")
                    {
                        fechaIngreso = Convert.ToDateTime(fechaIngresoString);
                    }

                    string tipoSangre = Convert.ToString(reader["tipo_sangre"].ToString());
                    string lugarResidencia = Convert.ToString(reader["lugar_residencia"].ToString());
                    string tipoLicenciaConducir = Convert.ToString(reader["tipo_licencia_conducir"].ToString());
                    string puesto = Convert.ToString(reader["puesto"].ToString());
                    string correo = Convert.ToString(reader["correo"].ToString());
                    string extension = Convert.ToString(reader["extension"].ToString());
                    string observaciones = Convert.ToString(reader["observaciones"].ToString());
                    
                    string estadoString = reader["estado"].ToString();
                    bool estado = false;

                    if (estadoString.Trim() != "")
                    {
                        estado = Convert.ToBoolean(estadoString);
                    }

                    CategoriaLaboral categoriaLaboral = new CategoriaLaboral();
                    Seccion seccion = new Seccion();
                    UnidadProgramaLaboratorio unidadProgramaLaboratorio = new UnidadProgramaLaboratorio();

                    /// Se comprueba que el campo no sea NULL
                    try
                    {
                        int idCategoriaLaboral = Convert.ToInt32(reader["id_categoria_laboral"].ToString());
                        categoriaLaboral = informacionLaboralDatos.ObtenerCategoriaLaboralPorId(idCategoriaLaboral);
                    }
                    catch (Exception e)
                    {
                        Estado.ErrorBitacora(e.Message, "FuncionarioDatos:ObtenerTodos():103");
                    }

                    /// Se comprueba que el campo no sea NULL
                    try
                    {
                        int idSeccion = Convert.ToInt32(reader["id_seccion"].ToString());
                        seccion = informacionLaboralDatos.ObtenerSeccionPorId(idSeccion);
                    }
                    catch(Exception e)
                    {
                        Estado.ErrorBitacora(e.Message, "FuncionarioDatos:ObtenerTodos():111");
                    }

                    /// Se comprueba que el campo no sea NULL
                    try
                    {
                        int idUnidadProgramaLaboratorio = Convert.ToInt32(reader["id_unidad_programa_laboratorio"].ToString());
                        unidadProgramaLaboratorio = informacionLaboralDatos.ObtenerUnidadProgramaLaboratorioPorId(idUnidadProgramaLaboratorio);
                    }
                    catch(Exception e) 
                    {
                        Estado.ErrorBitacora(e.Message, "FuncionarioDatos:ObtenerTodos():119");
                    }

                    Funcionario funcionario = new Funcionario(numeroIdentificacion, rutaFotografia, nombre, primerApellido,
                        segundoApellido, numeroTelefono, fechaIngreso, /*tipoSangre, lugarResidencia,*/ tipoLicenciaConducir,
                        puesto, correo, extension, observaciones, estado, categoriaLaboral, seccion, unidadProgramaLaboratorio);

                    funcionarios.Add(funcionario);
                }

                sqlConnection.Close();
            }
            catch (Exception exception)
            {
                Estado.ErrorBitacora(exception.Message, "FuncionarioDatos:ObtenerTodos()");
            }

            return funcionarios;
        }

        /// <summary>
        /// Inserta la entidad Funcionario en la base de datos
        /// </summary>
        /// <param name="funcionario">Elemento de tipo <code>Funcionario</code> que va a ser insertado</param>
        /// <exception cref="Exception">Lanza una excepción si el número de identifiación ya
        /// se encuentra registado o si ocurre un error durante la escritura a la base de datos</exception>
        /// <returns>Retorna un entero con el código según sea el resultado</returns>
        public int Insertar(Funcionario funcionario)
        {
            SqlConnection sqlConnection = conexion.conexionEDP();
            int resultado = Estado.ERROR_INESPERADO;

            SqlCommand sqlCommand = new SqlCommand("insert into funcionarios(numero_identificacion," +
                "nombre,primer_apellido,segundo_apellido,estado) output INSERTED.numero_identificacion " +
                "values(@numero_identificacion,@nombre,@primer_apellido,@segundo_apellido,@estado);", sqlConnection);

            sqlCommand.Parameters.AddWithValue("@numero_identificacion", funcionario.NumeroIdentificacion);
            sqlCommand.Parameters.AddWithValue("@nombre", funcionario.Nombre);
            sqlCommand.Parameters.AddWithValue("@primer_apellido", funcionario.PrimerApellido);
            sqlCommand.Parameters.AddWithValue("@segundo_apellido", funcionario.SegundoApellido);
            sqlCommand.Parameters.AddWithValue("@estado", funcionario.Estado);

            sqlConnection.Open();
            
            try
            {
                if (Convert.ToString(sqlCommand.ExecuteScalar()) == funcionario.NumeroIdentificacion)
                {
                    resultado = Estado.CREADO;
                }
            }
            catch (SqlException sqlException)
            {
                /// El número de identifiación ya se encuentra registado
                if (sqlException.Number.Equals(Estado.CLAVE_DUPLICADA) || sqlException.Number.Equals(Estado.INDICE_DUPLICADO))
                {
                    resultado = Estado.CLAVE_DUPLICADA;
                }
            }
            catch (Exception exception)
            {
                /// Ocurre un error durante la escritura a la base de datos
                resultado = Estado.ERROR_INESPERADO;
                Estado.ErrorBitacora(exception.Message, "FuncionarioDatos:Insertar()");
            }

            sqlConnection.Close();

            return resultado;
        }

        /// <summary>
        /// Actualiza la entidad Funcionario en la base de datos
        /// </summary>
        /// <param name="funcionario">Elemento de tipo <code>Funcionario</code> que va a ser actualizado</param>
        /// <exception cref="Exception">Lanza una excepción si ocurre un error durante la escritura a la base de datos</exception>
        /// <returns>Retorna un entero con el código según sea el resultado</returns>
        public int Actualizar(Funcionario funcionario)
        {
            SqlConnection sqlConnection = conexion.conexionEDP();
            int resultado = Estado.ERROR_INESPERADO;

            SqlCommand sqlCommand = new SqlCommand("update funcionarios set ruta_fotografia=@ruta_fotografia,nombre=@nombre,numero_telefono=@numero_telefono," +
                "primer_apellido=@primer_apellido,segundo_apellido=@segundo_apellido,fecha_ingreso=@fecha_ingreso," +/*tipo_sangre=@tipo_sangre,lugar_residencia=@lugar_residencia,*/
                "tipo_licencia_conducir=@tipo_licencia_conducir,puesto=@puesto,correo=@correo,extension=@extension,observaciones=@observaciones," +
                "estado=@estado, id_categoria_laboral=@id_categoria_laboral, id_seccion=@id_seccion, id_unidad_programa_laboratorio=@id_unidad_programa_laboratorio" +
                " output INSERTED.numero_identificacion where numero_identificacion=@numero_identificacion;", sqlConnection);

            sqlCommand.Parameters.AddWithValue("@numero_identificacion", funcionario.NumeroIdentificacion);
            sqlCommand.Parameters.AddWithValue("@ruta_fotografia", funcionario.RutaFotografia);
            sqlCommand.Parameters.AddWithValue("@nombre", funcionario.Nombre);
            sqlCommand.Parameters.AddWithValue("@primer_apellido", funcionario.PrimerApellido);
            sqlCommand.Parameters.AddWithValue("@segundo_apellido", funcionario.SegundoApellido);
            sqlCommand.Parameters.AddWithValue("@numero_telefono", funcionario.NumeroTelefono);
            sqlCommand.Parameters.AddWithValue("@fecha_ingreso", funcionario.FechaIngreso);
            //sqlCommand.Parameters.AddWithValue("@tipo_sangre", funcionario.TipoSangre);
            //sqlCommand.Parameters.AddWithValue("@lugar_residencia", funcionario.LugarResidencia);
            sqlCommand.Parameters.AddWithValue("@tipo_licencia_conducir", funcionario.TipoLicenciaConducir);
            sqlCommand.Parameters.AddWithValue("@puesto", funcionario.Puesto);
            sqlCommand.Parameters.AddWithValue("@correo", funcionario.Correo);
            sqlCommand.Parameters.AddWithValue("@extension", funcionario.Extension);
            sqlCommand.Parameters.AddWithValue("@observaciones", funcionario.Observaciones);
            sqlCommand.Parameters.AddWithValue("@estado", funcionario.Estado);
            sqlCommand.Parameters.AddWithValue("@id_categoria_laboral", funcionario.CategoriaLaboral.IdCategoriaLaboral);
            sqlCommand.Parameters.AddWithValue("@id_seccion", funcionario.Seccion.IdSeccion);
            sqlCommand.Parameters.AddWithValue("@id_unidad_programa_laboratorio", funcionario.UnidadProgramaLaboratorio.IdUnidadProgramaLaboratorio);
            
            sqlConnection.Open();

            try
            {
                if (Convert.ToString(sqlCommand.ExecuteScalar()) == funcionario.NumeroIdentificacion)
                {
                    resultado = Estado.ACTUALIZADO;
                }
            }
            catch (Exception exception)
            {
                /// Ocurre un error durante la escritura a la base de datos
                resultado = Estado.ERROR_INESPERADO;
                Estado.ErrorBitacora(exception.Message, "FuncionarioDatos:Actualizar()");
            }

            sqlConnection.Close();

            return resultado;
        }

        /// <summary>
        /// Elimina la entidad Funcionario en la base de datos
        /// </summary>
        /// <param name="funcionario">Elemento de tipo <code>Funcionario</code> que va a ser eliminado</param>
        /// <exception cref="Exception">Lanza una excepción si ocurre un error durante la escritura a la base de datos</exception>
        /// <returns>Retorna un entero con el código según sea el resultado</returns>
        public int Eliminar(Funcionario funcionario)
        {
            int resultado = Estado.ERROR_INESPERADO;

            return resultado;
        }

        /// <summary>
        /// Habilita o deshabilita la entidad Funcionario en la base de datos
        /// </summary>
        /// <param name="numeroIdentificacion">Número de identificación del funcionario a habilitar o deshabilitar</param>
        /// <exception cref="Exception">Lanza una excepción si ocurre un error durante la escritura a la base de datos</exception>
        /// <returns>Retorna un entero con el código según sea el resultado</returns>
        public int HabilitarDeshabilitar(string numeroIdentificacion, bool habilitar)
        {
            SqlConnection sqlConnection = conexion.conexionEDP();
            int resultado = Estado.ERROR_INESPERADO;

            SqlCommand sqlCommand = new SqlCommand("update funcionarios set estado=@estado output INSERTED.numero_identificacion " +
                "where numero_identificacion=@numero_identificacion;", sqlConnection);

            sqlCommand.Parameters.AddWithValue("@numero_identificacion", numeroIdentificacion);
            sqlCommand.Parameters.AddWithValue("@estado", habilitar);
            
            sqlConnection.Open();

            try
            {
                if (Convert.ToString(sqlCommand.ExecuteScalar()) == numeroIdentificacion)
                {
                    resultado = Estado.ACTUALIZADO;
                }
            }
            catch (Exception exception)
            {
                /// Ocurre un error durante la escritura a la base de datos
                resultado = Estado.ERROR_INESPERADO;
                Estado.ErrorBitacora(exception.Message, "HabilitarDeshabilitar:Actualizar()");
            }

            sqlConnection.Close();

            return resultado;
        }
    }
}
