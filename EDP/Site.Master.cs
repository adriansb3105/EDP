using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EDP
{
    public partial class Site : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["nombreCompleto"] != null)
            {
                username.InnerText = "Bienvenid@ " + Session["nombreCompleto"].ToString();
            }
        }

        protected void BtnSalir_Click(object sender, EventArgs e)
        {
            Session.RemoveAll();
            Session.Abandon();
            Session.Clear();
            String url = Page.ResolveUrl("~/login.aspx");
            Response.Redirect(url);
        }

    }
}