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
    public partial class Conference : System.Web.UI.Page
    {
        static string connString = "SERVER=localhost;DATABASE=naucen_trud;UID=root;PWD=filip;Allow Zero Datetime=True;";
        MySqlConnection conn = new MySqlConnection(connString);
        protected DataSet dsHolidays;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
             //Session["New"] = "filip@finki.com";
            if (Session["New"] == null)
            {
                 Session["Page"] = "Conference.aspx";
                //Response.Redirect("Login.aspx");

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
                        // if (Session["edit"].ToString() == "1") {

                        // Page.MaintainScrollPositionOnPostBack 

                        //  }


                        //dropdownlist select naucen trud
                        string selectScienceSork = "SELECT sw.* from science_work sw, created c, user_info u  WHERE c.science_work_ID=sw.id and c.user_info_ID=u.id  and sw.autores LIKE '%" + Session["New"].ToString() + "%' OR sw.Corresponding_autor LIKE '%" + Session["New"].ToString() + "%' and c.science_work_ID=sw.id and c.user_info_ID=u.id ";
                        MySqlCommand cmd = new MySqlCommand(selectScienceSork, conn);
                        MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                        DataSet ds = new DataSet();
                        adapter.Fill(ds);

                        ddlNaucenTrud.DataSource = ds;
                        ddlNaucenTrud.DataBind();

                        //gridview list conference
                        string selectConference = "SELECT c.* FROM conference c,science_work sw WHERE sw.id=c.science_work_id  and sw.autores LIKE '%" + Session["New"].ToString() + "%' OR sw.Corresponding_autor LIKE '%" + Session["New"].ToString() + "%' and sw.id=c.science_work_id";
                        MySqlCommand cmd1 = new MySqlCommand(selectConference, conn);
                        MySqlDataAdapter adapter1 = new MySqlDataAdapter(cmd1);
                        DataSet ds1 = new DataSet();
                        adapter1.Fill(ds1);
                        if (ds1.Tables[0].Rows.Count != 0)
                        {
                            gvConference.DataSource = ds1;
                            gvConference.DataBind();

                        }
                        else
                        {
                            lblTitleEmpty.Text = "Во моментот немате конференции.";
                        }


                        //code za kalendar ... (aside)
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

                    }
                    catch (Exception ex)
                    {
                        //       lblException.Text = ex.ToString();

                    }
                    finally
                    {
                        conn.Close();
                    }

                }
                else {

                    Response.Redirect("Login.aspx");
                }
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
            string query = "Select HolidayDate, h.id from holidays h, conference c, science_work sw where h.conference_ID=c.id and c.science_work_id=sw.id and sw.autores LIKE '%" + Session["New"].ToString() + "%' and HolidayDate >= ?firstDate and HolidayDate<?lastDate OR sw.Corresponding_autor LIKE '%" + Session["New"].ToString() + "%' and h.conference_ID=c.id and c.science_work_id=sw.id and HolidayDate >= ?firstDate and HolidayDate<?lastDate";
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


        /**  Kode za aside **/





        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                string sqlInsert = "INSERT INTO conference(place, state, full_name, short_name, year, reden_broj, date_abstract, date_full_paper, date_izvestuvajne, date_camera_redy, date_konferencija, cena_na_kotizacija, science_work_id) VALUES (?place, ?state, ?full_name, ?short_name, ?year, ?reden_broj, ?date_abstract, ?date_full_paper, ?date_izvestuvajne, ?date_camera_redy, ?date_konferencija, ?cena_na_kotizacija, ?science_work_id)";
                MySqlCommand cmd = new MySqlCommand(sqlInsert, conn);

                cmd.Parameters.Add("?place", txtPlace.Text);
                cmd.Parameters.Add("?state", txtState.Text);
                cmd.Parameters.Add("?full_name", txtFullName.Text);
                cmd.Parameters.Add("?short_name", txtShortName.Text);
                cmd.Parameters.Add("?year", txtYear.Text);
                cmd.Parameters.Add("?reden_broj", txtRedenBr.Text);
                cmd.Parameters.Add("?date_abstract", txtDateAbstract.Text);
                cmd.Parameters.Add("?date_full_paper", txtDateFullPaper.Text);
                cmd.Parameters.Add("?date_izvestuvajne", txtDateIzvestuvanje.Text);
                cmd.Parameters.Add("?date_camera_redy", txtDateCameraRedy.Text);
                cmd.Parameters.Add("?date_konferencija", txtDateConferencija.Text);
                cmd.Parameters.Add("?cena_na_kotizacija", txtCenaNaKotizacija.Text);
                cmd.Parameters.Add("?science_work_id", ddlNaucenTrud.SelectedValue);
                cmd.ExecuteNonQuery();



                if (txtDateAbstract.Text != null && txtDateAbstract.Text != "")
                {

                    string insEvent = "Insert into holidays(HolidayDate, Holiday, conference_ID) values(?HolidayDate, ?Holiday, ?conference_ID)";
                    MySqlCommand cmd1 = new MySqlCommand(insEvent, conn);
                    cmd1.Parameters.Add("?HolidayDate", txtDateAbstract.Text);
                    cmd1.Parameters.Add("?Holiday", "date abstract");
                    cmd1.Parameters.Add("?conference_ID", Convert.ToInt32(cmd.LastInsertedId));
                    cmd1.ExecuteNonQuery();
                }

                if (txtDateFullPaper.Text != null && txtDateFullPaper.Text != "")
                {

                    string insEvent = "Insert into holidays(HolidayDate, Holiday, conference_ID) values(?HolidayDate, ?Holiday, ?conference_ID)";
                    MySqlCommand cmd1 = new MySqlCommand(insEvent, conn);
                    cmd1.Parameters.Add("?HolidayDate", txtDateFullPaper.Text);
                    cmd1.Parameters.Add("?Holiday", "date full paper");
                    cmd1.Parameters.Add("?conference_ID", Convert.ToInt32(cmd.LastInsertedId));
                    cmd1.ExecuteNonQuery();
                }
                if (txtDateIzvestuvanje.Text != null && txtDateIzvestuvanje.Text != "")
                {

                    string insEvent = "Insert into holidays(HolidayDate, Holiday, conference_ID) values(?HolidayDate, ?Holiday, ?conference_ID)";
                    MySqlCommand cmd1 = new MySqlCommand(insEvent, conn);
                    cmd1.Parameters.Add("?HolidayDate", txtDateIzvestuvanje.Text);
                    cmd1.Parameters.Add("?Holiday", "date izvestuvajne");
                    cmd1.Parameters.Add("?conference_ID", Convert.ToInt32(cmd.LastInsertedId));
                    cmd1.ExecuteNonQuery();
                }
                if (txtDateCameraRedy.Text != null && txtDateCameraRedy.Text != "")
                {

                    string insEvent = "Insert into holidays(HolidayDate, Holiday, conference_ID) values(?HolidayDate, ?Holiday, ?conference_ID)";
                    MySqlCommand cmd1 = new MySqlCommand(insEvent, conn);
                    cmd1.Parameters.Add("?HolidayDate", txtDateCameraRedy.Text);
                    cmd1.Parameters.Add("?Holiday", "date camera redy");
                    cmd1.Parameters.Add("?conference_ID", Convert.ToInt32(cmd.LastInsertedId));
                    cmd1.ExecuteNonQuery();
                }
                if (txtDateConferencija.Text != null && txtDateConferencija.Text != "")
                {

                    string insEvent = "Insert into holidays(HolidayDate, Holiday, conference_ID) values(?HolidayDate, ?Holiday, ?conference_ID)";
                    MySqlCommand cmd1 = new MySqlCommand(insEvent, conn);
                    cmd1.Parameters.Add("?HolidayDate", txtDateConferencija.Text);
                    cmd1.Parameters.Add("?Holiday", "date konferencija");
                    cmd1.Parameters.Add("?conference_ID", Convert.ToInt32(cmd.LastInsertedId));
                    cmd1.ExecuteNonQuery();
                }




            }
            catch (Exception ex)
            {
                lblException.Text = ex.ToString();
            }
            finally
            {

                conn.Close();
                Response.Redirect("Conference.aspx");
                // addConference.Visible = false;
            }

        }




        /**   protected void btnAddConference_Click(object sender, EventArgs e)
           {
               //addConference.Visible = true;
               //btnAddConference.Attributes.Add("onclick", "scroll()");
           } **/

        protected void Button1_Click(object sender, EventArgs e)
        {
            Session["New"] = null;
            Response.Redirect("Login.aspx");
        }

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

                string sqlDelete = "DELETE FROM  conference WHERE id =@id";
                MySqlCommand cmd = new MySqlCommand(sqlDelete, conn);

                cmd.Parameters.AddWithValue("@id", Convert.ToInt32(rowIndex.ToString()));
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                //lblEx.Text = ex.ToString();
            }
            finally
            {
                conn.Close();
                Response.Redirect("Conference.aspx");
            }


        }


        protected void OrderGrid_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            Button lb = e.Row.FindControl("btnEdit") as Button;
            if (lb != null)
            {
                ScriptManager.GetCurrent(this).RegisterAsyncPostBackControl(lb);
            }

        }

        static int rId;
        //edit gridview row
        protected void btnEdit_click(object sender, EventArgs e)
        {
            btnSave.Visible = false;
            btnUpdate.Visible = true;

            lblTitle.Text = "Уреди Конференција";

            Button btnEdit = sender as Button;
            int rowIndex = Convert.ToInt32(btnEdit.Attributes["RowIndex"]);
            rId = rowIndex;
            try
            {
                conn.Open();
                string sqlSelect = "SELECT id, place, state, full_name, short_name, year, reden_broj,DATE_FORMAT(date_abstract, GET_FORMAT(DATE,'ISO'))as date_abstract, DATE_FORMAT(date_full_paper, GET_FORMAT(DATE,'ISO'))as date_full_paper, DATE_FORMAT(date_izvestuvajne, GET_FORMAT(DATE,'ISO'))as date_izvestuvajne, DATE_FORMAT(date_camera_redy, GET_FORMAT(DATE,'ISO'))as date_camera_redy, DATE_FORMAT(date_konferencija, GET_FORMAT(DATE,'ISO'))as date_konferencija, cena_na_kotizacija, science_work_id from conference where id=?id";
                MySqlCommand cmd = new MySqlCommand(sqlSelect, conn);
                //cmd.Parameters.Add("?id", "22");
                cmd.Parameters.Add("?id", rowIndex);
                cmd.ExecuteNonQuery();
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adapter.Fill(ds);


                txtPlace.Text = ds.Tables[0].Rows[0]["place"].ToString();
                txtState.Text = ds.Tables[0].Rows[0]["state"].ToString();
                txtFullName.Text = ds.Tables[0].Rows[0]["full_name"].ToString();
                txtShortName.Text = ds.Tables[0].Rows[0]["short_name"].ToString();
                txtYear.Text = ds.Tables[0].Rows[0]["year"].ToString();
                txtRedenBr.Text = ds.Tables[0].Rows[0]["reden_broj"].ToString();
                txtDateAbstract.Text = ds.Tables[0].Rows[0]["date_abstract"].ToString();
                txtDateFullPaper.Text = ds.Tables[0].Rows[0]["date_full_paper"].ToString();
                txtDateIzvestuvanje.Text = ds.Tables[0].Rows[0]["date_izvestuvajne"].ToString();
                txtDateCameraRedy.Text = ds.Tables[0].Rows[0]["date_camera_redy"].ToString();
                txtDateConferencija.Text = ds.Tables[0].Rows[0]["date_konferencija"].ToString();
                txtCenaNaKotizacija.Text = ds.Tables[0].Rows[0]["cena_na_kotizacija"].ToString();

                ddlNaucenTrud.SelectedIndex = ddlNaucenTrud.Items.IndexOf(ddlNaucenTrud.Items.FindByValue(ds.Tables[0].Rows[0]["science_work_id"].ToString()));

            }
            catch (Exception)
            {
                //lblEx.Text = ex.ToString();
            }
            finally
            {
                conn.Close();
                //Session["edit"] = "1";


            }


        }


        //on mouse over gridview
        protected void gvConference_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onmouseover"] = "this.style.background='#D1DDF1';";

                if ((e.Row.RowIndex % 2) == 0)  // if even row
                    e.Row.Attributes["onmouseout"] = "this.style.background='#EFF3FB';";
                else  // alternate row
                    e.Row.Attributes["onmouseout"] = "this.style.background='White';";
                // e.Row.ToolTip = "Click to see details the event";
                //e.Row.Attributes["onclick"] = this.Page.ClientScript.GetPostBackClientHyperlink(this.gvConference, "Select$" + e.Row.RowIndex);


            }
        }


        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            //try
            // {
            conn.Open();
            string sqlInsert = "Update conference set place=?place, state=?state, full_name=?full_name, short_name=?short_name, year=?year, reden_broj=?reden_broj, date_abstract=?date_abstract, date_full_paper=?date_full_paper, date_izvestuvajne=?date_izvestuvajne, date_camera_redy=?date_camera_redy, date_konferencija=?date_konferencija, cena_na_kotizacija=?cena_na_kotizacija, science_work_id=?science_work_id where id=?id";
            MySqlCommand cmd = new MySqlCommand(sqlInsert, conn);

            cmd.Parameters.Add("id", rId.ToString());
            cmd.Parameters.Add("?place", txtPlace.Text);
            cmd.Parameters.Add("?state", txtState.Text);
            cmd.Parameters.Add("?full_name", txtFullName.Text);
            cmd.Parameters.Add("?short_name", txtShortName.Text);
            cmd.Parameters.Add("?year", txtYear.Text);
            cmd.Parameters.Add("?reden_broj", txtRedenBr.Text);
            cmd.Parameters.Add("?date_abstract", txtDateAbstract.Text);
            cmd.Parameters.Add("?date_full_paper", txtDateFullPaper.Text);
            cmd.Parameters.Add("?date_izvestuvajne", txtDateIzvestuvanje.Text);
            cmd.Parameters.Add("?date_camera_redy", txtDateCameraRedy.Text);
            cmd.Parameters.Add("?date_konferencija", txtDateConferencija.Text);
            cmd.Parameters.Add("?cena_na_kotizacija", txtCenaNaKotizacija.Text);
            cmd.Parameters.Add("?science_work_id", ddlNaucenTrud.SelectedValue);
            cmd.ExecuteNonQuery();

            if (txtDateAbstract.Text != null)
            {

                string insEvent = "UPDATE holidays set HolidayDate=?HolidayDate where conference_ID=?conference_ID and Holiday=?Holiday";
                MySqlCommand cmd1 = new MySqlCommand(insEvent, conn);
                cmd1.Parameters.Add("?HolidayDate", txtDateAbstract.Text);
                cmd1.Parameters.Add("?Holiday", "date_abstract");
                cmd1.Parameters.Add("?conference_ID", rId.ToString());
                cmd1.ExecuteNonQuery();
            }

            if (txtDateFullPaper.Text != null)
            {

                string insEvent = "UPDATE holidays set HolidayDate=?HolidayDate where conference_ID=?conference_ID and Holiday=?Holiday";
                MySqlCommand cmd1 = new MySqlCommand(insEvent, conn);
                cmd1.Parameters.Add("?HolidayDate", txtDateFullPaper.Text);
                cmd1.Parameters.Add("?Holiday", "date_full_paper");
                cmd1.Parameters.Add("?conference_ID", rId.ToString());
                cmd1.ExecuteNonQuery();
            }
            if (txtDateIzvestuvanje.Text != null)
            {

                string insEvent = "UPDATE holidays set HolidayDate=?HolidayDate where conference_ID=?conference_ID and Holiday=?Holiday";
                MySqlCommand cmd1 = new MySqlCommand(insEvent, conn);
                cmd1.Parameters.Add("?HolidayDate", txtDateIzvestuvanje.Text);
                cmd1.Parameters.Add("?Holiday", "date_izvestuvajne");
                cmd1.Parameters.Add("?conference_ID", rId.ToString());
                cmd1.ExecuteNonQuery();
            }
            if (txtDateCameraRedy.Text != null)
            {

                string insEvent = "UPDATE holidays set HolidayDate=?HolidayDate where conference_ID=?conference_ID and Holiday=?Holiday";
                MySqlCommand cmd1 = new MySqlCommand(insEvent, conn);
                cmd1.Parameters.Add("?HolidayDate", txtDateCameraRedy.Text);
                cmd1.Parameters.Add("?Holiday", "date_camera_redy");
                cmd1.Parameters.Add("?conference_ID", rId.ToString());
                cmd1.ExecuteNonQuery();
            }
            if (txtDateConferencija.Text != null)
            {

                string insEvent = "UPDATE holidays set HolidayDate=?HolidayDate  where conference_ID=?conference_ID and Holiday=?Holiday";
                MySqlCommand cmd1 = new MySqlCommand(insEvent, conn);
                cmd1.Parameters.Add("?HolidayDate", txtDateConferencija.Text);
                cmd1.Parameters.Add("?Holiday", "date_konferencija");
                cmd1.Parameters.Add("?conference_ID", rId.ToString());
                cmd1.ExecuteNonQuery();
            }






            // }
            // catch (Exception ex)
            // {
            //     lblException.Text = ex.ToString();
            // }
            //  finally
            //  {

            conn.Close();
            Response.Redirect("Conference.aspx");

            // }

        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            lblTitle.Text = "Креирај Конференција";
            btnSave.Visible = true;
            btnUpdate.Visible = false;
        }

        protected void btnDetails_Click(object sender, EventArgs e)
        {
         
            Button btnDetails = sender as Button;
            int rowIndex = Convert.ToInt32(btnDetails.Attributes["RowIndex"].ToString());
           
            ModalPopupExtender2.Show();
            btnDetails.Attributes.Add("onclick", "return ShowModalPopup1()");
            try
            {
                conn.Open();
                string sqlSel = "select * from conference where id=?id";
                MySqlCommand cmd = new MySqlCommand(sqlSel, conn);
                cmd.Parameters.Add("?id", rowIndex);
                cmd.ExecuteNonQuery();
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adapter.Fill(ds);

                lblPopUpSWDetails.Text = "Детали";
                lblPopTitle.Text = ds.Tables[0].Rows[0]["full_name"].ToString();
                lblPopAutors.Text = ds.Tables[0].Rows[0]["short_name"].ToString();
                lblPopCorrAutors.Text = ds.Tables[0].Rows[0]["state"].ToString();
                lblPopPrice.Text = ds.Tables[0].Rows[0]["place"].ToString();
                lblPopDate.Text = ds.Tables[0].Rows[0]["year"].ToString();
                lblPopAcc.Text = ds.Tables[0].Rows[0]["reden_broj"].ToString();
                lblPopDescription.Text = ds.Tables[0].Rows[0]["date_abstract"].ToString();
                lblPopDFP.Text = ds.Tables[0].Rows[0]["date_full_paper"].ToString();
                lblPopDI.Text = ds.Tables[0].Rows[0]["date_izvestuvajne"].ToString();
                lblPopDCR.Text = ds.Tables[0].Rows[0]["date_camera_redy"].ToString();
                lblPopDK.Text = ds.Tables[0].Rows[0]["date_konferencija"].ToString();
                lblPopCenaK.Text = ds.Tables[0].Rows[0]["cena_na_kotizacija"].ToString();


            }
            catch { }
            finally
            {
               
                conn.Close();
            }
        }

        protected void btnPopCancel_Click(object sender, EventArgs e)
        {

        }





        /**     protected void gvReminder_SelectedIndexChanged(object sender, EventArgs e)
             {
                 // lblErr.Text = gvReminder.SelectedDataKey.Value.ToString();
                 Session["Reminder"] = gvReminder.SelectedDataKey.Value.ToString();
                 Response.Redirect("ViewEvent.aspx");
             }**/


    }
}