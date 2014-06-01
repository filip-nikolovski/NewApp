using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Diplomska
{
    public partial class ErrorPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) {

                if (Session["Msg"] != null)
                {

                    lblMesage.Text = Session["Msg"].ToString();
                }
                else {
                    lblMesage.Text = "Не постои бараната соджина!";
                }
            }
        }
    }
}