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
    public partial class Login : System.Web.UI.Page
    {
        static string connString = "SERVER=localhost;DATABASE=naucen_trud;UID=root;PWD=filip;";
        MySqlConnection conn = new MySqlConnection(connString);

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) {

                Session["New"] = null;
                HttpCookie cookie = Request.Cookies["cedentials"];
                if (cookie != null)
                {
                    TextBox1.Text = cookie["user"].ToString();
                    TextBox2.Attributes["value"] = cookie["password"].ToString();
                    chkRemember.Checked = true;
                }
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            conn.Open();

            string sqlSelect = "SELECT COUNT(*), accsepted FROM user_info WHERE email = ?email AND password = ?password";
            MySqlCommand cmd = new MySqlCommand(sqlSelect, conn);
            cmd.Parameters.Add("?email", TextBox1.Text);
            cmd.Parameters.Add("?password", TextBox2.Text);
            cmd.ExecuteNonQuery();

            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
              DataSet ds = new DataSet();
              adapter.Fill(ds);

            int temp = Convert.ToInt32(cmd.ExecuteScalar().ToString());

            //Label3.Text = temp.ToString();
            if (ds.Tables[0].Rows[0]["accsepted"].ToString() == "2")
            {
                Session["Msg"] = "Не сте прифатени од администраторот за влез во апликацијата.";
                Response.Redirect("ErrorPage.aspx");

            }
            else if (ds.Tables[0].Rows[0]["accsepted"].ToString() == "3") {
                Session["Msg"] = "Вашата лозинка сеуште не е рестартирана.";
                Response.Redirect("ErrorPage.aspx");
            }

            else
            {

                if (temp == 1)
                {


                    Session["New"] = TextBox1.Text;

                    if (chkRemember.Checked)
                    {
                        HttpCookie cookie = new HttpCookie("cedentials");
                        cookie["user"] = TextBox1.Text;
                        cookie["password"] = TextBox2.Text;
                        cookie.Expires = DateTime.Now.AddDays(30d);
                        Response.Cookies.Add(cookie);
                    }
                    else
                    {
                        HttpCookie cookie = new HttpCookie("cedentials");
                        cookie.Expires = DateTime.Now.AddDays(-1d);
                        Response.Cookies.Add(cookie);
                    }


                    if (Session["New"].ToString() == "admin@finki.com")
                    {

                        Response.Redirect("Admin.aspx");
                    }
                    else
                    {
                        if (Session["Page"] == null)
                        {
                            Response.Redirect("Index.aspx");
                        }
                        else
                        {
                            string page = Session["Page"].ToString();
                            Response.Redirect(page);
                        }


                    }
                }
                else
                {


                    Label3.Visible = true;
                    Label3.Text = "Корисничкото име или лозинката не се точни!";

                }



            }

            /**  protected void Button1_Click(object sender, EventArgs e)
              {
                  Response.Redirect("Register.aspx");
              }**/
        }
    }
}