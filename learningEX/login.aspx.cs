using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace learningEX
{
    public partial class login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine($"loginLoad");
        }
        [WebMethod]
        public static bool Login(string username, string password)
        {

            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                connection.Open();
                string query = "SELECT * FROM Users WHERE Username=@username AND Password=@password";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@password", password);
                SqlDataReader reader = cmd.ExecuteReader();
                connection.Close();
                return true;
            }
        }
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Text;

            System.Diagnostics.Debug.WriteLine($"使用者名稱：{username}");
            System.Diagnostics.Debug.WriteLine($"密碼：{password}");

            if (Login(username, password))
            {
                Session["UserName"] = username;
                Response.Redirect("mainpage.aspx");
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("登錄失敗");
                // 登錄失敗的處理邏輯
            }
        }
    }
}