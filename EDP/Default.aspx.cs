using AccesoDatos;
using Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EDP
{
    public partial class Default : System.Web.UI.Page
    {
        #region variables globales
        private FuncionarioDatos funcionarioDatos;
        #endregion

        #region Paginación
        private readonly PagedDataSource pgsource = new PagedDataSource();
        int primerIndex, ultimoIndex;
        private int elementosMostrar = 5;
        private int paginaActual
        {
            get
            {
                if (ViewState["paginaActual"] == null)
                {
                    return 0;
                }
                return ((int)ViewState["paginaActual"]);
            }
            set
            {
                ViewState["paginaActual"] = value;
            }
        }
        #endregion

        #region page load
        protected void Page_Load(object sender, EventArgs e)
        {
            //controla los menus q se muestran y las pantallas que se muestras segun el rol que tiene el usuario
            //si no tiene permiso de ver la pagina se redirecciona a login
            int[] rolesPermitidos = { 2, 3 };
            Utilidades.escogerMenu(Page, rolesPermitidos);

            this.funcionarioDatos = new FuncionarioDatos();

            if (!Page.IsPostBack)
            {
                Session["listaFuncionarios"] = null;
                Session["funcionarioVer"] = null;
                Session["numeroIdentificacion"] = null;

                mostrarDatosTabla();
            }
        }
        #endregion

        #region logica

        /// <summary>
        /// Adrián Serrano
        /// 11/agosto/2021
        /// Efecto: LLena y muestra la tabla de funcionarios después de extraerlos de la base de datos
        /// </summary>
        private List<Funcionario> cargarFuncionarios()
        {
            List<Funcionario> listaFuncionarios = new List<Funcionario>();
            List<Funcionario> listaFuncionariosFiltrada = new List<Funcionario>();
            listaFuncionarios = this.funcionarioDatos.ObtenerTodos(true);

            string numeroIdentificacion = "";
            string nombre = "";
            string apellidos = "";
            string extension = "";

            if (!string.IsNullOrEmpty(txtBuscarNumeroIdentificacion.Text))
            {
                numeroIdentificacion = txtBuscarNumeroIdentificacion.Text;
            }

            if (!string.IsNullOrEmpty(txtBuscarNombre.Text))
            {
                nombre = txtBuscarNombre.Text;
            }

            if (!string.IsNullOrEmpty(txtBuscarApellidos.Text))
            {
                apellidos = txtBuscarApellidos.Text;
            }

            if (!string.IsNullOrEmpty(txtBuscarExtension.Text))
            {
                extension = txtBuscarExtension.Text;
            }

            listaFuncionariosFiltrada = (List<Funcionario>)listaFuncionarios.Where(funcionario => 
                funcionario.NumeroIdentificacion.Trim().ToUpper().Contains(numeroIdentificacion.Trim().ToUpper()) &&
                funcionario.Nombre.Trim().ToUpper().Contains(nombre.Trim().ToUpper()) &&
                (
                    funcionario.PrimerApellido.Trim().ToUpper().Contains(apellidos.Trim().ToUpper()) ||
                    funcionario.SegundoApellido.Trim().ToUpper().Contains(apellidos.Trim().ToUpper())
                ) &&
                funcionario.NumeroTelefono.Trim().ToUpper().Contains(extension.Trim().ToUpper())
            ).ToList();

            Session["listaFuncionarios"] = listaFuncionariosFiltrada;

            return listaFuncionariosFiltrada;
        }

        private void mostrarDatosTabla()
        {
            var dt = cargarFuncionarios();
            pgsource.DataSource = dt;
            pgsource.AllowPaging = true;
            //numero de items que se muestran en el Repeater
            pgsource.PageSize = elementosMostrar;
            pgsource.CurrentPageIndex = paginaActual;
            //mantiene el total de paginas en View State
            ViewState["TotalPaginas"] = pgsource.PageCount;
            //Ejemplo: "Página 1 al 10"
            lblpagina.Text = "Página " + (paginaActual + 1) + " de " + pgsource.PageCount + " (" + dt.Count + " - elementos)";
            //Habilitar los botones primero, último, anterior y siguiente
            lbAnterior.Enabled = !pgsource.IsFirstPage;
            lbSiguiente.Enabled = !pgsource.IsLastPage;
            lbPrimero.Enabled = !pgsource.IsFirstPage;
            lbUltimo.Enabled = !pgsource.IsLastPage;

            rpFuncionario.DataSource = pgsource;
            rpFuncionario.DataBind();

            //metodo que realiza la paginacion
            Paginacion();
        }
        #endregion

        #region eventos

        protected void btnAgregarFuncionario_Click(object sender, EventArgs e)
        {
            string url = Page.ResolveUrl("~/Catalogos/Funcionarios/AgregarFuncionario.aspx");
            Response.Redirect(url);
        }

        /// <summary>
        /// Adrián Serrano
        /// 11/agosto/2021
        /// Efecto: Metodo que se activa cuando se da click al boton de ver
        /// Redirecciona a la pantalla con toda la información del funcionario seleccionado
        /// </summary>
        protected void btnVer_Click(object sender, EventArgs e)
        {
            string numeroIdentificacion = (((LinkButton)(sender)).CommandArgument).ToString();

            List<Funcionario> listaFuncionarios = (List<Funcionario>)Session["listaFuncionarios"];

            Funcionario funcionarioVer = new Funcionario();

            foreach (Funcionario funcionario in listaFuncionarios)
            {
                if (funcionario.NumeroIdentificacion == numeroIdentificacion)
                {
                    funcionarioVer = funcionario;
                    break;
                }
            }

            Session["funcionarioVer"] = funcionarioVer;

            String url = Page.ResolveUrl("~/Catalogos/Funcionarios/VerFuncionario.aspx");
            Response.Redirect(url);
        }

        protected void btnEditar_Click(object sender, EventArgs e)
        {
        }

        protected void btnDeshabilitar_Click(object sender, EventArgs e)
        {
            string numeroIdentificacion = (((LinkButton)(sender)).CommandArgument).ToString();
            Session["numeroIdentificacion"] = numeroIdentificacion;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "activar", "levantarModalDeshabilitarFuncionario();", true);
        }

        protected void btnConfirmarDeshabilitar_Click(object sender, EventArgs e)
        {
            string numeroIdentificacion = Convert.ToString(Session["numeroIdentificacion"]);
            this.funcionarioDatos.HabilitarDeshabilitar(numeroIdentificacion, false);

            Session["listaFuncionarios"] = null;
            Session["funcionarioVer"] = null;

            mostrarDatosTabla();

            Session["numeroIdentificacion"] = null;

            //String url = Page.ResolveUrl("~/Default.aspx");
            //Response.Redirect(url);
        }

        protected void btnFiltroBuscar_Click(object sender, EventArgs e)
        {
            paginaActual = 0;
            mostrarDatosTabla();
        }
        protected void rptPaginacion_ItemCommand(object source, DataListCommandEventArgs e)
        {
            if (!e.CommandName.Equals("nuevaPagina")) return;
            paginaActual = Convert.ToInt32(e.CommandArgument.ToString());
            mostrarDatosTabla();
        }
        protected void rptPaginacion_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            var lnkPagina = (LinkButton)e.Item.FindControl("lbPaginacion");
            if (lnkPagina.CommandArgument != paginaActual.ToString()) return;
            lnkPagina.Enabled = false;
            lnkPagina.BackColor = Color.FromName("#005da4");
            lnkPagina.ForeColor = Color.FromName("#FFFFFF");
        }

        private void Paginacion()
		{
			var dt = new DataTable();
			dt.Columns.Add("IndexPagina"); //Inicia en 0
			dt.Columns.Add("PaginaText"); //Inicia en 1
			primerIndex = paginaActual - 2;
			if (paginaActual > 2)
				ultimoIndex = paginaActual + 2;
			else
				ultimoIndex = 4;

			//se revisa que la ultima pagina sea menor que el total de paginas a mostrar, sino se resta para que muestre bien la paginacion
			if (ultimoIndex > Convert.ToInt32(ViewState["TotalPaginas"]))
			{
				ultimoIndex = Convert.ToInt32(ViewState["TotalPaginas"]);
				primerIndex = ultimoIndex - 4;
			}

			if (primerIndex < 0)
				primerIndex = 0;

			//se crea el numero de paginas basado en la primera y ultima pagina
			for (var i = primerIndex; i < ultimoIndex; i++)
			{
				var dr = dt.NewRow();
				dr[0] = i;
				dr[1] = i + 1;
				dt.Rows.Add(dr);
			}

			rptPaginacion.DataSource = dt;
			rptPaginacion.DataBind();
		}

        protected void lbAnterior_Click(object sender, EventArgs e)
        {
            paginaActual -= 1;
            mostrarDatosTabla();
        }

        protected void lbSiguiente_Click(object sender, EventArgs e)
        {
            paginaActual += 1;
            mostrarDatosTabla();
        }

        protected void lbPrimero_Click(object sender, EventArgs e)
        {
            paginaActual = 0;
            mostrarDatosTabla();
        }

        protected void rpFuncionario_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                LinkButton btnEditar = e.Item.FindControl("btnEditar") as LinkButton;
                LinkButton btnEliminar = e.Item.FindControl("btnEliminar") as LinkButton;

                int rol = Session["rol"] == null ? 0 : Int32.Parse(Session["rol"].ToString());

                if (rol != Utilidades.ROL_ADMINISTRADOR)
                {
                    //btnEditar.Visible = false;
                    btnEliminar.Visible = false;
                }
            }
        }

        protected void lbUltimo_Click(object sender, EventArgs e)
        {
            paginaActual = (Convert.ToInt32(ViewState["TotalPaginas"]) - 1);
            mostrarDatosTabla();
        }
        #endregion
    }
}


/**
 

        //e = items del repeater
        //protected void rpFuncionarios_ItemDataBound(object sender, RepeaterItemEventArgs e)
        //{
        //    if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem) //no agarra filas nulas o vacias
        //    {
        //        LinkButton btnEditar = e.Item.FindControl("btnEditar") as LinkButton;

        //        //string cedulaCA = btnEditar.CommandArgument; //id del usuario
        //        //Funcionario f = (Funcionario)listaFuncionariosenSesion.where(x => x.cedula = cedulaCA).toList.First;
        //        //agregarle la image
        //        //meter todo en el repeater


        //        LinkButton btnEliminar = e.Item.FindControl("btnEliminar") as LinkButton;


        //    }
        //}
 */
