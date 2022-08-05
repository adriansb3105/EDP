using AccesoDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EDP
{
    public partial class Ayuda : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //controla los menus q se muestran y las pantallas que se muestras segun el rol que tiene el usuario
            //si no tiene permiso de ver la pagina se redirecciona a login
            int[] rolesPermitidos = { 2, 3 };
            Utilidades.escogerMenu(Page, rolesPermitidos);

            if (!Page.IsPostBack)
            {
                divIntroduccion.Visible = true;
                divAgregarFuncionario.Visible = false;
            }
        }

        #region Mostrar y ocultar las secciones desde el menú lateral
        /// <summary>
        /// Adrián Serrano
        /// 05/10/2021
        /// Efecto: Esta sección contiene las funciones para mostrar y ocultar las secciones desde el menú lateral
        /// modales.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void showHideIntroduccion(object sender, EventArgs e)
        {
            if (!divIntroduccion.Visible)
            {
                aAgregarFuncionario.CssClass = "";
                aIntroduccion.CssClass = "active";

                divAgregarFuncionario.Visible = false;
                divIntroduccion.Visible = true;
            }
        }
        protected void showHideAgregarFuncionario(object sender, EventArgs e)
        {
            if (!divAgregarFuncionario.Visible)
            {
                aIntroduccion.CssClass = "";
                aAgregarFuncionario.CssClass = "active";

                divIntroduccion.Visible = false;
                divAgregarFuncionario.Visible = true;
            }
        }
        
        #endregion
    }
}