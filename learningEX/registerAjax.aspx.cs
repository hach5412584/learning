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
    public partial class registerAjax : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        [WebMethod]
        public static string Register(string username, string password, string email, string name, string studentID)
        {
            try
            {
                // 檢查Username是否已存在於資料庫
                if (IsUserExists(username))
                {
                    return "用戶名已被使用，請嘗試其他名稱。";
                }

                // 檢查Email是否已存在於資料庫
                if (IsEmailExists(email))
                {
                    return "該電子信箱已經存在於資料庫，你可以使用找回帳號密碼功能。";
                }

                // 檢查StudentID是否已存在於資料庫
                if (IsStudentIDExists(studentID))
                {
                    return "該學號已被使用，你可以使用找回帳號密碼功能。";
                }

                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
                {
                    connection.Open();
                    string sql = "INSERT INTO Users (Username, Password, Email, Name, StudentID) VALUES (@Username, @Password, @Email, @Name, @StudentID)";
                    SqlCommand cmd = new SqlCommand(sql, connection);
                    cmd.Parameters.AddWithValue("@Username", username);
                    cmd.Parameters.AddWithValue("@Password", password);
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@name", name);
                    cmd.Parameters.AddWithValue("@StudentID", studentID);
                    
                    cmd.ExecuteNonQuery();
                    return "註冊成功"; // 註冊成功，返回 true
                }
            }
            catch (Exception ex)
            {
                // 處理例外
                return "error";
            }
        }

        private static bool IsUserExists(string username)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
                {
                    connection.Open();
                    string sql = "SELECT COUNT(*) FROM Users WHERE Username = @Username";
                    SqlCommand cmd = new SqlCommand(sql, connection);
                    cmd.Parameters.AddWithValue("@Username", username);
                    int count = (int)cmd.ExecuteScalar();

                    return count > 0;
                }
            }
            catch (Exception ex)
            {
                // 處理例外
                return false;
            }
        }

        private static bool IsEmailExists(string email)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
                {
                    connection.Open();
                    string sql = "SELECT COUNT(*) FROM Users WHERE Email = @Email";
                    SqlCommand cmd = new SqlCommand(sql, connection);
                    cmd.Parameters.AddWithValue("@Email", email);
                    int count = (int)cmd.ExecuteScalar();

                    return count > 0;
                }
            }
            catch (Exception ex)
            {
                // 處理例外
                return false;
            }

        }
        private static bool IsStudentIDExists(string studentID)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
                {
                    connection.Open();
                    string sql = "SELECT COUNT(*) FROM Users WHERE StudentID = @StudentID";
                    SqlCommand cmd = new SqlCommand(sql, connection);
                    cmd.Parameters.AddWithValue("@StudentID", studentID);
                    int count = (int)cmd.ExecuteScalar();

                    return count > 0;
                }
            }
            catch (Exception ex)
            {
                // 處理例外
                return false;
            }
        }
    }
}