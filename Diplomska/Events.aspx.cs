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
    public partial class Events : System.Web.UI.Page
    {
        static string connString = "SERVER=localhost;DATABASE=naucen_trud;UID=root;PWD=filip;Allow Zero Datetime=True;";
        MySqlConnection conn = new MySqlConnection(connString);
        protected DataSet dsHolidays;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                Session["New"] = "filip@finki.com";
                if (Session["New"] == null)
                {
                    Response.Redirect("Login.aspx");
                }
                else
                {
                    if (Session["New"].ToString() != "admin@finki.com")
                    {
                        lblLogedAs.Text = "You are logged in as ";
                        lblLogedAs1.Text = Session["New"].ToString() + " ";

                        try
                        {
                            conn.Open();

                            //code za eventi gridview 
                            string selectEvent = "Select DISTINCT h.id, HolidayDate,CONCAT(title ,' - ',full_name,', ',Holiday) as event from holidays h, conference c, science_work sw where h.conference_ID=c.id and sw.id=c.science_work_id  and sw.autores LIKE '%" + Session["New"].ToString() + "%' OR sw.Corresponding_autor LIKE '%" + Session["New"].ToString() + "%' and h.conference_ID=c.id and sw.id=c.science_work_id ORDER BY HolidayDate ASC ";
                            MySqlCommand cmd = new MySqlCommand(selectEvent, conn);
                            //cmd.Parameters.Add("?Corresponding_autor", "filip@finki.com");
                            // cmd.Parameters.Add("?Corresponding_autor", Session["New"].ToString());
                            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                            DataSet ds = new DataSet();
                            adapter.Fill(ds);

                            if (ds.Tables[0].Rows.Count != 0)
                            {
                                gvEvents.DataSource = ds;
                                gvEvents.DataBind();
                            }
                            else
                            {
                                lblTitleEmpty.Text = "Немате настани во моментот. ";
                            }


                            // lblErr.Text = Session["New"].ToString();

                            //code za event remider greeedView
                            //string selectReminder = "SELECT distinct h.id, Holiday from holidays h, conference c, science_work sw where h.conference_ID=c.id and c.science_work_id=sw.id and sw.autores LIKE '%" + Session["New"].ToString() + "%' OR sw.Corresponding_autor LIKE '%" + Session["New"].ToString() + "%' and h.conference_ID=c.id and c.science_work_id=sw.id ORDER BY HolidayDate ASC";
                            string sqlSel = "SELECT DISTINCT h.id, CONCAT(full_name,'-',Holiday) as hol, HolidayDate  FROM holidays h, conference c, science_work sw WHERE h.conference_ID = c.id AND c.science_work_id = sw.id AND sw.autores LIKE  '%" + Session["New"].ToString() + "%' and HolidayDate>=CURDATE( ) +0 and HolidayDate<=DATE_ADD(NOW(), INTERVAL 5 DAY) OR h.conference_ID = c.id AND c.science_work_id = sw.id AND sw.Corresponding_autor LIKE  '%" + Session["New"].ToString() + "%' and HolidayDate>=CURDATE( ) +0 and HolidayDate<=DATE_ADD(NOW(), INTERVAL 5 DAY) ORDER BY HolidayDate ASC";

                            MySqlCommand cmdReminder = new MySqlCommand(sqlSel, conn);
                            // cmdReminder.Parameters.Add("?HolidayDate", DateTime.Now.ToString("yyyy-MM-dd"));
                            MySqlDataAdapter ad = new MySqlDataAdapter(cmdReminder);
                            DataSet das = new DataSet();
                            ad.Fill(das);

                            gvReminder.DataSource = das;
                            gvReminder.DataBind();


                            //calendar code
                            Calendar1.VisibleDate = DateTime.Today;
                            FillHolidayDataSet();

                            //ddl code
                            string selectScienceSork = "SELECT c.*, CONCAT(title,' - ',full_name) as fname from conference c,science_work sw where sw.id=c.science_work_id  and sw.autores LIKE '%" + Session["New"].ToString() + "%' OR sw.Corresponding_autor LIKE '%" + Session["New"].ToString() + "%' and sw.id=c.science_work_id";
                            MySqlCommand cmdDDL = new MySqlCommand(selectScienceSork, conn);
                            MySqlDataAdapter adapterDDL = new MySqlDataAdapter(cmdDDL);
                            DataSet dsDDL = new DataSet();
                            adapterDDL.Fill(dsDDL);

                            ddlSW.DataSource = dsDDL;
                            ddlSW.DataBind();

                        }
                        catch (Exception) { }
                        finally
                        {
                            conn.Close();
                        }


                    }
                    else
                    {

                        Response.Redirect("Login.aspx");
                    }
                }
            }
        }

        //calendar code
        protected void FillHolidayDataSet()
        {
            DateTime firstDate = new DateTime(Calendar1.VisibleDate.Year, Calendar1.VisibleDate.Month, 1);
            DateTime lastDate = GetFirstDayOfNextMonth();
            dsHolidays = GetCurrentMonthData(firstDate, lastDate);
        }

        protected DateTime GetFirstDayOfNextMonth()
        {

            int monthNumer, yearNumber;
            if (Calendar1.VisibleDate.Month == 12)
            {
                monthNumer = 1;
                yearNumber = Calendar1.VisibleDate.Year + 1;

            }
            else
            {
                monthNumer = Calendar1.VisibleDate.Month + 1;
                yearNumber = Calendar1.VisibleDate.Year;

            }
            DateTime lastDate = new DateTime(yearNumber, monthNumer, 1);
            return lastDate;
        }

        protected DataSet GetCurrentMonthData(DateTime firstDate, DateTime lastDate)
        {
            DataSet dsMonth = new DataSet();

            string connString = "SERVER=localhost;DATABASE=naucen_trud;UID=root;PWD=filip;";
            MySqlConnection dbConnection = new MySqlConnection(connString);
            string query = "Select HolidayDate, h.id, Holiday from holidays h, conference c, science_work sw where h.conference_ID=c.id and c.science_work_id=sw.id and sw.autores LIKE '%" + Session["New"].ToString() + "%' and HolidayDate >= ?firstDate and HolidayDate<?lastDate OR sw.Corresponding_autor LIKE '%" + Session["New"].ToString() + "%' and h.conference_ID=c.id and c.science_work_id=sw.id and HolidayDate >= ?firstDate and HolidayDate<?lastDate";
            MySqlCommand dbCommand = new MySqlCommand(query, dbConnection);
            dbCommand.Parameters.Add("?firstDate", firstDate);
            dbCommand.Parameters.Add("?lastDate", lastDate);

            MySqlDataAdapter sqlDataAdapter = new MySqlDataAdapter(dbCommand);

            try
            {
                sqlDataAdapter.Fill(dsMonth);
            }
            catch { }
            return dsMonth;
        }

        protected void Calendar1_DayRender(object sender, DayRenderEventArgs e)
        {
            conn.Open();
            e.Day.IsSelectable = false;
            DateTime nextDate;
            //DateTime today;
            Label l3 = new Label();
            if (dsHolidays != null)
            {
                DataTable table = new DataTable();
                table.Columns.Add("id");
                foreach (DataRow dr in dsHolidays.Tables[0].Rows)
                {
                    nextDate = (DateTime)dr["HolidayDate"];
                    if (nextDate == e.Day.Date)
                    {
                        e.Cell.Style["background"] = "#2FBDF1";
                        Label l = new Label();
                        l.Text = "";
                        Label l2 = new Label();
                        l3.Text = e.Day.Date.ToString("yyyy-MM-dd");


                        


                       gvCalendar.DataSource = dsHolidays;
                       gvCalendar.DataBind();



                       Session["Reminder"] = dr["id"].ToString();    
                        e.Cell.Attributes.Add("onmouseover", "popupModal('" + l2.Text + "','" + l3.Text + "')");

                    }

                   

                }
            }

/**
            string sqlsel = "SELECT h.* from holidays h, conference c, science_work sw where h.conference_ID=c.id and c.science_work_id=sw.id and sw.autores LIKE '%" + Session["New"].ToString() + "%'  and HolidayDate = date (?selDate ) OR  HolidayDate = date(?selDate) and sw.Corresponding_autor LIKE '%" + Session["New"].ToString() + "%' and h.conference_ID=c.id and c.science_work_id=sw.id";
            MySqlCommand command = new MySqlCommand(sqlsel, conn);
            command.Parameters.AddWithValue("?selDate", l3.Text);
            command.ExecuteNonQuery();
            MySqlDataAdapter ad = new MySqlDataAdapter(command);
            DataSet dataSet = new DataSet();
            ad.Fill(dataSet);

            

                    gvCalendar.DataSource = dataSet;
                    gvCalendar.DataBind();
                
            **/


            conn.Close();
           




        }
        protected void Calendar1_VisibleMonthChanged(object sender,
   MonthChangedEventArgs e)
        {
            FillHolidayDataSet();
        }

        protected void Calendar1_SelectionChanged(object sender, EventArgs e)
        {
            FillHolidayDataSet();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Session["New"] = null;
            Response.Redirect("Login.aspx");
        }

        /**  protected void btnNew_Click(object sender, EventArgs e)
          {
              Response.Redirect("CreateIvent.aspx");
          }
          **/

        //GridView code 
        protected void gvEvents_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                e.Row.Attributes["onmouseover"] = "this.style.background='#D1DDF1';";

                if ((e.Row.RowIndex % 2) == 0)  // if even row
                    e.Row.Attributes["onmouseout"] = "this.style.background='#EFF3FB';";
                else  // alternate row
                    e.Row.Attributes["onmouseout"] = "this.style.background='White';";

                e.Row.ToolTip = "Click to view the event";
                e.Row.Attributes["onclick"] = this.Page.ClientScript.GetPostBackClientHyperlink(this.gvEvents, "Select$" + e.Row.RowIndex);
            }
        }

        protected void gvEvents_SelectedIndexChanged(object sender, EventArgs e)
        {
            // lblErr.Text = gvReminder.SelectedDataKey.Value.ToString();
            Session["Reminder"] = gvEvents.SelectedDataKey.Value.ToString();
            Response.Redirect("ViewEvent.aspx");
        }


        //gvReminder
        protected void gvReminder_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onmouseover"] = "this.style.background='#D1DDF1';";

                if ((e.Row.RowIndex % 2) == 0)  // if even row
                    e.Row.Attributes["onmouseout"] = "this.style.background='#EFF3FB';";
                else  // alternate row
                    e.Row.Attributes["onmouseout"] = "this.style.background='White';";
                e.Row.ToolTip = "Click to view the event";
                e.Row.Attributes["onclick"] = this.Page.ClientScript.GetPostBackClientHyperlink(this.gvReminder, "Select$" + e.Row.RowIndex);
            }
        }

        protected void gvReminder_SelectedIndexChanged(object sender, EventArgs e)
        {
            // lblErr.Text = gvReminder.SelectedDataKey.Value.ToString();

            //   if (gvReminder.SelectedDataKey == null)
            //  {
            //    Session["Reminder"] = gvEvents.SelectedDataKey.Value.ToString();
            //  }
            //   else {
            Session["Reminder"] = gvReminder.SelectedDataKey.Value.ToString();
            Response.Redirect("ViewEvent.aspx");
            // }

        }




        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {

                conn.Open();
                string sel = "SELECT count(*) from holidays where conference_ID=?conference_ID and Holiday=?Holiday";
                MySqlCommand cmm = new MySqlCommand(sel, conn);
                cmm.Parameters.Add("?conference_ID", ddlSW.SelectedValue);
                cmm.Parameters.Add("?Holiday", ddlEvent.SelectedValue);
                cmm.ExecuteNonQuery();
                int temp = Convert.ToInt32(cmm.ExecuteScalar().ToString());
                if (temp == 0)
                {



                    string InsertE = "Insert INTO holidays (HolidayDate, Holiday, conference_ID) Values(?HolidayDate, ?Holiday, ?conference_ID)";
                    MySqlCommand cmdI = new MySqlCommand(InsertE, conn);
                    cmdI.Parameters.Add("?HolidayDate", txtEventDate.Text);
                    cmdI.Parameters.Add("?Holiday", ddlEvent.SelectedItem.Text);
                    cmdI.Parameters.Add("?conference_ID", ddlSW.SelectedValue);
                    cmdI.ExecuteNonQuery();

                    if (ddlEvent.SelectedValue == "date_abstract")
                    {
                        string insertEvent = "UPDATE conference set date_abstract=?date_abstract where id=?id";
                        MySqlCommand cmd = new MySqlCommand(insertEvent, conn);
                        cmd.Parameters.Add("?date_abstract", txtEventDate.Text);
                        cmd.Parameters.Add("?id", ddlSW.SelectedValue);
                        cmd.ExecuteNonQuery();
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
                    Response.Redirect("Events.aspx");
                }
                else
                {

                    lblErr.Text = "Настанот што сакате да го креирате за конференцијата " + ddlSW.DataTextField + "веќе постои";
                }

            }
            catch (Exception)
            {
                //lblException.Text = ex.ToString();
            }
            finally
            {
                conn.Close();

            }
        }

        protected void gvCalendar_RowCreated(object sender, GridViewRowEventArgs e)
        {

            e.Row.ToolTip = "Click to view the event";
            e.Row.Attributes["onclick"] = this.Page.ClientScript.GetPostBackClientHyperlink(this.gvCalendar, "Select$" + e.Row.RowIndex);
       
        }



        protected void gvCalendar_SelectedIndexChanged(object sender, EventArgs e)
        {

            //Session["Reminder"] = gvCalendar.SelectedDataKey.Value.ToString();               
            Response.Redirect("ViewEvent.aspx");
        }

   

   
      


    }
}