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
    public partial class Admin : System.Web.UI.Page
    {
        static int add;
        static int remove;
        static string connString = "SERVER=localhost;DATABASE=naucen_trud;UID=root;PWD=filip;";
        MySqlConnection conn = new MySqlConnection(connString);

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["New"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            else
            {
                if (Session["New"].ToString() == "admin@finki.com")
                {
                    lblLogedAs.Text = "You are logged in as " + Session["New"].ToString() + " ";
                    if (!IsPostBack)
                    {

                        try
                        {

                            conn.Open();

                            //main gridview za listajne korisnici
                            string sqlSelect = "SELECT * FROM user_info WHERE email NOT LIKE ?email AND accsepted =2 or accsepted =0 and email NOT LIKE ?email";
                            MySqlCommand cmd = new MySqlCommand(sqlSelect, conn);
                            cmd.Parameters.Add("?email", Session["New"].ToString());

                            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                            DataSet ds = new DataSet();
                            adapter.Fill(ds);

                            gvListUsers.DataSource = ds;
                            gvListUsers.DataBind();

                           //gridview za resset na password
                            string sqlSelect1 = "SELECT id, concat(`name`,' ',`surname`,' ',`email`) as User FROM user_info WHERE email NOT LIKE ?email AND accsepted =3";
                            MySqlCommand cmd1 = new MySqlCommand(sqlSelect1, conn);
                            cmd1.Parameters.Add("?email", Session["New"].ToString());

                            MySqlDataAdapter adapter1 = new MySqlDataAdapter(cmd1);
                            DataSet ds1 = new DataSet();
                            adapter1.Fill(ds1);

                            gvResserPass.DataSource = ds1;
                            gvResserPass.DataBind();

                        }
                        catch (Exception ex)
                        {
                            lblEx.Text = ex.ToString();
                        }
                        finally
                        {
                            conn.Clone();
                        }
                    }
                }
                else {

                    Response.Redirect("Login.aspx");
                }

        }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Session["New"] = null;
            Response.Redirect("Login.aspx");
        }

        static int keyInt;

        protected void gvListUsers_OnRowCommand(object sender, GridViewCommandEventArgs e)
             {

                    int index = Convert.ToInt32(e.CommandArgument);
                    GridViewRow gvRow = gvListUsers.Rows[index];         

             var radio = (RadioButton)gvRow.FindControl("rd2");
             var radio1 = (RadioButton)gvRow.FindControl("rd1");

                        if (radio != null && radio.Checked == true)
                        {
                            add = 3;
                                                      
                        }
                        else if (radio1.Checked == true)
                        {
                            add = 1;
                        }
                        else {
                            
                            add = 0;
                        }
                  

            
             }


        protected void gvListUsers_SelectedIndexChanged(object sender, EventArgs e)
        {
           
            string key = gvListUsers.SelectedValue.ToString();
            //Label1.Text = key;
            keyInt = Convert.ToInt32(key);

            // int i = 0;

             try
             {
                 conn.Open();

                 
                
                 if (add == 3) {

                     string deleteUSR = "DELETE FROM user_info WHERE id=?id";
                     MySqlCommand cmd3 = new MySqlCommand(deleteUSR, conn);
                     cmd3.Parameters.Add("?id", key);

                     cmd3.ExecuteNonQuery();
                     Response.Redirect("Admin.aspx");
                 }
                 else if (add == 1)
                 {
                     // name=?name , surname=?surname, email=?email, password=?password , labs_id=?labs_id 
                     string sqlUpdate = "UPDATE user_info SET  accsepted=?accsepted WHERE id=?id";
                     MySqlCommand cmd = new MySqlCommand(sqlUpdate, conn);

                     cmd.Parameters.Add("?accsepted", add);
                     cmd.Parameters.Add("?id", key);

                     cmd.ExecuteNonQuery();
                     Response.Redirect("Admin.aspx");
                 }
                 else {
                     lblEx.Visible = true;
                     lblEx.Text = "Избери Вредност";
                 }

                
                
             }
             catch (Exception ex)
             {

                 lblEx.Text = ex.ToString();
            }
             finally {

                 conn.Close();
                
             }


        }

        //button listaj
        protected void btnGO_Click(object sender, EventArgs e)
        {
            try
            {
                lblEx.Text = "";
                conn.Open();
                if (ddlUsers.SelectedValue == "1") {
                    
                    string sqlSelect = "SELECT * FROM user_info WHERE email NOT LIKE ?email AND accsepted =1";
                    MySqlCommand cmd = new MySqlCommand(sqlSelect, conn);
                    cmd.Parameters.Add("?email", Session["New"].ToString());

                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    adapter.Fill(ds);

                    gvListUsers.DataSource = ds;
                    gvListUsers.DataBind();

                    foreach (GridViewRow gr in gvListUsers.Rows) {

                        if (gr.RowType == DataControlRowType.DataRow)
                        {
                            var radio = (RadioButton)gr.FindControl("rd1");
                            radio.Visible = false;
                        }
                    }
                }
                else if (ddlUsers.SelectedValue == "0")
                {

                    string sqlSelect = "SELECT * FROM user_info WHERE email NOT LIKE ?email AND accsepted =2 or accsepted =0 and email NOT LIKE ?email";
                    MySqlCommand cmd = new MySqlCommand(sqlSelect, conn);
                    cmd.Parameters.Add("?email", Session["New"].ToString());

                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    adapter.Fill(ds);

                    gvListUsers.DataSource = ds;
                    gvListUsers.DataBind();
                }
                else {

                    string sqlSelect = "SELECT * FROM user_info WHERE email NOT LIKE ?email and accsepted <> 3";
                    MySqlCommand cmd = new MySqlCommand(sqlSelect, conn);
                    cmd.Parameters.Add("?email", Session["New"].ToString());

                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    adapter.Fill(ds);

                    gvListUsers.DataSource = ds;
                    gvListUsers.DataBind();


                    foreach (GridViewRow gr in gvListUsers.Rows)
                    {

                        if (gr.RowType == DataControlRowType.DataRow)
                        {

                            string sqlSelect1 = "SELECT count(*) FROM user_info WHERE email NOT LIKE ?email and accsepted <> 3";
                            MySqlCommand cmd1 = new MySqlCommand(sqlSelect1, conn);
                            cmd1.Parameters.Add("?email", Session["New"].ToString());

                          

                            int n = Convert.ToInt32(cmd1.ExecuteScalar().ToString());
                            string tt = gr.DataItemIndex.ToString();
                            //string ttt = gvListUsers.DataKeyNames.ToString();
                            for (int i = 0; i < n; i++) {
                                if (ds.Tables[0].Rows[i]["accsepted"].ToString() == "1" && ds.Tables[0].Rows[i]["id"].ToString() == gvListUsers.DataKeys[gr.RowIndex]["id"].ToString())
                                {

                                    var radio = (RadioButton)gr.FindControl("rd1");
                                    radio.Visible = false;
                                }
                            }
                               
                           
                        }
                    }
                    
                }
            }
            catch (Exception ex)
            {
                lblEx.Text = ex.ToString();
            }
            finally
            {
                conn.Clone();
            }
        }

        //za resset password resset button
        protected void button_click(object sender, EventArgs e)
        {
            Button btnResset = sender as Button;
            int rowIndex = Convert.ToInt32(btnResset.Attributes["RowIndex"]);

            try
            {
                conn.Open();
                string sqlUpdate = "update user_info set accsepted=?accsepted, password=?password where id=?id";
                MySqlCommand cmd = new MySqlCommand(sqlUpdate, conn);
                cmd.Parameters.Add("?accsepted", "1");
                cmd.Parameters.Add("?password", "");
                cmd.Parameters.Add("?id", Convert.ToInt32(rowIndex.ToString()));
                cmd.ExecuteNonQuery();

            }
            catch { }
            finally {
                conn.Close();
                Response.Redirect("Admin.aspx");
            }


        }


        //za delete usser od password reset
        protected void button11_click(object sender, EventArgs e)
        {
            Button btnDelete = sender as Button;
            int rowIndex = Convert.ToInt32(btnDelete.Attributes["RowIndex"]);

            try
            {
                conn.Open();
                string sqlDelete = "Delete from user_info where id=?id";
                MySqlCommand cmd = new MySqlCommand(sqlDelete, conn);
                cmd.Parameters.Add("?id", Convert.ToInt32(rowIndex.ToString()));
                cmd.ExecuteNonQuery();
            }
            catch { }
            finally {
                conn.Close();
                Response.Redirect("Admin.aspx");
            }

        }

        //gvlistUsers on row created 
        protected void gvListUsers_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onmouseover"] = "this.style.background='#D1DDF1';";

                if ((e.Row.RowIndex % 2) == 0)  // if even row
                    e.Row.Attributes["onmouseout"] = "this.style.background='#EFF3FB';";
                else  // alternate row
                    e.Row.Attributes["onmouseout"] = "this.style.background='White';";
                //e.Row.ToolTip = "Click to view the event";
                //e.Row.Attributes["onclick"] = this.Page.ClientScript.GetPostBackClientHyperlink(this.gvReminder, "Select$" + e.Row.RowIndex);
            }
        }


        //gvResserPass_RowCreated
        protected void gvResserPass_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onmouseover"] = "this.style.background='#D1DDF1';";

                if ((e.Row.RowIndex % 2) == 0)  // if even row
                    e.Row.Attributes["onmouseout"] = "this.style.background='#EFF3FB';";
                else  // alternate row
                    e.Row.Attributes["onmouseout"] = "this.style.background='White';";
                //e.Row.ToolTip = "Click to view the event";
                //e.Row.Attributes["onclick"] = this.Page.ClientScript.GetPostBackClientHyperlink(this.gvReminder, "Select$" + e.Row.RowIndex);
            }
        }
    }
}