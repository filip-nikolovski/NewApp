using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using System.Data;
using AjaxControlToolkit;
using System.IO;
using Ionic.Zip;

namespace Diplomska
{
    public partial class Versions : System.Web.UI.Page
    {
        static string connString = "SERVER=localhost;DATABASE=naucen_trud;UID=root;PWD=filip;Allow Zero Datetime=True;";
        MySqlConnection conn = new MySqlConnection(connString);
        protected DataSet dsHolidays;
        static string swName = "";
        static string verName = "";

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
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
                            string sqlSelect = "SELECT v.*, sw.title FROM versions v, science_work sw WHERE v.id_science_work = sw.id and sw.id=" + Convert.ToInt32(Session["sess"].ToString());
                            MySqlCommand cmd = new MySqlCommand(sqlSelect, conn);

                            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                            DataSet ds = new DataSet();
                            adapter.Fill(ds);

                            gvVersions.DataSource = ds;
                            gvVersions.DataBind();
                            
                            if (ds.Tables[0].Rows[0]["title"] != null)
                            {
                                lblTitle.Text = ds.Tables[0].Rows[0]["title"].ToString();
                                swName = ds.Tables[0].Rows[0]["title"].ToString();
                               
                            }
                            // else {

                            //   lblTitle.Text = "Избраниот труд нема креирано верзии.";
                            // } 

            /**                
                            foreach (GridViewRow gv in gvVersions.Rows) {

                                if (gv.RowType == DataControlRowType.DataRow) {
                                    if (ds.Tables[0].Rows[0]["active"].ToString() == "1") {
                                        var radio = (RadioButton)gv.FindControl("rb");
                                        radio.Checked = true;
                                    }

                                    
                                }
                            }**/
                        
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

                            //RadioButtonList11.SelectedIndex = Math.Abs(Convert.ToInt32(ds.Tables[0].Rows[0]["active"].ToString()) - 1);


                        }
                        catch (Exception)
                        {
                            //lblEx.Text = ex.ToString();
                            btnSelectVesion.Visible = false;
                        }
                        finally
                        {
                            conn.Clone();
                        }



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


                        e.Cell.Style["background"] = "#2FBDF1";
                        //e.Cell.BackColor = System.Drawing.Color.Blue;
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
                            l.Text += ds.Tables[0].Rows[0]["Holiday"] + " " ;
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


        protected void Button1_Click(object sender, EventArgs e)
        {
            Session["New"] = null;
            Response.Redirect("Login.aspx");
        }

