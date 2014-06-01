using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using System.Data;
using System.Net.Mail;

namespace Diplomska
{
    public partial class RessetPassword : System.Web.UI.Page
    {
        static string connString = "SERVER=localhost;DATABASE=naucen_trud;UID=root;PWD=filip;";
        MySqlConnection conn = new MySqlConnection(connString);

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("Login.aspx");
        }
        static string newPassword;
        protected void btnSend_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();

                string sqlSelect = "Select count(*) from user_info where email=?email";
                MySqlCommand cmd1 = new MySqlCommand(sqlSelect, conn);
                cmd1.Parameters.Add("?email", txtEmail.Text);
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd1);
                DataSet ds = new DataSet();
                adapter.Fill(ds);

                int temp = Convert.ToInt32(cmd1.ExecuteScalar().ToString());

                if (temp == 1)
                {
                    newPassword = newPass();
                    //ako treba admin da ja resetira lozinkata accsepted = 3 treba da e
                    string update = "Update user_info set accsepted=?accsepted, password=?password where email=?email";
                    MySqlCommand cmd = new MySqlCommand(update, conn);
                    cmd.Parameters.AddWithValue("?accsepted", "1");
                    cmd.Parameters.AddWithValue("?email", txtEmail.Text);
                    cmd.Parameters.AddWithValue("?password", newPassword);
                    cmd.ExecuteNonQuery();
                    
                    sendMail();

                    Response.Redirect("Login.aspx");
                }
                else {

                    lblErr.Text = "Не постои внесената email адреса!";
                }

                

            }
            catch { }
            finally {

                conn.Close();
                
            }
        }

        protected void sendMail() {
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient smtpC = new SmtpClient("smtp.gmail.com");

                mail.From = new MailAddress("filip.nikolovski.peh@gmail.com");
                mail.To.Add(txtEmail.Text);
                mail.Body = "Вашата нова лозинка е: "+newPassword;
                mail.Subject = "Нова лозинка!";
                smtpC.Port = 587;
                smtpC.Credentials = new System.Net.NetworkCredential("filip.nikolovski.peh@gmail.com", "badzovski");
                smtpC.EnableSsl = true;
                smtpC.Send(mail);
                lblErr.Text = "bravo";

            }
            catch (Exception ex) {

              lblErr.Text = ex.ToString();
            }
        }
        protected String newPass() {

            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var random = new Random();
            var result = new string(Enumerable.Repeat(chars, 8).Select(s => s[random.Next(s.Length)]).ToArray());

            return result;
        }
    }
}