using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LigaPro.Datos;

namespace LigaPro.Web
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Listar
            DeporteDatos deporteDatos = new DeporteDatos();
            dgvDeportes.DataSource = deporteDatos.Listar();
            dgvDeportes.DataBind();
        }
    }
}