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
        public static bool Login(string username, string password, out string message, out int userID)
        {
            message = null;
            userID = -1;

            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                connection.Open();
                string query = "SELECT * FROM Users WHERE Username=@username";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@username", username);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    string storedPassword = reader["password"].ToString();

                    if (storedPassword == password)
                    {
                        userID = Convert.ToInt32(reader["UserID"]);
                        reader.Close();
                        connection.Close();
                        return true;
                    }
                    else
                    {
                        message = "密碼錯誤";
                        reader.Close();
                        connection.Close();
                        return false;
                    }
                }
                else
                {
                    message = "用戶不存在";
                    reader.Close();
                    connection.Close();
                    return false;
                }
            }
        }
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Text;
            string message;
            int userID;

            System.Diagnostics.Debug.WriteLine($"使用者名稱：{username}");
            System.Diagnostics.Debug.WriteLine($"密碼：{password}");

            if (Login(username, password, out message, out userID))
            {
                Session["UserName"] = username;
                Session["UserID"] = userID;
                Response.Redirect("learning_map.aspx");
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("登錄失敗");
                lblMessage.Text = message;
            }
        }
    }
}