        protected void gvVersions_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblEx.Text = gvVersions.SelectedValue.ToString();
            //newVersion.Visible = true;
           // btnConfirmAdd.Visible = false;
           // btnUpdate.Visible = true;
        }

        //za aktivna verzija
        protected void btnSelectVesion_Click(object sender, EventArgs e)
        {
            string selectedValue = Request.Form["MyRadioButton"];
            //string  selectedValue="";
            //lblEx.Text = selectedValue;

            


            try
            {

                conn.Open();

                string sqlSetActiveVersion = "UPDATE versions SET  active=?active WHERE id_version=?id_version";
                MySqlCommand cmd = new MySqlCommand(sqlSetActiveVersion, conn);
                cmd.Parameters.Add("?active", 1);
                cmd.Parameters.Add("?id_version", selectedValue);
                cmd.ExecuteNonQuery();

                string sqlUpdateNonActiveVersion = "UPDATE versions SET  active=?active WHERE id_version NOT LIKE ?id_version and id_science_work=?id_science_work";
                MySqlCommand cmd1 = new MySqlCommand(sqlUpdateNonActiveVersion, conn);
                cmd1.Parameters.Add("?active", '0');
                cmd1.Parameters.Add("?id_version", selectedValue);
                cmd1.Parameters.Add("?id_science_work", Convert.ToInt32(Session["sess"].ToString()));
                cmd1.ExecuteNonQuery();
            }
            catch
           {

            }
            finally
            {

                conn.Clone();
                Response.Redirect("Versions.aspx");
            }
        }
    /**    protected void btnNew_Click1(object sender, EventArgs e)
        {
            
            newVersion.Visible = true;
            btnUpdate.Visible = false;
            btnConfirmAdd.Visible = true;
        } **/

        protected void btnConfirmAdd_Click(object sender, EventArgs e)
        {
            if (FileUpload1.HasFile)
            {



                conn.Open();
                string file_name;
                int file_size;

                string fileExt = System.IO.Path.GetExtension(FileUpload1.FileName);

                string sqlSelect = "SELECT sw.title FROM science_work sw WHERE  sw.id=" + Convert.ToInt32(Session["sess"].ToString());
                MySqlCommand cmd = new MySqlCommand(sqlSelect, conn);

                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adapter.Fill(ds);


                string sw = ds.Tables[0].Rows[0]["title"].ToString();

               //ok so razlicen folder za sekoj proekt
                 CreateDirectoryIfNotExist(Server.MapPath("~/uploads/projects/" + sw+"/"+txtVersionName.Text));
                 FileUpload1.SaveAs(Server.MapPath("~/uploads/projects/" + sw + "/" + txtVersionName.Text + "/" + FileUpload1.FileName));

                //folder za source file za sekoja verzija
                //CreateDirectoryIfNotExist(Server.MapPath("~/Images/Projects/" + txtVersionName.Text));
                //FileUpload1.SaveAs(Server.MapPath("~/Images/Projects/" + swName + "/" + FileUpload1.FileName));

               // FileUpload1.SaveAs(Server.MapPath("~/Images/Projects/" + swName + "/" + FileUpload1.FileName));
              //  FileUpload1.SaveAs("D:\\Uploads\\" +txtVersionName.Text+"_"+ FileUpload1.FileName);
                file_name = FileUpload1.PostedFile.FileName;
                file_size = FileUpload1.PostedFile.ContentLength;
            
                lblMsg.Text += "Success!!!";

                string file_path = "D:\\Uploads\\" + FileUpload1.FileName;
            }
            else
            {

                lblMsg.Text = "You have not specified a file";
            }


            //try {


                string sqlInsert1 = "INSERT INTO versions (id_science_work, date_upload, active, uploader, file_name, file_size, file_path, version_name) VALUES(?id_science_work, ?date_upload, ?active, ?uploader, ?file_name, ?file_size, ?file_path, ?version_name)";
                MySqlCommand cmd1 = new MySqlCommand(sqlInsert1, conn);

                cmd1.Parameters.Add("?id_science_work", Convert.ToInt32(Session["sess"].ToString()));
                cmd1.Parameters.Add("?date_upload", System.DateTime.Now.ToString("yyyy-MM-dd"));
                cmd1.Parameters.Add("?active", RadioButtonList1.SelectedValue);
                cmd1.Parameters.Add("?uploader", Session["New"].ToString());
                cmd1.Parameters.Add("?file_name", FileUpload1.PostedFile.FileName);
                cmd1.Parameters.Add("?file_size",  FileUpload1.PostedFile.ContentLength);
                cmd1.Parameters.Add("?file_path", Server.MapPath("~/uploads/projects/" + swName + "/" +txtVersionName.Text+"/"+ FileUpload1.FileName));
                //cmd1.Parameters.Add("?file_path", Server.MapPath("~/Images/Projects/"+ FileUpload1.FileName));
                cmd1.Parameters.Add("?version_name", txtVersionName.Text);

                cmd1.ExecuteNonQuery();

                
                //           Treba da voda ID_VRRSION ka da ja dobiam
                if (RadioButtonList1.SelectedValue == "1") { 
                
                 string sqlUpdateNonActiveVersion = "UPDATE versions SET  active=?active WHERE id_version NOT LIKE ?id_version and id_science_work=?id_science_work";
                MySqlCommand cmd2 = new MySqlCommand(sqlUpdateNonActiveVersion, conn);
                cmd2.Parameters.Add("?active", '0');
                cmd2.Parameters.Add("?id_version", Convert.ToInt32(cmd1.LastInsertedId));
                cmd2.Parameters.Add("?id_science_work", Convert.ToInt32(Session["sess"].ToString()));
                cmd2.ExecuteNonQuery();
                }


            //}
           // catch (Exception ex)
           // {
            ///
            //    lblMsg.Text = ex.ToString();
           // }
           // finally
          //  {

                conn.Close();
                Response.Redirect("Versions.aspx");


           // }


        }

    
        static int b;
         protected void button_click(object sender, EventArgs e)
        {
            Button ibtn1 = sender as Button;
            int rowIndex = Convert.ToInt32(ibtn1.Attributes["RowIndex"]);
            b = rowIndex;
            //Use this rowIndex in your code
            lblEx.Text = rowIndex.ToString();

          //  try
           // {

                conn.Open();

                string sqlSelect = "SELECT * from versions WHERE id_version=?id";
                MySqlCommand cmd1 = new MySqlCommand(sqlSelect, conn);
                cmd1.Parameters.AddWithValue("?id", Convert.ToInt32(rowIndex.ToString()));
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd1);
                DataSet ds = new DataSet();
                adapter.Fill(ds);
