using AccesoDatos;
using Entidades;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EDP.Catalogos.Funcionarios
{
    public partial class VerFuncionario : System.Web.UI.Page
    {
        #region Variables Globales
        EstudioDatos estudioDatos = new EstudioDatos();
        //SalarioDatos salarioDatos = new SalarioDatos();
        AntecedenteDatos antecedenteDatos = new AntecedenteDatos();
        FuncionarioDatos funcionarioDatos = new FuncionarioDatos();
        TipoEstudioDatos tipoEstudioDatos = new TipoEstudioDatos();
        ComprobantesDatos comprobantesDatos = new ComprobantesDatos();
        HabilidadBlandaDatos habilidadBlandaDatos = new HabilidadBlandaDatos();
        InteresPersonalDatos interesPersonalDatos = new InteresPersonalDatos();
        SuspensionPermisoDatos suspensionPermisoDatos = new SuspensionPermisoDatos();
        PensionOEmbargoDatos pensionOEmbargoDatos = new PensionOEmbargoDatos();
        TipoComprobantesDatos tipoComprobantesDatos = new TipoComprobantesDatos();
        //AccionesPersonalDatos accionesPersonalDatos = new AccionesPersonalDatos();
        DocumentoTramiteDatos documentoTramiteDatos = new DocumentoTramiteDatos();
        InformacionLaboralDatos informacionLaboralDatos = new InformacionLaboralDatos();
        //ExperienciaLaboralDatos experienciaLaboralDatos = new ExperienciaLaboralDatos();
        TipoAntecedentesDatos tipoAntecedentesDatos = new TipoAntecedentesDatos();
        TipoAccionesPersonalDatos tipoAccionesPersonalDatos = new TipoAccionesPersonalDatos();
        CurriculumDatos curriculumDatos = new CurriculumDatos();
        #endregion

        #region Page load
        protected void Page_Load(object sender, EventArgs e)
        {
            //controla los menus q se muestran y las pantallas que se muestras segun el rol que tiene el usuario
            //si no tiene permiso de ver la pagina se redirecciona a login
            int[] rolesPermitidos = { 2, 3 };
            Utilidades.escogerMenu(Page, rolesPermitidos);

            if (!Page.IsPostBack)
            {
                Funcionario funcionario = (Funcionario)Session["funcionarioVer"];
                Session["listaEstudios"] = null;
                Session["listaCursos"] = null;
                Session["listaCertificados"] = null;
                Session["listaExperienciasLaborales"] = null;
                Session["listaHabilidadesBlandas"] = null;
                Session["listaInteresesPersonales"] = null;
                Session["listaPensionOEmbargos"] = null;
                Session["listaAccionesPersonal"] = null;
                Session["listaComprobantes"] = null;
                Session["listaAntecedentes"] = null;
                Session["listaSuspensionPermisos"] = null;

                Session["idEstudio"] = null;
                Session["idCurso"] = null;
                Session["idCertificado"] = null;
                Session["idExperienciaLaboral"] = null;
                Session["idHabilidadBlanda"] = null;
                Session["idInteresPersonal"] = null;
                Session["idPensionOEmbargo"] = null;
                Session["idAccionPersonal"] = null;
                Session["idComprobante"] = null;
                Session["idAntecedente"] = null;
                Session["idSuspensionPermiso"] = null;

                Session["rutaDocumentoEstudio"] = null;
                Session["rutaDocumentoCurso"] = null;
                Session["rutaDocumentoCertificado"] = null;
                Session["rutaDocumentoPensionOEmbargo"] = null;
                Session["rutaDocumentoAccionPersonal"] = null;
                Session["rutaDocumentoComprobante"] = null;
                Session["rutaDocumentoAntecedente"] = null;
                Session["rutaDocumentoSuspensionPermiso"] = null;

                Session["nombreDocumentoEstudio"] = null;
                Session["nombreDocumentoCurso"] = null;
                Session["nombreDocumentoCertificado"] = null;
                Session["nombreDocumentoPensionOEmbargo"] = null;
                Session["nombreDocumentoAccionPersonal"] = null;
                Session["nombreDocumentoComprobante"] = null;
                Session["nombreDocumentoAntecedente"] = null;
                Session["nombreDocumentoSuspensionPermiso"] = null;

                if (funcionario != null)
                {
                    LlenarEstudiosDDL();
                    //LlenarSangreDDL();
                    LlenarEstadoDDL();
                    LlenarSeccionDDL();
                    LlenarLicenciasDDL();
                    LlenarCategoriaLaboralDDL();
                    LlenarUnidadProgramaLaboratorioDDL();
                    //LlenarAccionesPersonalDDL();
                    LlenarTipoSuspensionPermisoDDL();
                    LlenarComprobantesDDL();
                    LlenarAntecedentesDDL();
                    LlenarEstudiosDDLAgregar();
                    //LlenarAccionesPersonalDDLAgregar();
                    LlenarTipoSuspensionPermisoDDLAgregar();
                    LlenarComprobantesDDLAgregar();
                    LlenarAntecedentesDDLAgregar();

                    divInformacionPersonal.Visible = true;
                    divCurriculo.Visible = false;
                    divInformacionFinanciera.Visible = false;
                    divAcciones.Visible = false;
                    divComprobantes.Visible = false;
                    divAmonestacionesAntecedentes.Visible = false;

                    imgFoto.Attributes.Add("src", funcionario.RutaFotografia);
                    txtIdentificacion.Text = funcionario.NumeroIdentificacion;
                    txtNombre.Text = funcionario.Nombre;
                    txtPrimerApellido.Text = funcionario.PrimerApellido;
                    txtSegundoApellido.Text = funcionario.SegundoApellido;
                    txtTelefono.Text = funcionario.NumeroTelefono;
                    //txtResidencia.Text = funcionario.LugarResidencia;
                    txtPuesto.Text = funcionario.Puesto;
                    txtCorreo.Text = funcionario.Correo;
                    txtExtension.Text = funcionario.Extension;
                    txtObservaciones.Text = funcionario.Observaciones;
                    LicenciaDDL.SelectedValue = funcionario.TipoLicenciaConducir;
                    //SangreDDL.SelectedValue = funcionario.TipoSangre;
                    CategoriaLaboralDDL.SelectedValue = funcionario.CategoriaLaboral.IdCategoriaLaboral.ToString();
                    SecciónDDL.SelectedValue = funcionario.Seccion.IdSeccion.ToString();
                    UnidadProgramaLaboratorioDDL.SelectedValue = funcionario.UnidadProgramaLaboratorio.IdUnidadProgramaLaboratorio.ToString();
                    EstadoDDL.SelectedValue = funcionario.Estado ? "True" : "False";

                    if (funcionario.FechaIngreso.Year == 1900)
                    {
                        txtFechaIngreso.Text = "";
                    }
                    else
                    {
                        string anio = funcionario.FechaIngreso.Year.ToString();
                        string mes = funcionario.FechaIngreso.Month.ToString();
                        string dia = funcionario.FechaIngreso.Day.ToString();
                        if (dia.Length == 1) { dia = "0" + dia; }
                        if (mes.Length == 1) { mes = "0" + mes; }
                        txtFechaIngreso.Text = anio + "-" + mes + "-" + dia;
                    }

                    LlenarEstudios();
                    LlenarCursos();
                    LlenarCertificados();
                    LlenarExperiencia();
                    LlenarHabilidadBlanda();
                    LlenarInteresPersonal();
                    LlenarPensionOEmbargo();
                    LlenarAccionPersonal();
                    LlenarComprobante();
                    LlenarAntecedente();
                    LlenarSuspensionPermiso();
                }
                else
                {
                    String url = Page.ResolveUrl("~/Default.aspx");
                    Response.Redirect(url);
                }
            }
            else
            {
                ///En caso de que la variable de sesión no esté vacía, persistirán los datos ingresados anteriormente.
                //if (Session["sessionFotografia"] != null && !fuFotografia.HasFiles)
                //{
                //    fuFotografia = (FileUpload)Session["sessionFotografia"];
                //}
                //if (Session["sessionFotografia"] == null && fuFotografia.HasFiles)
                //{
                //    Session["sessionFotografia"] = fuFotografia;
                //}

                if (Session["sessionArchivoEstudio"] != null && !fuCertificadoEstudio.HasFiles)
                {
                    fuCertificadoEstudio = (FileUpload)Session["sessionArchivoEstudio"];
                }
                if (Session["sessionArchivoEstudio"] == null && fuCertificadoEstudio.HasFiles)
                {
                    Session["sessionArchivoEstudio"] = fuCertificadoEstudio;
                }

                if (Session["sessionArchivoCurso"] != null && !fuCertificadoCurso.HasFiles)
                {
                    fuCertificadoCurso = (FileUpload)Session["sessionArchivoCurso"];
                }
                if (Session["sessionArchivoCurso"] == null && fuCertificadoCurso.HasFiles)
                {
                    Session["sessionArchivoCurso"] = fuCertificadoCurso;
                }

                if (Session["sessionArchivoCertificado"] != null && !fuCertificadoCertificado.HasFiles)
                {
                    fuCertificadoCertificado = (FileUpload)Session["sessionArchivoCertificado"];
                }
                if (Session["sessionArchivoCertificado"] == null && fuCertificadoCertificado.HasFiles)
                {
                    Session["sessionArchivoCertificado"] = fuCertificadoCertificado;
                }

                if (Session["sessionArchivoPensionOEmbargo"] != null && !fuCertificadoPensionOEmbargo.HasFiles)
                {
                    fuCertificadoPensionOEmbargo = (FileUpload)Session["sessionArchivoPensionOEmbargo"];
                }
                if (Session["sessionArchivoPensionOEmbargo"] == null && fuCertificadoPensionOEmbargo.HasFiles)
                {
                    Session["sessionArchivoPensionOEmbargo"] = fuCertificadoPensionOEmbargo;
                }

                if (Session["sessionArchivoAccionesPersonal"] != null && !fuCertificadoAccionesPersonal.HasFiles)
                {
                    fuCertificadoAccionesPersonal = (FileUpload)Session["sessionArchivoAccionesPersonal"];
                }
                if (Session["sessionArchivoAccionesPersonal"] == null && fuCertificadoAccionesPersonal.HasFiles)
                {
                    Session["sessionArchivoAccionesPersonal"] = fuCertificadoAccionesPersonal;
                }

                if (Session["sessionCurriculum"] != null && !fuCurriculumVerEditar.HasFiles)
                {
                    fuCurriculumVerEditar = (FileUpload)Session["sessionCurriculum"];
                }
                if (Session["sessionCurriculum"] == null && fuCurriculumVerEditar.HasFiles)
                {
                    Session["sessionCurriculum"] = fuCurriculumVerEditar;
                }

                if (Session["sessionArchivoComprobantes"] != null && !fuCertificadoComprobantes.HasFiles)
                {
                    fuCertificadoComprobantes = (FileUpload)Session["sessionArchivoComprobantes"];
                }
                if (Session["sessionArchivoComprobantes"] == null && fuCertificadoComprobantes.HasFiles)
                {
                    Session["sessionArchivoComprobantes"] = fuCertificadoComprobantes;
                }

                if (Session["sessionArchivoAntecedentes"] != null && !fuCertificadoAntecedentes.HasFiles)
                {
                    fuCertificadoAntecedentes = (FileUpload)Session["sessionArchivoAntecedentes"];
                }
                if (Session["sessionArchivoAntecedentes"] == null && fuCertificadoAntecedentes.HasFiles)
                {
                    Session["sessionArchivoAntecedentes"] = fuCertificadoAntecedentes;
                }

                if (Session["sessionArchivoSuspensionPermiso"] != null && !fuArchivoSuspensionPermisoAgregar.HasFiles)
                {
                    fuArchivoSuspensionPermisoAgregar = (FileUpload)Session["sessionArchivoSuspensionPermiso"];
                }
                if (Session["sessionArchivoSuspensionPermiso"] == null && fuArchivoSuspensionPermisoAgregar.HasFiles)
                {
                    Session["sessionArchivoSuspensionPermiso"] = fuArchivoSuspensionPermisoAgregar;
                }
            }
        }
        #endregion

        #region logica

        #region Validar datos ingresados
        /// <summary>
        /// Adrián Serrano
        /// 10/10/2021
        /// Efecto: Valida que los campos en el formulario de Funcionarios sean correctamente llenos
        /// Devuelve: true si todos los campos están correctamente llenos, false si existe algún campo sin llenar
        //public Boolean ValidarCamposObligatorios()
        //{
        //    Boolean validados = true;

        //    string identificacion = txtIdentificacion.Text;
        //    string nombre = txtNombre.Text;
        //    string primerApellido = txtPrimerApellido.Text;
        //    string segundoApellido = txtSegundoApellido.Text;

        //    if (identificacion.Trim() == "")
        //    {
        //        txtIdentificacion.CssClass = "form-control alert-danger";
        //        divIdentificacionIncorrecto.Style.Add("display", "block");

        //        validados = false;
        //    }

        //    if (nombre.Trim() == "")
        //    {
        //        txtNombre.CssClass = "form-control alert-danger";
        //        divNombreIncorrecto.Style.Add("display", "block");

        //        validados = false;
        //    }

        //    if (primerApellido.Trim() == "")
        //    {
        //        txtPrimerApellido.CssClass = "form-control alert-danger";
        //        divPrimerApellidoIncorrecto.Style.Add("display", "block");

        //        validados = false;
        //    }

        //    if (segundoApellido.Trim() == "")
        //    {
        //        txtSegundoApellido.CssClass = "form-control alert-danger";
        //        divSegundoApellidoIncorrecto.Style.Add("display", "block");

        //        validados = false;
        //    }

        //    return validados;
        //}
        public Boolean ValidarCamposNoObligatorios()
        {
            Boolean validados = true;

            string correo = txtCorreo.Text;

            //try
            //{
            //    DateTime fechaIngreso = Convert.ToDateTime(txtFechaIngreso.Text);
            //}
            //catch (Exception e)
            //{
            //    txtFechaIngreso.CssClass = "form-control alert-danger";
            //    validados = false;
            //}

            if (correo.Trim() != "")
            {
                Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
                //"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                Match match = regex.Match(txtCorreo.Text);

                if (!match.Success)
                {
                    txtCorreo.CssClass = "form-control alert-danger";
                    divNoEsCorreo.Style.Add("display", "block");
                    validados = false;
                }
            }

            if (correo.Trim().Length > 200)
            {
                txtCorreo.CssClass = "form-control alert-danger";
                divCorreoIncorrectoTamano.Style.Add("display", "block");
                validados = false;
            }

            return validados;
        }
        public Boolean ValidarCamposEstudio()
        {
            Boolean validados = true;

            string nombreEstudio = txtNombreEstudio.Text;

            if (nombreEstudio.Trim() == "")
            {
                txtNombreEstudio.CssClass = "form-control alert-danger";
                divNombreEstudioIncorrecto.Style.Add("display", "block");

                validados = false;
            }

            if (nombreEstudio.Trim().Length > 100)
            {
                txtNombreEstudio.CssClass = "form-control alert-danger";
                divNombreEstudioIncorrectoTamano.Style.Add("display", "block");

                validados = false;
            }

            try
            {
                DateTime fechaInicio = Convert.ToDateTime(txtFechaInicioEstudio.Text);

                try
                {
                    DateTime fechaFinalizacion = Convert.ToDateTime(txtFechaFinalizacionEstudio.Text);

                    int comparacionFechas = 0;
                    comparacionFechas = DateTime.Compare(fechaInicio, fechaFinalizacion);

                    if (comparacionFechas > 0)
                    {
                        txtFechaFinalizacionEstudio.CssClass = "form-control alert-danger";
                        divFechaFinalizacionEstudioIncorrecto.Style.Add("display", "block");

                        validados = false;
                    }
                }
                catch (Exception ex) { }
            }
            catch (Exception e)
            {
                txtFechaInicioEstudio.CssClass = "form-control alert-danger";
                divFechaInicioEstudioIncorrecto.Style.Add("display", "block");

                validados = false;
            }

            return validados;
        }
        public Boolean ValidarCamposCurso()
        {
            Boolean validados = true;

            string nombreCurso = txtNombreCurso.Text;

            if (nombreCurso.Trim() == "")
            {
                txtNombreCurso.CssClass = "form-control alert-danger";
                divNombreCursoIncorrecto.Style.Add("display", "block");

                validados = false;
            }

            if (nombreCurso.Trim().Length > 100)
            {
                txtNombreCurso.CssClass = "form-control alert-danger";
                divNombreCursoIncorrectoTamano.Style.Add("display", "block");

                validados = false;
            }

            try
            {
                DateTime fechaInicio = Convert.ToDateTime(txtFechaInicioCurso.Text);

                try
                {
                    DateTime fechaFinalizacion = Convert.ToDateTime(txtFechaFinalizacionCurso.Text);

                    int comparacionFechas = 0;
                    comparacionFechas = DateTime.Compare(fechaInicio, fechaFinalizacion);

                    if (comparacionFechas > 0)
                    {
                        txtFechaFinalizacionCurso.CssClass = "form-control alert-danger";
                        divFechaFinalizacionCursoIncorrecto.Style.Add("display", "block");

                        validados = false;
                    }
                }
                catch (Exception ex) { }
            }
            catch (Exception e)
            {
                txtFechaInicioCurso.CssClass = "form-control alert-danger";
                divFechaInicioCursoIncorrecto.Style.Add("display", "block");

                validados = false;
            }

            return validados;
        }
        public Boolean ValidarCamposCertificado()
        {
            Boolean validados = true;

            string nombreCertificado = txtNombreCertificado.Text;

            if (nombreCertificado.Trim() == "")
            {
                txtNombreCertificado.CssClass = "form-control alert-danger";
                divNombreCertificadoIncorrecto.Style.Add("display", "block");

                validados = false;
            }

            if (nombreCertificado.Trim().Length > 100)
            {
                txtNombreCertificado.CssClass = "form-control alert-danger";
                divNombreCertificadoIncorrectoTamano.Style.Add("display", "block");

                validados = false;
            }

            try
            {
                DateTime fechaInicio = Convert.ToDateTime(txtFechaInicioCertificado.Text);

                try
                {
                    DateTime fechaFinalizacion = Convert.ToDateTime(txtFechaFinalizacionCertificado.Text);

                    int comparacionFechas = 0;
                    comparacionFechas = DateTime.Compare(fechaInicio, fechaFinalizacion);

                    if (comparacionFechas > 0)
                    {
                        txtFechaFinalizacionCertificado.CssClass = "form-control alert-danger";
                        divFechaFinalizacionCertificadoIncorrecto.Style.Add("display", "block");

                        validados = false;
                    }
                }
                catch (Exception ex) { }
            }
            catch (Exception e)
            {
                txtFechaInicioCertificado.CssClass = "form-control alert-danger";
                divFechaInicioCertificadoIncorrecto.Style.Add("display", "block");

                validados = false;
            }

            return validados;
        }
        public Boolean ValidarCamposExperienciaLaboral()
        {
            Boolean validados = true;

            string nombreEmpresa = txtNombreEmpresaExperienciaLaboralVerEditar.Text;

            if (nombreEmpresa.Trim() == "")
            {
                txtNombreEmpresaExperienciaLaboralVerEditar.CssClass = "form-control alert-danger";
                divNombreEmpresaExperienciaLaboralIncorrectoVerEditarTamano.Style.Add("display", "block");

                validados = false;
            }

            if (nombreEmpresa.Trim().Length > 100)
            {
                txtNombreEmpresaExperienciaLaboralVerEditar.CssClass = "form-control alert-danger";
                divNombreEmpresaExperienciaLaboralIncorrectoVerEditarTamano.Style.Add("display", "block");

                validados = false;
            }

            return validados;
        }
        public Boolean ValidarCamposHabilidadBlanda()
        {
            Boolean validados = true;

            string descripcionHabilidadBlanda = txtDescripcionHabilidadBlanda.Text;

            if (descripcionHabilidadBlanda.Trim() == "")
            {
                txtDescripcionHabilidadBlanda.CssClass = "form-control alert-danger";
                divDescripcionHabilidadBlandaIncorrecto.Style.Add("display", "block");

                validados = false;
            }

            if (descripcionHabilidadBlanda.Trim().Length > 250)
            {
                txtDescripcionHabilidadBlanda.CssClass = "form-control alert-danger";
                divDescripcionHabilidadBlandaIncorrectoTamano.Style.Add("display", "block");

                validados = false;
            }

            return validados;
        }
        public Boolean ValidarCamposInteresPersonal()
        {
            Boolean validados = true;

            string descripcionInteresPersonal = txtDescripcionInteresPersonal.Text;

            if (descripcionInteresPersonal.Trim() == "")
            {
                txtDescripcionInteresPersonal.CssClass = "form-control alert-danger";
                divDescripcionInteresPersonalIncorrecto.Style.Add("display", "block");

                validados = false;
            }

            if (descripcionInteresPersonal.Trim().Length > 250)
            {
                txtDescripcionInteresPersonal.CssClass = "form-control alert-danger";
                divDescripcionInteresPersonalIncorrectoTamano.Style.Add("display", "block");

                validados = false;
            }

            return validados;
        }
        public Boolean ValidarCamposPensionOEmbargo()
        {
            Boolean validados = true;

            return validados;
        }
        public Boolean ValidarCamposAccionesPersonal()
        {
            Boolean validados = true;

            //string nombreAccionesPersonal = txtNombreAccionesPersonal.Text;

            //if (nombreAccionesPersonal.Trim() == "")
            //{
            //    txtNombreAccionesPersonal.CssClass = "form-control alert-danger";
            //    divNombreAccionesPersonalIncorrecto.Style.Add("display", "block");

            //    validados = false;
            //}

            //try
            //{
            //    DateTime periodo = Convert.ToDateTime(txtPeriodoAccionesPersonal.Text);
            //}
            //catch (Exception e)
            //{
            //    txtPeriodoAccionesPersonal.CssClass = "form-control alert-danger";
            //    divPeriodoAccionesPersonalIncorrecto.Style.Add("display", "block");

            //    validados = false;
            //}

            return validados;
        }
        public Boolean ValidarCamposComprobantes()
        {
            Boolean validados = true;

            try
            {
                DateTime fecha = Convert.ToDateTime(txtFechaComprobantes.Text);
            }
            catch (Exception e)
            {
                txtFechaComprobantes.CssClass = "form-control alert-danger";
                divFechaComprobantesIncorrecto.Style.Add("display", "block");

                validados = false;
            }

            return validados;
        }
        public Boolean ValidarCamposAntecedentes()
        {
            Boolean validados = true;

            string nombreAntecedentes = txtNombreAntecedentes.Text;

            if (nombreAntecedentes.Trim() == "")
            {
                txtNombreAntecedentes.CssClass = "form-control alert-danger";
                divNombreAntecedentesIncorrecto.Style.Add("display", "block");

                validados = false;
            }

            if (nombreAntecedentes.Trim().Length > 100)
            {
                txtNombreAntecedentes.CssClass = "form-control alert-danger";
                divNombreAntecedentesIncorrectoTamano.Style.Add("display", "block");

                validados = false;
            }

            return validados;
        }
        public Boolean ValidarCamposSuspensionPermiso()
        {
            Boolean validados = true;

            string descripcionSuspensionPermiso = txtDescripcionSuspensionPermiso.Text;

            if (descripcionSuspensionPermiso.Trim() == "")
            {
                txtDescripcionSuspensionPermiso.CssClass = "form-control alert-danger";
                divDescripcionSuspensionPermisoIncorrecto.Style.Add("display", "block");

                validados = false;
            }

            if (descripcionSuspensionPermiso.Trim().Length > 250)
            {
                txtDescripcionSuspensionPermiso.CssClass = "form-control alert-danger";
                divDescripcionSuspensionPermisoIncorrectoTamano.Style.Add("display", "block");

                validados = false;
            }

            try
            {
                DateTime fechaSalida = Convert.ToDateTime(txtFechaSalidaSuspensionPermiso.Text);

                try
                {
                    DateTime fechaRegreso = Convert.ToDateTime(txtFechaRegresoSuspensionPermiso.Text);

                    int comparacionFechas = 0;
                    comparacionFechas = DateTime.Compare(fechaSalida, fechaRegreso);

                    if (comparacionFechas > 0)
                    {
                        txtFechaRegresoSuspensionPermiso.CssClass = "form-control alert-danger";
                        divFechaRegresoSuspensionPermisoIncorrecto.Style.Add("display", "block");

                        validados = false;
                    }
                }
                catch (Exception ex) { }
            }
            catch (Exception e)
            {
                txtFechaSalidaSuspensionPermiso.CssClass = "form-control alert-danger";
                divFechaSalidaSuspensionPermisoIncorrecto.Style.Add("display", "block");

                validados = false;
            }

            return validados;
        }
        public Boolean ValidarCamposEstudioAgregar()
        {
            Boolean validados = true;

            string nombreEstudio = txtNombreEstudioAgregar.Text;

            if (nombreEstudio.Trim() == "")
            {
                txtNombreEstudioAgregar.CssClass = "form-control alert-danger";
                divNombreEstudioIncorrectoAgregar.Style.Add("display", "block");

                validados = false;
            }

            if (nombreEstudio.Trim().Length > 100)
            {
                txtNombreEstudioAgregar.CssClass = "form-control alert-danger";
                divNombreEstudioIncorrectoAgregarTamano.Style.Add("display", "block");

                validados = false;
            }

            try
            {
                DateTime fechaInicio = Convert.ToDateTime(txtFechaInicioEstudioAgregar.Text);

                try
                {
                    DateTime fechaFinalizacion = Convert.ToDateTime(txtFechaFinalizacionEstudioAgregar.Text);

                    int comparacionFechas = 0;
                    comparacionFechas = DateTime.Compare(fechaInicio, fechaFinalizacion);

                    if (comparacionFechas > 0)
                    {
                        txtFechaFinalizacionEstudioAgregar.CssClass = "form-control alert-danger";
                        divFechaFinalizacionEstudioIncorrectoAgregar.Style.Add("display", "block");

                        validados = false;
                    }
                }
                catch (Exception ex) { }
            }
            catch (Exception e)
            {
                txtFechaInicioEstudioAgregar.CssClass = "form-control alert-danger";
                divFechaInicioEstudioIncorrectoAgregar.Style.Add("display", "block");

                validados = false;
            }

            return validados;
        }
        public Boolean ValidarCamposCursoAgregar()
        {
            Boolean validados = true;

            string nombreCurso = txtNombreCursoAgregar.Text;

            if (nombreCurso.Trim() == "")
            {
                txtNombreCursoAgregar.CssClass = "form-control alert-danger";
                divNombreCursoIncorrectoAgregar.Style.Add("display", "block");

                validados = false;
            }

            if (nombreCurso.Trim().Length > 100)
            {
                txtNombreCursoAgregar.CssClass = "form-control alert-danger";
                divNombreCursoIncorrectoAgregarTamano.Style.Add("display", "block");

                validados = false;
            }

            try
            {
                DateTime fechaInicio = Convert.ToDateTime(txtFechaInicioCursoAgregar.Text);

                try
                {
                    DateTime fechaFinalizacion = Convert.ToDateTime(txtFechaFinalizacionCursoAgregar.Text);

                    int comparacionFechas = 0;
                    comparacionFechas = DateTime.Compare(fechaInicio, fechaFinalizacion);

                    if (comparacionFechas > 0)
                    {
                        txtFechaFinalizacionCursoAgregar.CssClass = "form-control alert-danger";
                        divFechaFinalizacionCursoIncorrectoAgregar.Style.Add("display", "block");

                        validados = false;
                    }
                }
                catch (Exception ex) { }
            }
            catch (Exception e)
            {
                txtFechaInicioCursoAgregar.CssClass = "form-control alert-danger";
                divFechaInicioCursoIncorrectoAgregar.Style.Add("display", "block");

                validados = false;
            }

            return validados;
        }
        public Boolean ValidarCamposCertificadoAgregar()
        {
            Boolean validados = true;

            string nombreCertificado = txtNombreCertificadoAgregar.Text;

            if (nombreCertificado.Trim() == "")
            {
                txtNombreCertificadoAgregar.CssClass = "form-control alert-danger";
                divNombreCertificadoIncorrectoAgregar.Style.Add("display", "block");

                validados = false;
            }

            if (nombreCertificado.Trim().Length > 100)
            {
                txtNombreCertificadoAgregar.CssClass = "form-control alert-danger";
                divNombreCertificadoIncorrectoAgregarTamano.Style.Add("display", "block");

                validados = false;
            }

            try
            {
                DateTime fechaInicio = Convert.ToDateTime(txtFechaInicioCertificadoAgregar.Text);

                try
                {
                    DateTime fechaFinalizacion = Convert.ToDateTime(txtFechaFinalizacionCertificadoAgregar.Text);

                    int comparacionFechas = 0;
                    comparacionFechas = DateTime.Compare(fechaInicio, fechaFinalizacion);

                    if (comparacionFechas > 0)
                    {
                        txtFechaFinalizacionCertificadoAgregar.CssClass = "form-control alert-danger";
                        divFechaFinalizacionCertificadoIncorrectoAgregar.Style.Add("display", "block");

                        validados = false;
                    }
                }
                catch (Exception ex) { }
            }
            catch (Exception e)
            {
                txtFechaInicioCertificadoAgregar.CssClass = "form-control alert-danger";
                divFechaInicioCertificadoIncorrectoAgregar.Style.Add("display", "block");

                validados = false;
            }

            return validados;
        }
        public Boolean ValidarCamposExperienciaLaboralAgregar()
        {
            Boolean validados = true;

            string nombreEmpresa = txtNombreEmpresaExperienciaLaboral.Text;

            if (nombreEmpresa.Trim() == "")
            {
                txtNombreEmpresaExperienciaLaboral.CssClass = "form-control alert-danger";
                divNombreEmpresaExperienciaLaboralIncorrecto.Style.Add("display", "block");

                validados = false;
            }

            if (nombreEmpresa.Trim().Length > 100)
            {
                txtNombreEmpresaExperienciaLaboral.CssClass = "form-control alert-danger";
                divNombreEmpresaExperienciaLaboralIncorrectoTamano.Style.Add("display", "block");

                validados = false;
            }

            return validados;
        }
        public Boolean ValidarCamposHabilidadBlandaAgregar()
        {
            Boolean validados = true;

            string descripcionHabilidadBlanda = txtDescripcionHabilidadBlandaAgregar.Text;

            if (descripcionHabilidadBlanda.Trim() == "")
            {
                txtDescripcionHabilidadBlandaAgregar.CssClass = "form-control alert-danger";
                divDescripcionHabilidadBlandaIncorrectoAgregar.Style.Add("display", "block");

                validados = false;
            }

            if (descripcionHabilidadBlanda.Trim().Length > 250)
            {
                txtDescripcionHabilidadBlandaAgregar.CssClass = "form-control alert-danger";
                divDescripcionHabilidadBlandaIncorrectoAgregarTamano.Style.Add("display", "block");

                validados = false;
            }

            return validados;
        }
        public Boolean ValidarCamposInteresPersonalAgregar()
        {
            Boolean validados = true;

            string descripcionInteresPersonal = txtDescripcionInteresPersonalAgregar.Text;

            if (descripcionInteresPersonal.Trim() == "")
            {
                txtDescripcionInteresPersonalAgregar.CssClass = "form-control alert-danger";
                divDescripcionInteresPersonalIncorrectoAgregar.Style.Add("display", "block");

                validados = false;
            }

            if (descripcionInteresPersonal.Trim().Length > 250)
            {
                txtDescripcionInteresPersonalAgregar.CssClass = "form-control alert-danger";
                divDescripcionInteresPersonalIncorrectoAgregarTamano.Style.Add("display", "block");

                validados = false;
            }

            return validados;
        }
        public Boolean ValidarCamposPensionOEmbargoAgregar()
        {
            Boolean validados = true;

            return validados;
        }
        //public Boolean ValidarCamposAccionesPersonalAgregar()
        //{
        //    Boolean validados = true;

            //string nombreAccionesPersonal = txtNombreAccionesPersonalAgregar.Text;

            //if (nombreAccionesPersonal.Trim() == "")
            //{
            //    txtNombreAccionesPersonalAgregar.CssClass = "form-control alert-danger";
            //    divNombreAccionesPersonalIncorrectoAgregar.Style.Add("display", "block");

            //    validados = false;
            //}

            //try
            //{
            //    DateTime periodo = Convert.ToDateTime(txtPeriodoAccionesPersonalAgregar.Text);
            //}
            //catch (Exception e)
            //{
            //    txtPeriodoAccionesPersonalAgregar.CssClass = "form-control alert-danger";
            //    divPeriodoAccionesPersonalIncorrectoAgregar.Style.Add("display", "block");

            //    validados = false;
            //}

            //return validados;
        //}
        public Boolean ValidarCamposComprobantesAgregar()
        {
            Boolean validados = true;

            try
            {
                DateTime fecha = Convert.ToDateTime(txtFechaComprobantesAgregar.Text);
            }
            catch (Exception e)
            {
                txtFechaComprobantesAgregar.CssClass = "form-control alert-danger";
                divFechaComprobantesIncorrectoAgregar.Style.Add("display", "block");

                validados = false;
            }

            return validados;
        }
        public Boolean ValidarCamposAntecedentesAgregar()
        {
            Boolean validados = true;

            string nombreAntecedentes = txtNombreAntecedentesAgregar.Text;

            if (nombreAntecedentes.Trim() == "")
            {
                txtNombreAntecedentesAgregar.CssClass = "form-control alert-danger";
                divNombreAntecedentesIncorrectoAgregar.Style.Add("display", "block");

                validados = false;
            }

            if (nombreAntecedentes.Trim().Length > 100)
            {
                txtNombreAntecedentesAgregar.CssClass = "form-control alert-danger";
                divNombreAntecedentesIncorrectoAgregarTamano.Style.Add("display", "block");

                validados = false;
            }

            return validados;
        }
        public Boolean ValidarCamposSuspensionPermisoAgregar()
        {
            Boolean validados = true;

            string descripcionSuspensionPermiso = txtDescripcionSuspensionPermisoAgregar.Text;

            if (descripcionSuspensionPermiso.Trim() == "")
            {
                txtDescripcionSuspensionPermisoAgregar.CssClass = "form-control alert-danger";
                divDescripcionSuspensionPermisoIncorrectoAgregar.Style.Add("display", "block");

                validados = false;
            }

            if (descripcionSuspensionPermiso.Trim().Length > 250)
            {
                txtDescripcionSuspensionPermisoAgregar.CssClass = "form-control alert-danger";
                divDescripcionSuspensionPermisoIncorrectoAgregarTamano.Style.Add("display", "block");

                validados = false;
            }

            try
            {
                DateTime fechaSalida = Convert.ToDateTime(txtFechaSalidaSuspensionPermisoAgregar.Text);

                try
                {
                    DateTime fechaRegreso = Convert.ToDateTime(txtFechaRegresoSuspensionPermisoAgregar.Text);

                    int comparacionFechas = 0;
                    comparacionFechas = DateTime.Compare(fechaSalida, fechaRegreso);

                    if (comparacionFechas > 0)
                    {
                        txtFechaRegresoSuspensionPermisoAgregar.CssClass = "form-control alert-danger";
                        divFechaRegresoSuspensionPermisoIncorrectoAgregar.Style.Add("display", "block");

                        validados = false;
                    }
                }
                catch (Exception ex) { }
            }
            catch (Exception e)
            {
                txtFechaSalidaSuspensionPermisoAgregar.CssClass = "form-control alert-danger";
                divFechaSalidaSuspensionPermisoIncorrectoAgregar.Style.Add("display", "block");

                validados = false;
            }

            return validados;
        }
        #endregion

        #region Llenar DropDownLists
        /// <summary>
        /// Adrián Serrano
        /// 05/10/2021
        /// Efecto: Esta sección contiene las funciones para llenar los DropDownList
        /// modales.
        /// </summary>

        /// Efecto: Llena el DropDownList para los tipos de estudios formales
        private void LlenarEstudiosDDL()
        {
            List<TipoEstudio> tiposEstudio = this.tipoEstudioDatos.ObtenerTodosEstudio();

            if (tiposEstudio.Count > 0)
            {
                foreach (TipoEstudio tipoEstudio in tiposEstudio)
                {
                    ListItem item = new ListItem(tipoEstudio.Nombre, tipoEstudio.IdTipoEstudio.ToString());
                    TipoEstudioDDL.Items.Add(item);
                }
            }
        }

        /// Efecto: Llena el DropDownList para los tipos de sangre
        //private void LlenarSangreDDL()
        //{
        //    Dictionary<string, string> tiposSangre = new Dictionary<string, string>();
        //    tiposSangre.Add("1", "O+");
        //    tiposSangre.Add("2", "O-");
        //    tiposSangre.Add("3", "A+");
        //    tiposSangre.Add("4", "A-");
        //    tiposSangre.Add("5", "B+");
        //    tiposSangre.Add("6", "B-");
        //    tiposSangre.Add("7", "AB+");
        //    tiposSangre.Add("8", "AB-");

        //    foreach (KeyValuePair<string, string> cv in tiposSangre)
        //    {
        //        ListItem item = new ListItem(cv.Value, cv.Key);
        //        SangreDDL.Items.Add(item);
        //    }
        //}
        /// Efecto: Llena el DropDownList para los tipos de licencia de conducir
        private void LlenarLicenciasDDL()
        {
            Dictionary<string, string> tiposLicencia = new Dictionary<string, string>();
            tiposLicencia.Add("1", "No cuenta con licencia");
            tiposLicencia.Add("2", "A1 (Motocicletas)");
            tiposLicencia.Add("3", "A2 (Motocicletas)");
            tiposLicencia.Add("4", "A3 (Motocicletas)");
            tiposLicencia.Add("5", "B1 (Automóviles y camiones)");
            tiposLicencia.Add("6", "B2 (Automóviles y camiones)");
            tiposLicencia.Add("7", "B3 (Automóviles y camiones)");
            tiposLicencia.Add("8", "B4 (Automóviles y camiones)");
            tiposLicencia.Add("9", "C1 (Taxis)");
            tiposLicencia.Add("10", "C2 (Autobuses)");
            tiposLicencia.Add("11", "D1 (Tractores y maquinaria)");
            tiposLicencia.Add("12", "D2 (Tractores y maquinaria)");
            tiposLicencia.Add("13", "D3 (Tractores y maquinaria)");
            tiposLicencia.Add("14", "E1 (Universales)");
            tiposLicencia.Add("15", "E2 (Universales)");

            foreach (KeyValuePair<string, string> cv in tiposLicencia)
            {
                ListItem item = new ListItem(cv.Value, cv.Key);
                LicenciaDDL.Items.Add(item);
            }
        }

        /// Efecto: Llena el DropDownList para los estados
        private void LlenarEstadoDDL()
        {
            Dictionary<bool, string> tiposEstados = new Dictionary<bool, string>();
            tiposEstados.Add(true, "Habilitado");
            tiposEstados.Add(false, "Deshabilitado");

            foreach (KeyValuePair<bool, string> cv in tiposEstados)
            {
                ListItem item = new ListItem(cv.Value, Convert.ToString(cv.Key));
                EstadoDDL.Items.Add(item);
            }
        }
        /// Efecto: Llena el DropDownList para las categorías laborales
        private void LlenarCategoriaLaboralDDL()
        {
            Dictionary<int, string> categoriasLaborales = this.informacionLaboralDatos.ObtenerCategoriasLaborales();

            if (categoriasLaborales.Count > 0)
            {
                foreach (KeyValuePair<int, string> cv in categoriasLaborales)
                {
                    ListItem item = new ListItem(cv.Value, Convert.ToString(cv.Key));
                    CategoriaLaboralDDL.Items.Add(item);
                }
            }
        }
        /// Efecto: Llena el DropDownList para las secciones
        private void LlenarSeccionDDL()
        {
            Dictionary<int, string> secciones = this.informacionLaboralDatos.ObtenerSecciones();

            if (secciones.Count > 0)
            {
                foreach (KeyValuePair<int, string> cv in secciones)
                {
                    ListItem item = new ListItem(cv.Value, Convert.ToString(cv.Key));
                    SecciónDDL.Items.Add(item);
                }
            }
        }
        /// Efecto: Llena el DropDownList para la unidad, programa o laboratorio
        private void LlenarUnidadProgramaLaboratorioDDL()
        {
            List<UnidadProgramaLaboratorio> unidadProgramaLaboratorios = this.informacionLaboralDatos.ObtenerUnidadesProgramasLaboratorios();

            if (unidadProgramaLaboratorios.Count > 0)
            {
                foreach (UnidadProgramaLaboratorio upl in unidadProgramaLaboratorios)
                {
                    ListItem item = new ListItem(upl.Sigla + " - " + upl.Nombre, upl.IdUnidadProgramaLaboratorio.ToString());
                    UnidadProgramaLaboratorioDDL.Items.Add(item);
                }
            }
        }
        /// Efecto: Llena el DropDownList para los tipos de acciones de personal
        //private void LlenarAccionesPersonalDDL()
        //{
        //    List<TipoAccionPersonal> tiposAccionesPersonal = this.tipoAccionesPersonalDatos.ObtenerTodos();

        //    if (tiposAccionesPersonal.Count > 0)
        //    {
        //        foreach (TipoAccionPersonal tipoAccionPersonal in tiposAccionesPersonal)
        //        {
        //            ListItem item = new ListItem(tipoAccionPersonal.Nombre, tipoAccionPersonal.IdAccionPersonal.ToString());
        //            TipoAccionesPersonalDDL.Items.Add(item);
        //        }
        //    }
        //}
        /// Efecto: Llena el DropDownList para los tipos de comprobantes
        private void LlenarComprobantesDDL()
        {
            List<TipoComprobante> tiposComprobantes = this.tipoComprobantesDatos.ObtenerTodos();

            if (tiposComprobantes.Count > 0)
            {
                foreach (TipoComprobante tipoComprobantes in tiposComprobantes)
                {
                    ListItem item = new ListItem(tipoComprobantes.Nombre, tipoComprobantes.IdComprobante.ToString());
                    TipoComprobantesDDL.Items.Add(item);
                }
            }
        }
        /// Efecto: Llena el DropDownList para los tipos de antecedentes
        private void LlenarAntecedentesDDL()
        {
            List<TipoAntecedente> tiposAntecedentes = this.tipoAntecedentesDatos.ObtenerTodos();

            if (tiposAntecedentes.Count > 0)
            {
                foreach (TipoAntecedente tipoAntecedentes in tiposAntecedentes)
                {
                    ListItem item = new ListItem(tipoAntecedentes.Nombre, tipoAntecedentes.IdAntecedente.ToString());
                    TipoAntecedentesDDL.Items.Add(item);
                }
            }
        }
        /// Efecto: Llena el DropDownList para los tipos de suspensiones o permisos
        private void LlenarTipoSuspensionPermisoDDL()
        {
            Dictionary<string, string> tiposSuspensionPermiso = new Dictionary<string, string>();
            tiposSuspensionPermiso.Add("1", "Permiso");
            tiposSuspensionPermiso.Add("2", "Suspensión");

            foreach (KeyValuePair<string, string> cv in tiposSuspensionPermiso)
            {
                ListItem item = new ListItem(cv.Value, cv.Key);
                TipoSuspensionPermisoDDL.Items.Add(item);
            }
        }
        /// Efecto: Llena el DropDownList para los tipos de estudios formales
        private void LlenarEstudiosDDLAgregar()
        {
            List<TipoEstudio> tiposEstudio = this.tipoEstudioDatos.ObtenerTodosEstudio();

            if (tiposEstudio.Count > 0)
            {
                foreach (TipoEstudio tipoEstudio in tiposEstudio)
                {
                    ListItem item = new ListItem(tipoEstudio.Nombre, tipoEstudio.IdTipoEstudio.ToString());
                    TipoEstudioDDLAgregar.Items.Add(item);
                }
            }
        }

        /// Efecto: Llena el DropDownList para los tipos de acciones de personal
        //private void LlenarAccionesPersonalDDLAgregar()
        //{
        //    List<TipoAccionPersonal> tiposAccionesPersonal = this.tipoAccionesPersonalDatos.ObtenerTodos();

        //    if (tiposAccionesPersonal.Count > 0)
        //    {
        //        foreach (TipoAccionPersonal tipoAccionPersonal in tiposAccionesPersonal)
        //        {
        //            ListItem item = new ListItem(tipoAccionPersonal.Nombre, tipoAccionPersonal.IdAccionPersonal.ToString());
        //            TipoAccionesPersonalDDLAgregar.Items.Add(item);
        //        }
        //    }
        //}
        /// Efecto: Llena el DropDownList para los tipos de comprobantes
        private void LlenarComprobantesDDLAgregar()
        {
            List<TipoComprobante> tiposComprobantes = this.tipoComprobantesDatos.ObtenerTodos();

            if (tiposComprobantes.Count > 0)
            {
                foreach (TipoComprobante tipoComprobantes in tiposComprobantes)
                {
                    ListItem item = new ListItem(tipoComprobantes.Nombre, tipoComprobantes.IdComprobante.ToString());
                    TipoComprobantesDDLAgregar.Items.Add(item);
                }
            }
        }
        /// Efecto: Llena el DropDownList para los tipos de antecedentes
        private void LlenarAntecedentesDDLAgregar()
        {
            List<TipoAntecedente> tiposAntecedentes = this.tipoAntecedentesDatos.ObtenerTodos();

            if (tiposAntecedentes.Count > 0)
            {
                foreach (TipoAntecedente tipoAntecedentes in tiposAntecedentes)
                {
                    ListItem item = new ListItem(tipoAntecedentes.Nombre, tipoAntecedentes.IdAntecedente.ToString());
                    TipoAntecedentesDDLAgregar.Items.Add(item);
                }
            }
        }
        /// Efecto: Llena el DropDownList para los tipos de suspensiones o permisos
        private void LlenarTipoSuspensionPermisoDDLAgregar()
        {
            Dictionary<string, string> tiposSuspensionPermiso = new Dictionary<string, string>();
            tiposSuspensionPermiso.Add("1", "Permiso");
            tiposSuspensionPermiso.Add("2", "Suspensión");

            foreach (KeyValuePair<string, string> cv in tiposSuspensionPermiso)
            {
                ListItem item = new ListItem(cv.Value, cv.Key);
                TipoSuspensionPermisoDDLAgregar.Items.Add(item);
            }
        }
        #endregion

        #region Llenar las secciones
        /// <summary>
        /// Adrián Serrano
        /// 05/11/2021
        /// Efecto: Esta sección contiene las funciones para llenar las secciones
        /// </summary>

        /// Efecto: Llena la sección de Estudios
        private void LlenarEstudios()
        {
            string identificacion = txtIdentificacion.Text;
            List<Estudio> listaEstudios = estudioDatos.ObtenerPorId("estudio", identificacion);
            Session["listaEstudios"] = listaEstudios;

            rpEstudios.DataSource = listaEstudios;
            rpEstudios.DataBind();
        }

        /// Efecto: Llena la sección de Cursos
        private void LlenarCursos()
        {
            string identificacion = txtIdentificacion.Text;
            List<Estudio> listaCursos = this.estudioDatos.ObtenerPorId("curso", identificacion);
            Session["listaCursos"] = listaCursos;

            rpCursos.DataSource = listaCursos;
            rpCursos.DataBind();
        }

        /// Efecto: Llena la sección de Certificados
        private void LlenarCertificados()
        {
            string identificacion = txtIdentificacion.Text;
            List<Estudio> listaCertificados = this.estudioDatos.ObtenerPorId("certificado", identificacion);
            Session["listaCertificados"] = listaCertificados;

            rpCertificados.DataSource = listaCertificados;
            rpCertificados.DataBind();
        }

        /// Efecto: Llena la sección de Experiencia
        private void LlenarExperiencia()
        {
            string identificacion = txtIdentificacion.Text;
            List<Curriculum> listaExperienciasLaborales = this.curriculumDatos.ObtenerPorId(identificacion);
            Session["listaExperienciasLaborales"] = listaExperienciasLaborales;

            rpExperienciaLaboral.DataSource = listaExperienciasLaborales;
            rpExperienciaLaboral.DataBind();
        }

        /// Efecto: Llena la sección de HabilidadBlanda
        private void LlenarHabilidadBlanda()
        {
            string identificacion = txtIdentificacion.Text;
            List<HabilidadBlanda> listaHabilidadesBlandas = this.habilidadBlandaDatos.ObtenerPorId(identificacion);
            Session["listaHabilidadesBlandas"] = listaHabilidadesBlandas;

            rpHabilidadBlanda.DataSource = listaHabilidadesBlandas;
            rpHabilidadBlanda.DataBind();
        }

        /// Efecto: Llena la sección de InteresPersonal
        private void LlenarInteresPersonal()
        {
            string identificacion = txtIdentificacion.Text;
            List<InteresPersonal> listaInteresesPersonales = this.interesPersonalDatos.ObtenerPorId(identificacion);
            Session["listaInteresesPersonales"] = listaInteresesPersonales;

            rpInteresesPersonales.DataSource = listaInteresesPersonales;
            rpInteresesPersonales.DataBind();
        }

        /// Efecto: Llena la sección de Salarios
        //private void LlenarSalarios()
        //{
        //    string identificacion = txtIdentificacion.Text;
        //    List<Salario> listaSalarios = this.salarioDatos.ObtenerPorId(identificacion);
        //    Session["listaSalarios"] = listaSalarios;

        //    rpSalarios.DataSource = listaSalarios;
        //    rpSalarios.DataBind();
        //}

        /// Efecto: Llena la sección de PensionOEmbargo
        private void LlenarPensionOEmbargo()
        {
            string identificacion = txtIdentificacion.Text;
            List<PensionOEmbargo> listaPensionOEmbargos = this.pensionOEmbargoDatos.ObtenerPorId(identificacion);
            Session["listaPensionOEmbargos"] = listaPensionOEmbargos;

            rpPensionOEmbargos.DataSource = listaPensionOEmbargos;
            rpPensionOEmbargos.DataBind();
        }

        /// Efecto: Llena la sección de AccionPersonal
        private void LlenarAccionPersonal()
        {
            string identificacion = txtIdentificacion.Text;
            List<DocumentoTramite> listaAccionesPersonal = this.documentoTramiteDatos.ObtenerPorId(identificacion);
            Session["listaAccionesPersonal"] = listaAccionesPersonal;

            rpAccionesPersonal.DataSource = listaAccionesPersonal;
            rpAccionesPersonal.DataBind();
        }

        /// Efecto: Llena la sección de Comprobante
        private void LlenarComprobante()
        {
            string identificacion = txtIdentificacion.Text;
            List<Comprobante> listaComprobantes = this.comprobantesDatos.ObtenerPorId(identificacion);
            Session["listaComprobantes"] = listaComprobantes;

            rpComprobantes.DataSource = listaComprobantes;
            rpComprobantes.DataBind();
        }

        /// Efecto: Llena la sección de Antecedente
        private void LlenarAntecedente()
        {
            string identificacion = txtIdentificacion.Text;
            List<Antecedente> listaAntecedentes = this.antecedenteDatos.ObtenerPorId(identificacion);
            Session["listaAntecedentes"] = listaAntecedentes;

            rpAntecedentes.DataSource = listaAntecedentes;
            rpAntecedentes.DataBind();
        }

        /// Efecto: Llena la sección de SuspensionPermiso
        private void LlenarSuspensionPermiso()
        {
            string identificacion = txtIdentificacion.Text;
            List<SuspensionPermiso> listaSuspensionPermisos = this.suspensionPermisoDatos.ObtenerPorId(identificacion);
            Session["listaSuspensionPermisos"] = listaSuspensionPermisos;

            rpSuspensionPermiso.DataSource = listaSuspensionPermisos;
            rpSuspensionPermiso.DataBind();
        }

        #endregion

        #region Ver o Editar
        /// <summary>
        /// Adrián Serrano
        /// 13/11/2021
        /// Efecto: Metodos que llenan los modales para ver o editar segun sea el caso
        /// </summary>
        private void VerEditarEstudio(object sender, bool editar)
        {
            LlenarEstudios();

            TipoEstudioDDL.Enabled = editar;
            txtNombreEstudio.ReadOnly = !editar;
            txtObservacionesEstudio.ReadOnly = !editar;
            txtFechaInicioEstudio.ReadOnly = !editar;
            txtFechaFinalizacionEstudio.ReadOnly = !editar;
            fuCertificadoEstudio.Visible = editar;
            btnSeleccionarArchivoEstudio.Visible = editar;
            btnEditarEstudio.Visible = editar;

            Session["idEstudio"] = null;
            int idEstudio = Convert.ToInt32((((LinkButton)(sender)).CommandArgument).ToString());
            List<Estudio> listaEstudios = new List<Estudio>();

            if (Session["listaEstudios"] != null)
            {
                listaEstudios = (List<Estudio>)Session["listaEstudios"];
            }

            foreach (Estudio estudio in listaEstudios)
            {
                if (estudio.IdEstudio == idEstudio)
                {
                    Session["idEstudio"] = idEstudio;
                    TipoEstudioDDL.SelectedValue = estudio.TipoEstudio.IdTipoEstudio.ToString();
                    txtNombreEstudio.Text = estudio.Nombre;
                    txtObservacionesEstudio.Text = estudio.Observacion;
                    ckbEntregadoEstudio.Checked = estudio.Entregado;

                    if (estudio.NombreDocumento != "")
                    {
                        btnVerCertificadoEstudio.Text = estudio.NombreDocumento;
                        btnVerCertificadoEstudio.CommandArgument = estudio.RutaDocumento + "," + estudio.NombreDocumento;
                        Session["rutaDocumentoEstudio"] = estudio.RutaDocumento;
                        Session["nombreDocumentoEstudio"] = estudio.NombreDocumento;
                    }
                    else
                    {
                        btnVerCertificadoEstudio.Text = "-";
                        btnVerCertificadoEstudio.Enabled = false;
                    }

                    if (estudio.FechaInicio.Year == 1900)
                    {
                        txtFechaInicioEstudio.Text = "";
                    }
                    else
                    {
                        string anioInicio = estudio.FechaInicio.Year.ToString();
                        string mesInicio = estudio.FechaInicio.Month.ToString();
                        string diaInicio = estudio.FechaInicio.Day.ToString();
                        if (diaInicio.Length == 1) { diaInicio = "0" + diaInicio; }
                        if (mesInicio.Length == 1) { mesInicio = "0" + mesInicio; }
                        txtFechaInicioEstudio.Text = anioInicio + "-" + mesInicio + "-" + diaInicio;
                    }

                    if (estudio.FechaFinalizacion.Year == 1900)
                    {
                        txtFechaFinalizacionEstudio.Text = "";
                    }
                    else
                    {
                        string anioFinalizacion = estudio.FechaFinalizacion.Year.ToString();
                        string mesFinalizacion = estudio.FechaFinalizacion.Month.ToString();
                        string diaFinalizacion = estudio.FechaFinalizacion.Day.ToString();
                        if (diaFinalizacion.Length == 1) { diaFinalizacion = "0" + diaFinalizacion; }
                        if (mesFinalizacion.Length == 1) { mesFinalizacion = "0" + mesFinalizacion; }
                        txtFechaFinalizacionEstudio.Text = anioFinalizacion + "-" + mesFinalizacion + "-" + diaFinalizacion;
                    }

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "activar", "levantarModalVerEstudio();", true);
                    break;
                }
            }
        }
        private void VerEditarCurso(object sender, bool editar)
        {
            LlenarCursos();

            txtNombreCurso.ReadOnly = !editar;
            txtObservacionesCurso.ReadOnly = !editar;
            txtFechaInicioCurso.ReadOnly = !editar;
            txtFechaFinalizacionCurso.ReadOnly = !editar;
            fuCertificadoCurso.Visible = editar;
            btnSeleccionarArchivoCurso.Visible = editar;
            btnEditarCurso.Visible = editar;

            Session["idCurso"] = null;
            int idCurso = Convert.ToInt32((((LinkButton)(sender)).CommandArgument).ToString());
            List<Estudio> listaCursos = new List<Estudio>();

            if (Session["listaCursos"] != null)
            {
                listaCursos = (List<Estudio>)Session["listaCursos"];
            }

            foreach (Estudio curso in listaCursos)
            {
                if (curso.IdEstudio == idCurso)
                {
                    Session["idCurso"] = idCurso;
                    // Tipo de Estudio = 10 para curso
                    txtNombreCurso.Text = curso.Nombre;
                    txtObservacionesCurso.Text = curso.Observacion;
                    ckbEntregadoCurso.Checked = curso.Entregado;

                    if (curso.NombreDocumento != "")
                    {
                        btnVerCertificadoCurso.Text = curso.NombreDocumento;
                        btnVerCertificadoCurso.CommandArgument = curso.RutaDocumento + "," + curso.NombreDocumento;
                        Session["rutaDocumentoCurso"] = curso.RutaDocumento;
                        Session["nombreDocumentoCurso"] = curso.NombreDocumento;
                    }
                    else
                    {
                        btnVerCertificadoCurso.Text = "-";
                        btnVerCertificadoCurso.Enabled = false;
                    }

                    if (curso.FechaInicio.Year == 1900)
                    {
                        txtFechaInicioCurso.Text = "";
                    }
                    else
                    {
                        string anioInicio = curso.FechaInicio.Year.ToString();
                        string mesInicio = curso.FechaInicio.Month.ToString();
                        string diaInicio = curso.FechaInicio.Day.ToString();
                        if (diaInicio.Length == 1) { diaInicio = "0" + diaInicio; }
                        txtFechaInicioCurso.Text = anioInicio + "-" + mesInicio + "-" + diaInicio;
                    }

                    if (curso.FechaFinalizacion.Year == 1900)
                    {
                        txtFechaFinalizacionCurso.Text = "";
                    }
                    else
                    {
                        string anioFinalizacion = curso.FechaFinalizacion.Year.ToString();
                        string mesFinalizacion = curso.FechaFinalizacion.Month.ToString();
                        string diaFinalizacion = curso.FechaFinalizacion.Day.ToString();
                        if (diaFinalizacion.Length == 1) { diaFinalizacion = "0" + diaFinalizacion; }
                        txtFechaFinalizacionCurso.Text = anioFinalizacion + "-" + mesFinalizacion + "-" + diaFinalizacion;
                    }

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "activar", "levantarModalVerCurso();", true);
                    break;
                }
            }
        }
        private void VerEditarCertificado(object sender, bool editar)
        {
            LlenarCertificados();

            txtNombreCertificado.ReadOnly = !editar;
            txtObservacionesCertificado.ReadOnly = !editar;
            txtFechaInicioCertificado.ReadOnly = !editar;
            txtFechaFinalizacionCertificado.ReadOnly = !editar;
            fuCertificadoCertificado.Visible = editar;
            btnSeleccionarArchivoCertificado.Visible = editar;
            btnEditarCertificado.Visible = editar;

            Session["idCertificado"] = null;
            int idCertificado = Convert.ToInt32((((LinkButton)(sender)).CommandArgument).ToString());
            List<Estudio> listaCertificados = new List<Estudio>();

            if (Session["listaCertificados"] != null)
            {
                listaCertificados = (List<Estudio>)Session["listaCertificados"];
            }

            foreach (Estudio certificado in listaCertificados)
            {
                if (certificado.IdEstudio == idCertificado)
                {
                    Session["idCertificado"] = idCertificado;
                    // Tipo de Estudio = 11 para certificado
                    txtNombreCertificado.Text = certificado.Nombre;
                    txtObservacionesCertificado.Text = certificado.Observacion;
                    ckbEntregadoCertificado.Checked = certificado.Entregado;

                    if (certificado.NombreDocumento != "")
                    {
                        btnVerCertificadoCertificado.Text = certificado.NombreDocumento;
                        btnVerCertificadoCertificado.CommandArgument = certificado.RutaDocumento + "," + certificado.NombreDocumento;
                        Session["rutaDocumentoCertificado"] = certificado.RutaDocumento;
                        Session["nombreDocumentoCertificado"] = certificado.NombreDocumento;
                    }
                    else
                    {
                        btnVerCertificadoCertificado.Text = "-";
                        btnVerCertificadoCertificado.Enabled = false;
                    }

                    if (certificado.FechaInicio.Year == 1900)
                    {
                        txtFechaInicioCertificado.Text = "";
                    }
                    else
                    {
                        string anioInicio = certificado.FechaInicio.Year.ToString();
                        string mesInicio = certificado.FechaInicio.Month.ToString();
                        string diaInicio = certificado.FechaInicio.Day.ToString();
                        if (diaInicio.Length == 1) { diaInicio = "0" + diaInicio; }
                        txtFechaInicioCertificado.Text = anioInicio + "-" + mesInicio + "-" + diaInicio;
                    }

                    if (certificado.FechaFinalizacion.Year == 1900)
                    {
                        txtFechaFinalizacionCertificado.Text = "";
                    }
                    else
                    {
                        string anioFinalizacion = certificado.FechaFinalizacion.Year.ToString();
                        string mesFinalizacion = certificado.FechaFinalizacion.Month.ToString();
                        string diaFinalizacion = certificado.FechaFinalizacion.Day.ToString();
                        if (diaFinalizacion.Length == 1) { diaFinalizacion = "0" + diaFinalizacion; }
                        txtFechaFinalizacionCertificado.Text = anioFinalizacion + "-" + mesFinalizacion + "-" + diaFinalizacion;
                    }

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "activar", "levantarModalVerCertificado();", true);
                    break;
                }
            }
        }
        private void VerEditarExperienciaLaboral(object sender, bool editar)
        {
            LlenarExperiencia();

            txtNombreEmpresaExperienciaLaboralVerEditar.ReadOnly = !editar;
            txtDescripcionPuestoExperienciaLaboralVerEditar.ReadOnly = !editar;
            btnVerEditarExperienciaLaboralVerEditar.Visible = editar;
            fuCurriculumVerEditar.Visible = editar;

            Session["idExperienciaLaboral"] = null;
            int idExperienciaLaboral = Convert.ToInt32((((LinkButton)(sender)).CommandArgument).ToString());
            List<Curriculum> listaExperienciaLaborales = new List<Curriculum>();

            if (Session["listaExperienciasLaborales"] != null)
            {
                listaExperienciaLaborales = (List<Curriculum>)Session["listaExperienciasLaborales"];
            }

            foreach (Curriculum experienciaLaboral in listaExperienciaLaborales)
            {
                if (experienciaLaboral.idCurriculum == idExperienciaLaboral)
                {
                    Session["idExperienciaLaboral"] = idExperienciaLaboral;
                    txtNombreEmpresaExperienciaLaboralVerEditar.Text = experienciaLaboral.nombre;
                    txtDescripcionPuestoExperienciaLaboralVerEditar.Text = experienciaLaboral.descripcion;

                    if (experienciaLaboral.nombre != "")
                    {

                        btnVerCurriculum.Text = experienciaLaboral.nombre;
                        btnVerCurriculum.CommandArgument = experienciaLaboral.ruta + "," + experienciaLaboral.nombre;
                        Session["rutaCurriculum"] = experienciaLaboral.ruta;
                        Session["nombreCurriculum"] = experienciaLaboral.nombre;
                    }

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "activar", "levantarModalVerEditarExperienciaLaboral();", true);
                    break;
                }
            }
        }
        private void VerEditarHabilidadBlanda(object sender, bool editar)
        {
            LlenarHabilidadBlanda();

            txtDescripcionHabilidadBlanda.ReadOnly = !editar;
            btnEditarHabilidadBlanda.Visible = editar;

            Session["idHabilidadBlanda"] = null;
            int idHabilidadBlanda = Convert.ToInt32((((LinkButton)(sender)).CommandArgument).ToString());
            List<HabilidadBlanda> listaHabilidadesBlandas = new List<HabilidadBlanda>();

            if (Session["listaHabilidadesBlandas"] != null)
            {
                listaHabilidadesBlandas = (List<HabilidadBlanda>)Session["listaHabilidadesBlandas"];
            }

            foreach (HabilidadBlanda habilidadesBlanda in listaHabilidadesBlandas)
            {
                if (habilidadesBlanda.IdHabilidadBlanda == idHabilidadBlanda)
                {
                    Session["idHabilidadBlanda"] = idHabilidadBlanda;
                    txtDescripcionHabilidadBlanda.Text = habilidadesBlanda.Descripcion;

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "activar", "levantarModalVerHabilidadBlanda();", true);
                    break;
                }
            }
        }
        private void VerEditarInteresPersonal(object sender, bool editar)
        {
            LlenarInteresPersonal();

            txtDescripcionInteresPersonal.ReadOnly = !editar;
            btnEditarInteresPersonal.Visible = editar;

            Session["idInteresPersonal"] = null;
            int idInteresPersonal = Convert.ToInt32((((LinkButton)(sender)).CommandArgument).ToString());
            List<InteresPersonal> listaInteresPersonal = new List<InteresPersonal>();

            if (Session["listaInteresesPersonales"] != null)
            {
                listaInteresPersonal = (List<InteresPersonal>)Session["listaInteresesPersonales"];
            }

            foreach (InteresPersonal interesPersonal in listaInteresPersonal)
            {
                if (interesPersonal.IdInteresPersonal == idInteresPersonal)
                {
                    Session["idInteresPersonal"] = idInteresPersonal;
                    txtDescripcionInteresPersonal.Text = interesPersonal.Descripcion;

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "activar", "levantarModalVerInteresPersonal();", true);
                    break;
                }
            }
        }
        private void VerEditarPensionOEmbargo(object sender, bool editar)
        {
            LlenarPensionOEmbargo();

            txtDescripcionPensionOEmbargo.ReadOnly = !editar;
            fuCertificadoPensionOEmbargo.Visible = editar;
            btnSeleccionarArchivoPensionOEmbargo.Visible = editar;
            btnEditarPensionOEmbargo.Visible = editar;

            Session["idPensionOEmbargo"] = null;
            int idPensionOEmbargo = Convert.ToInt32((((LinkButton)(sender)).CommandArgument).ToString());
            List<PensionOEmbargo> listaPensionOEmbargos = new List<PensionOEmbargo>();

            if (Session["listaPensionOEmbargos"] != null)
            {
                listaPensionOEmbargos = (List<PensionOEmbargo>)Session["listaPensionOEmbargos"];
            }

            foreach (PensionOEmbargo pensionOEmbargo in listaPensionOEmbargos)
            {
                if (pensionOEmbargo.IdPensionOEmbargo == idPensionOEmbargo)
                {
                    Session["idPensionOEmbargo"] = idPensionOEmbargo;
                    txtDescripcionPensionOEmbargo.Text = pensionOEmbargo.Descripcion;

                    if (pensionOEmbargo.NombreDocumento != "")
                    {
                        btnVerCertificadoPensionOEmbargo.Text = pensionOEmbargo.NombreDocumento;
                        btnVerCertificadoPensionOEmbargo.CommandArgument = pensionOEmbargo.RutaDocumento + "," + pensionOEmbargo.NombreDocumento;
                        Session["rutaDocumentoPensionOEmbargo"] = pensionOEmbargo.RutaDocumento;
                        Session["nombreDocumentoPensionOEmbargo"] = pensionOEmbargo.NombreDocumento;
                    }
                    else
                    {
                        btnVerCertificadoPensionOEmbargo.Text = "-";
                        btnVerCertificadoPensionOEmbargo.Enabled = false;
                    }

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "activar", "levantarModalVerPensionOEmbargo();", true);
                    break;
                }
            }
        }
        private void VerEditarAccionPersonal(object sender, bool editar)
        {
            LlenarAccionPersonal();

            txtNombreAccionesPersonalVerEditar.ReadOnly = !editar;
            fuCertificadoAccionesPersonalVerEditar.Visible = editar;
            txtDescripcionAccionesPersonalVerEditar.ReadOnly = !editar;
            txtNumeroVerEditar.ReadOnly = !editar;

            btnVerEditarAccionesPersonalVerEditar.Visible = editar;

            Session["idAccionPersonal"] = null;
            int idAccionPersonal = Convert.ToInt32((((LinkButton)(sender)).CommandArgument).ToString());
            List<DocumentoTramite> accionesPersonales = new List<DocumentoTramite>();

            if (Session["listaAccionesPersonal"] != null)
            {
                accionesPersonales = (List<DocumentoTramite>)Session["listaAccionesPersonal"];
            }

            foreach (DocumentoTramite accionPersonal in accionesPersonales)
            {
                if (accionPersonal.idDocumentoTramite == idAccionPersonal)
                {
                    Session["idAccionPersonal"] = idAccionPersonal;

                    txtNombreAccionesPersonalVerEditar.Text = accionPersonal.nombreDocumento;
                    txtDescripcionAccionesPersonalVerEditar.Text = accionPersonal.descripcion;
                    txtNumeroVerEditar.Text = accionPersonal.numero;

                    if (accionPersonal.nombreDocumento != "")
                    {

                        btnVerCertificadoAccionesPersonal.Text = accionPersonal.nombreDocumento;
                        btnVerCertificadoAccionesPersonal.CommandArgument = accionPersonal.rutaDocumento + "," + accionPersonal.nombreDocumento;
                        Session["rutaDocumentoAccionPersonal"] = accionPersonal.rutaDocumento;
                        Session["nombreDocumentoAccionPersonal"] = accionPersonal.nombreDocumento;
                    }
                    else
                    {
                        //btnVerCertificadoAccionesPersonal.Text = "-";
                        //btnVerCertificadoAccionesPersonal.Enabled = false;
                    }

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "activar", "levantarModalVerEditarAccionesPersonal();", true);
                    break;
                }
            }
        }
        private void VerEditarComprobante(object sender, bool editar)
        {
            LlenarComprobante();

            TipoComprobantesDDL.Enabled = editar;
            txtFechaComprobantes.ReadOnly = !editar;
            fuCertificadoComprobantes.Visible = editar;
            btnSeleccionarArchivoComprobantes.Visible = editar;
            btnEditarComprobantes.Visible = editar;
            txtDescripcionComprobantes.ReadOnly = !editar;

            Session["idComprobante"] = null;
            int idComprobante = Convert.ToInt32((((LinkButton)(sender)).CommandArgument).ToString());
            List<Comprobante> listaComprobantes = new List<Comprobante>();

            if (Session["listaComprobantes"] != null)
            {
                listaComprobantes = (List<Comprobante>)Session["listaComprobantes"];
            }

            foreach (Comprobante comprobante in listaComprobantes)
            {
                if (comprobante.IdComprobante == idComprobante)
                {
                    Session["idComprobante"] = idComprobante;
                    TipoComprobantesDDL.SelectedValue = comprobante.TipoComprobante.IdComprobante.ToString();
                    txtDescripcionComprobantes.Text = comprobante.Descripcion;

                    if (comprobante.NombreDocumento != "")
                    {
                        btnVerCertificadoComprobantes.Text = comprobante.NombreDocumento;
                        btnVerCertificadoComprobantes.CommandArgument = comprobante.RutaDocumento + "," + comprobante.NombreDocumento;
                        Session["rutaDocumentoComprobante"] = comprobante.RutaDocumento;
                        Session["nombreDocumentoComprobante"] = comprobante.NombreDocumento;
                    }
                    else
                    {
                        btnVerCertificadoComprobantes.Text = "-";
                        btnVerCertificadoComprobantes.Enabled = false;
                    }

                    if (comprobante.Fecha.Year == 1900)
                    {
                        txtFechaComprobantes.Text = "";
                    }
                    else
                    {
                        string anioInicio = comprobante.Fecha.Year.ToString();
                        string mesInicio = comprobante.Fecha.Month.ToString();
                        string diaInicio = comprobante.Fecha.Day.ToString();
                        if (diaInicio.Length == 1) { diaInicio = "0" + diaInicio; }
                        txtFechaComprobantes.Text = anioInicio + "-" + mesInicio + "-" + diaInicio;
                    }

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "activar", "levantarModalVerComprobantes();", true);
                    break;
                }
            }
        }
        private void VerEditarAntecedente(object sender, bool editar)
        {
            LlenarAntecedente();

            TipoAntecedentesDDL.Enabled = editar;
            txtNombreAntecedentes.ReadOnly = !editar;
            txtFechaAntecedentes.ReadOnly = !editar;
            fuCertificadoAntecedentes.Visible = editar;
            btnSeleccionarArchivoAntecedentes.Visible = editar;
            btnEditarAntecedentes.Visible = editar;
            txtDescripcionAntecedentes.ReadOnly = !editar;

            Session["idAntecedente"] = null;
            int idAntecedente = Convert.ToInt32((((LinkButton)(sender)).CommandArgument).ToString());
            List<Antecedente> listaAntecedentes = new List<Antecedente>();

            if (Session["listaAntecedentes"] != null)
            {
                listaAntecedentes = (List<Antecedente>)Session["listaAntecedentes"];
            }

            foreach (Antecedente antecedente in listaAntecedentes)
            {
                if (antecedente.IdAntecedente == idAntecedente)
                {
                    Session["idAntecedente"] = idAntecedente;
                    TipoAntecedentesDDL.SelectedValue = antecedente.TipoAntecedente.IdAntecedente.ToString();
                    txtNombreAntecedentes.Text = antecedente.Nombre;
                    txtDescripcionAntecedentes.Text = antecedente.Descripcion;

                    if (antecedente.NombreDocumento != "")
                    {
                        btnVerCertificadoAntecedentes.Text = antecedente.NombreDocumento;
                        btnVerCertificadoAntecedentes.CommandArgument = antecedente.RutaDocumento + "," + antecedente.NombreDocumento;
                        Session["rutaDocumentoAntecedente"] = antecedente.RutaDocumento;
                        Session["nombreDocumentoAntecedente"] = antecedente.NombreDocumento;
                    }
                    else
                    {
                        btnVerCertificadoAntecedentes.Text = "-";
                        btnVerCertificadoAntecedentes.Enabled = false;
                    }
                    
                    if (antecedente.Fecha.Year == 1900)
                    {
                        txtFechaAntecedentes.Text = "";
                    }
                    else
                    {
                        string anioInicio = antecedente.Fecha.Year.ToString();
                        string mesInicio = antecedente.Fecha.Month.ToString();
                        string diaInicio = antecedente.Fecha.Day.ToString();
                        if (diaInicio.Length == 1) { diaInicio = "0" + diaInicio; }
                        txtFechaAntecedentes.Text = anioInicio + "-" + mesInicio + "-" + diaInicio;
                    }

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "activar", "levantarModalVerAntecedentes();", true);
                    break;
                }
            }
        }
        private void VerEditarSuspensionPermiso(object sender, bool editar)
        {
            LlenarSuspensionPermiso();

            TipoSuspensionPermisoDDL.Enabled = editar;
            txtFechaSalidaSuspensionPermiso.ReadOnly = !editar;
            txtFechaRegresoSuspensionPermiso.ReadOnly = !editar;
            txtDescripcionSuspensionPermiso.ReadOnly = !editar;
            btnEditarSuspensionPermiso.Visible = editar;
            fuArchivoSuspensionPermiso.Visible = editar;
            btnSeleccionarArchivoSuspensionPermisoAgregar.Visible = editar;

            Session["idSuspensionPermiso"] = null;
            int idSuspensionPermiso = Convert.ToInt32((((LinkButton)(sender)).CommandArgument).ToString());
            List<SuspensionPermiso> listaSuspensionPermisos = new List<SuspensionPermiso>();

            if (Session["listaSuspensionPermisos"] != null)
            {
                listaSuspensionPermisos = (List<SuspensionPermiso>)Session["listaSuspensionPermisos"];
            }

            foreach (SuspensionPermiso suspensionPermiso in listaSuspensionPermisos)
            {
                if (suspensionPermiso.IdSuspensionPermiso == idSuspensionPermiso)
                {
                    Session["idSuspensionPermiso"] = idSuspensionPermiso;
                    txtDescripcionSuspensionPermiso.Text = suspensionPermiso.Descripcion;
                    TipoSuspensionPermisoDDL.SelectedValue = suspensionPermiso.Tipo.ToString();

                    if (suspensionPermiso.NombreDocumento != "")
                    {
                        btnVerArchivoSuspensionPermiso.Text = suspensionPermiso.NombreDocumento;
                        btnVerArchivoSuspensionPermiso.CommandArgument = suspensionPermiso.RutaDocumento + "," + suspensionPermiso.NombreDocumento;
                        Session["rutaDocumentoSuspensionPermiso"] = suspensionPermiso.RutaDocumento;
                        Session["nombreDocumentoSuspensionPermiso"] = suspensionPermiso.NombreDocumento;
                    }
                    else
                    {
                        btnVerArchivoSuspensionPermiso.Text = "-";
                        btnVerArchivoSuspensionPermiso.Enabled = false;
                    }

                    if (suspensionPermiso.FechaSalida.Year == 1900)
                    {
                        txtFechaSalidaSuspensionPermiso.Text = "";
                    }
                    else
                    {
                        string anioInicio = suspensionPermiso.FechaSalida.Year.ToString();
                        string mesInicio = suspensionPermiso.FechaSalida.Month.ToString();
                        string diaInicio = suspensionPermiso.FechaSalida.Day.ToString();
                        if (diaInicio.Length == 1) { diaInicio = "0" + diaInicio; }
                        txtFechaSalidaSuspensionPermiso.Text = anioInicio + "-" + mesInicio + "-" + diaInicio;
                    }

                    if (suspensionPermiso.FechaRegreso.Year == 1900)
                    {
                        txtFechaRegresoSuspensionPermiso.Text = "";
                    }
                    else
                    {
                        string anioFinalizacion = suspensionPermiso.FechaRegreso.Year.ToString();
                        string mesFinalizacion = suspensionPermiso.FechaRegreso.Month.ToString();
                        string diaFinalizacion = suspensionPermiso.FechaRegreso.Day.ToString();
                        if (diaFinalizacion.Length == 1) { diaFinalizacion = "0" + diaFinalizacion; }
                        txtFechaRegresoSuspensionPermiso.Text = anioFinalizacion + "-" + mesFinalizacion + "-" + diaFinalizacion;
                    }

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "activar", "levantarModalVerSuspensionPermiso();", true);
                    break;
                }
            }
        }
        #endregion

        #region Utilidades
        /// <summary>
        /// Adrián Serrano
        /// 09/11/2021
        /// Efecto: Metodo que descarga el archivo seleccionado
        /// </summary>
        private void Toastr(string tipo, string mensaje)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "toastr." + tipo + "('" + mensaje + "');", true);
        }
        private void descargar(string fileName, Byte[] file)
        {
            try
            {
                Response.Buffer = true;
                Response.Charset = "";
                Response.Cache.SetCacheability(HttpCacheability.NoCache);

                Response.AddHeader("content-disposition", "attachment;filename=" + fileName);
                Response.BinaryWrite(file);
            }
            catch (Exception exception)
            {
                Estado.ErrorBitacora(exception.Message, "VerFuncionario.aspx.cs:descargar()");
            }

            Response.Flush();
            Response.End();
        }
        #endregion

        #endregion

        #region eventos

        #region Acciones para insertar un funcionario y actualizarlo
        /// <summary>
        /// Adrián Serrano
        /// 11/10/2021
        /// Efecto: Esta función sobreescribe la información personal del usuario actual en la base de datos.
        /// </summary>
        private void GuardarInformacionPersonal()
        {
            if (ValidarCamposNoObligatorios())
            {
                string identificacion = txtIdentificacion.Text.Trim();
                string nombre = txtNombre.Text.Trim();
                string primerApellido = txtPrimerApellido.Text.Trim();
                string segundoApellido = txtSegundoApellido.Text.Trim();
                string rutaFotografia = "";
                //HttpPostedFile fotografia = fuFotografia.PostedFile;

                //if (fotografia != null)
                //{
                //    string extensionDeArchivo = Path.GetExtension(fotografia.FileName);
                //    bool fotoGuardada = Utilidades.GuardarFotografias(fotografia, identificacion + extensionDeArchivo);

                //    if (fotoGuardada)
                //    {
                //        rutaFotografia = Utilidades.fotos_path + "\\" + identificacion + extensionDeArchivo;
                //    }
                //    else
                //    {
                //        Toastr("error", "Se produjo un error al guardar la fotografía. Consulte al administrador.");
                //    }
                //}

                string numeroTelefono = txtTelefono.Text.Trim();

                DateTime fechaIngreso = new DateTime(1900, 01, 01);
                if (txtFechaIngreso.Text.Trim() != "")
                {
                    fechaIngreso = Convert.ToDateTime(txtFechaIngreso.Text.Trim());
                }

                //string tipoSangre = SangreDDL.SelectedValue;
                //string lugarResidencia = txtResidencia.Text.Trim();
                string tipoLicenciaConducir = LicenciaDDL.SelectedValue;
                string puesto = txtPuesto.Text.Trim();
                string correo = txtCorreo.Text.Trim();
                string extension = txtExtension.Text.Trim();
                string observaciones = txtObservaciones.Text.Trim();
                bool estado = Convert.ToBoolean(EstadoDDL.SelectedValue);
                CategoriaLaboral categoriaLaboral = new CategoriaLaboral(Convert.ToInt32(CategoriaLaboralDDL.SelectedValue));
                Seccion seccion = new Seccion(Convert.ToInt32(SecciónDDL.SelectedValue));
                UnidadProgramaLaboratorio unidadProgramaLaboratorio = new UnidadProgramaLaboratorio(Convert.ToInt32(UnidadProgramaLaboratorioDDL.SelectedValue));

                Funcionario funcionario = new Funcionario(identificacion, rutaFotografia, nombre, primerApellido, segundoApellido, numeroTelefono, fechaIngreso, /*tipoSangre, lugarResidencia,*/
                    tipoLicenciaConducir, puesto, correo, extension, observaciones, estado, categoriaLaboral, seccion, unidadProgramaLaboratorio);

                int funcionarioActualizado = this.funcionarioDatos.Actualizar(funcionario);

                if (funcionarioActualizado == Utilidades.ACTUALIZADO)
                {
                    Toastr("success", "Todos los cambios han sido guardados.");
                    //Session["sessionFotografia"] = null;
                }
                else
                {
                    Toastr("error", "Se produjo un error inesperado. Consulte al administrador.");
                }
            }
        }

        /// <summary>
        /// Adrián Serrano
        /// 11/10/2021
        /// Efecto: Esta función verifica si es la primera vez que se guarda la información del funcionario.
        /// Si es así, entonces pide una confirmación y crea el funcioario, de lo contrario, se actualiza el funcionario.
        /// Llama a la función ValidarCampos para validar que los campos obligatorios se hayan ingresado
        /// correctamente
        /// </summary>
        protected void btnGuardarInformacion_Click(object sender, EventArgs e)
        {
            GuardarInformacionPersonal();
        }
        #endregion

        #region Acciones para agregar los diferentes datos para el funcionario desde las ventanas modales
        /// <summary>
        /// Adrián Serrano
        /// 05/10/2021
        /// Efecto: Esta sección contiene los métodos para agregar los diferentes datos para el funcionario
        /// desde las ventanas modales
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAgregarEstudio_Click(object sender, EventArgs e)
        {
            if (ValidarCamposEstudioAgregar())
            {
                HttpPostedFile certificadoEstudio = fuCertificadoEstudioAgregar.PostedFile;
                DateTime fechaActual = DateTime.Now;
                string identificacion = txtIdentificacion.Text;
                string rutaDocumento = "";
                string nombreDocumento = "";

                if (certificadoEstudio != null)
                {
                    String nombreArchivo = Path.GetFileName(certificadoEstudio.FileName);
                    nombreDocumento = nombreArchivo;
                    nombreArchivo = "estudio" + "_" + fechaActual.ToString().Replace("/", "").Replace(" ", "").Replace(":", "")
                        + "_" + identificacion + "_" + nombreArchivo.Replace(' ', '_');

                    Boolean guardado = Utilidades.GuardarDocumentos(certificadoEstudio, nombreArchivo);

                    if (guardado)
                    {
                        rutaDocumento = Utilidades.archivos_path + "\\" + nombreArchivo;
                    }
                    else
                    {
                        rutaDocumento = "";
                        nombreDocumento = "";
                        Toastr("error", "Se produjo un error al guardar el documento. Consulte al administrador.");
                    }
                }

                Estudio estudio = new Estudio();
                estudio.Nombre = txtNombreEstudioAgregar.Text;
                estudio.RutaDocumento = rutaDocumento;
                estudio.NombreDocumento = nombreDocumento;
                estudio.FechaInicio = Convert.ToDateTime(txtFechaInicioEstudioAgregar.Text);

                DateTime fechaFinalizacion = new DateTime(1900, 01, 01);
                if (txtFechaFinalizacionEstudioAgregar.Text.Trim() != "")
                {
                    fechaFinalizacion = Convert.ToDateTime(txtFechaFinalizacionEstudioAgregar.Text.Trim());
                }

                estudio.FechaFinalizacion = fechaFinalizacion;
                estudio.Observacion = txtObservacionesEstudioAgregar.Text;
                estudio.Entregado = ckbEntregadoEstudioAgregar.Checked;
                estudio.Funcionario = new Funcionario(identificacion);
                estudio.TipoEstudio = new TipoEstudio(Convert.ToInt32(TipoEstudioDDLAgregar.SelectedValue));

                int resultado = this.estudioDatos.Insertar(estudio);

                if (resultado > 0)
                {
                    Toastr("success", "El estudio fue agregado con éxito.");

                    List<Estudio> listaEstudios = estudioDatos.ObtenerPorId("estudio", identificacion);

                    rpEstudios.DataSource = listaEstudios;
                    rpEstudios.DataBind();

                    txtNombreEstudioAgregar.Text = "";
                    txtFechaInicioEstudioAgregar.Text = "";
                    txtFechaFinalizacionEstudioAgregar.Text = "";
                    txtObservacionesEstudioAgregar.Text = "";
                    ckbEntregadoEstudioAgregar.Checked = false;

                    Session["sessionArchivoEstudio"] = null;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "activar", "cerrarModalAgregarEstudio();", true);
                }
                else
                {
                    Toastr("error", "Se produjo un error inesperado. Consulte al administrador.");
                }
            }
        }
        protected void btnAgregarCurso_Click(object sender, EventArgs e)
        {
            if (ValidarCamposCursoAgregar())
            {
                HttpPostedFile certificadoCurso = fuCertificadoCursoAgregar.PostedFile;
                DateTime fechaActual = DateTime.Now;
                string identificacion = txtIdentificacion.Text;
                string rutaDocumento = "";
                string nombreDocumento = "";

                if (certificadoCurso != null)
                {
                    String nombreArchivo = Path.GetFileName(certificadoCurso.FileName);
                    nombreDocumento = nombreArchivo;
                    nombreArchivo = "curso" + "_" + fechaActual.ToString().Replace("/", "").Replace(" ", "").Replace(":", "")
                        + "_" + identificacion + "_" + nombreArchivo.Replace(' ', '_');

                    Boolean guardado = Utilidades.GuardarDocumentos(certificadoCurso, nombreArchivo);

                    if (guardado)
                    {
                        rutaDocumento = Utilidades.archivos_path + "\\" + nombreArchivo;
                    }
                    else
                    {
                        rutaDocumento = "";
                        nombreDocumento = "";
                        Toastr("error", "Se produjo un error al guardar el documento. Consulte al administrador.");
                    }
                }

                Estudio estudio = new Estudio();
                estudio.Nombre = txtNombreCursoAgregar.Text;
                estudio.RutaDocumento = rutaDocumento;
                estudio.NombreDocumento = nombreDocumento;
                estudio.FechaInicio = Convert.ToDateTime(txtFechaInicioCursoAgregar.Text);

                DateTime fechaFinalizacion = new DateTime(1900, 01, 01);
                if (txtFechaFinalizacionCursoAgregar.Text.Trim() != "")
                {
                    fechaFinalizacion = Convert.ToDateTime(txtFechaFinalizacionCursoAgregar.Text.Trim());
                }

                estudio.FechaFinalizacion = fechaFinalizacion;
                estudio.Observacion = txtObservacionesCursoAgregar.Text;
                estudio.Entregado = ckbEntregadoCursoAgregar.Checked;
                estudio.Funcionario = new Funcionario(identificacion);

                // 10 es el ID para cursos en la base de datos
                estudio.TipoEstudio = new TipoEstudio(10);

                int resultado = this.estudioDatos.Insertar(estudio);

                if (resultado > 0)
                {
                    Toastr("success", "El curso fue agregado con éxito.");

                    List<Estudio> listaCursos = this.estudioDatos.ObtenerPorId("curso", identificacion);

                    rpCursos.DataSource = listaCursos;
                    rpCursos.DataBind();

                    txtNombreCursoAgregar.Text = "";
                    txtFechaInicioCursoAgregar.Text = "";
                    txtFechaFinalizacionCursoAgregar.Text = "";
                    txtObservacionesCursoAgregar.Text = "";
                    ckbEntregadoCursoAgregar.Checked = false;

                    Session["sessionArchivoCurso"] = null;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "activar", "cerrarModalAgregarCurso();", true);
                }
                else
                {
                    Toastr("error", "Se produjo un error inesperado. Consulte al administrador.");
                }
            }
        }
        protected void btnAgregarCertificado_Click(object sender, EventArgs e)
        {
            if (ValidarCamposCertificadoAgregar())
            {
                HttpPostedFile certificadoCertificado = fuCertificadoCertificadoAgregar.PostedFile;
                DateTime fechaActual = DateTime.Now;
                string identificacion = txtIdentificacion.Text;
                string rutaDocumento = "";
                string nombreDocumento = "";

                if (certificadoCertificado != null)
                {
                    String nombreArchivo = Path.GetFileName(certificadoCertificado.FileName);
                    nombreDocumento = nombreArchivo;
                    nombreArchivo = "certificado" + "_" + fechaActual.ToString().Replace("/", "").Replace(" ", "").Replace(":", "")
                        + "_" + identificacion + "_" + nombreArchivo.Replace(' ', '_');

                    Boolean guardado = Utilidades.GuardarDocumentos(certificadoCertificado, nombreArchivo);

                    if (guardado)
                    {
                        rutaDocumento = Utilidades.archivos_path + "\\" + nombreArchivo;
                    }
                    else
                    {
                        rutaDocumento = "";
                        nombreDocumento = "";
                        Toastr("error", "Se produjo un error al guardar el documento. Consulte al administrador.");
                    }
                }

                Estudio estudio = new Estudio();
                estudio.Nombre = txtNombreCertificadoAgregar.Text;
                estudio.RutaDocumento = rutaDocumento;
                estudio.NombreDocumento = nombreDocumento;
                estudio.FechaInicio = Convert.ToDateTime(txtFechaInicioCertificadoAgregar.Text);

                DateTime fechaFinalizacion = new DateTime(1900, 01, 01);
                if (txtFechaFinalizacionCertificadoAgregar.Text.Trim() != "")
                {
                    fechaFinalizacion = Convert.ToDateTime(txtFechaFinalizacionCertificadoAgregar.Text.Trim());
                }

                estudio.FechaFinalizacion = fechaFinalizacion;
                estudio.Observacion = txtObservacionesCertificadoAgregar.Text;
                estudio.Entregado = ckbEntregadoCertificadoAgregar.Checked;
                estudio.Funcionario = new Funcionario(identificacion);

                // 11 es el ID para certificados en la base de datos
                estudio.TipoEstudio = new TipoEstudio(11);

                int resultado = this.estudioDatos.Insertar(estudio);

                if (resultado > 0)
                {
                    Toastr("success", "El certificado fue agregado con éxito.");

                    List<Estudio> listaCertificados = this.estudioDatos.ObtenerPorId("certificado", identificacion);

                    rpCertificados.DataSource = listaCertificados;
                    rpCertificados.DataBind();

                    txtNombreCertificadoAgregar.Text = "";
                    txtFechaInicioCertificadoAgregar.Text = "";
                    txtFechaFinalizacionCertificadoAgregar.Text = "";
                    txtObservacionesCertificadoAgregar.Text = "";
                    ckbEntregadoCertificadoAgregar.Checked = false;

                    Session["sessionArchivoCertificado"] = null;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "activar", "cerrarModalAgregarCertificado();", true);
                }
                else
                {
                    Toastr("error", "Se produjo un error inesperado. Consulte al administrador.");
                }
            }
        }
        protected void btnAgregarExperienciaLaboral_Click(object sender, EventArgs e)
        {
            if (ValidarCamposExperienciaLaboralAgregar())
            {

                HttpPostedFile curriculoVitae = fuCurriculumVerEditar.PostedFile;
                DateTime fechaActual = DateTime.Now;
                string rutaDocumento = "";
                string nombreDocumento = "";

                string identificacion = txtIdentificacion.Text;

                if (curriculoVitae != null)
                {
                    String nombreArchivo = Path.GetFileName(curriculoVitae.FileName);
                    nombreDocumento = nombreArchivo;
                    nombreArchivo = "curriculo_vitae" + "_" + fechaActual.ToString().Replace("/", "").Replace(" ", "").Replace(":", "")
                        + "_" + identificacion + "_" + nombreArchivo.Replace(' ', '_');

                    Boolean guardado = Utilidades.GuardarDocumentos(curriculoVitae, nombreArchivo);

                    if (guardado)
                    {
                        rutaDocumento = Utilidades.archivos_path + "\\" + nombreArchivo;
                    }
                    else
                    {
                        rutaDocumento = "";
                        nombreDocumento = "";
                        Toastr("error", "Se produjo un error al guardar el documento. Consulte al administrador.");
                    }
                }

                Curriculum curriculim = new Curriculum();
                curriculim.nombre = nombreDocumento;
                curriculim.descripcion = txtDescripcionPuestoExperienciaLaboralVerEditar.Text;
                curriculim.ruta = rutaDocumento;
                curriculim.funcionario = new Funcionario(identificacion);

                int resultado = this.curriculumDatos.Insertar(curriculim);

                if (resultado > 0)
                {
                    Toastr("success", "El currículo vitae fue agregado con éxito.");

                    List<Curriculum> listaCurriculums = this.curriculumDatos.ObtenerPorId(identificacion);

                    rpExperienciaLaboral.DataSource = listaCurriculums;
                    rpExperienciaLaboral.DataBind();

                    txtNombreEmpresaExperienciaLaboralVerEditar.Text = "";
                    txtDescripcionPuestoExperienciaLaboralVerEditar.Text = "";

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "activar", "cerrarModalAgregarExperienciaLaboral();", true);
                }
                else
                {
                    Toastr("error", "Se produjo un error inesperado. Consulte al administrador.");
                }
            }
        }
        protected void btnAgregarHabilidadBlanda_Click(object sender, EventArgs e)
        {
            if (ValidarCamposHabilidadBlandaAgregar())
            {
                HabilidadBlanda habilidadBlanda = new HabilidadBlanda();
                habilidadBlanda.Descripcion = txtDescripcionHabilidadBlandaAgregar.Text;
                string identificacion = txtIdentificacion.Text;
                habilidadBlanda.Funcionario = new Funcionario(identificacion);

                int resultado = this.habilidadBlandaDatos.Insertar(habilidadBlanda);

                if (resultado > 0)
                {
                    Toastr("success", "La habilidad blanda fue agregada con éxito.");

                    List<HabilidadBlanda> listaHabilidadesBlandas = this.habilidadBlandaDatos.ObtenerPorId(identificacion);

                    rpHabilidadBlanda.DataSource = listaHabilidadesBlandas;
                    rpHabilidadBlanda.DataBind();

                    txtDescripcionHabilidadBlandaAgregar.Text = "";

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "activar", "cerrarModalAgregarHabilidadBlanda();", true);
                }
                else
                {
                    Toastr("error", "Se produjo un error inesperado. Consulte al administrador.");
                }
            }
        }
        protected void btnAgregarInteresPersonal_Click(object sender, EventArgs e)
        {
            if (ValidarCamposInteresPersonalAgregar())
            {
                InteresPersonal interesPersonal = new InteresPersonal();
                interesPersonal.Descripcion = txtDescripcionInteresPersonalAgregar.Text;
                string identificacion = txtIdentificacion.Text;
                interesPersonal.Funcionario = new Funcionario(identificacion);

                int resultado = this.interesPersonalDatos.Insertar(interesPersonal);

                if (resultado > 0)
                {
                    Toastr("success", "El interés personal fue agregado con éxito.");

                    List<InteresPersonal> listaInteresesPersonales = this.interesPersonalDatos.ObtenerPorId(identificacion);

                    rpInteresesPersonales.DataSource = listaInteresesPersonales;
                    rpInteresesPersonales.DataBind();

                    txtDescripcionInteresPersonalAgregar.Text = "";

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "activar", "cerrarModalAgregarInteresPersonal();", true);
                }
                else
                {
                    Toastr("error", "Se produjo un error inesperado. Consulte al administrador.");
                }
            }
        }
        protected void btnAgregarPensionOEmbargo_Click(object sender, EventArgs e)
        {
            if (ValidarCamposPensionOEmbargoAgregar())
            {
                HttpPostedFile certificadoPensionOEmbargo = fuCertificadoPensionOEmbargoAgregar.PostedFile;
                DateTime fechaActual = DateTime.Now;
                string identificacion = txtIdentificacion.Text;
                string rutaDocumento = "";
                string nombreDocumento = "";

                if (certificadoPensionOEmbargo != null)
                {
                    String nombreArchivo = Path.GetFileName(certificadoPensionOEmbargo.FileName);
                    nombreDocumento = nombreArchivo;
                    nombreArchivo = "pension_o_embargo" + "_" + fechaActual.ToString().Replace("/", "").Replace(" ", "").Replace(":", "")
                        + "_" + identificacion + "_" + nombreArchivo.Replace(' ', '_');

                    Boolean guardado = Utilidades.GuardarDocumentos(certificadoPensionOEmbargo, nombreArchivo);

                    if (guardado)
                    {
                        rutaDocumento = Utilidades.archivos_path + "\\" + nombreArchivo;
                    }
                    else
                    {
                        rutaDocumento = "";
                        nombreDocumento = "";
                        Toastr("error", "Se produjo un error al guardar el documento. Consulte al administrador.");
                    }
                }

                PensionOEmbargo pensionOEmbargo = new PensionOEmbargo();
                pensionOEmbargo.RutaDocumento = rutaDocumento;
                pensionOEmbargo.NombreDocumento = nombreDocumento;
                pensionOEmbargo.FechaIngreso = fechaActual;
                pensionOEmbargo.Descripcion = txtDescripcionPensionOEmbargoAgregar.Text;
                pensionOEmbargo.Funcionario = new Funcionario(identificacion);

                int resultado = this.pensionOEmbargoDatos.Insertar(pensionOEmbargo);

                if (resultado > 0)
                {
                    Toastr("success", "La pensión/embargo fue agregado con éxito.");

                    List<PensionOEmbargo> listaPensionOEmbargos = this.pensionOEmbargoDatos.ObtenerPorId(identificacion);

                    rpPensionOEmbargos.DataSource = listaPensionOEmbargos;
                    rpPensionOEmbargos.DataBind();

                    txtDescripcionPensionOEmbargoAgregar.Text = "";

                    Session["sessionArchivoPensionOEmbargo"] = null;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "activar", "cerrarModalAgregarPensionOEmbargo();", true);
                }
                else
                {
                    Toastr("error", "Se produjo un error inesperado. Consulte al administrador.");
                }
            }
        }
        protected void btnAgregarAccionesPersonal_Click(object sender, EventArgs e)
        {
            if (ValidarCamposAccionesPersonal())
            {
                HttpPostedFile certificadoAccionesPersonal = fuCertificadoAccionesPersonal.PostedFile;
                DateTime fechaActual = DateTime.Now;
                string identificacion = txtIdentificacion.Text;
                string rutaDocumento = "";
                string nombreDocumento = "";

                if (certificadoAccionesPersonal != null)
                {
                    String nombreArchivo = Path.GetFileName(certificadoAccionesPersonal.FileName);
                    nombreDocumento = nombreArchivo;
                    nombreArchivo = "accion_de_personal" + "_" + fechaActual.ToString().Replace("/", "").Replace(" ", "").Replace(":", "")
                        + "_" + identificacion + "_" + nombreArchivo.Replace(' ', '_');

                    Boolean guardado = Utilidades.GuardarDocumentos(certificadoAccionesPersonal, nombreArchivo);

                    if (guardado)
                    {
                        rutaDocumento = Utilidades.archivos_path + "\\" + nombreArchivo;
                    }
                    else
                    {
                        rutaDocumento = "";
                        nombreDocumento = "";
                        Toastr("error", "Se produjo un error al guardar el documento. Consulte al administrador.");
                    }
                }

                DocumentoTramite accionPersonal = new DocumentoTramite();
                accionPersonal.nombreDocumento = txtNombreAccionesPersonal.Text.Trim();
                //accionPersonal.Periodo = Convert.ToDateTime(txtPeriodoAccionesPersonal.Text);
                accionPersonal.descripcion = txtDescripcionAccionesPersonal.Text;
                accionPersonal.rutaDocumento = rutaDocumento;
                accionPersonal.nombreDocumento = nombreDocumento;
                accionPersonal.numero = txtNumero.Text;
                accionPersonal.funcionario = new Funcionario(identificacion);
                //accionPersonal.TipoAccionPersonal = new TipoAccionPersonal(Convert.ToInt32(TipoAccionesPersonalDDL.SelectedValue));

                int resultado = this.documentoTramiteDatos.Insertar(accionPersonal);

                if (resultado > 0)
                {
                    Toastr("success", "La acción personal fue agregada con éxito.");

                    List<DocumentoTramite> listaAccionesPersonal = this.documentoTramiteDatos.ObtenerPorId(identificacion);

                    rpAccionesPersonal.DataSource = listaAccionesPersonal;
                    rpAccionesPersonal.DataBind();

                    txtNombreAccionesPersonal.Text = "";
                    txtDescripcionAccionesPersonal.Text = "";

                    Session["sessionArchivoAccionesPersonal"] = null;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "activar", "cerrarModalAgregarAccionesPersonal();", true);
                }
                else
                {
                    Toastr("error", "Se produjo un error inesperado. Consulte al administrador.");
                }
            }
        }
        protected void btnAgregarComprobantes_Click(object sender, EventArgs e)
        {
            if (ValidarCamposComprobantesAgregar())
            {
                HttpPostedFile certificadoComprobantes = fuCertificadoComprobantesAgregar.PostedFile;
                DateTime fechaActual = DateTime.Now;
                string identificacion = txtIdentificacion.Text;
                string rutaDocumento = "";
                string nombreDocumento = "";

                if (certificadoComprobantes != null)
                {
                    String nombreArchivo = Path.GetFileName(certificadoComprobantes.FileName);
                    nombreDocumento = nombreArchivo;
                    nombreArchivo = "comprobante" + "_" + fechaActual.ToString().Replace("/", "").Replace(" ", "").Replace(":", "")
                        + "_" + identificacion + "_" + nombreArchivo.Replace(' ', '_');

                    Boolean guardado = Utilidades.GuardarDocumentos(certificadoComprobantes, nombreArchivo);

                    if (guardado)
                    {
                        rutaDocumento = Utilidades.archivos_path + "\\" + nombreArchivo;
                    }
                    else
                    {
                        rutaDocumento = "";
                        nombreDocumento = "";
                        Toastr("error", "Se produjo un error al guardar el documento. Consulte al administrador.");
                    }
                }

                Comprobante comprobante = new Comprobante();
                comprobante.Fecha = Convert.ToDateTime(txtFechaComprobantesAgregar.Text);
                comprobante.Descripcion = txtDescripcionComprobantesAgregar.Text;
                comprobante.RutaDocumento = rutaDocumento;
                comprobante.NombreDocumento = nombreDocumento;
                comprobante.Funcionario = new Funcionario(identificacion);
                comprobante.TipoComprobante = new TipoComprobante(Convert.ToInt32(TipoComprobantesDDLAgregar.SelectedValue));

                int resultado = this.comprobantesDatos.Insertar(comprobante);

                if (resultado > 0)
                {
                    Toastr("success", "El comprobante fue agregado con éxito.");

                    List<Comprobante> listaComprobantes = this.comprobantesDatos.ObtenerPorId(identificacion);

                    rpComprobantes.DataSource = listaComprobantes;
                    rpComprobantes.DataBind();

                    txtDescripcionComprobantesAgregar.Text = "";

                    Session["sessionArchivoComprobantes"] = null;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "activar", "cerrarModalAgregarComprobantes();", true);
                }
                else
                {
                    Toastr("error", "Se produjo un error inesperado. Consulte al administrador.");
                }
            }
        }
        protected void btnAgregarAntecedentes_Click(object sender, EventArgs e)
        {
            if (ValidarCamposAntecedentesAgregar())
            {
                HttpPostedFile certificadoAntecedentes = fuCertificadoAntecedentesAgregar.PostedFile;
                DateTime fechaActual = DateTime.Now;
                string identificacion = txtIdentificacion.Text;
                string rutaDocumento = "";
                string nombreDocumento = "";

                if (certificadoAntecedentes != null)
                {
                    String nombreArchivo = Path.GetFileName(certificadoAntecedentes.FileName);
                    nombreDocumento = nombreArchivo;
                    nombreArchivo = "antecedente" + "_" + fechaActual.ToString().Replace("/", "").Replace(" ", "").Replace(":", "")
                        + "_" + identificacion + "_" + nombreArchivo.Replace(' ', '_');

                    Boolean guardado = Utilidades.GuardarDocumentos(certificadoAntecedentes, nombreArchivo);

                    if (guardado)
                    {
                        rutaDocumento = Utilidades.archivos_path + "\\" + nombreArchivo;
                    }
                    else
                    {
                        rutaDocumento = "";
                        nombreDocumento = "";
                        Toastr("error", "Se produjo un error al guardar el documento. Consulte al administrador.");
                    }
                }

                Antecedente antecedente = new Antecedente();
                antecedente.Nombre = txtNombreAntecedentesAgregar.Text.Trim();
                antecedente.Fecha = Convert.ToDateTime(txtFechaAntecedentesAgregar.Text);
                antecedente.Descripcion = txtDescripcionAntecedentesAgregar.Text;
                antecedente.RutaDocumento = rutaDocumento;
                antecedente.NombreDocumento = nombreDocumento;
                antecedente.Funcionario = new Funcionario(identificacion);
                antecedente.TipoAntecedente = new TipoAntecedente(Convert.ToInt32(TipoAntecedentesDDLAgregar.SelectedValue));

                int resultado = this.antecedenteDatos.Insertar(antecedente);

                if (resultado > 0)
                {
                    Toastr("success", "El antecedente fue agregado con éxito.");

                    List<Antecedente> listaAntecedentes = this.antecedenteDatos.ObtenerPorId(identificacion);

                    rpAntecedentes.DataSource = listaAntecedentes;
                    rpAntecedentes.DataBind();

                    txtNombreAntecedentesAgregar.Text = "";
                    txtDescripcionAntecedentesAgregar.Text = "";

                    Session["sessionArchivoAntecedentes"] = null;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "activar", "cerrarModalAgregarAntecedentes();", true);
                }
                else
                {
                    Toastr("error", "Se produjo un error inesperado. Consulte al administrador.");
                }
            }
        }
        protected void btnAgregarSuspensionPermiso_Click(object sender, EventArgs e)
        {
            if (ValidarCamposSuspensionPermisoAgregar())
            {
                SuspensionPermiso suspensionPermiso = new SuspensionPermiso();
                suspensionPermiso.FechaSalida = Convert.ToDateTime(txtFechaSalidaSuspensionPermisoAgregar.Text);

                HttpPostedFile archivoSuspensionPermiso = fuArchivoSuspensionPermiso.PostedFile;
                DateTime fechaActual = DateTime.Now;
                string identificacion = txtIdentificacion.Text;
                string rutaDocumento = "";
                string nombreDocumento = "";

                if (archivoSuspensionPermiso != null)
                {
                    String nombreArchivo = Path.GetFileName(archivoSuspensionPermiso.FileName);
                    nombreDocumento = nombreArchivo;
                    nombreArchivo = "suspension_permiso" + "_" + fechaActual.ToString().Replace("/", "").Replace(" ", "").Replace(":", "")
                        + "_" + identificacion + "_" + nombreArchivo.Replace(' ', '_');

                    Boolean guardado = Utilidades.GuardarDocumentos(archivoSuspensionPermiso, nombreArchivo);

                    if (guardado)
                    {
                        rutaDocumento = Utilidades.archivos_path + "\\" + nombreArchivo;
                    }
                    else
                    {
                        rutaDocumento = "";
                        nombreDocumento = "";
                        Toastr("error", "Se produjo un error al guardar el documento. Consulte al administrador.");
                    }
                }

                DateTime fechaRegreso = new DateTime(1900, 01, 01);
                if (txtFechaRegresoSuspensionPermisoAgregar.Text.Trim() != "")
                {
                    fechaRegreso = Convert.ToDateTime(txtFechaRegresoSuspensionPermisoAgregar.Text.Trim());
                }

                suspensionPermiso.RutaDocumento = rutaDocumento;
                suspensionPermiso.NombreDocumento = nombreDocumento;
                suspensionPermiso.FechaRegreso = fechaRegreso;
                suspensionPermiso.Descripcion = txtDescripcionSuspensionPermisoAgregar.Text;
                suspensionPermiso.Tipo = Convert.ToInt32(TipoSuspensionPermisoDDLAgregar.SelectedValue);
                suspensionPermiso.Funcionario = new Funcionario(identificacion);

                int resultado = this.suspensionPermisoDatos.Insertar(suspensionPermiso);

                if (resultado > 0)
                {
                    Toastr("success", "La suspensión/permiso fue agregado con éxito.");

                    List<SuspensionPermiso> listaSuspensionPermisos = this.suspensionPermisoDatos.ObtenerPorId(identificacion);

                    rpSuspensionPermiso.DataSource = listaSuspensionPermisos;
                    rpSuspensionPermiso.DataBind();

                    txtFechaSalidaSuspensionPermisoAgregar.Text = "";
                    txtFechaRegresoSuspensionPermisoAgregar.Text = "";
                    txtDescripcionSuspensionPermisoAgregar.Text = "";

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "activar", "cerrarModalAgregarSuspensionPermiso();", true);
                }
                else
                {
                    Toastr("error", "Se produjo un error inesperado. Consulte al administrador.");
                }
            }
        }
        #endregion

        #region Mostrar y ocultar las secciones desde el menú lateral
        /// <summary>
        /// Adrián Serrano
        /// 05/10/2021
        /// Efecto: Esta sección contiene las funciones para mostrar y ocultar las secciones desde el menú lateral
        /// modales.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void showHideInformacionPersonal(object sender, EventArgs e)
        {
            if (!divInformacionPersonal.Visible)
            {
                aCurriculo.CssClass = "";
                aInformacionFinanciera.CssClass = "";
                aAcciones.CssClass = "";
                aComprobantes.CssClass = "";
                aAmonestacionesAntecedentes.CssClass = "";
                aInformacionPersonal.CssClass = "active";

                divCurriculo.Visible = false;
                divInformacionFinanciera.Visible = false;
                divAcciones.Visible = false;
                divComprobantes.Visible = false;
                divAmonestacionesAntecedentes.Visible = false;
                divInformacionPersonal.Visible = true;
            }
        }
        protected void showHideCurriculo(object sender, EventArgs e)
        {
            if (!divCurriculo.Visible)
            {
                aInformacionPersonal.CssClass = "";
                aInformacionFinanciera.CssClass = "";
                aAcciones.CssClass = "";
                aComprobantes.CssClass = "";
                aAmonestacionesAntecedentes.CssClass = "";
                aCurriculo.CssClass = "active";

                divInformacionPersonal.Visible = false;
                divInformacionFinanciera.Visible = false;
                divAcciones.Visible = false;
                divComprobantes.Visible = false;
                divAmonestacionesAntecedentes.Visible = false;
                divCurriculo.Visible = true;
            }
        }
        protected void showHideInformacionFinanciera(object sender, EventArgs e)
        {
            if (!divInformacionFinanciera.Visible)
            {
                aInformacionPersonal.CssClass = "";
                aAcciones.CssClass = "";
                aCurriculo.CssClass = "";
                aComprobantes.CssClass = "";
                aAmonestacionesAntecedentes.CssClass = "";
                aInformacionFinanciera.CssClass = "active";

                divInformacionPersonal.Visible = false;
                divCurriculo.Visible = false;
                divAcciones.Visible = false;
                divComprobantes.Visible = false;
                divAmonestacionesAntecedentes.Visible = false;
                divInformacionFinanciera.Visible = true;
            }
        }
        protected void showHideAcciones(object sender, EventArgs e)
        {
            if (!divAcciones.Visible)
            {
                aInformacionPersonal.CssClass = "";
                aCurriculo.CssClass = "";
                aInformacionFinanciera.CssClass = "";
                aComprobantes.CssClass = "";
                aAmonestacionesAntecedentes.CssClass = "";
                aAcciones.CssClass = "active";

                divInformacionPersonal.Visible = false;
                divCurriculo.Visible = false;
                divInformacionFinanciera.Visible = false;
                divComprobantes.Visible = false;
                divAmonestacionesAntecedentes.Visible = false;
                divAcciones.Visible = true;
            }
        }
        protected void showHideComprobantes(object sender, EventArgs e)
        {
            if (!divComprobantes.Visible)
            {
                aInformacionPersonal.CssClass = "";
                aCurriculo.CssClass = "";
                aInformacionFinanciera.CssClass = "";
                aAcciones.CssClass = "";
                aAmonestacionesAntecedentes.CssClass = "";
                aComprobantes.CssClass = "active";

                divInformacionPersonal.Visible = false;
                divCurriculo.Visible = false;
                divInformacionFinanciera.Visible = false;
                divAcciones.Visible = false;
                divAmonestacionesAntecedentes.Visible = false;
                divComprobantes.Visible = true;
            }
        }
        protected void showHideAmonestacionesAntecedentes(object sender, EventArgs e)
        {
            if (!divAmonestacionesAntecedentes.Visible)
            {
                aInformacionPersonal.CssClass = "";
                aCurriculo.CssClass = "";
                aInformacionFinanciera.CssClass = "";
                aAcciones.CssClass = "";
                aComprobantes.CssClass = "";
                aAmonestacionesAntecedentes.CssClass = "active";

                divInformacionPersonal.Visible = false;
                divCurriculo.Visible = false;
                divInformacionFinanciera.Visible = false;
                divAcciones.Visible = false;
                divComprobantes.Visible = false;
                divAmonestacionesAntecedentes.Visible = true;
            }
        }
        #endregion

        #region Confirmar y cancelar el guardado de un nuevo funcionario
        /// <summary>
        /// Adrián Serrano
        /// 05/10/2021
        /// Efecto: Esta sección contiene las funciones para confirmar y cancelar el guardado de un nuevo funcionario
        /// modales.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "activar", "levantarModalCancelar();", true);
        }
        protected void btnConfirmarCancelar_Click(object sender, EventArgs e)
        {
            String url = Page.ResolveUrl("~/Default.aspx");
            Response.Redirect(url);
        }

        #endregion

        #region Acciones para seleccionar y quitar archivos multimedia
        /// <summary>
        /// Adrián Serrano
        /// 05/10/2021
        /// Efecto: Esta sección contiene los métodos para seleccionar y quitar los archivos multimedia
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //protected void btnSeleccionarFotografia_Click(object sender, EventArgs e)
        //{
        //    Session["sessionFotografia"] = fuFotografia;
        //    IList<HttpPostedFile> fotografias = fuFotografia.PostedFiles;

        //    if (fuFotografia.HasFiles)
        //    {
        //        btnQuitarFotografia.Visible = true;
        //        lblFotografiaVacia.Text = "";

        //        foreach (HttpPostedFile foto in fotografias)
        //        {
        //            lblFotografiaVacia.Text += foto.FileName;
        //        }
        //    }
        //}
        //protected void btnQuitarFotografia_Click(object sender, EventArgs e)
        //{
        //    Session["sessionFotografia"] = null;
        //    fuFotografia = null;
        //    btnQuitarFotografia.Visible = false;
        //    lblFotografiaVacia.Text = "";
        //}
        protected void btnSeleccionarArchivoEstudio_Click(object sender, EventArgs e)
        {
            Session["sessionArchivoEstudio"] = fuCertificadoEstudio;
            IList<HttpPostedFile> certificadoEstudios = fuCertificadoEstudio.PostedFiles;

            if (fuCertificadoEstudio.HasFiles)
            {
                btnQuitarArchivoEstudio.Visible = true;
                lblArchivoEstudioVacio.Text = "";

                foreach (HttpPostedFile certificado in certificadoEstudios)
                {
                    lblArchivoEstudioVacio.Text += certificado.FileName;
                }
                ckbEntregadoEstudio.Checked = true;

            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "activar", "levantarModalAgregarEstudio();", true);
        }
        protected void btnQuitarArchivoEstudio_Click(object sender, EventArgs e)
        {
            Session["sessionArchivoEstudio"] = null;
            fuCertificadoEstudio = null;
            btnQuitarArchivoEstudio.Visible = false;
            lblArchivoEstudioVacio.Text = "";
            ckbEntregadoEstudio.Checked = false;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "activar", "levantarModalAgregarEstudio();", true);
        }
        protected void btnSeleccionarArchivoCurso_Click(object sender, EventArgs e)
        {
            Session["sessionArchivoCurso"] = fuCertificadoCurso;
            IList<HttpPostedFile> certificadoCursos = fuCertificadoCurso.PostedFiles;

            if (fuCertificadoCurso.HasFiles)
            {
                btnQuitarArchivoCurso.Visible = true;
                lblArchivoCursoVacio.Text = "";

                foreach (HttpPostedFile curso in certificadoCursos)
                {
                    lblArchivoCursoVacio.Text += curso.FileName;
                }
                ckbEntregadoCurso.Checked = true;

            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "activar", "levantarModalAgregarCurso();", true);
        }
        protected void btnQuitarArchivoCurso_Click(object sender, EventArgs e)
        {
            Session["sessionArchivoCurso"] = null;
            fuCertificadoCurso = null;
            btnQuitarArchivoCurso.Visible = false;
            lblArchivoCursoVacio.Text = "";
            ckbEntregadoCurso.Checked = false;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "activar", "levantarModalAgregarCurso();", true);
        }
        protected void btnSeleccionarArchivoCertificado_Click(object sender, EventArgs e)
        {
            Session["sessionArchivoCertificado"] = fuCertificadoCertificado;
            IList<HttpPostedFile> certificadoCertificados = fuCertificadoCertificado.PostedFiles;

            if (fuCertificadoCertificado.HasFiles)
            {
                btnQuitarArchivoCertificado.Visible = true;
                lblArchivoCertificadoVacio.Text = "";

                foreach (HttpPostedFile certificado in certificadoCertificados)
                {
                    lblArchivoCertificadoVacio.Text += certificado.FileName;
                }
                ckbEntregadoCertificado.Checked = true;

            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "activar", "levantarModalAgregarCertificado();", true);
        }
        protected void btnQuitarArchivoCertificado_Click(object sender, EventArgs e)
        {
            Session["sessionArchivoCertificado"] = null;
            fuCertificadoCertificado = null;
            btnQuitarArchivoCertificado.Visible = false;
            lblArchivoCertificadoVacio.Text = "";
            ckbEntregadoCertificado.Checked = false;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "activar", "levantarModalAgregarCertificado();", true);
        }
        protected void btnSeleccionarArchivoPensionOEmbargo_Click(object sender, EventArgs e)
        {
            Session["sessionArchivoPensionOEmbargo"] = fuCertificadoPensionOEmbargo;
            IList<HttpPostedFile> certificadoPensionOEmbargos = fuCertificadoPensionOEmbargo.PostedFiles;

            if (fuCertificadoPensionOEmbargo.HasFiles)
            {
                btnQuitarArchivoPensionOEmbargo.Visible = true;
                lblArchivoPensionOEmbargoVacio.Text = "";

                foreach (HttpPostedFile certificado in certificadoPensionOEmbargos)
                {
                    lblArchivoPensionOEmbargoVacio.Text += certificado.FileName;
                }
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "activar", "levantarModalAgregarPensionOEmbargo();", true);
        }
        protected void btnQuitarArchivoPensionOEmbargo_Click(object sender, EventArgs e)
        {
            Session["sessionArchivoPensionOEmbargo"] = null;
            fuCertificadoPensionOEmbargo = null;
            btnQuitarArchivoPensionOEmbargo.Visible = false;
            lblArchivoPensionOEmbargoVacio.Text = "";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "activar", "levantarModalAgregarPensionOEmbargo();", true);
        }
        protected void btnSeleccionarArchivoAccionesPersonal_Click(object sender, EventArgs e)
        {
            Session["sessionAccionesPersonal"] = fuCertificadoAccionesPersonal;
            IList<HttpPostedFile> certificadoAccionesPersonal = fuCertificadoAccionesPersonal.PostedFiles;

            if (fuCertificadoAccionesPersonal.HasFiles)
            {
                btnQuitarArchivoAccionesPersonal.Visible = true;
                lblArchivoAccionesPersonalVacio.Text = "";

                foreach (HttpPostedFile certificado in certificadoAccionesPersonal)
                {
                    lblArchivoAccionesPersonalVacio.Text += certificado.FileName;
                }
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "activar", "levantarModalAgregarAccionesPersonal();", true);
        }
        protected void btnQuitarArchivoAccionesPersonal_Click(object sender, EventArgs e)
        {
            Session["sessionArchivoAccionesPersonal"] = null;
            fuCertificadoAccionesPersonal = null;
            btnQuitarArchivoAccionesPersonal.Visible = false;
            lblArchivoAccionesPersonalVacio.Text = "";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "activar", "levantarModalAgregarAccionesPersonal();", true);
        }
        protected void btnSeleccionarArchivoComprobantes_Click(object sender, EventArgs e)
        {
            Session["sessionComprobantes"] = fuCertificadoComprobantes;
            IList<HttpPostedFile> certificadoComprobantes = fuCertificadoComprobantes.PostedFiles;

            if (fuCertificadoComprobantes.HasFiles)
            {
                btnQuitarArchivoComprobantes.Visible = true;
                lblArchivoComprobantesVacio.Text = "";

                foreach (HttpPostedFile certificado in certificadoComprobantes)
                {
                    lblArchivoComprobantesVacio.Text += certificado.FileName;
                }
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "activar", "levantarModalAgregarComprobantes();", true);
        }
        protected void btnQuitarArchivoComprobantes_Click(object sender, EventArgs e)
        {
            Session["sessionArchivoComprobantes"] = null;
            fuCertificadoComprobantes = null;
            btnQuitarArchivoComprobantes.Visible = false;
            lblArchivoComprobantesVacio.Text = "";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "activar", "levantarModalAgregarComprobantes();", true);
        }
        protected void btnSeleccionarArchivoAntecedentes_Click(object sender, EventArgs e)
        {
            Session["sessionAccionesAntecedentes"] = fuCertificadoAntecedentes;
            IList<HttpPostedFile> certificadoAntecedentes = fuCertificadoAntecedentes.PostedFiles;

            if (fuCertificadoAntecedentes.HasFiles)
            {
                btnQuitarArchivoAntecedentes.Visible = true;
                lblArchivoAntecedentesVacio.Text = "";

                foreach (HttpPostedFile certificado in certificadoAntecedentes)
                {
                    lblArchivoAntecedentesVacio.Text += certificado.FileName;
                }
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "activar", "levantarModalAgregarAntecedentes();", true);
        }
        protected void btnQuitarArchivoAntecedentes_Click(object sender, EventArgs e)
        {
            Session["sessionArchivoAntecedentes"] = null;
            fuCertificadoAntecedentes = null;
            btnQuitarArchivoAntecedentes.Visible = false;
            lblArchivoAntecedentesVacio.Text = "";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "activar", "levantarModalAgregarAntecedentes();", true);
        }
        protected void btnSeleccionarArchivoEstudioAgregar_Click(object sender, EventArgs e)
        {
            Session["sessionArchivoEstudio"] = fuCertificadoEstudioAgregar;
            IList<HttpPostedFile> certificadoEstudios = fuCertificadoEstudioAgregar.PostedFiles;

            if (fuCertificadoEstudioAgregar.HasFiles)
            {
                btnQuitarArchivoEstudioAgregar.Visible = true;
                lblArchivoEstudioVacioAgregar.Text = "";

                foreach (HttpPostedFile certificado in certificadoEstudios)
                {
                    lblArchivoEstudioVacioAgregar.Text += certificado.FileName;
                }
                ckbEntregadoEstudioAgregar.Checked = true;

            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "activar", "levantarModalAgregarEstudio();", true);
        }
        protected void btnQuitarArchivoEstudioAgregar_Click(object sender, EventArgs e)
        {
            Session["sessionArchivoEstudio"] = null;
            fuCertificadoEstudioAgregar = null;
            btnQuitarArchivoEstudioAgregar.Visible = false;
            lblArchivoEstudioVacioAgregar.Text = "";
            ckbEntregadoEstudioAgregar.Checked = false;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "activar", "levantarModalAgregarEstudio();", true);
        }
        protected void btnSeleccionarArchivoCursoAgregar_Click(object sender, EventArgs e)
        {
            Session["sessionArchivoCurso"] = fuCertificadoCursoAgregar;
            IList<HttpPostedFile> certificadoCursos = fuCertificadoCursoAgregar.PostedFiles;

            if (fuCertificadoCursoAgregar.HasFiles)
            {
                btnQuitarArchivoCursoAgregar.Visible = true;
                lblArchivoCursoVacioAgregar.Text = "";

                foreach (HttpPostedFile curso in certificadoCursos)
                {
                    lblArchivoCursoVacioAgregar.Text += curso.FileName;
                }
                ckbEntregadoCursoAgregar.Checked = true;

            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "activar", "levantarModalAgregarCurso();", true);
        }
        protected void btnQuitarArchivoCursoAgregar_Click(object sender, EventArgs e)
        {
            Session["sessionArchivoCurso"] = null;
            fuCertificadoCursoAgregar = null;
            btnQuitarArchivoCursoAgregar.Visible = false;
            lblArchivoCursoVacioAgregar.Text = "";
            ckbEntregadoCursoAgregar.Checked = false;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "activar", "levantarModalAgregarCurso();", true);
        }
        protected void btnSeleccionarArchivoCertificadoAgregar_Click(object sender, EventArgs e)
        {
            Session["sessionArchivoCertificado"] = fuCertificadoCertificadoAgregar;
            IList<HttpPostedFile> certificadoCertificados = fuCertificadoCertificadoAgregar.PostedFiles;

            if (fuCertificadoCertificadoAgregar.HasFiles)
            {
                btnQuitarArchivoCertificadoAgregar.Visible = true;
                lblArchivoCertificadoVacioAgregar.Text = "";

                foreach (HttpPostedFile certificado in certificadoCertificados)
                {
                    lblArchivoCertificadoVacioAgregar.Text += certificado.FileName;
                }
                ckbEntregadoCertificadoAgregar.Checked = true;

            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "activar", "levantarModalAgregarCertificado();", true);
        }
        protected void btnQuitarArchivoCertificadoAgregar_Click(object sender, EventArgs e)
        {
            Session["sessionArchivoCertificado"] = null;
            fuCertificadoCertificadoAgregar = null;
            btnQuitarArchivoCertificadoAgregar.Visible = false;
            lblArchivoCertificadoVacioAgregar.Text = "";
            ckbEntregadoCertificadoAgregar.Checked = false;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "activar", "levantarModalAgregarCertificado();", true);
        }
        protected void btnSeleccionarArchivoPensionOEmbargoAgregar_Click(object sender, EventArgs e)
        {
            Session["sessionArchivoPensionOEmbargo"] = fuCertificadoPensionOEmbargoAgregar;
            IList<HttpPostedFile> certificadoPensionOEmbargos = fuCertificadoPensionOEmbargoAgregar.PostedFiles;

            if (fuCertificadoPensionOEmbargoAgregar.HasFiles)
            {
                btnQuitarArchivoPensionOEmbargoAgregar.Visible = true;
                lblArchivoPensionOEmbargoVacioAgregar.Text = "";

                foreach (HttpPostedFile certificado in certificadoPensionOEmbargos)
                {
                    lblArchivoPensionOEmbargoVacioAgregar.Text += certificado.FileName;
                }
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "activar", "levantarModalAgregarPensionOEmbargo();", true);
        }
        protected void btnQuitarArchivoPensionOEmbargoAgregar_Click(object sender, EventArgs e)
        {
            Session["sessionArchivoPensionOEmbargo"] = null;
            fuCertificadoPensionOEmbargoAgregar = null;
            btnQuitarArchivoPensionOEmbargoAgregar.Visible = false;
            lblArchivoPensionOEmbargoVacioAgregar.Text = "";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "activar", "levantarModalAgregarPensionOEmbargo();", true);
        }
        //protected void btnSeleccionarArchivoAccionesPersonalAgregar_Click(object sender, EventArgs e)
        //{
        //    Session["sessionAccionesPersonal"] = fuCertificadoAccionesPersonalAgregar;
        //    IList<HttpPostedFile> certificadoAccionesPersonal = fuCertificadoAccionesPersonalAgregar.PostedFiles;

        //    if (fuCertificadoAccionesPersonalAgregar.HasFiles)
        //    {
        //        btnQuitarArchivoAccionesPersonalAgregar.Visible = true;
        //        lblArchivoAccionesPersonalVacioAgregar.Text = "";

        //        foreach (HttpPostedFile certificado in certificadoAccionesPersonal)
        //        {
        //            lblArchivoAccionesPersonalVacioAgregar.Text += certificado.FileName;
        //        }
        //    }
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "activar", "levantarModalAgregarAccionesPersonal();", true);
        //}
        //protected void btnQuitarArchivoAccionesPersonalAgregar_Click(object sender, EventArgs e)
        //{
        //    Session["sessionArchivoAccionesPersonal"] = null;
        //    fuCertificadoAccionesPersonalAgregar = null;
        //    btnQuitarArchivoAccionesPersonalAgregar.Visible = false;
        //    lblArchivoAccionesPersonalVacioAgregar.Text = "";
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "activar", "levantarModalAgregarAccionesPersonal();", true);
        //}
        protected void btnSeleccionarArchivoComprobantesAgregar_Click(object sender, EventArgs e)
        {
            Session["sessionComprobantes"] = fuCertificadoComprobantesAgregar;
            IList<HttpPostedFile> certificadoComprobantes = fuCertificadoComprobantesAgregar.PostedFiles;

            if (fuCertificadoComprobantesAgregar.HasFiles)
            {
                btnQuitarArchivoComprobantesAgregar.Visible = true;
                lblArchivoComprobantesVacioAgregar.Text = "";

                foreach (HttpPostedFile certificado in certificadoComprobantes)
                {
                    lblArchivoComprobantesVacioAgregar.Text += certificado.FileName;
                }
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "activar", "levantarModalAgregarComprobantes();", true);
        }
        protected void btnQuitarArchivoComprobantesAgregar_Click(object sender, EventArgs e)
        {
            Session["sessionArchivoComprobantes"] = null;
            fuCertificadoComprobantesAgregar = null;
            btnQuitarArchivoComprobantesAgregar.Visible = false;
            lblArchivoComprobantesVacioAgregar.Text = "";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "activar", "levantarModalAgregarComprobantes();", true);
        }
        protected void btnSeleccionarArchivoAntecedentesAgregar_Click(object sender, EventArgs e)
        {
            Session["sessionAccionesAntecedentes"] = fuCertificadoAntecedentesAgregar;
            IList<HttpPostedFile> certificadoAntecedentes = fuCertificadoAntecedentesAgregar.PostedFiles;

            if (fuCertificadoAntecedentesAgregar.HasFiles)
            {
                btnQuitarArchivoAntecedentesAgregar.Visible = true;
                lblArchivoAntecedentesVacioAgregar.Text = "";

                foreach (HttpPostedFile certificado in certificadoAntecedentes)
                {
                    lblArchivoAntecedentesVacioAgregar.Text += certificado.FileName;
                }
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "activar", "levantarModalAgregarAntecedentes();", true);
        }
        protected void btnQuitarArchivoAntecedentesAgregar_Click(object sender, EventArgs e)
        {
            Session["sessionArchivoAntecedentes"] = null;
            fuCertificadoAntecedentesAgregar = null;
            btnQuitarArchivoAntecedentesAgregar.Visible = false;
            lblArchivoAntecedentesVacioAgregar.Text = "";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "activar", "levantarModalAgregarAntecedentes();", true);
        }

        protected void btnSeleccionarArchivoSuspensionPermisoAgregar_Click(object sender, EventArgs e)
        {
            Session["sessionArchivoSuspensionPermiso"] = fuArchivoSuspensionPermiso;
            IList<HttpPostedFile> archivoSuspensionPermiso = fuArchivoSuspensionPermiso.PostedFiles;

            if (fuArchivoSuspensionPermiso.HasFiles)
            {
                btnQuitarArchivoSuspensionPermisoAgregar.Visible = true;
                lblArchivoSuspensionPermisoVacioAgregar.Text = "";

                foreach (HttpPostedFile certificado in archivoSuspensionPermiso)
                {
                    lblArchivoSuspensionPermisoVacioAgregar.Text += certificado.FileName;
                }
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "activar", "levantarModalAgregarSuspensionPermiso();", true);
        }
        protected void btnQuitarArchivoSuspensionPermisoAgregar_Click(object sender, EventArgs e)
        {
            Session["sessionArchivoSuspensionPermiso"] = null;
            fuArchivoSuspensionPermiso = null;
            btnQuitarArchivoSuspensionPermisoAgregar.Visible = false;
            lblArchivoSuspensionPermisoVacioAgregar.Text = "";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "activar", "levantarModalAgregarSuspensionPermiso();", true);
        }
            
        #endregion

        #region Acciones TextChange
        /// <summary>
        /// Adrián Serrano
        /// 05/10/2021
        /// Efecto: Esta sección contiene los métodos TextChange para los TextBox
        /// Serie de funciones que se activan para mostrar un error en un campo cuando se ingrese texto
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //protected void txtIdentificacion_TextChanged(object sender, EventArgs e)
        //{
        //    txtIdentificacion.CssClass = "form-control";
        //    divIdentificacionIncorrecto.Style.Add("display", "none");
        //}
        //protected void txtNombre_TextChanged(object sender, EventArgs e)
        //{
        //    txtNombre.CssClass = "form-control";
        //    divNombreIncorrecto.Style.Add("display", "none");
        //}
        //protected void txtPrimerApellido_TextChanged(object sender, EventArgs e)
        //{
        //    txtPrimerApellido.CssClass = "form-control";
        //    divPrimerApellidoIncorrecto.Style.Add("display", "none");
        //}
        //protected void txtSegundoApellido_TextChanged(object sender, EventArgs e)
        //{
        //    txtSegundoApellido.CssClass = "form-control";
        //    divSegundoApellidoIncorrecto.Style.Add("display", "none");
        //}
        protected void txtNombreEstudio_TextChanged(object sender, EventArgs e)
        {
            txtNombreEstudio.CssClass = "form-control";
            divNombreEstudioIncorrecto.Style.Add("display", "none");
        }
        protected void txtNombreCurso_TextChanged(object sender, EventArgs e)
        {
            txtNombreCurso.CssClass = "form-control";
            divNombreCursoIncorrecto.Style.Add("display", "none");
        }
        protected void txtNombreCertificado_TextChanged(object sender, EventArgs e)
        {
            txtNombreCertificado.CssClass = "form-control";
            divNombreCertificadoIncorrecto.Style.Add("display", "none");
        }
        protected void txtDescripcionHabilidadBlanda_TextChanged(object sender, EventArgs e)
        {
            txtDescripcionHabilidadBlanda.CssClass = "form-control";
            divDescripcionHabilidadBlandaIncorrecto.Style.Add("display", "none");
        }
        protected void txtDescripcionInteresPersonal_TextChanged(object sender, EventArgs e)
        {
            txtDescripcionInteresPersonal.CssClass = "form-control";
            divDescripcionInteresPersonalIncorrecto.Style.Add("display", "none");
        }
        protected void txtNombreEmpresaExperienciaLaboral_TextChanged(object sender, EventArgs e)
        {
            txtNombreEmpresaExperienciaLaboralVerEditar.CssClass = "form-control";
            divNombreEmpresaExperienciaLaboralIncorrectoVerEditar.Style.Add("display", "none");
        }
        protected void txtNombreAccionesPersonal_TextChanged(object sender, EventArgs e)
        {
            txtNombreAccionesPersonal.CssClass = "form-control";
            divNombreAccionesPersonalIncorrecto.Style.Add("display", "none");
        }
        protected void txtNombreAntecedentes_TextChanged(object sender, EventArgs e)
        {
            txtNombreAntecedentes.CssClass = "form-control";
            divNombreAntecedentesIncorrecto.Style.Add("display", "none");
        }
        protected void txtDescripcionSuspensionPermiso_TextChanged(object sender, EventArgs e)
        {
            txtDescripcionSuspensionPermiso.CssClass = "form-control";
            divDescripcionSuspensionPermisoIncorrecto.Style.Add("display", "none");
        }
        protected void txtNombreEstudioAgregar_TextChanged(object sender, EventArgs e)
        {
            txtNombreEstudioAgregar.CssClass = "form-control";
            divNombreEstudioIncorrectoAgregar.Style.Add("display", "none");
        }
        protected void txtNombreCursoAgregar_TextChanged(object sender, EventArgs e)
        {
            txtNombreCursoAgregar.CssClass = "form-control";
            divNombreCursoIncorrectoAgregar.Style.Add("display", "none");
        }
        protected void txtNombreCertificadoAgregar_TextChanged(object sender, EventArgs e)
        {
            txtNombreCertificadoAgregar.CssClass = "form-control";
            divNombreCertificadoIncorrectoAgregar.Style.Add("display", "none");
        }
        protected void txtDescripcionHabilidadBlandaAgregar_TextChanged(object sender, EventArgs e)
        {
            txtDescripcionHabilidadBlandaAgregar.CssClass = "form-control";
            divDescripcionHabilidadBlandaIncorrectoAgregar.Style.Add("display", "none");
        }
        protected void txtDescripcionInteresPersonalAgregar_TextChanged(object sender, EventArgs e)
        {
            txtDescripcionInteresPersonalAgregar.CssClass = "form-control";
            divDescripcionInteresPersonalIncorrectoAgregar.Style.Add("display", "none");
        }
        //protected void txtNombreEmpresaExperienciaLaboralAgregar_TextChanged(object sender, EventArgs e)
        //{
        //    txtNombreEmpresaExperienciaLaboralAgregar.CssClass = "form-control";
        //    divNombreEmpresaExperienciaLaboralIncorrectoAgregar.Style.Add("display", "none");
        //}
        //protected void txtNombreAccionesPersonalAgregar_TextChanged(object sender, EventArgs e)
        //{
        //    txtNombreAccionesPersonalAgregar.CssClass = "form-control";
        //    divNombreAccionesPersonalIncorrectoAgregar.Style.Add("display", "none");
        //}
        protected void txtNombreAntecedentesAgregar_TextChanged(object sender, EventArgs e)
        {
            txtNombreAntecedentesAgregar.CssClass = "form-control";
            divNombreAntecedentesIncorrectoAgregar.Style.Add("display", "none");
        }
        protected void txtDescripcionSuspensionPermisoAgregar_TextChanged(object sender, EventArgs e)
        {
            txtDescripcionSuspensionPermisoAgregar.CssClass = "form-control";
            divDescripcionSuspensionPermisoIncorrectoAgregar.Style.Add("display", "none");
        }
        protected void TipoEstudioDDL_TextChanged(object sender, EventArgs e)
        {
            int idTipoEstudio = Convert.ToInt32(TipoEstudioDDL.SelectedValue);
            List<TipoEstudio> tiposEstudio = tipoEstudioDatos.ObtenerTodosEstudio();

            foreach (TipoEstudio tipoEstudio in tiposEstudio)
            {
                if (tipoEstudio.IdTipoEstudio == idTipoEstudio)
                {
                    if (tipoEstudio.Nombre.Contains("incompleta"))
                    {
                        CertificadoEstudioUpdatePanel.Visible = false;
                    }
                    else
                    {
                        CertificadoEstudioUpdatePanel.Visible = true;
                    }
                    break;
                }
            }
        }
        protected void TipoEstudioDDLAgregar_TextChanged(object sender, EventArgs e)
        {
            int idTipoEstudio = Convert.ToInt32(TipoEstudioDDLAgregar.SelectedValue);
            List<TipoEstudio> tiposEstudio = tipoEstudioDatos.ObtenerTodosEstudio();

            foreach (TipoEstudio tipoEstudio in tiposEstudio)
            {
                if (tipoEstudio.IdTipoEstudio == idTipoEstudio)
                {
                    if (tipoEstudio.Nombre.Contains("incompleta"))
                    {
                        CertificadoEstudioUpdatePanelAgregar.Visible = false;
                    }
                    else
                    {
                        CertificadoEstudioUpdatePanelAgregar.Visible = true;
                    }
                    break;
                }
            }
        }
        #endregion

        #region Acciones OnChange
        /// <summary>
        /// Adrián Serrano
        /// 05/10/2021
        /// Efecto: Esta sección contiene los métodos OnChange para los TextBox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //protected void txtFechaSalario_OnChanged(object sender, EventArgs e)
        //{
        //    txtFechaSalario.CssClass = "form-control";
        //    divFechaSalarioIncorrecto.Style.Add("display", "none");
        //}
        protected void txtFechaInicioEstudio_OnChanged(object sender, EventArgs e)
        {
            txtFechaInicioEstudio.CssClass = "form-control";
            divFechaInicioEstudioIncorrecto.Style.Add("display", "none");
        }
        protected void txtFechaInicioCurso_OnChanged(object sender, EventArgs e)
        {
            txtFechaInicioCurso.CssClass = "form-control";
            divFechaInicioCursoIncorrecto.Style.Add("display", "none");
        }
        protected void txtFechaInicioCertificado_OnChanged(object sender, EventArgs e)
        {
            txtFechaInicioCertificado.CssClass = "form-control";
            divFechaInicioCertificadoIncorrecto.Style.Add("display", "none");
        }
        //protected void txtFechaInicioExperienciaLaboral_OnChanged(object sender, EventArgs e)
        //{
        //    txtFechaInicioExperienciaLaboral.CssClass = "form-control";
        //    divFechaInicioExperienciaLaboralIncorrecto.Style.Add("display", "none");
        //}
        //protected void txtPeriodoAccionesPersonal_OnChanged(object sender, EventArgs e)
        //{
        //    txtPeriodoAccionesPersonal.CssClass = "form-control";
        //    divPeriodoAccionesPersonalIncorrecto.Style.Add("display", "none");
        //}
        protected void txtFechaComprobantes_OnChanged(object sender, EventArgs e)
        {
            txtFechaComprobantes.CssClass = "form-control";
            divFechaComprobantesIncorrecto.Style.Add("display", "none");
        }
        protected void txtFechaSalidaSuspensionPermiso_OnChanged(object sender, EventArgs e)
        {
            txtFechaSalidaSuspensionPermiso.CssClass = "form-control";
            divFechaSalidaSuspensionPermisoIncorrecto.Style.Add("display", "none");
        }
        protected void txtFechaInicioEstudioAgregar_OnChanged(object sender, EventArgs e)
        {
            txtFechaInicioEstudioAgregar.CssClass = "form-control";
            divFechaInicioEstudioIncorrectoAgregar.Style.Add("display", "none");
        }
        protected void txtFechaInicioCursoAgregar_OnChanged(object sender, EventArgs e)
        {
            txtFechaInicioCursoAgregar.CssClass = "form-control";
            divFechaInicioCursoIncorrectoAgregar.Style.Add("display", "none");
        }
        protected void txtFechaInicioCertificadoAgregar_OnChanged(object sender, EventArgs e)
        {
            txtFechaInicioCertificadoAgregar.CssClass = "form-control";
            divFechaInicioCertificadoIncorrectoAgregar.Style.Add("display", "none");
        }
        //protected void txtFechaInicioExperienciaLaboralAgregar_OnChanged(object sender, EventArgs e)
        //{
        //    txtFechaInicioExperienciaLaboralAgregar.CssClass = "form-control";
        //    divFechaInicioExperienciaLaboralIncorrectoAgregar.Style.Add("display", "none");
        //}
        //protected void txtPeriodoAccionesPersonalAgregar_OnChanged(object sender, EventArgs e)
        //{
        //    txtPeriodoAccionesPersonalAgregar.CssClass = "form-control";
        //    divPeriodoAccionesPersonalIncorrectoAgregar.Style.Add("display", "none");
        //}
        protected void txtFechaComprobantesAgregar_OnChanged(object sender, EventArgs e)
        {
            txtFechaComprobantesAgregar.CssClass = "form-control";
            divFechaComprobantesIncorrectoAgregar.Style.Add("display", "none");
        }
        protected void txtFechaSalidaSuspensionPermisoAgregar_OnChanged(object sender, EventArgs e)
        {
            txtFechaSalidaSuspensionPermisoAgregar.CssClass = "form-control";
            divFechaSalidaSuspensionPermisoIncorrectoAgregar.Style.Add("display", "none");
        }
        #endregion

        #region Funciones para ver la información en modales
        /// <summary>
        /// Adrián Serrano
        /// 09/11/2021
        /// Efecto: Metodo que se activa cuando se da click al boton de ver en cada una de las secciones
        /// </summary>
        protected void btnVerEstudio_Click(object sender, EventArgs e)
        {
            VerEditarEstudio(sender, false);
        }
        protected void btnVerCurso_Click(object sender, EventArgs e)
        {
            VerEditarCurso(sender, false);
        }
        protected void btnVerCertificado_Click(object sender, EventArgs e)
        {
            VerEditarCertificado(sender, false);
        }
        protected void btnVerExperienciaLaboral_Click(object sender, EventArgs e)
        {
            VerEditarExperienciaLaboral(sender, false);
        }
        protected void btnVerHabilidadBlanda_Click(object sender, EventArgs e)
        {
            VerEditarHabilidadBlanda(sender, false);
        }
        protected void btnVerInteresPersonal_Click(object sender, EventArgs e)
        {
            VerEditarInteresPersonal(sender, false);
        }
        //protected void btnVerSalario_Click(object sender, EventArgs e)
        //{
        //    VerEditarSalario(sender, false);
        //}
        protected void btnVerPensionesOEmbargos_Click(object sender, EventArgs e)
        {
            VerEditarPensionOEmbargo(sender, false);
        }
        protected void btnVerAccionPersonal_Click(object sender, EventArgs e)
        {
            VerEditarAccionPersonal(sender, false);
        }
        protected void btnVerComprobante_Click(object sender, EventArgs e)
        {
            VerEditarComprobante(sender, false);
        }
        protected void btnVerAntecedente_Click(object sender, EventArgs e)
        {
            VerEditarAntecedente(sender, false);
        }
        protected void btnVerSuspensionOPermiso_Click(object sender, EventArgs e)
        {
            VerEditarSuspensionPermiso(sender, false);
        }
        #endregion

        #region Funciones para levantar los modales para agregar información
        /// <summary>
        /// Adrián Serrano
        /// 05/10/2021
        /// Efecto: Esta sección contiene las funciones para levantar las ventanas
        /// modales.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnLevantarAgregarEstudio_Click(object sender, EventArgs e)
        {
            
            ScriptManager.RegisterStartupScript(this, this.GetType(), "activar", "levantarModalAgregarEstudio();", true);
        }
        protected void btnLevantarAgregarCurso_Click(object sender, EventArgs e)
        {
            
            ScriptManager.RegisterStartupScript(this, this.GetType(), "activar", "levantarModalAgregarCurso();", true);
        }
        protected void btnLevantarAgregarCertificado_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "activar", "levantarModalAgregarCertificado();", true);
        }
        protected void btnLevantarAgregarExperienciaLaboral_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "activar", "levantarModalAgregarExperienciaLaboral();", true);
        }
        protected void btnLevantarAgregarHabilidadBlanda_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "activar", "levantarModalAgregarHabilidadBlanda();", true);
        }
        protected void btnLevantarAgregarInteresPersonal_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "activar", "levantarModalAgregarInteresPersonal();", true);
        }
        protected void btnLevantarAgregarPensionOEmbargo_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "activar", "levantarModalAgregarPensionOEmbargo();", true);
        }
        protected void btnLevantarAgregarAccionesPersonal_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "activar", "levantarModalAgregarAccionesPersonal();", true);
        }
        protected void btnLevantarAgregarComprobantes_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "activar", "levantarModalAgregarComprobantes();", true);
        }
        protected void btnLevantarAgregarAntecedentes_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "activar", "levantarModalAgregarAntecedentes();", true);
        }
        protected void btnLevantarAgregarSuspensionPermiso_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "activar", "levantarModalAgregarSuspensionPermiso();", true);
        }
        #endregion

        #region Funciones para editar la información en modales
        /// <summary>
        /// Adrián Serrano
        /// 09/11/2021
        /// Efecto: Metodo que se activa cuando se da click al boton de editar en cada una de las secciones
        /// </summary>
        protected void btnEditarEstudio_Click(object sender, EventArgs e)
        {
            VerEditarEstudio(sender, true);
        }
        protected void btnEditarCurso_Click(object sender, EventArgs e)
        {
            VerEditarCurso(sender, true);
        }
        protected void btnEditarCertificado_Click(object sender, EventArgs e)
        {
            VerEditarCertificado(sender, true);
        }
        protected void btnEditarExperienciaLaboral_Click(object sender, EventArgs e)
        {
            VerEditarExperienciaLaboral(sender, true);
        }
        protected void btnEditarHabilidadBlanda_Click(object sender, EventArgs e)
        {
            VerEditarHabilidadBlanda(sender, true);
        }
        protected void btnEditarInteresPersonal_Click(object sender, EventArgs e)
        {
            VerEditarInteresPersonal(sender, true);
        }
        //protected void btnEditarSalario_Click(object sender, EventArgs e)
        //{
        //    VerEditarSalario(sender, true);
        //}
        protected void btnEditarPensionesOEmbargos_Click(object sender, EventArgs e)
        {
            VerEditarPensionOEmbargo(sender, true);
        }
        protected void btnEditarAccionPersonal_Click(object sender, EventArgs e)
        {
            VerEditarAccionPersonal(sender, true);
        }
        protected void btnEditarComprobante_Click(object sender, EventArgs e)
        {
            VerEditarComprobante(sender, true);
        }
        protected void btnEditarAntecedente_Click(object sender, EventArgs e)
        {
            VerEditarAntecedente(sender, true);
        }
        protected void btnEditarSuspensionOPermiso_Click(object sender, EventArgs e)
        {
            VerEditarSuspensionPermiso(sender, true);
        }
        #endregion

        #region Funciones para eliminar la información en modales
        /// <summary>
        /// Adrián Serrano
        /// 09/11/2021
        /// Efecto: Metodo que se activa cuando se da click al boton de eliminar en cada una de las secciones
        /// </summary>
        protected void btnEliminarEstudio_Click(object sender, EventArgs e)
        {
            int idEstudio = Convert.ToInt32((((LinkButton)(sender)).CommandArgument).ToString());
            int resultado = this.estudioDatos.Eliminar(idEstudio);
            if (resultado != -1)
            {
                Toastr("success", "El estudio se ha eliminado con éxito.");
                LlenarEstudios();
            }
            else
            {
                Toastr("error", "Se produjo un error inesperado. Consulte al administrador.");
            }
        }
        protected void btnEliminarCurso_Click(object sender, EventArgs e)
        {
            int idCurso = Convert.ToInt32((((LinkButton)(sender)).CommandArgument).ToString());
            int resultado = this.estudioDatos.Eliminar(idCurso);
            if (resultado != -1)
            {
                Toastr("success", "El curso se ha eliminado con éxito.");
                LlenarCursos();
            }
            else
            {
                Toastr("error", "Se produjo un error inesperado. Consulte al administrador.");
            }
        }
        protected void btnEliminarCertificado_Click(object sender, EventArgs e)
        {
            int idCertificado = Convert.ToInt32((((LinkButton)(sender)).CommandArgument).ToString());
            int resultado = this.estudioDatos.Eliminar(idCertificado);
            if (resultado != -1)
            {
                Toastr("success", "El certificado se ha eliminado con éxito.");
                LlenarCertificados();
            }
            else
            {
                Toastr("error", "Se produjo un error inesperado. Consulte al administrador.");
            }
        }
        protected void btnEliminarExperienciaLaboral_Click(object sender, EventArgs e)
        {
            int idExperienciaLaboral = Convert.ToInt32((((LinkButton)(sender)).CommandArgument).ToString());
            this.curriculumDatos.Eliminar(idExperienciaLaboral);
            int resultado = 0;
            if (resultado != -1)
            {
                Toastr("success", "El currículo vitae se ha eliminado con éxito.");
                LlenarExperiencia();
            }
            else
            {
                Toastr("error", "Se produjo un error inesperado. Consulte al administrador.");
            }
        }
        protected void btnEliminarHabilidadBlanda_Click(object sender, EventArgs e)
        {
            int idHabilidadBlanda = Convert.ToInt32((((LinkButton)(sender)).CommandArgument).ToString());
            int resultado = this.habilidadBlandaDatos.Eliminar(idHabilidadBlanda);
            if (resultado != -1)
            {
                Toastr("success", "La habilidad blanda se ha eliminado con éxito.");
                LlenarHabilidadBlanda();
            }
            else
            {
                Toastr("error", "Se produjo un error inesperado. Consulte al administrador.");
            }
        }
        protected void btnEliminarInteresPersonal_Click(object sender, EventArgs e)
        {
            int idInteresPersonal = Convert.ToInt32((((LinkButton)(sender)).CommandArgument).ToString());
            int resultado = this.interesPersonalDatos.Eliminar(idInteresPersonal);
            if (resultado != -1)
            {
                Toastr("success", "El interés personal se ha eliminado con éxito.");
                LlenarInteresPersonal();
            }
            else
            {
                Toastr("error", "Se produjo un error inesperado. Consulte al administrador.");
            }
        }
        protected void btnEliminarPensionesOEmbargos_Click(object sender, EventArgs e)
        {
            int idPensionOEmbargo = Convert.ToInt32((((LinkButton)(sender)).CommandArgument).ToString());
            int resultado = this.pensionOEmbargoDatos.Eliminar(idPensionOEmbargo);
            if (resultado != -1)
            {
                Toastr("success", "La pensión/embargo se ha eliminado con éxito.");
                LlenarPensionOEmbargo();
            }
            else
            {
                Toastr("error", "Se produjo un error inesperado. Consulte al administrador.");
            }
        }
        protected void btnEliminarAccionPersonal_Click(object sender, EventArgs e)
        {
            int idAccionPersonal = Convert.ToInt32((((LinkButton)(sender)).CommandArgument).ToString());
            /*int resultado =*/ this.documentoTramiteDatos.Eliminar(idAccionPersonal);
            //if (resultado != -1)
            //{
                Toastr("success", "La acción personal se ha eliminado con éxito.");
                LlenarAccionPersonal();
            //}
            //else
            //{
            //    Toastr("error", "Se produjo un error inesperado. Consulte al administrador.");
            //}
        }
        protected void btnEliminarComprobante_Click(object sender, EventArgs e)
        {
            int idComprobante = Convert.ToInt32((((LinkButton)(sender)).CommandArgument).ToString());
            int resultado = this.comprobantesDatos.Eliminar(idComprobante);
            if (resultado != -1)
            {
                Toastr("success", "El comprobante se ha eliminado con éxito.");
                LlenarComprobante();
            }
            else
            {
                Toastr("error", "Se produjo un error inesperado. Consulte al administrador.");
            }
        }
        protected void btnEliminarSuspensionPermiso_Click(object sender, EventArgs e)
        {
            int idSuspensionPermiso = Convert.ToInt32((((LinkButton)(sender)).CommandArgument).ToString());
            int resultado = this.suspensionPermisoDatos.Eliminar(idSuspensionPermiso);
            if (resultado != -1)
            {
                Toastr("success", "La suspensión/permiso se ha eliminado con éxito.");
                LlenarSuspensionPermiso();
            }
            else
            {
                Toastr("error", "Se produjo un error inesperado. Consulte al administrador.");
            }
        }
        protected void btnEliminarAntecedente_Click(object sender, EventArgs e)
        {
            int idAntecedente = Convert.ToInt32((((LinkButton)(sender)).CommandArgument).ToString());
            int resultado = this.antecedenteDatos.Eliminar(idAntecedente);
            if (resultado != -1)
            {
                Toastr("success", "El antecedente se ha eliminado con éxito.");
                LlenarAntecedente();
            }
            else
            {
                Toastr("error", "Se produjo un error inesperado. Consulte al administrador.");
            }
        }
        
        #endregion

        #region Funciones para ver los archivos
        /// <summary>
        /// Adrián Serrano
        /// 09/11/2021
        /// Efecto: Metodo que se activa cuando se da click al boton de ver el documento
        /// </summary>
        protected void btnVerCertificadoEstudio_Click(object sender, EventArgs e)
        {
            String[] infoArchivo = (((LinkButton)(sender)).CommandArgument).ToString().Split(',');
            String rutaArchivo = infoArchivo[0];
            String nombreArchivo = infoArchivo[1];
            FileStream fileStream = null;
            BinaryReader binaryReader = null;

            try
            {
                fileStream = new FileStream(rutaArchivo, FileMode.Open, FileAccess.Read);
                binaryReader = new BinaryReader(fileStream);
                Byte[] blobValue = binaryReader.ReadBytes(Convert.ToInt32(fileStream.Length));

                descargar(nombreArchivo, blobValue);

                fileStream.Close();
                binaryReader.Close();
            }
            catch (ArgumentException argumentException)
            {
                Estado.ErrorBitacora(argumentException.Message, "VerFuncionario.aspx.cs:btnVerCertificadoEstudio_Click():ArgumentException");
            }
            catch (NotSupportedException notSupportedException)
            {
                Estado.ErrorBitacora(notSupportedException.Message, "VerFuncionario.aspx.cs:btnVerCertificadoEstudio_Click():NotSupportedException");
            }
            catch (IOException iOException)
            {
                Estado.ErrorBitacora(iOException.Message, "VerFuncionario.aspx.cs:btnVerCertificadoEstudio_Click():IOException");
            }
            catch (UnauthorizedAccessException unauthorizedAccessException)
            {
                Estado.ErrorBitacora(unauthorizedAccessException.Message, "VerFuncionario.aspx.cs:btnVerCertificadoEstudio_Click():UnauthorizedAccessException");
            }
        }
        protected void btnVerCertificadoCurso_Click(object sender, EventArgs e)
        {
            String[] infoArchivo = (((LinkButton)(sender)).CommandArgument).ToString().Split(',');
            String rutaArchivo = infoArchivo[0];
            String nombreArchivo = infoArchivo[1];
            FileStream fileStream = null;
            BinaryReader binaryReader = null;

            try
            {
                fileStream = new FileStream(rutaArchivo, FileMode.Open, FileAccess.Read);
                binaryReader = new BinaryReader(fileStream);
                Byte[] blobValue = binaryReader.ReadBytes(Convert.ToInt32(fileStream.Length));

                descargar(nombreArchivo, blobValue);

                fileStream.Close();
                binaryReader.Close();
            }
            catch (ArgumentException argumentException)
            {
                Estado.ErrorBitacora(argumentException.Message, "VerFuncionario.aspx.cs:btnVerCertificadoCurso_Click():ArgumentException");
            }
            catch (NotSupportedException notSupportedException)
            {
                Estado.ErrorBitacora(notSupportedException.Message, "VerFuncionario.aspx.cs:btnVerCertificadoCurso_Click():NotSupportedException");
            }
            catch (IOException iOException)
            {
                Estado.ErrorBitacora(iOException.Message, "VerFuncionario.aspx.cs:btnVerCertificadoCurso_Click():IOException");
            }
            catch (UnauthorizedAccessException unauthorizedAccessException)
            {
                Estado.ErrorBitacora(unauthorizedAccessException.Message, "VerFuncionario.aspx.cs:btnVerCertificadoCurso_Click():UnauthorizedAccessException");
            }
        }
        protected void btnVerCertificadoCertificado_Click(object sender, EventArgs e)
        {
            String[] infoArchivo = (((LinkButton)(sender)).CommandArgument).ToString().Split(',');
            String rutaArchivo = infoArchivo[0];
            String nombreArchivo = infoArchivo[1];
            FileStream fileStream = null;
            BinaryReader binaryReader = null;

            try
            {
                fileStream = new FileStream(rutaArchivo, FileMode.Open, FileAccess.Read);
                binaryReader = new BinaryReader(fileStream);
                Byte[] blobValue = binaryReader.ReadBytes(Convert.ToInt32(fileStream.Length));

                descargar(nombreArchivo, blobValue);

                fileStream.Close();
                binaryReader.Close();
            }
            catch (ArgumentException argumentException)
            {
                Estado.ErrorBitacora(argumentException.Message, "VerFuncionario.aspx.cs:btnVerCertificadoCertificado_Click():ArgumentException");
            }
            catch (NotSupportedException notSupportedException)
            {
                Estado.ErrorBitacora(notSupportedException.Message, "VerFuncionario.aspx.cs:btnVerCertificadoCertificado_Click():NotSupportedException");
            }
            catch (IOException iOException)
            {
                Estado.ErrorBitacora(iOException.Message, "VerFuncionario.aspx.cs:btnVerCertificadoCertificado_Click():IOException");
            }
            catch (UnauthorizedAccessException unauthorizedAccessException)
            {
                Estado.ErrorBitacora(unauthorizedAccessException.Message, "VerFuncionario.aspx.cs:btnVerCertificadoCertificado_Click():UnauthorizedAccessException");
            }
        }
        protected void btnVerCertificadoSalario_Click(object sender, EventArgs e)
        {
            String[] infoArchivo = (((LinkButton)(sender)).CommandArgument).ToString().Split(',');
            String rutaArchivo = infoArchivo[0];
            String nombreArchivo = infoArchivo[1];
            FileStream fileStream = null;
            BinaryReader binaryReader = null;

            try
            {
                fileStream = new FileStream(rutaArchivo, FileMode.Open, FileAccess.Read);
                binaryReader = new BinaryReader(fileStream);
                Byte[] blobValue = binaryReader.ReadBytes(Convert.ToInt32(fileStream.Length));

                descargar(nombreArchivo, blobValue);

                fileStream.Close();
                binaryReader.Close();
            }
            catch (ArgumentException argumentException)
            {
                Estado.ErrorBitacora(argumentException.Message, "VerFuncionario.aspx.cs:btnVerCertificadoSalario_Click():ArgumentException");
            }
            catch (NotSupportedException notSupportedException)
            {
                Estado.ErrorBitacora(notSupportedException.Message, "VerFuncionario.aspx.cs:btnVerCertificadoSalario_Click():NotSupportedException");
            }
            catch (IOException iOException)
            {
                Estado.ErrorBitacora(iOException.Message, "VerFuncionario.aspx.cs:btnVerCertificadoSalario_Click():IOException");
            }
            catch (UnauthorizedAccessException unauthorizedAccessException)
            {
                Estado.ErrorBitacora(unauthorizedAccessException.Message, "VerFuncionario.aspx.cs:btnVerCertificadoSalario_Click():UnauthorizedAccessException");
            }
        }
        protected void btnVerCertificadoPensionOEmbargo_Click(object sender, EventArgs e)
        {
            String[] infoArchivo = (((LinkButton)(sender)).CommandArgument).ToString().Split(',');
            String rutaArchivo = infoArchivo[0];
            String nombreArchivo = infoArchivo[1];
            FileStream fileStream = null;
            BinaryReader binaryReader = null;

            try
            {
                fileStream = new FileStream(rutaArchivo, FileMode.Open, FileAccess.Read);
                binaryReader = new BinaryReader(fileStream);
                Byte[] blobValue = binaryReader.ReadBytes(Convert.ToInt32(fileStream.Length));

                descargar(nombreArchivo, blobValue);

                fileStream.Close();
                binaryReader.Close();
            }
            catch (ArgumentException argumentException)
            {
                Estado.ErrorBitacora(argumentException.Message, "VerFuncionario.aspx.cs:btnVerCertificadoPensionOEmbargo_Click():ArgumentException");
            }
            catch (NotSupportedException notSupportedException)
            {
                Estado.ErrorBitacora(notSupportedException.Message, "VerFuncionario.aspx.cs:btnVerCertificadoPensionOEmbargo_Click():NotSupportedException");
            }
            catch (IOException iOException)
            {
                Estado.ErrorBitacora(iOException.Message, "VerFuncionario.aspx.cs:btnVerCertificadoPensionOEmbargo_Click():IOException");
            }
            catch (UnauthorizedAccessException unauthorizedAccessException)
            {
                Estado.ErrorBitacora(unauthorizedAccessException.Message, "VerFuncionario.aspx.cs:btnVerCertificadoPensionOEmbargo_Click():UnauthorizedAccessException");
            }
        }
        protected void btnVerCertificadoAccionesPersonal_Click(object sender, EventArgs e)
        {
            String[] infoArchivo = (((LinkButton)(sender)).CommandArgument).ToString().Split(',');
            String rutaArchivo = infoArchivo[0];
            String nombreArchivo = infoArchivo[1];
            FileStream fileStream = null;
            BinaryReader binaryReader = null;

            try
            {
                fileStream = new FileStream(rutaArchivo, FileMode.Open, FileAccess.Read);
                binaryReader = new BinaryReader(fileStream);
                Byte[] blobValue = binaryReader.ReadBytes(Convert.ToInt32(fileStream.Length));

                descargar(nombreArchivo, blobValue);

                fileStream.Close();
                binaryReader.Close();
            }
            catch (ArgumentException argumentException)
            {
                Estado.ErrorBitacora(argumentException.Message, "VerFuncionario.aspx.cs:btnVerCertificadoAccionesPersonal_Click():ArgumentException");
            }
            catch (NotSupportedException notSupportedException)
            {
                Estado.ErrorBitacora(notSupportedException.Message, "VerFuncionario.aspx.cs:btnVerCertificadoAccionesPersonal_Click():NotSupportedException");
            }
            catch (IOException iOException)
            {
                Estado.ErrorBitacora(iOException.Message, "VerFuncionario.aspx.cs:btnVerCertificadoAccionesPersonal_Click():IOException");
            }
            catch (UnauthorizedAccessException unauthorizedAccessException)
            {
                Estado.ErrorBitacora(unauthorizedAccessException.Message, "VerFuncionario.aspx.cs:btnVerCertificadoAccionesPersonal_Click():UnauthorizedAccessException");
            }
        }
        protected void btnVerCertificadoComprobantes_Click(object sender, EventArgs e)
        {
            String[] infoArchivo = (((LinkButton)(sender)).CommandArgument).ToString().Split(',');
            String rutaArchivo = infoArchivo[0];
            String nombreArchivo = infoArchivo[1];
            FileStream fileStream = null;
            BinaryReader binaryReader = null;

            try
            {
                fileStream = new FileStream(rutaArchivo, FileMode.Open, FileAccess.Read);
                binaryReader = new BinaryReader(fileStream);
                Byte[] blobValue = binaryReader.ReadBytes(Convert.ToInt32(fileStream.Length));

                descargar(nombreArchivo, blobValue);

                fileStream.Close();
                binaryReader.Close();
            }
            catch (ArgumentException argumentException)
            {
                Estado.ErrorBitacora(argumentException.Message, "VerFuncionario.aspx.cs:btnVerCertificadoComprobantes_Click():ArgumentException");
            }
            catch (NotSupportedException notSupportedException)
            {
                Estado.ErrorBitacora(notSupportedException.Message, "VerFuncionario.aspx.cs:btnVerCertificadoComprobantes_Click():NotSupportedException");
            }
            catch (IOException iOException)
            {
                Estado.ErrorBitacora(iOException.Message, "VerFuncionario.aspx.cs:btnVerCertificadoComprobantes_Click():IOException");
            }
            catch (UnauthorizedAccessException unauthorizedAccessException)
            {
                Estado.ErrorBitacora(unauthorizedAccessException.Message, "VerFuncionario.aspx.cs:btnVerCertificadoComprobantes_Click():UnauthorizedAccessException");
            }
        }
        protected void btnVerCertificadoAntecedentes_Click(object sender, EventArgs e)
        {
            String[] infoArchivo = (((LinkButton)(sender)).CommandArgument).ToString().Split(',');
            String rutaArchivo = infoArchivo[0];
            String nombreArchivo = infoArchivo[1];
            FileStream fileStream = null;
            BinaryReader binaryReader = null;

            try
            {
                fileStream = new FileStream(rutaArchivo, FileMode.Open, FileAccess.Read);
                binaryReader = new BinaryReader(fileStream);
                Byte[] blobValue = binaryReader.ReadBytes(Convert.ToInt32(fileStream.Length));

                descargar(nombreArchivo, blobValue);

                fileStream.Close();
                binaryReader.Close();
            }
            catch (ArgumentException argumentException)
            {
                Estado.ErrorBitacora(argumentException.Message, "VerFuncionario.aspx.cs:btnVerCertificadoAntecedentes_Click():ArgumentException");
            }
            catch (NotSupportedException notSupportedException)
            {
                Estado.ErrorBitacora(notSupportedException.Message, "VerFuncionario.aspx.cs:btnVerCertificadoAntecedentes_Click():NotSupportedException");
            }
            catch (IOException iOException)
            {
                Estado.ErrorBitacora(iOException.Message, "VerFuncionario.aspx.cs:btnVerCertificadoAntecedentes_Click():IOException");
            }
            catch (UnauthorizedAccessException unauthorizedAccessException)
            {
                Estado.ErrorBitacora(unauthorizedAccessException.Message, "VerFuncionario.aspx.cs:btnVerCertificadoAntecedentes_Click():UnauthorizedAccessException");
            }
        }
        protected void btnVerArchivoSuspensionPermiso_Click(object sender, EventArgs e)
        {
            String[] infoArchivo = (((LinkButton)(sender)).CommandArgument).ToString().Split(',');
            String rutaArchivo = infoArchivo[0];
            String nombreArchivo = infoArchivo[1];
            FileStream fileStream = null;
            BinaryReader binaryReader = null;

            try
            {
                fileStream = new FileStream(rutaArchivo, FileMode.Open, FileAccess.Read);
                binaryReader = new BinaryReader(fileStream);
                Byte[] blobValue = binaryReader.ReadBytes(Convert.ToInt32(fileStream.Length));

                descargar(nombreArchivo, blobValue);

                fileStream.Close();
                binaryReader.Close();
            }
            catch (ArgumentException argumentException)
            {
                Estado.ErrorBitacora(argumentException.Message, "VerFuncionario.aspx.cs:btnVerArchivoSuspensionPermiso_Click():ArgumentException");
            }
            catch (NotSupportedException notSupportedException)
            {
                Estado.ErrorBitacora(notSupportedException.Message, "VerFuncionario.aspx.cs:btnVerArchivoSuspensionPermiso_Click():NotSupportedException");
            }
            catch (IOException iOException)
            {
                Estado.ErrorBitacora(iOException.Message, "VerFuncionario.aspx.cs:btnVerArchivoSuspensionPermiso_Click():IOException");
            }
            catch (UnauthorizedAccessException unauthorizedAccessException)
            {
                Estado.ErrorBitacora(unauthorizedAccessException.Message, "VerFuncionario.aspx.cs:btnVerArchivoSuspensionPermiso_Click():UnauthorizedAccessException");
            }
        }
        #endregion

        #region Funciones para guardar los cambios de la edición
        /// <summary>
        /// Adrián Serrano
        /// 24/11/2021
        /// Efecto: Metodo que se activa cuando se da click al boton guardar los cambios en los modales
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAgregarCambiosEstudio_Click(object sender, EventArgs e)
        {
            if (ValidarCamposEstudio())
            {
                HttpPostedFile certificadoEstudio = fuCertificadoEstudio.PostedFile;
                DateTime fechaActual = DateTime.Now;
                string identificacion = txtIdentificacion.Text;
                string rutaDocumento = "";
                string nombreDocumento = "";

                if (Session["rutaDocumentoEstudio"] != null)
                {
                    rutaDocumento = Session["rutaDocumentoEstudio"].ToString();
                }
                if (Session["nombreDocumentoEstudio"] != null)
                {
                    nombreDocumento = Session["nombreDocumentoEstudio"].ToString();
                }

                if (certificadoEstudio != null)
                {
                    ///Se agrega un certificado nuevo, que va a sustituir al actual
                    String nombreArchivo = Path.GetFileName(certificadoEstudio.FileName);
                    nombreDocumento = nombreArchivo;
                    nombreArchivo = "estudio" + "_" + fechaActual.ToString().Replace("/", "").Replace(" ", "").Replace(":", "")
                        + "_" + identificacion + "_" + nombreArchivo.Replace(' ', '_');

                    Boolean guardado = Utilidades.GuardarDocumentos(certificadoEstudio, nombreArchivo);

                    if (guardado)
                    {
                        rutaDocumento = Utilidades.archivos_path + "\\" + nombreArchivo;
                    }
                    else
                    {
                        Toastr("error", "Se produjo un error al guardar el documento. Consulte al administrador.");
                    }
                }

                Estudio estudio = new Estudio();
                estudio.Nombre = txtNombreEstudio.Text;
                estudio.RutaDocumento = rutaDocumento;
                estudio.NombreDocumento = nombreDocumento;
                estudio.FechaInicio = Convert.ToDateTime(txtFechaInicioEstudio.Text);

                DateTime fechaFinalizacion = new DateTime(1900, 01, 01);
                if (txtFechaFinalizacionEstudio.Text.Trim() != "")
                {
                    fechaFinalizacion = Convert.ToDateTime(txtFechaFinalizacionEstudio.Text.Trim());
                }

                estudio.FechaFinalizacion = fechaFinalizacion;
                estudio.Observacion = txtObservacionesEstudio.Text;
                estudio.Entregado = ckbEntregadoEstudio.Checked;
                estudio.Funcionario = new Funcionario(identificacion);
                estudio.TipoEstudio = new TipoEstudio(Convert.ToInt32(TipoEstudioDDL.SelectedValue));

                int resultado = 0;
                if (Session["idEstudio"] != null)
                {
                    estudio.IdEstudio = Convert.ToInt32(Session["idEstudio"]);
                    resultado = this.estudioDatos.Actualizar(estudio);
                    Session["idEstudio"] = null;
                }

                if (resultado > 0)
                {
                    Toastr("success", "El estudio fue actualizado con éxito.");

                    List<Estudio> listaEstudios = estudioDatos.ObtenerPorId("estudio", identificacion);

                    rpEstudios.DataSource = listaEstudios;
                    rpEstudios.DataBind();

                    txtNombreEstudio.Text = "";
                    txtFechaInicioEstudio.Text = "";
                    txtFechaFinalizacionEstudio.Text = "";
                    txtObservacionesEstudio.Text = "";
                    ckbEntregadoEstudio.Checked = false;

                    Session["sessionArchivoEstudio"] = null;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "activar", "cerrarModalAgregarEstudio();", true);
                }
                else
                {
                    Toastr("error", "Se produjo un error inesperado. Consulte al administrador.");
                }
            }
        }
        protected void btnAgregarCambiosCurso_Click(object sender, EventArgs e)
        {
            if (ValidarCamposCurso())
            {
                HttpPostedFile certificadoCurso = fuCertificadoCurso.PostedFile;
                DateTime fechaActual = DateTime.Now;
                string identificacion = txtIdentificacion.Text;
                string rutaDocumento = "";
                string nombreDocumento = "";

                if (Session["rutaDocumentoCurso"] != null)
                {
                    rutaDocumento = Session["rutaDocumentoCurso"].ToString();
                }
                if (Session["nombreDocumentoCurso"] != null)
                {
                    nombreDocumento = Session["nombreDocumentoCurso"].ToString();
                }

                if (certificadoCurso != null)
                {
                    String nombreArchivo = Path.GetFileName(certificadoCurso.FileName);
                    nombreDocumento = nombreArchivo;
                    nombreArchivo = "curso" + "_" + fechaActual.ToString().Replace("/", "").Replace(" ", "").Replace(":", "")
                        + "_" + identificacion + "_" + nombreArchivo.Replace(' ', '_');

                    Boolean guardado = Utilidades.GuardarDocumentos(certificadoCurso, nombreArchivo);

                    if (guardado)
                    {
                        rutaDocumento = Utilidades.archivos_path + "\\" + nombreArchivo;
                    }
                    else
                    {
                        Toastr("error", "Se produjo un error al guardar el documento. Consulte al administrador.");
                    }
                }

                Estudio estudio = new Estudio();
                estudio.Nombre = txtNombreCurso.Text;
                estudio.RutaDocumento = rutaDocumento;
                estudio.NombreDocumento = nombreDocumento;
                estudio.FechaInicio = Convert.ToDateTime(txtFechaInicioCurso.Text);

                DateTime fechaFinalizacion = new DateTime(1900, 01, 01);
                if (txtFechaFinalizacionCurso.Text.Trim() != "")
                {
                    fechaFinalizacion = Convert.ToDateTime(txtFechaFinalizacionCurso.Text.Trim());
                }

                estudio.FechaFinalizacion = fechaFinalizacion;
                estudio.Observacion = txtObservacionesCurso.Text;
                estudio.Entregado = ckbEntregadoCurso.Checked;
                estudio.Funcionario = new Funcionario(identificacion);

                // 10 es el ID para cursos en la base de datos
                estudio.TipoEstudio = new TipoEstudio(10);

                int resultado = 0;
                if (Session["idCurso"] != null)
                {
                    estudio.IdEstudio = Convert.ToInt32(Session["idCurso"]);
                    resultado = this.estudioDatos.Actualizar(estudio);
                    Session["idCurso"] = null;
                }

                if (resultado > 0)
                {
                    Toastr("success", "El curso fue actualizado con éxito.");

                    List<Estudio> listaCursos = this.estudioDatos.ObtenerPorId("curso", identificacion);

                    rpCursos.DataSource = listaCursos;
                    rpCursos.DataBind();

                    txtNombreCurso.Text = "";
                    txtFechaInicioCurso.Text = "";
                    txtFechaFinalizacionCurso.Text = "";
                    txtObservacionesCurso.Text = "";
                    ckbEntregadoCurso.Checked = false;

                    Session["sessionArchivoCurso"] = null;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "activar", "cerrarModalAgregarCurso();", true);
                }
                else
                {
                    Toastr("error", "Se produjo un error inesperado. Consulte al administrador.");
                }
            }
        }
        protected void btnAgregarCambiosCertificado_Click(object sender, EventArgs e)
        {
            if (ValidarCamposCertificado())
            {
                HttpPostedFile certificadoCertificado = fuCertificadoCertificado.PostedFile;
                DateTime fechaActual = DateTime.Now;
                string identificacion = txtIdentificacion.Text;
                string rutaDocumento = "";
                string nombreDocumento = "";

                if (Session["rutaDocumentoCertificado"] != null)
                {
                    rutaDocumento = Session["rutaDocumentoCertificado"].ToString();
                }
                if (Session["nombreDocumentoCertificado"] != null)
                {
                    nombreDocumento = Session["nombreDocumentoCertificado"].ToString();
                }

                if (certificadoCertificado != null)
                {
                    String nombreArchivo = Path.GetFileName(certificadoCertificado.FileName);
                    nombreDocumento = nombreArchivo;
                    nombreArchivo = "certificado" + "_" + fechaActual.ToString().Replace("/", "").Replace(" ", "").Replace(":", "")
                        + "_" + identificacion + "_" + nombreArchivo.Replace(' ', '_');

                    Boolean guardado = Utilidades.GuardarDocumentos(certificadoCertificado, nombreArchivo);

                    if (guardado)
                    {
                        rutaDocumento = Utilidades.archivos_path + "\\" + nombreArchivo;
                    }
                    else
                    {
                        Toastr("error", "Se produjo un error al guardar el documento. Consulte al administrador.");
                    }
                }

                Estudio estudio = new Estudio();
                estudio.Nombre = txtNombreCertificado.Text;
                estudio.RutaDocumento = rutaDocumento;
                estudio.NombreDocumento = nombreDocumento;
                estudio.FechaInicio = Convert.ToDateTime(txtFechaInicioCertificado.Text);

                DateTime fechaFinalizacion = new DateTime(1900, 01, 01);
                if (txtFechaFinalizacionCertificado.Text.Trim() != "")
                {
                    fechaFinalizacion = Convert.ToDateTime(txtFechaFinalizacionCertificado.Text.Trim());
                }

                estudio.FechaFinalizacion = fechaFinalizacion;
                estudio.Observacion = txtObservacionesCertificado.Text;
                estudio.Entregado = ckbEntregadoCertificado.Checked;
                estudio.Funcionario = new Funcionario(identificacion);

                // 11 es el ID para certificados en la base de datos
                estudio.TipoEstudio = new TipoEstudio(11);

                int resultado = 0;
                if (Session["idCertificado"] != null)
                {
                    estudio.IdEstudio = Convert.ToInt32(Session["idCertificado"]);
                    resultado = this.estudioDatos.Actualizar(estudio);
                    Session["idCertificado"] = null;
                }

                if (resultado > 0)
                {
                    Toastr("success", "El certificado fue actualizado con éxito.");

                    List<Estudio> listaCertificados = this.estudioDatos.ObtenerPorId("certificado", identificacion);

                    rpCertificados.DataSource = listaCertificados;
                    rpCertificados.DataBind();

                    txtNombreCertificado.Text = "";
                    txtFechaInicioCertificado.Text = "";
                    txtFechaFinalizacionCertificado.Text = "";
                    txtObservacionesCertificado.Text = "";
                    ckbEntregadoCertificado.Checked = false;

                    Session["sessionArchivoCertificado"] = null;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "activar", "cerrarModalAgregarCertificado();", true);
                }
                else
                {
                    Toastr("error", "Se produjo un error inesperado. Consulte al administrador.");
                }
            }
        }
        protected void btnAgregarCambiosExperienciaLaboral_Click(object sender, EventArgs e)
        {
            if (ValidarCamposExperienciaLaboral())
            {
                HttpPostedFile curriculum = fuCurriculumVerEditar.PostedFile;
                DateTime fechaActual = DateTime.Now;
                string identificacion = txtIdentificacion.Text;
                string rutaDocumento = "";
                string nombreDocumento = "";

                if (Session["rutaCurriculum"] != null)
                {
                    rutaDocumento = Session["rutaCurriculum"].ToString();
                }
                if (Session["nombreCurriclum"] != null)
                {
                    nombreDocumento = Session["nombreCurriculum"].ToString();
                }

                if (curriculum != null)
                {
                    String nombreArchivo = Path.GetFileName(curriculum.FileName);
                    nombreDocumento = nombreArchivo;
                    nombreArchivo = "curriculo_vitae" + "_" + fechaActual.ToString().Replace("/", "").Replace(" ", "").Replace(":", "")
                        + "_" + identificacion + "_" + nombreArchivo.Replace(' ', '_');

                    Boolean guardado = Utilidades.GuardarDocumentos(curriculum, nombreArchivo);

                    if (guardado)
                    {
                        rutaDocumento = Utilidades.archivos_path + "\\" + nombreArchivo;
                    }
                    else
                    {
                        Toastr("error", "Se produjo un error al guardar el documento. Consulte al administrador.");
                    }
                }

                Curriculum curriculoVitae = new Curriculum();
                curriculoVitae.nombre = txtNombreEmpresaExperienciaLaboralVerEditar.Text;
                curriculoVitae.descripcion = txtDescripcionPuestoExperienciaLaboralVerEditar.Text;
                curriculoVitae.ruta = rutaDocumento;
                curriculoVitae.funcionario = new Funcionario(identificacion);

                int resultado = 0;
                if (Session["idExperienciaLaboral"] != null)
                {
                    curriculoVitae.idCurriculum = Convert.ToInt32(Session["idExperienciaLaboral"]);
                    this.curriculumDatos.Actualizar(curriculoVitae);
                    resultado = 1;
                    Session["idExperienciaLaboral"] = null;
                }

                if (resultado > 0)
                {
                    Toastr("success", "El currículo vitae fue actualizada con éxito.");

                    List<Curriculum> listaExperienciasLaborales = this.curriculumDatos.ObtenerPorId(identificacion);

                    rpExperienciaLaboral.DataSource = listaExperienciasLaborales;
                    rpExperienciaLaboral.DataBind();

                    txtNombreEmpresaExperienciaLaboralVerEditar.Text = "";
                    txtDescripcionPuestoExperienciaLaboralVerEditar.Text = "";
                    Session["sessionArchivoCurriculum"] = null;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "activar", "cerrarModalVerEditarExperienciaLaboral();", true);
                }
                else
                {
                    Toastr("error", "Se produjo un error inesperado. Consulte al administrador.");
                }
            }
        }
        protected void btnAgregarCambiosHabilidadBlanda_Click(object sender, EventArgs e)
        {
            if (ValidarCamposHabilidadBlanda())
            {
                HabilidadBlanda habilidadBlanda = new HabilidadBlanda();
                habilidadBlanda.Descripcion = txtDescripcionHabilidadBlanda.Text;
                string identificacion = txtIdentificacion.Text;
                habilidadBlanda.Funcionario = new Funcionario(identificacion);

                int resultado = 0;
                if (Session["idHabilidadBlanda"] != null)
                {
                    habilidadBlanda.IdHabilidadBlanda = Convert.ToInt32(Session["idHabilidadBlanda"]);
                    resultado = this.habilidadBlandaDatos.Actualizar(habilidadBlanda);
                    Session["idHabilidadBlanda"] = null;
                }

                if (resultado > 0)
                {
                    Toastr("success", "La habilidad blanda fue actualizada con éxito.");

                    List<HabilidadBlanda> listaHabilidadesBlandas = this.habilidadBlandaDatos.ObtenerPorId(identificacion);

                    rpHabilidadBlanda.DataSource = listaHabilidadesBlandas;
                    rpHabilidadBlanda.DataBind();

                    txtDescripcionHabilidadBlanda.Text = "";

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "activar", "cerrarModalAgregarHabilidadBlanda();", true);
                }
                else
                {
                    Toastr("error", "Se produjo un error inesperado. Consulte al administrador.");
                }
            }
        }
        protected void btnAgregarCambiosInteresPersonal_Click(object sender, EventArgs e)
        {
            if (ValidarCamposInteresPersonal())
            {
                InteresPersonal interesPersonal = new InteresPersonal();
                interesPersonal.Descripcion = txtDescripcionInteresPersonal.Text;
                string identificacion = txtIdentificacion.Text;
                interesPersonal.Funcionario = new Funcionario(identificacion);

                int resultado = 0;
                if (Session["idInteresPersonal"] != null)
                {
                    interesPersonal.IdInteresPersonal = Convert.ToInt32(Session["idInteresPersonal"]);
                    resultado = this.interesPersonalDatos.Actualizar(interesPersonal);
                    Session["idInteresPersonal"] = null;
                }

                if (resultado > 0)
                {
                    Toastr("success", "El interés personal fue actualizado con éxito.");

                    List<InteresPersonal> listaInteresesPersonales = this.interesPersonalDatos.ObtenerPorId(identificacion);

                    rpInteresesPersonales.DataSource = listaInteresesPersonales;
                    rpInteresesPersonales.DataBind();

                    txtDescripcionInteresPersonal.Text = "";

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "activar", "cerrarModalAgregarInteresPersonal();", true);
                }
                else
                {
                    Toastr("error", "Se produjo un error inesperado. Consulte al administrador.");
                }
            }
        }
        protected void btnAgregarCambiosPensionOEmbargo_Click(object sender, EventArgs e)
        {
            if (ValidarCamposPensionOEmbargo())
            {
                HttpPostedFile certificadoPensionOEmbargo = fuCertificadoPensionOEmbargo.PostedFile;
                DateTime fechaActual = DateTime.Now;
                string identificacion = txtIdentificacion.Text;
                string rutaDocumento = "";
                string nombreDocumento = "";

                if (Session["rutaDocumentoPensionOEmbargo"] != null)
                {
                    rutaDocumento = Session["rutaDocumentoPensionOEmbargo"].ToString();
                }
                if (Session["nombreDocumentoPensionOEmbargo"] != null)
                {
                    nombreDocumento = Session["nombreDocumentoPensionOEmbargo"].ToString();
                }

                if (certificadoPensionOEmbargo != null)
                {
                    String nombreArchivo = Path.GetFileName(certificadoPensionOEmbargo.FileName);
                    nombreDocumento = nombreArchivo;
                    nombreArchivo = "pension_o_embargo" + "_" + fechaActual.ToString().Replace("/", "").Replace(" ", "").Replace(":", "")
                        + "_" + identificacion + "_" + nombreArchivo.Replace(' ', '_');

                    Boolean guardado = Utilidades.GuardarDocumentos(certificadoPensionOEmbargo, nombreArchivo);

                    if (guardado)
                    {
                        rutaDocumento = Utilidades.archivos_path + "\\" + nombreArchivo;
                    }
                    else
                    {
                        Toastr("error", "Se produjo un error al guardar el documento. Consulte al administrador.");
                    }
                }

                PensionOEmbargo pensionOEmbargo = new PensionOEmbargo();
                pensionOEmbargo.RutaDocumento = rutaDocumento;
                pensionOEmbargo.NombreDocumento = nombreDocumento;
                pensionOEmbargo.Descripcion = txtDescripcionPensionOEmbargo.Text;
                pensionOEmbargo.Funcionario = new Funcionario(identificacion);

                int resultado = 0;
                if (Session["idPensionOEmbargo"] != null)
                {
                    pensionOEmbargo.IdPensionOEmbargo = Convert.ToInt32(Session["idPensionOEmbargo"]);
                    resultado = this.pensionOEmbargoDatos.Actualizar(pensionOEmbargo);
                    Session["idPensionOEmbargo"] = null;
                }

                if (resultado > 0)
                {
                    Toastr("success", "La pensión/embargo fue actualizado con éxito.");

                    List<PensionOEmbargo> listaPensionOEmbargos = this.pensionOEmbargoDatos.ObtenerPorId(identificacion);

                    rpPensionOEmbargos.DataSource = listaPensionOEmbargos;
                    rpPensionOEmbargos.DataBind();

                    txtDescripcionPensionOEmbargo.Text = "";

                    Session["sessionArchivoPensionOEmbargo"] = null;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "activar", "cerrarModalAgregarPensionOEmbargo();", true);
                }
                else
                {
                    Toastr("error", "Se produjo un error inesperado. Consulte al administrador.");
                }
            }
        }
        protected void btnAgregarCambiosAccionesPersonal_Click(object sender, EventArgs e)
        {
            
                HttpPostedFile certificadoAccionesPersonal = fuCertificadoAccionesPersonalVerEditar.PostedFile;
                DateTime fechaActual = DateTime.Now;
                string identificacion = txtIdentificacion.Text;
                string rutaDocumento = "";
                string nombreDocumento = "";

                if (Session["rutaDocumentoAccionPersonal"] != null)
                {
                    rutaDocumento = Session["rutaDocumentoAccionPersonal"].ToString();
                }
                if (Session["nombreDocumentoAccionPersonal"] != null)
                {
                    nombreDocumento = Session["nombreDocumentoAccionPersonal"].ToString();
                }

                if (certificadoAccionesPersonal != null)
                {
                    String nombreArchivo = Path.GetFileName(certificadoAccionesPersonal.FileName);
                    nombreDocumento = nombreArchivo;
                    nombreArchivo = "accion_de_personal" + "_" + fechaActual.ToString().Replace("/", "").Replace(" ", "").Replace(":", "")
                        + "_" + identificacion + "_" + nombreArchivo.Replace(' ', '_');

                    Boolean guardado = Utilidades.GuardarDocumentos(certificadoAccionesPersonal, nombreArchivo);

                    if (guardado)
                    {
                        rutaDocumento = Utilidades.archivos_path + "\\" + nombreArchivo;
                    }
                    else
                    {
                        Toastr("error", "Se produjo un error al guardar el documento. Consulte al administrador.");
                    }
                }

                DocumentoTramite accionPersonal = new DocumentoTramite();
                accionPersonal.nombreDocumento = txtNombreAccionesPersonalVerEditar.Text.Trim();
                //accionPersonal.Periodo = Convert.ToDateTime(txtPeriodoAccionesPersonalVerEditar.Text);
                accionPersonal.descripcion = txtDescripcionAccionesPersonalVerEditar.Text;
                accionPersonal.rutaDocumento = rutaDocumento;
                accionPersonal.nombreDocumento = nombreDocumento;
                accionPersonal.funcionario = new Funcionario(identificacion);
                accionPersonal.numero = txtNumeroVerEditar.Text;
                //accionPersonal.TipoAccionPersonal = new TipoAccionPersonal(Convert.ToInt32(TipoAccionesPersonalDDLVerEditar.SelectedValue));

                int resultado = 0;
                if (Session["idAccionPersonal"] != null)
                {
                    accionPersonal.idDocumentoTramite = Convert.ToInt32(Session["idAccionPersonal"]);
                    /*resultado =*/
                    this.documentoTramiteDatos.Actualizar(accionPersonal);
                    resultado = 1;
                    Session["idAccionPersonal"] = null;
                }

                if (resultado > 0)
                {
                    Toastr("success", "La acción personal fue actualizada con éxito.");

                    List<DocumentoTramite> listaAccionesPersonal = this.documentoTramiteDatos.ObtenerPorId(identificacion);

                    rpAccionesPersonal.DataSource = listaAccionesPersonal;
                    rpAccionesPersonal.DataBind();

                    txtNombreAccionesPersonalVerEditar.Text = "";
                    txtDescripcionAccionesPersonalVerEditar.Text = "";

                    Session["sessionArchivoAccionesPersonal"] = null;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "activar", "cerrarModalVerEditarAccionesPersonal();", true);
                }
                else
                {
                    Toastr("error", "Se produjo un error inesperado. Consulte al administrador.");
                }
            
        }
        protected void btnAgregarCambiosComprobantes_Click(object sender, EventArgs e)
        {
            if (ValidarCamposComprobantes())
            {
                HttpPostedFile certificadoComprobantes = fuCertificadoComprobantes.PostedFile;
                DateTime fechaActual = DateTime.Now;
                string identificacion = txtIdentificacion.Text;
                string rutaDocumento = "";
                string nombreDocumento = "";

                if (Session["rutaDocumentoComprobante"] != null)
                {
                    rutaDocumento = Session["rutaDocumentoComprobante"].ToString();
                }
                if (Session["nombreDocumentoComprobante"] != null)
                {
                    nombreDocumento = Session["nombreDocumentoComprobante"].ToString();
                }

                if (certificadoComprobantes != null)
                {
                    String nombreArchivo = Path.GetFileName(certificadoComprobantes.FileName);
                    nombreDocumento = nombreArchivo;
                    nombreArchivo = "comprobante" + "_" + fechaActual.ToString().Replace("/", "").Replace(" ", "").Replace(":", "")
                        + "_" + identificacion + "_" + nombreArchivo.Replace(' ', '_');

                    Boolean guardado = Utilidades.GuardarDocumentos(certificadoComprobantes, nombreArchivo);

                    if (guardado)
                    {
                        rutaDocumento = Utilidades.archivos_path + "\\" + nombreArchivo;
                    }
                    else
                    {
                        Toastr("error", "Se produjo un error al guardar el documento. Consulte al administrador.");
                    }
                }

                Comprobante comprobante = new Comprobante();
                comprobante.Fecha = Convert.ToDateTime(txtFechaComprobantes.Text);
                comprobante.Descripcion = txtDescripcionComprobantes.Text;
                comprobante.RutaDocumento = rutaDocumento;
                comprobante.NombreDocumento = nombreDocumento;
                comprobante.Funcionario = new Funcionario(identificacion);
                comprobante.TipoComprobante = new TipoComprobante(Convert.ToInt32(TipoComprobantesDDL.SelectedValue));

                int resultado = 0;
                if (Session["idComprobante"] != null)
                {
                    comprobante.IdComprobante = Convert.ToInt32(Session["idComprobante"]);
                    resultado = this.comprobantesDatos.Actualizar(comprobante);
                    Session["idComprobante"] = null;
                }

                if (resultado > 0)
                {
                    Toastr("success", "El comprobante fue actualizado con éxito.");

                    List<Comprobante> listaComprobantes = this.comprobantesDatos.ObtenerPorId(identificacion);

                    rpComprobantes.DataSource = listaComprobantes;
                    rpComprobantes.DataBind();

                    txtDescripcionComprobantes.Text = "";

                    Session["sessionArchivoComprobantes"] = null;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "activar", "cerrarModalAgregarComprobantes();", true);
                }
                else
                {
                    Toastr("error", "Se produjo un error inesperado. Consulte al administrador.");
                }
            }
        }
        protected void btnAgregarCambiosAntecedentes_Click(object sender, EventArgs e)
        {
            if (ValidarCamposAntecedentes())
            {
                HttpPostedFile certificadoAntecedentes = fuCertificadoAntecedentes.PostedFile;
                DateTime fechaActual = DateTime.Now;
                string identificacion = txtIdentificacion.Text;
                string rutaDocumento = "";
                string nombreDocumento = "";

                if (Session["rutaDocumentoAntecedente"] != null)
                {
                    rutaDocumento = Session["rutaDocumentoAntecedente"].ToString();
                }
                if (Session["nombreDocumentoAntecedente"] != null)
                {
                    nombreDocumento = Session["nombreDocumentoAntecedente"].ToString();
                }

                if (certificadoAntecedentes != null)
                {
                    String nombreArchivo = Path.GetFileName(certificadoAntecedentes.FileName);
                    nombreDocumento = nombreArchivo;
                    nombreArchivo = "antecedente" + "_" + fechaActual.ToString().Replace("/", "").Replace(" ", "").Replace(":", "")
                        + "_" + identificacion + "_" + nombreArchivo.Replace(' ', '_');

                    Boolean guardado = Utilidades.GuardarDocumentos(certificadoAntecedentes, nombreArchivo);

                    if (guardado)
                    {
                        rutaDocumento = Utilidades.archivos_path + "\\" + nombreArchivo;
                    }
                    else
                    {
                        Toastr("error", "Se produjo un error al guardar el documento. Consulte al administrador.");
                    }
                }

                Antecedente antecedente = new Antecedente();
                antecedente.Nombre = txtNombreAntecedentes.Text.Trim();
                antecedente.Fecha = Convert.ToDateTime(txtFechaAntecedentes.Text);
                antecedente.Descripcion = txtDescripcionAntecedentes.Text;
                antecedente.RutaDocumento = rutaDocumento;
                antecedente.NombreDocumento = nombreDocumento;
                antecedente.Funcionario = new Funcionario(identificacion);
                antecedente.TipoAntecedente = new TipoAntecedente(Convert.ToInt32(TipoAntecedentesDDL.SelectedValue));

                int resultado = 0;
                if (Session["idAntecedente"] != null)
                {
                    antecedente.IdAntecedente = Convert.ToInt32(Session["idAntecedente"]);
                    resultado = this.antecedenteDatos.Actualizar(antecedente);
                    Session["idAntecedente"] = null;
                }

                if (resultado > 0)
                {
                    Toastr("success", "El antecedente fue actualizado con éxito.");

                    List<Antecedente> listaAntecedentes = this.antecedenteDatos.ObtenerPorId(identificacion);

                    rpAntecedentes.DataSource = listaAntecedentes;
                    rpAntecedentes.DataBind();

                    txtNombreAntecedentes.Text = "";
                    txtDescripcionAntecedentes.Text = "";

                    Session["sessionArchivoAntecedentes"] = null;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "activar", "cerrarModalAgregarAntecedentes();", true);
                }
                else
                {
                    Toastr("error", "Se produjo un error inesperado. Consulte al administrador.");
                }
            }
        }
        protected void btnAgregarCambiosSuspensionPermiso_Click(object sender, EventArgs e)
        {
            if (ValidarCamposSuspensionPermiso())
            {
                SuspensionPermiso suspensionPermiso = new SuspensionPermiso();
                suspensionPermiso.FechaSalida = Convert.ToDateTime(txtFechaSalidaSuspensionPermiso.Text);

                DateTime fechaRegreso = new DateTime(1900, 01, 01);
                if (txtFechaRegresoSuspensionPermiso.Text.Trim() != "")
                {
                    fechaRegreso = Convert.ToDateTime(txtFechaRegresoSuspensionPermiso.Text.Trim());
                }

                suspensionPermiso.FechaRegreso = fechaRegreso;
                suspensionPermiso.Descripcion = txtDescripcionSuspensionPermiso.Text;
                suspensionPermiso.Tipo = Convert.ToInt32(TipoSuspensionPermisoDDL.SelectedValue);
                string identificacion = txtIdentificacion.Text;
                suspensionPermiso.Funcionario = new Funcionario(identificacion);

                int resultado = 0;
                if (Session["idSuspensionPermiso"] != null)
                {
                    suspensionPermiso.IdSuspensionPermiso = Convert.ToInt32(Session["idSuspensionPermiso"]);
                    resultado = this.suspensionPermisoDatos.Actualizar(suspensionPermiso);
                    Session["idSuspensionPermiso"] = null;
                }

                if (resultado > 0)
                {
                    Toastr("success", "La suspensión/permiso fue actualizado con éxito.");

                    List<SuspensionPermiso> listaSuspensionPermisos = this.suspensionPermisoDatos.ObtenerPorId(identificacion);

                    rpSuspensionPermiso.DataSource = listaSuspensionPermisos;
                    rpSuspensionPermiso.DataBind();

                    txtFechaSalidaSuspensionPermiso.Text = "";
                    txtFechaRegresoSuspensionPermiso.Text = "";
                    txtDescripcionSuspensionPermiso.Text = "";

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "activar", "cerrarModalAgregarSuspensionPermiso();", true);
                }
                else
                {
                    Toastr("error", "Se produjo un error inesperado. Consulte al administrador.");
                }
            }
        }
        #endregion

        #region Funciones para ocultar la opcion de eliminar
        /// <summary>
        /// Adrián Serrano
        /// 24/11/2021
        /// Efecto: Metodo que se activa cuando se llenan las tablas
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void rpEstudios_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                LinkButton btnEliminar = e.Item.FindControl("btnEliminarEstudio") as LinkButton;

                int rol = Session["rol"] == null ? 0 : Int32.Parse(Session["rol"].ToString());

                if (rol != Utilidades.ROL_ADMINISTRADOR)
                {
                    btnEliminar.Visible = false;
                }
            }
        }
        protected void rpExperienciaLaboral_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                LinkButton btnEliminar = e.Item.FindControl("btnEliminarExperienciaLaboral") as LinkButton;

                int rol = Session["rol"] == null ? 0 : Int32.Parse(Session["rol"].ToString());

                if (rol != Utilidades.ROL_ADMINISTRADOR)
                {
                    btnEliminar.Visible = false;
                }
            }
        }
        protected void rpCursos_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                LinkButton btnEliminar = e.Item.FindControl("btnEliminarCurso") as LinkButton;

                int rol = Session["rol"] == null ? 0 : Int32.Parse(Session["rol"].ToString());

                if (rol != Utilidades.ROL_ADMINISTRADOR)
                {
                    btnEliminar.Visible = false;
                }
            }
        }
        protected void rpCertificados_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                LinkButton btnEliminar = e.Item.FindControl("btnEliminarCertificado") as LinkButton;

                int rol = Session["rol"] == null ? 0 : Int32.Parse(Session["rol"].ToString());

                if (rol != Utilidades.ROL_ADMINISTRADOR)
                {
                    btnEliminar.Visible = false;
                }
            }
        }
        protected void rpPensionOEmbargos_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                LinkButton btnEliminar = e.Item.FindControl("btnEliminarPensionOEmbargo") as LinkButton;

                int rol = Session["rol"] == null ? 0 : Int32.Parse(Session["rol"].ToString());

                if (rol != Utilidades.ROL_ADMINISTRADOR)
                {
                    btnEliminar.Visible = false;
                }
            }
        }
        protected void rpAccionesPersonal_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                LinkButton btnEliminar = e.Item.FindControl("btnEliminarAccionPersonal") as LinkButton;

                int rol = Session["rol"] == null ? 0 : Int32.Parse(Session["rol"].ToString());

                if (rol != Utilidades.ROL_ADMINISTRADOR)
                {
                    btnEliminar.Visible = false;
                }
            }
        }
        protected void rpComprobantes_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                LinkButton btnEliminar = e.Item.FindControl("btnEliminarComprobante") as LinkButton;

                int rol = Session["rol"] == null ? 0 : Int32.Parse(Session["rol"].ToString());

                if (rol != Utilidades.ROL_ADMINISTRADOR)
                {
                    btnEliminar.Visible = false;
                }
            }
        }
        protected void rpSuspensionPermiso_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                LinkButton btnEliminar = e.Item.FindControl("btnEliminarSuspensionPermiso") as LinkButton;

                int rol = Session["rol"] == null ? 0 : Int32.Parse(Session["rol"].ToString());

                if (rol != Utilidades.ROL_ADMINISTRADOR)
                {
                    btnEliminar.Visible = false;
                }
            }
        }
        protected void rpAntecedentes_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                LinkButton btnEliminar = e.Item.FindControl("btnEliminarAntecedente") as LinkButton;

                int rol = Session["rol"] == null ? 0 : Int32.Parse(Session["rol"].ToString());

                if (rol != Utilidades.ROL_ADMINISTRADOR)
                {
                    btnEliminar.Visible = false;
                }
            }
        }
        protected void rpHabilidadBlanda_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                LinkButton btnEliminar = e.Item.FindControl("btnEliminarHabilidadBlanda") as LinkButton;

                int rol = Session["rol"] == null ? 0 : Int32.Parse(Session["rol"].ToString());

                if (rol != Utilidades.ROL_ADMINISTRADOR)
                {
                    btnEliminar.Visible = false;
                }
            }
        }
        protected void rpInteresesPersonales_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                LinkButton btnEliminar = e.Item.FindControl("btnEliminarInteresesPersonales") as LinkButton;

                int rol = Session["rol"] == null ? 0 : Int32.Parse(Session["rol"].ToString());

                if (rol != Utilidades.ROL_ADMINISTRADOR)
                {
                    btnEliminar.Visible = false;
                }
            }
        }
        #endregion

        #endregion eventos
        protected void txtNombreEmpresaExperienciaLaboralVerEditar_TextChanged(object sender, EventArgs e)
        {
            txtNombreEmpresaExperienciaLaboralVerEditar.CssClass = "form-control";
            divNombreEmpresaExperienciaLaboralIncorrectoVerEditar.Style.Add("display", "none");
        }

        protected void btnSeleccionarArchivoCurriculumVerEditar_Click(object sender, EventArgs e)
        {
            Session["sessionCurriculum"] = fuCurriculumVerEditar;
            IList<HttpPostedFile> certificadoAccionesPersonal = fuCurriculumVerEditar.PostedFiles;

            if (fuCurriculumVerEditar.HasFiles)
            {
                btnQuitarArchivoCurriculumVerEditar.Visible = true;
                lblArchivoCurriculumVacioVerEditar.Text = "";

                foreach (HttpPostedFile certificado in certificadoAccionesPersonal)
                {
                    lblArchivoCurriculumVacioVerEditar.Text += certificado.FileName;
                }
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "activar", "levantarModalVerEditarExperienciaLaboral();", true);
        }

        protected void btnQuitarArchivoCurriculumVerEditar_Click(object sender, EventArgs e)
        {
            Session["sessionCurriculum"] = null;
            fuCurriculumVerEditar = null;
            btnQuitarArchivoCurriculumVerEditar.Visible = false;
            lblArchivoCurriculumVacioVerEditar.Text = "";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "activar", "levantarModalVerEditarExperienciaLaboral();", true);
        }

        protected void btnSeleccionarArchivoCurriculum_Click(object sender, EventArgs e)
        {
            Session["sessionCurriculum"] = fuCurriculum;
            IList<HttpPostedFile> curriculums = fuCurriculum.PostedFiles;

            if (fuCurriculum.HasFiles)
            {
                btnQuitarArchivoAccionesPersonal.Visible = true;
                lblArchivoAccionesPersonalVacio.Text = "";

                foreach (HttpPostedFile curriculum in curriculums)
                {
                    lblArchivoCurriculumVacio.Text += curriculum.FileName;
                }
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "activar", "levantarModalAgregarExperienciaLaboral();", true);
        }

        protected void btnQuitarArchivoCurriculum_Click(object sender, EventArgs e)
        {
            Session["sessionCurriculum"] = null;
            fuCurriculum = null;
            btnQuitarArchivoCurriculum.Visible = false;
            lblArchivoCurriculumVacio.Text = "";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "activar", "levantarModalAgregarExperienciaLaboral();", true);
        }

        protected void txtNombreAccionesPersonalVerEditar_TextChanged(object sender, EventArgs e)
        {
            txtNombreAccionesPersonalVerEditar.CssClass = "form-control";
            divNombreAccionesPersonalIncorrectoVerEditar.Style.Add("display", "none");
        }

        protected void btnSeleccionarArchivoAccionesPersonalVerEditar_Click(object sender, EventArgs e)
        {
            Session["sessionAccionesPersonal"] = fuCertificadoAccionesPersonalVerEditar;
            IList<HttpPostedFile> certificadoAccionesPersonal = fuCertificadoAccionesPersonalVerEditar.PostedFiles;

            if (fuCertificadoAccionesPersonalVerEditar.HasFiles)
            {
                btnQuitarArchivoAccionesPersonalVerEditar.Visible = true;
                lblArchivoAccionesPersonalVacioVerEditar.Text = "";

                foreach (HttpPostedFile certificado in certificadoAccionesPersonal)
                {
                    lblArchivoAccionesPersonalVacioVerEditar.Text += certificado.FileName;
                }
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "activar", "levantarModalVerEditarAccionesPersonal();", true);
        }

        protected void btnQuitarArchivoAccionesPersonalVerEditar_Click(object sender, EventArgs e)
        {
            Session["sessionArchivoAccionesPersonal"] = null;
            fuCertificadoAccionesPersonalVerEditar = null;
            btnQuitarArchivoAccionesPersonalVerEditar.Visible = false;
            lblArchivoAccionesPersonalVacioVerEditar.Text = "";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "activar", "levantarModalVerEditarAccionesPersonal();", true);
        }

    }
}