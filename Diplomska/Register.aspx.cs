using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using MySql.Data.MySqlClient;

namespace Diplomska
{
    public partial class Register : System.Web.UI.Page
    {
        //conn string
        //static string connString = "SERVER=localhost; DATABASE=naucen_trud; UID=ROOT; PWD=filip;";
        //static String connString = "SERVER=localhost;DATABASE=naucen_trud;UID=root;PWD=filip;";

        static string connString = "SERVER=sql5.freemysqlhosting.net;DATABASE=sql542315;UID=sql542315;PWD=pW5!hL3%;Allow Zero Datetime=True;";
        MySqlConnection conn = new MySqlConnection(connString);

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack) {
                try
                {


                    conn.Open();

                    //ddl UNIVERSITY
                    string sqlSelectUniversity = "SELECT * FROM university";
                    MySqlDataAdapter adapterUniversity = new MySqlDataAdapter(sqlSelectUniversity, conn);

                    DataSet dsUni = new DataSet();
                    adapterUniversity.Fill(dsUni);

                    ddlUniversity.DataSource = dsUni;
                    ddlUniversity.DataBind();
                }
                catch (Exception ex)
                {

                    // Label2.Text = ex.ToString();
                }
                finally
                {

                    conn.Close();
                }            
            
            }

            

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();

                //checked is email registred
                string sqlSelect = "select count(email) from user_info where email like ?email";
                MySqlCommand cmdd = new MySqlCommand(sqlSelect, conn);
                cmdd.Parameters.Add("?email", txtEmail.Text);
                cmdd.ExecuteNonQuery();

                MySqlDataAdapter adapter = new MySqlDataAdapter(cmdd);
                DataSet ds = new DataSet();
                adapter.Fill(ds);
                int temp = Convert.ToInt32(cmdd.ExecuteScalar().ToString());

                if (temp == 0) {
                   

                    //insert into user_info data about user registration

                    String sqlInsert = "INSERT INTO user_info(name, surname, email, password, accsepted, labs_id) VALUES(?name, ?surname, ?email, ?password, ?accsepted, ?labs_id)";
                    // MySqlCommand cmd = new MySqlCommand(sqlInsert, conn);
                    MySqlCommand cmd = conn.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = sqlInsert;

                    //da se dodadade za email validaciija vo vaza ili validator, i  za password isto
                    cmd.Parameters.Add("?name", txtName.Text);
                    cmd.Parameters.Add("?surname", txtSurname.Text);
                    cmd.Parameters.Add("?email", txtEmail.Text);
                    cmd.Parameters.Add("?password", txtPassword.Text);
                    cmd.Parameters.Add("?accsepted", 2);
                    cmd.Parameters.Add("?labs_id", ddlLab.SelectedValue);


                    cmd.ExecuteNonQuery();
                    Response.Redirect("Login.aspx");
                }else{

                    lblErr.Text = "email адресата е во употреба!";
            }
            }
            catch (Exception ex)
            {

                //Label1.Text = ex.ToString();
            }
            finally {

                conn.Close();
               
            }

        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("Login.aspx");
        }

        protected void ddlUniversity_SelectedIndexChanged(object sender, EventArgs e)
        {
            conn.Open();

            ddlFaculty.Items.Clear();
            ddlFaculty.Items.Add(new ListItem("-- Select faculty --"));
            ddlInstitute.Items.Clear();
            ddlInstitute.Items.Add(new ListItem("-- Select institute --"));
            ddlLab.Items.Clear();
            ddlLab.Items.Add(new ListItem("-- Select labaratory --"));
               

                //DDL FACULTY
                string sqlSelectFaculty = "SELECT f.* FROM faculty f, university u where f.university_id=u.id and f.university_id=?university_id";
                MySqlCommand cmdFaculty = new MySqlCommand(sqlSelectFaculty, conn);
                cmdFaculty.Parameters.AddWithValue("?university_id", ddlUniversity.SelectedValue);
                cmdFaculty.ExecuteNonQuery();

                MySqlDataAdapter adapterFaculty = new MySqlDataAdapter(cmdFaculty);

                DataSet dsFaculty = new DataSet();
                adapterFaculty.Fill(dsFaculty);

                ddlFaculty.DataSource = dsFaculty;
                ddlFaculty.DataBind();
            
            
            conn.Close();
        }

        protected void ddlFaculty_SelectedIndexChanged(object sender, EventArgs e)
        {
            conn.Open();
            ddlInstitute.Items.Clear();
            ddlInstitute.Items.Add(new ListItem("-- Select institute --"));
            ddlLab.Items.Clear();
            ddlLab.Items.Add(new ListItem("-- Select labaratory --"));

            //ddl INSTITUTE
            string sqlSelectInstitute = "SELECT i.* FROM institute i, faculty f where faculty_id=f.id and faculty_id=?faculty_id";
            MySqlCommand cmdInstitute = new MySqlCommand(sqlSelectInstitute, conn);
            cmdInstitute.Parameters.AddWithValue("?faculty_id", ddlFaculty.SelectedValue);
            cmdInstitute.ExecuteNonQuery();

            MySqlDataAdapter adapterInstitute = new MySqlDataAdapter(cmdInstitute);

            DataSet dsInstitute = new DataSet();
            adapterInstitute.Fill(dsInstitute);

            ddlInstitute.DataSource = dsInstitute;
            ddlInstitute.DataBind();

            conn.Close();
        }

        protected void ddlInstitute_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlLab.Items.Clear();
            ddlLab.Items.Add(new ListItem("-- Select labaratory --"));
            conn.Open();

            //DDL lABS
            string sqlSelect = "SELECT l.* FROM labs l, institute i where institute_id = i.id and institute_id=?institute_id";
            MySqlCommand cmdlab = new MySqlCommand(sqlSelect, conn);
            cmdlab.Parameters.AddWithValue("?institute_id", ddlInstitute.SelectedValue);
            cmdlab.ExecuteNonQuery();

            MySqlDataAdapter adapter = new MySqlDataAdapter(cmdlab);

            DataSet ds = new DataSet();
            adapter.Fill(ds);

            ddlLab.DataSource = ds;
            ddlLab.DataBind();

            conn.Close();
        }
    }
}