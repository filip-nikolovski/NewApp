using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using System.Data;

namespace Diplomska
{
    public partial class Download : System.Web.UI.Page
    {

        //static string connString = "SERVER=localhost;DATABASE=naucen_trud;UID=root;PWD=filip;";
        static string connString = "SERVER=sql5.freemysqlhosting.net;DATABASE=sql542315;UID=sql542315;PWD=pW5!hL3%;";
        MySqlConnection conn = new MySqlConnection(connString);

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) {

                try
                {
                    conn.Open();
                    string sqlSelect = "SELECT id_version, version_name, file_path FROM versions";
                    MySqlCommand cmd = new MySqlCommand(sqlSelect, conn);
                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    adapter.Fill(ds);

                    gvDownload.DataSource = ds;
                    gvDownload.DataBind();

                   // cmd.ExecuteNonQuery();

                }
                catch (Exception ex) { 
                    lblException.Text = ex.ToString(); 
                }

            }
        }
    }
}