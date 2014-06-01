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
    public partial class Popoup : System.Web.UI.Page
    {
        static string connString = "SERVER=localhost;DATABASE=naucen_trud;UID=root;PWD=filip;";
        MySqlConnection conn = new MySqlConnection(connString);

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    string em = "admin@finki.com";
                    conn.Open();
                    string sqlSelect = "SELECT id, CONCAT(name ,' ',surname) as fullName FROM user_info WHERE email NOT LIKE ?email and email not like ?user";
                    MySqlCommand cmd = new MySqlCommand(sqlSelect, conn);

                    cmd.Parameters.Add("?email", em);
                    cmd.Parameters.Add("?user", Session["New"].ToString());

                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    adapter.Fill(ds);

                    cblCorispondingAutors.DataSource = ds;
                    cblCorispondingAutors.DataBind();


                }
                catch { }
                finally
                {
                    conn.Close();
                }



            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {

            string autor = "";
            string fullName = "";
            string email = "";
            foreach (ListItem item in cblCorispondingAutors.Items)
            {
                if (item.Selected == true)
                {

                  


                    try
                      {
                        
                    conn.Open();
                    string selectAutor = "Select name, surname,email from user_info where id =?id";
                    MySqlCommand cmd = new MySqlCommand(selectAutor, conn);
                    cmd.Parameters.Add("?id", item.Value);
                   
                    cmd.ExecuteNonQuery();
                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    DataSet sd = new DataSet();
                    adapter.Fill(sd);

                    fullName += sd.Tables[0].Rows[0]["name"] + " " + sd.Tables[0].Rows[0]["surname"] + ", ";
                    email +=sd.Tables[0].Rows[0]["email"]+", ";
                  


                    autor += item.Value + ", ";
                      }
                      catch (Exception) { }
                      finally {
                    conn.Close();

                     }


                }


            }
            lblEmail.Text += email;
            lblHidd.Text += autor+" -- "+fullName;
            ScriptManager.RegisterStartupScript(Page, GetType(), "JsStatus", "passValues()", true);

        }
    }
}