using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using System.Data;
using System.IO;

namespace Diplomska
{
    public partial class EditSW : System.Web.UI.Page
    {
        static string connString = "SERVER=localhost;DATABASE=naucen_trud;UID=root;PWD=filip;Allow Zero Datetime=True;";
        MySqlConnection conn = new MySqlConnection(connString);
        protected DataSet dsHolidays;

        protected void Page_Load(object sender, EventArgs e)
        {
           // Session["New"] = "filip@finki.com";
            if (Session["New"] == null)
            {
                 Response.Redirect("Login.aspx");
            }
            else
            {
                if (Session["New"].ToString() != "admin@finki.com")
                {
                    lblLogedAs.Text = "You are logged in as " + Session["New"].ToString() + " ";

                    if (!IsPostBack)
                    {

                        try
                        {

                            conn.Open();
                   

                            //popup code
                            string em = "admin@finki.com";
                            //conn.Open();
                            string sqlSelect1 = "SELECT id, CONCAT(name ,' ',surname) as fullName FROM user_info WHERE email NOT LIKE ?email and email not like ?user and accsepted <> 2";
                            MySqlCommand cmd1 = new MySqlCommand(sqlSelect1, conn);

                            cmd1.Parameters.Add("?email", em);
                            cmd1.Parameters.Add("?user", Session["New"].ToString());

                            MySqlDataAdapter adapter1 = new MySqlDataAdapter(cmd1);
                            DataSet ds1 = new DataSet();
                            adapter1.Fill(ds1);

                            cblCorispondingAutors.DataSource = ds1;
                            cblCorispondingAutors.DataBind();


                            //code za kalendar ... (aside)
                            //code za event remider greeedView
                            //string selectReminder = "SELECT distinct h.id, Holiday from holidays h, conference c, science_work sw where h.conference_ID=c.id and c.science_work_id=sw.id and sw.autores LIKE '%" + Session["New"].ToString() + "%' OR sw.Corresponding_autor LIKE '%" + Session["New"].ToString() + "%' and h.conference_ID=c.id and c.science_work_id=sw.id ORDER BY HolidayDate ASC";
                            string sqlSel = "SELECT DISTINCT h.id, Holiday  FROM holidays h, conference c, science_work sw WHERE h.conference_ID = c.id AND c.science_work_id = sw.id AND sw.autores LIKE  '%" + Session["New"].ToString() + "%' and HolidayDate>=CURDATE( ) +0 and HolidayDate<=CURDATE( ) +5 OR h.conference_ID = c.id AND c.science_work_id = sw.id AND sw.Corresponding_autor LIKE  '%" + Session["New"].ToString() + "%' and HolidayDate>=CURDATE( ) +0 and HolidayDate<=CURDATE( ) +5 ORDER BY HolidayDate ASC";

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

                        }
                        catch (Exception ex)
                        {
                            //lblEx.Text = ex.ToString();
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

        /**  Kode za aside **/

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
            string query = "Select HolidayDate from holidays h, conference c, science_work sw where h.conference_ID=c.id and c.science_work_id=sw.id and sw.autores LIKE '%" + Session["New"].ToString() + "%' and HolidayDate >= ?firstDate and HolidayDate<?lastDate OR sw.Corresponding_autor LIKE '%" + Session["New"].ToString() + "%' and h.conference_ID=c.id and c.science_work_id=sw.id and HolidayDate >= ?firstDate and HolidayDate<?lastDate";
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
            // e.Cell.Attributes["onmouseover"] = "popupModal('" + e.Day.Date.ToString("yyyy-MM-dd") + "');";
            // e.Cell.Attributes["onmouseout"] = "popupModal1('" + e.Day.Date.ToString("yyyy-MM-dd") + "');";

            e.Day.IsSelectable = false;
            DateTime nextDate;
            //DateTime today;
            if (dsHolidays != null)
            {
                foreach (DataRow dr in dsHolidays.Tables[0].Rows)
                {
                    nextDate = (DateTime)dr["HolidayDate"];
                    if (nextDate == e.Day.Date)
                    {



                        e.Cell.BackColor = System.Drawing.Color.Blue;
                        //e.Day.IsSelectable = true;


                        string connString = "SERVER=localhost;DATABASE=naucen_trud;UID=root;PWD=filip;";
                        MySqlConnection conn = new MySqlConnection(connString);
                        conn.Open();
                        string query = "Select * from Holidays where HolidayDate=?HolidayDate";
                        MySqlCommand cmd = new MySqlCommand(query, conn);
                        cmd.Parameters.Add("?HolidayDate", nextDate);
                        MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                        DataSet ds = new DataSet();
                        adapter.Fill(ds);

                        Label l = new Label();
                        l.Text = "";
                        Label l2 = new Label();

                        foreach (DataRow dr1 in ds.Tables[0].Rows)
                        {
                            l.Text += ds.Tables[0].Rows[0]["Holiday"] + " " + e.Day.DayNumberText;
                            l2.Text += l.Text;
                            l.Text = "";
                        }

                        Session["Reminder"] = ds.Tables[0].Rows[0]["id"];
                        // Label1.Text = ds.Tables[0].Rows[0]["Holiday"] + " " + e.Day.DayNumberText;
                        e.Cell.Attributes.Add("onmouseover", "popupModal('" + l2.Text + "')");
                        //e.Cell.Attributes["onmouseout"] = "popupModal1('" + l2.Text + "')";
                        //e.Cell.Attributes.Add("onmouseout", "popupModal1('" + l2.Text + "')");
                        //  e.Cell.Attributes["onmouseover"] = "popupModal('" + ds.Tables[0].Rows[0]["Holiday"] + " " + e.Day.DayNumberText + "');";    
                        // e.Cell.Attributes["onmouseover"] = "scr('" + ds.Tables[0].Rows[0]["Holiday"] + " " + e.Day.DayNumberText + "');";
                        //  e.Cell.Attributes.Add("onmouseover", "popupModal('" + ds.Tables[0].Rows[0]["Holiday"] + " " + e.Day.DayNumberText + "')");
                        //e.Cell.Attributes["onclick"] = "popupModal1('" + ds.Tables[0].Rows[0]["Holiday"] + " " + e.Day.DayNumberText + "');";

                    }//else{

                    // e.Cell.Attributes["onmouseover"] = "popupModal1('NULL');";
                    // }

                }
            }
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


        /**  Kode za aside **/



        protected void Button1_Click(object sender, EventArgs e)
        {
            Session["New"] = null;
            Response.Redirect("Login.aspx");
        }

        protected void Button2_Click(object sender, EventArgs e)
        {

        }
    }
}