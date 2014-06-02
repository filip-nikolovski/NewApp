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
    public partial class ListingSW : System.Web.UI.Page
    {
       // static string connString = "SERVER=localhost;DATABASE=naucen_trud;UID=root;PWD=filip;Allow Zero Datetime=True;";
        static string connString = "SERVER=sql5.freemysqlhosting.net;DATABASE=sql542315;UID=sql542315;PWD=pW5!hL3%;Allow Zero Datetime=True;";
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
                            //DropDownList autor created code
                            /**         conn.Open();
                                     string selectUsers = "SELECT id, CONCAT(name ,' ',surname) as fullName  from user_info WHERE email NOT LIKE ?email";
                                     MySqlCommand cmd = new MySqlCommand(selectUsers, conn);
                                     cmd.Parameters.Add("?email", "admin@finki.com");
                                     MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                                     DataSet ds = new DataSet();
                                     adapter.Fill(ds);
                    
                                     ddlUsers.DataSource = ds;
                                     ddlUsers.DataBind(); **/

                            //DropDownList corrw
                            string selectCorrAut = "SELECT id, CONCAT(name ,' ',surname) as fullName, email  from user_info WHERE email NOT LIKE ?email";
                            MySqlCommand cmd11 = new MySqlCommand(selectCorrAut, conn);
                            cmd11.Parameters.Add("?email", "admin@finki.com");
                            MySqlDataAdapter adapter11 = new MySqlDataAdapter(cmd11);
                            DataSet ds11 = new DataSet();
                            adapter11.Fill(ds11);

                            ddlCorrAut.DataSource = ds11;
                            ddlCorrAut.DataBind();

                            //GridView code
                            string sqlSelect = "SELECT sw.id, sw.title, concat( sw.autores,' ',sw.Corresponding_autor) as autors , sw.price, sw.date FROM science_work sw, created c, user_info u WHERE   sw.id=c.science_work_ID  and c.user_info_ID=u.id";
                            MySqlCommand cmd1 = new MySqlCommand(sqlSelect, conn);

                            MySqlDataAdapter adapter1 = new MySqlDataAdapter(cmd1);
                            DataSet ds1 = new DataSet();
                            adapter1.Fill(ds1);

                            gvScienceWork.DataSource = ds1;
                            gvScienceWork.DataBind();
                        }
                        catch (Exception ex)
                        {
                            lblErr.Visible = true;
                            lblErr.Text += ex.ToString();
                        }
                        finally
                        {
                            conn.Close();
                        }
                    }
                }
                else
                {

                    Response.Redirect("Login.aspx");
                }
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();

                if (ddlCorrAut.SelectedValue == "0" )
                {
                   // string sqlSelect = "SELECT sw.id, sw.title, concat( sw.autores,' ',sw.Corresponding_autor) as autors , sw.price, sw.date FROM science_work sw, created c, user_info u WHERE   sw.id=c.science_work_ID  and c.user_info_ID=u.id";
                            
                    string sqlSelectUsr = "SELECT sw.id, sw.title,concat( sw.autores,' ',  sw.Corresponding_autor) as autors, sw.price, sw.date FROM science_work sw, created c, user_info u WHERE   sw.id=c.science_work_ID  and c.user_info_ID=u.id ";
                    MySqlCommand cmd2 = new MySqlCommand(sqlSelectUsr, conn);
                   // cmd2.Parameters.Add("?user_info_ID", ddlCorrAut.SelectedValue);

                    MySqlDataAdapter adapter1 = new MySqlDataAdapter(cmd2);
                    DataSet ds1 = new DataSet();
                    adapter1.Fill(ds1);

                    gvScienceWork.DataSource = ds1;
                    gvScienceWork.DataBind();



                }
                else {

               /**     string selectUID = "Select u.id from user_info u where email =?email";
                    MySqlCommand cmdUID = new MySqlCommand(selectUID, conn);
                    cmdUID.Parameters.Add("?email", ddlCorrAut.SelectedValue);
                    MySqlDataAdapter ada = new MySqlDataAdapter(cmdUID);
                    DataSet dss = new DataSet();
                    ada.Fill(dss);
                    string UID = dss.Tables[0].Rows[0]["id"].ToString();  **/

                    string sqlSelectUsr = "SELECT distinct sw.id, sw.title, concat( sw.autores,' ',sw.Corresponding_autor) as autors, sw.price, sw.date FROM science_work sw, created c, user_info u WHERE   sw.id=c.science_work_ID AND c.user_info_ID=u.id AND autores like '%" + ddlCorrAut.SelectedValue + "%' OR Corresponding_autor like '%" + ddlCorrAut.SelectedValue + "%' and sw.id=c.science_work_ID AND c.user_info_ID=u.id ";
                    MySqlCommand cmd2 = new MySqlCommand(sqlSelectUsr, conn);
                   // cmd2.Parameters.Add("?user_info_ID", ddlUsers.SelectedValue);
                    //cmd2.Parameters.Add("?UID", UID);

                    MySqlDataAdapter adapter1 = new MySqlDataAdapter(cmd2);
                    DataSet ds1 = new DataSet();
                    adapter1.Fill(ds1);

                    gvScienceWork.DataSource = ds1;
                    gvScienceWork.DataBind();
                }
                //lblErr.Text = ddlCorrAut.SelectedValue;
               
            }
            catch (Exception ex)
            {
                lblErr.Visible = true;
                lblErr.Text = ex.ToString();

            }
            finally {
                conn.Close();
            }
        }

      /**  protected void btnProba_Click(object sender, EventArgs e)
        {
            conn.Open();
            string sqlSelectUsr = "SELECT sw.id, sw.title, sw.autores,sw.Corresponding_autor, sw.price, sw.date FROM science_work sw, created c, user_info u WHERE  Corresponding_autor like '%" + ddlCorrAut.SelectedValue + "%' AND sw.id=c.science_work_ID  and c.user_info_ID=u.id";
            MySqlCommand cmd2 = new MySqlCommand(sqlSelectUsr, conn);
            cmd2.Parameters.Add("?user_info_ID", ddlCorrAut.SelectedValue);

            MySqlDataAdapter adapter1 = new MySqlDataAdapter(cmd2);
            DataSet ds1 = new DataSet();
            adapter1.Fill(ds1);

            gvScienceWork.DataSource = ds1;
            gvScienceWork.DataBind();
        }
        **/


        //delete gridview row
        protected void button_click(object sender, EventArgs e)
        {
            Button ibtn1 = sender as Button;
            int rowIndex = Convert.ToInt32(ibtn1.Attributes["RowIndex"]);

            //Use this rowIndex in your code
            //lblEx.Text = rowIndex.ToString();

            try
            {

                conn.Open();

                string sqlDelete = "DELETE FROM  science_work WHERE id =@id";
                MySqlCommand cmd = new MySqlCommand(sqlDelete, conn);

                cmd.Parameters.AddWithValue("@id", Convert.ToInt32(rowIndex.ToString()));
                cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                //lblEx.Text = ex.ToString();
            }
            finally
            {
                conn.Close();
                Response.Redirect("ListingSW.aspx");
            }


        }



          protected void button11_click(object sender, EventArgs e)
          {
              Button ibtn11 = sender as Button;
              int rowIndex = Convert.ToInt32(ibtn11.Attributes["RowIndex"]);

              //Use this rowIndex in your code
              //lblErr.Text = rowIndex.ToString();
              Session["sessi"] = rowIndex;
              Response.Redirect("VerziiAdmin.aspx");

          }


          //gvScienceWork_RowCreated
          protected void gvScienceWork_RowCreated(object sender, GridViewRowEventArgs e)
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

          protected void btnLoguot_Click(object sender, EventArgs e)
          {
              Session["New"] = null;
              Response.Redirect("Login.aspx");
          }
    }
}