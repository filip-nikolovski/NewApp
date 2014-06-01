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
    public partial class CreateIvent : System.Web.UI.Page
    {
        static string connString = "SERVER=localhost;DATABASE=naucen_trud;UID=root;PWD=filip;Allow Zero Datetime=True;";
        MySqlConnection conn = new MySqlConnection(connString);

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    conn.Open();
                    string selectScienceSork = "SELECT * from conference;";
                    MySqlCommand cmd = new MySqlCommand(selectScienceSork, conn);
                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    adapter.Fill(ds);

                    ddlSW.DataSource = ds;
                    ddlSW.DataBind();


               

                }
                catch (Exception ex)
                {
                    lblException.Text = ex.ToString();

                }
                finally
                {
                    conn.Close();
                }

            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {

                conn.Open();
                if (ddlEvent.SelectedValue == "date_abstract")
                {
                    string insertEvent = "UPDATE conference set date_abstract=?date_abstract where id=?id";
                    MySqlCommand cmd = new MySqlCommand(insertEvent, conn);
                    cmd.Parameters.Add("?date_abstract", txtEventDate.Text);
                    cmd.Parameters.Add("?id", ddlSW.SelectedValue);
                    cmd.ExecuteNonQuery();

                    string InsertE = "Insert INTO holidays (HolidayDate, Holiday, conference_ID) Values(?HolidayDate, ?Holiday, ?conference_ID)";
                    MySqlCommand cmdI = new MySqlCommand(InsertE, conn);
                    cmdI.Parameters.Add("?HolidayDate", txtEventDate.Text);
                    cmdI.Parameters.Add("?Holiday", ddlEvent.SelectedValue);
                    cmdI.Parameters.Add("?conference_ID", ddlSW.SelectedValue);
                    cmdI.ExecuteNonQuery();

                    /**  string insertEVE = "UPDATE holidays set  HolidayDate=?HolidayDate, Holiday=?Holiday where id=?id ";
                      MySqlCommand updateEVE = new MySqlCommand(insertEVE, conn);
                      updateEVE.Parameters.Add("?HolidayDate", txtEventDate.Text);
                      updateEVE.Parameters.Add("?Holiday", ddlEvent.SelectedValue);
                      updateEVE.Parameters.Add("?id", Convert.ToInt32(cmdI.LastInsertedId));
                      updateEVE.ExecuteNonQuery();  **/
                }
                else if (ddlEvent.SelectedValue == "date_full_paper")
                {
                    string insertEvent = "UPDATE conference set date_full_paper=?date_full_paper where id=?id";
                    MySqlCommand cmd = new MySqlCommand(insertEvent, conn);
                    cmd.Parameters.Add("?date_full_paper", txtEventDate.Text);
                    cmd.Parameters.Add("?id", ddlSW.SelectedValue);
                    cmd.ExecuteNonQuery();
                }
                else if (ddlEvent.SelectedValue == "date_izvestuvajne")
                {
                    string insertEvent = "UPDATE conference set date_izvestuvajne=?date_izvestuvajne where id=?id";
                    MySqlCommand cmd = new MySqlCommand(insertEvent, conn);
                    cmd.Parameters.Add("?date_izvestuvajne", txtEventDate.Text);
                    cmd.Parameters.Add("?id", ddlSW.SelectedValue);
                    cmd.ExecuteNonQuery();
                }
                else if (ddlEvent.SelectedValue == "date_camera_redy")
                {
                    string insertEvent = "UPDATE conference set date_camera_redy=?date_camera_redy where id=?id";
                    MySqlCommand cmd = new MySqlCommand(insertEvent, conn);
                    cmd.Parameters.Add("?date_camera_redy", txtEventDate.Text);
                    cmd.Parameters.Add("?id", ddlSW.SelectedValue);
                    cmd.ExecuteNonQuery();
                }
                else 
                {
                    string insertEvent = "UPDATE conference set date_konferencija=?date_konferencija where id=?id";
                    MySqlCommand cmd = new MySqlCommand(insertEvent, conn);
                    cmd.Parameters.Add("?date_konferencija", txtEventDate.Text);
                    cmd.Parameters.Add("?id", ddlSW.SelectedValue);
                    cmd.ExecuteNonQuery();
                }
            
            }
            catch (Exception ex)
            {
                lblException.Text = ex.ToString();
            }
            finally { 
                conn.Close();
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {

        }
    }
}