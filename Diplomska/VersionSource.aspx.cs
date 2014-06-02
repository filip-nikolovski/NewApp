using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using MySql.Data.MySqlClient;
using System.IO;
using Ionic.Zip;


namespace Diplomska
{
    public partial class VersionSource : System.Web.UI.Page
    {

      //  static string connString = "SERVER=localhost;DATABASE=naucen_trud;UID=root;PWD=filip;Allow Zero Datetime=True;";
        static string connString = "SERVER=sql5.freemysqlhosting.net;DATABASE=sql542315;UID=sql542315;PWD=pW5!hL3%;Allow Zero Datetime=True;";
        MySqlConnection conn = new MySqlConnection(connString);
        protected DataSet dsHolidays;


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
               // Session["New"] = "filip@finki.com";
               // Session["Source"] = "116";
                if (Session["New"] == null)
                {
                    Response.Redirect("Login.aspx");
                    //Response.Redirect("Events.aspx");
                }
                else
                {
                    if (Session["New"].ToString() != "admin@finki.com")
                    {

                        lblLogedAs.Text = "You are logged in as ";
                        lblLogedAs1.Text = Session["New"].ToString() + " ";



                        //  try
                        //   {
                        conn.Open();
/*

                        //code za kalendar ... (aside)
                        string sqlSel = "SELECT DISTINCT h.id, CONCAT(full_name,' - ',Holiday) as hol, HolidayDate  FROM holidays h, conference c, science_work sw WHERE h.conference_ID = c.id AND c.science_work_id = sw.id AND sw.autores LIKE  '%" + Session["New"].ToString() + "%' and HolidayDate>=CURDATE( ) +0 and HolidayDate<=DATE_ADD(NOW(), INTERVAL 5 DAY) OR h.conference_ID = c.id AND c.science_work_id = sw.id AND sw.Corresponding_autor LIKE  '%" + Session["New"].ToString() + "%' and HolidayDate>=CURDATE( ) +0 and HolidayDate<=DATE_ADD(NOW(), INTERVAL 5 DAY) ORDER BY HolidayDate ASC";

                        MySqlCommand cmdReminder = new MySqlCommand(sqlSel, conn);
                        MySqlDataAdapter ad = new MySqlDataAdapter(cmdReminder);
                        DataSet das = new DataSet();
                        ad.Fill(das);

                        gvReminder.DataSource = das;
                        gvReminder.DataBind();

                        //calendar code
                        Calendar1.VisibleDate = DateTime.Today;
                        FillHolidayDataSet();
                        */

                        Session.Add("filename", Session["Source"].ToString());
                        

                        //TITLE CODE
                        string selectTit = "Select v.*,sw.title from versions v,science_work sw where id_version = ?id and id_science_work = sw.id";
                        MySqlCommand cmdSelect = new MySqlCommand(selectTit, conn);
                        cmdSelect.Parameters.AddWithValue("?id", Session["Source"].ToString());
                        cmdSelect.ExecuteNonQuery();
                        MySqlDataAdapter adapterSelect = new MySqlDataAdapter(cmdSelect);
                        DataSet dsSel = new DataSet();

                        adapterSelect.Fill(dsSel);

                        lblSWTitle.Text ="source files ../"+ dsSel.Tables[0].Rows[0]["version_name"].ToString();
                        lblNav.Text = dsSel.Tables[0].Rows[0]["version_name"].ToString();

                        string sw = dsSel.Tables[0].Rows[0]["title"].ToString();
                        string vName = dsSel.Tables[0].Rows[0]["version_name"].ToString();

                        //GRIDVIEW FILES CODE
                        //if()
                        CreateDirectoryIfNotExist(Server.MapPath("~/uploads/projects/" +sw+"/"+ vName+"/sourcefiles"));
                        string[] filePaths = Directory.GetFiles(Server.MapPath("~/uploads/projects/" +sw+"/"+ vName + "/sourcefiles"));
                        //string[] filePaths = Directory.GetFiles(Server.MapPath("~/Files/"));
                        List<ListItem> files = new List<ListItem>();

                        foreach (string filePath in filePaths)
                        {
                            files.Add(new ListItem(Path.GetFileName(filePath), filePath));
                        }
                        GridView1.DataSource = files;
                        GridView1.DataBind();

                        // }
                        // catch (Exception) { }
                        //  finally
                        //  {
                        conn.Close();
                        // }

                
                    }
                    else
                    {

                        Response.Redirect("Login.aspx");
                    }
                }
               
            }

        }


        /**  Kode za aside **/

        //calendar code
     /*   protected void FillHolidayDataSet()
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

                        e.Cell.Style["background"] = "#2FBDF1";

                        // e.Cell.BackColor = System.Drawing.Color.Blue;
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
                        Label l3 = new Label();

                        foreach (DataRow dr1 in ds.Tables[0].Rows)
                        {
                            l.Text += ds.Tables[0].Rows[0]["Holiday"] + " ";
                            l3.Text = e.Day.Date.ToString("yyyy-MM-dd");
                            l2.Text += l.Text;
                            l.Text = "";
                        }

                        Session["Reminder"] = ds.Tables[0].Rows[0]["id"];
                        // Label1.Text = ds.Tables[0].Rows[0]["Holiday"] + " " + e.Day.DayNumberText;
                        e.Cell.Attributes.Add("onmouseover", "popupModal('" + l2.Text + "','" + l3.Text + "')");
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

*/
        /**  Kode za aside **/



        protected void Button1_Click(object sender, EventArgs e)
        {
            Session["New"] = null;
            Response.Redirect("Login.aspx");
        }

        protected void DownloadFiles(object sender, EventArgs e)
        {
            using (ZipFile zip = new ZipFile())
            {
                zip.AlternateEncodingUsage = ZipOption.AsNecessary;
                zip.AddDirectoryByName("Files");
                foreach (GridViewRow row in GridView1.Rows)
                {
                    if ((row.FindControl("chkSelect") as CheckBox).Checked)
                    {
                        string filePath = (row.FindControl("lblFilePath") as Label).Text;
                        zip.AddFile(filePath, "Files");
                    }
                }
                Response.Clear();
                Response.BufferOutput = false;
                string zipName = String.Format("Zip_{0}.zip", DateTime.Now.ToString("yyyy-MMM-dd-HHmmss"));
                Response.ContentType = "application/zip";
                Response.AddHeader("content-disposition", "attachment; filename=" + zipName);
                zip.Save(Response.OutputStream);
                Response.End();

                lblerr.Text = "Sucess!";
            }
        }


        protected void btnDownloadAll_Click(object sender, EventArgs e)
        {
            using (ZipFile zip = new ZipFile())
            {
                zip.AlternateEncodingUsage = ZipOption.AsNecessary;
                zip.AddDirectoryByName("Files");
                foreach (GridViewRow row in GridView1.Rows)
                {
                  
                        string filePath = (row.FindControl("lblFilePath") as Label).Text;
                        zip.AddFile(filePath, "Files");
                    
                }
                Response.Clear();
                Response.BufferOutput = false;
                string zipName = String.Format("Zip_{0}.zip", DateTime.Now.ToString("yyyy-MMM-dd-HHmmss"));
                Response.ContentType = "application/zip";
                Response.AddHeader("content-disposition", "attachment; filename=" + zipName);
                zip.Save(Response.OutputStream);
                Response.End();

                lblerr.Text = "Sucess!";
            }
        }

        protected void DownloadFile(object sender, EventArgs e)
        {
            string filePath = (sender as LinkButton).CommandArgument;
            Response.ContentType = ContentType;
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(filePath));
            Response.WriteFile(filePath);
            Response.End();
        }

        protected void DeleteFile(object sender, EventArgs e)
        {
            string filePath = (sender as LinkButton).CommandArgument;
            File.Delete(filePath);
            Response.Redirect(Request.Url.AbsoluteUri);
        }

        protected void CreateDirectoryIfNotExist(string NewDirectory)
        {
            try
            {
                if (!Directory.Exists(NewDirectory))
                {
                    Directory.CreateDirectory(NewDirectory);
                }
            }
            catch (Exception e)
            {
                lblerr.Text = e.Message;
            }
        }

        protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onmouseover"] = "this.style.background='#D1DDF1';";

               // if ((e.Row.RowIndex % 2) == 0)  // if even row
                e.Row.Attributes["onmouseout"] = "this.style.background='#F7F6F3';";
               // else  // alternate row
                 //   e.Row.Attributes["onmouseout"] = "this.style.background='White';";
            }
        }

   
    }
}