/**
                string sqlSelect = "SELECT file_name from versions WHERE id_version=@id";
                MySqlCommand cmd1 = new MySqlCommand(sqlSelect, conn);
                cmd1.Parameters.AddWithValue("@id", Convert.ToInt32(rowIndex.ToString()));
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd1);
                DataSet ds = new DataSet();
                adapter.Fill(ds);

                if (ds.Tables[0].Rows[0]["file_name"] != null)
                {

                    string path =  ds.Tables[0].Rows[0]["file_name"].ToString();
                    System.IO.File.Delete(Server.MapPath(path));
                } **/

             //string swName = ds.Tables[0].Rows[0][""]

             //DELETE source files for deleted version
               // CreateDirectoryIfNotExist(Server.MapPath("~/uploads/projects/" + swName + "/" + txtVersionName.Text));
                verName = ds.Tables[0].Rows[0]["version_name"].ToString();
                DeleteDirectoryIfNotExist(Server.MapPath("~/uploads/projects/" + swName + "/" + verName));

                string sqlDelete = "DELETE FROM  versions WHERE id_version =?id";
                MySqlCommand cmd = new MySqlCommand(sqlDelete, conn);

                cmd.Parameters.Add("?id", Convert.ToInt32(rowIndex.ToString()));

                cmd.ExecuteNonQuery();

                

          //  }
           // catch (Exception ex)
           // {

               // lblEx.Text = ex.ToString();
           // }
           // finally {

                conn.Close();
                Response.Redirect("Versions.aspx");
          //  }


        }

         protected void btnSource_Click(object sender, EventArgs e)
         {
             Button btnSource = sender as Button;
             int rowIndex = Convert.ToInt32(btnSource.Attributes["RowIndex"].ToString());
             Session["Source"] = rowIndex;
             Response.Redirect("VersionSource.aspx");

         }
 
         static int rID;
        //edit button code
         protected void btnEdit_click(object sender, EventArgs e)
         {
            // btnConfirmAdd.Visible = false;
             btnUpdate1.Visible = true;
             Button btnEdit = sender as Button;
             int rowIndex = Convert.ToInt32(btnEdit.Attributes["RowIndex"].ToString());
             rID = rowIndex;

           
             try
             {
                 conn.Open();
                 string sqlSelect = "SELECT * from versions where id_version=?id";
                 MySqlCommand cmd = new MySqlCommand(sqlSelect, conn);
                 cmd.Parameters.Add("?id", rowIndex);
                 cmd.ExecuteNonQuery();
                 MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                 DataSet ds = new DataSet();
                 adapter.Fill(ds);

                 txtVersionName1.Text = ds.Tables[0].Rows[0]["version_name"].ToString();
                 RadioButtonList11.SelectedIndex =Math.Abs( Convert.ToInt32(ds.Tables[0].Rows[0]["active"].ToString())-1);
                 
             }
             catch { }
             finally {
                 conn.Close();
             }


         }


         protected void btnUpdate_Click(object sender, EventArgs e)
         {
           //  try
            // {
             
                 conn.Open();
                 // name=?name , surname=?surname, email=?email, password=?password,, labs_id=?labs_id 
                 string sqlUpdate = "UPDATE versions SET  version_name=?version_name, active=?active  WHERE id_version=?id_version";
                 //file_name=?file_name, file_size=?file_size, file_path=?file_path
                 MySqlCommand cmd = new MySqlCommand(sqlUpdate, conn);

                 cmd.Parameters.Add("?version_name", txtVersionName1.Text);
                 cmd.Parameters.Add("?active", RadioButtonList11.SelectedValue);
                 //cmd.Parameters.Add("?file_name", FileUpload1.PostedFile.FileName);
                 //cmd.Parameters.Add("?file_size", FileUpload1.PostedFile.ContentLength);
                 //cmd.Parameters.Add("?file_path", Server.MapPath("~/Images/Projects/" + swName + "/" + FileUpload1.FileName));
                 cmd.Parameters.Add("?id_version", rID);

                 cmd.ExecuteNonQuery();
                 if (Convert.ToInt64(RadioButtonList11.SelectedValue) == 1)
                 {
                     string sqlUpdateNonActiveVersion = "UPDATE versions SET  active=?active WHERE id_version NOT LIKE ?id_version and id_science_work=?id_science_work";
                     MySqlCommand cmd1 = new MySqlCommand(sqlUpdateNonActiveVersion, conn);
                     cmd1.Parameters.Add("?active", '0');
                     cmd1.Parameters.Add("?id_version", rID);
                     cmd1.Parameters.Add("?id_science_work", Convert.ToInt32(Session["sess"].ToString()));
                     cmd1.ExecuteNonQuery();
                 }
            // }
           //  catch (Exception ex)
           //  {

            //     lblMsg.Text = ex.ToString();
            // }
           //  finally
            // {

                 conn.Close();
                 Response.Redirect("Versions.aspx");
                 //newVersion.Visible = false;
             //}
         }



         //gvVersions_RowCreated
         protected void gvVersions_RowCreated(object sender, GridViewRowEventArgs e)
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
                 lblEx.Text = e.Message;
             }
         }

         protected void DeleteDirectoryIfNotExist(string NewDirectory)
         {
             try
             {
                 if (Directory.Exists(NewDirectory))
                 {
                     Directory.Delete(NewDirectory,true);
                 }
             }
             catch (Exception e)
             {
                 lblEx.Text = e.Message;
             }
         }

         protected void DownloadFile(object sender, EventArgs e)
         {
             LinkButton lb = sender as LinkButton;
             int rowIndex = Convert.ToInt32(lb.Attributes["RowIndex"].ToString());


             conn.Open();
             string sqlSelect = "select v.*, title from versions v, science_work sw where id_version = ?id and id_science_work=sw.id";
             MySqlCommand cmd = new MySqlCommand(sqlSelect, conn);
             cmd.Parameters.AddWithValue("?id", rowIndex);
             MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
             DataSet ds = new DataSet();
             adapter.Fill(ds);

             string swtitle = ds.Tables[0].Rows[0]["title"].ToString();
             string vertitle = ds.Tables[0].Rows[0]["version_name"].ToString();
             string filename = ds.Tables[0].Rows[0]["file_name"].ToString();


             //string filePath = "d:/";
            string filePath = Server.MapPath("~/uploads/projects/"+swtitle+"/"+vertitle);

             Response.ContentType = ContentType;
             Response.AppendHeader("Content-Disposition", "attachment; filename="+filename);
             Response.WriteFile(filePath);
             Response.End();
             /**
             Response.ContentType = "application/octet-stream";
             Response.AppendHeader("Content-Disposition", "attachment;filename=" + filename);
             string aaa = Server.MapPath("~/uploads/projects/"+swtitle+"/"+vertitle+"/" + filename);
             Response.TransmitFile(Server.MapPath("~/uploads/projects/" + swtitle + "/" + vertitle));
             Response.End();
             **/
             
         }


    }
}

