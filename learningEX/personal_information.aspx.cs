using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace learningEX
{
    public partial class personal_information : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // 在這裡編寫檢索使用者資料的邏輯
                string userId = Convert.ToString(HttpContext.Current.Session["UserID"]);  // 填入實際的使用者 ID

                // 假設您有一個方法來從資料庫中獲取使用者資料
                UserData userData = GetUserById(userId);

                // 將資料填入表單控件
                if (userData != null)
                {
                    fullName.Text = userData.Name;
                    email.Text = userData.Email;
                    username.Text = userData.Username;
                    studentID.Text = userData.StudentID;
                }

                DataTable dt = LoadHistoricalAnswersFromDatabase(userId); // 从数据库加载历史答案数据
                FillGridView(dt);
            }
        }
        public class UserData
        {
            public string Name { get; set; }
            public string Email { get; set; }
            public string Username { get; set; }
            public string StudentID { get; set; }
        }
        private UserData GetUserById(string userId)
        {
            UserData userData = null;
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                connection.Open();

                // 使用參數化查詢以防 SQL 注入
                string query = "SELECT Name, Email, Username, StudentID FROM Users WHERE UserId = @UserId";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@UserId", userId);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        // 創建 UserData 對象並填充資料
                        userData = new UserData
                        {
                            Name = reader["Name"].ToString(),
                            Email = reader["Email"].ToString(),
                            Username = reader["Username"].ToString(),
                            StudentID = reader["StudentID"].ToString()
                        };
                    }
                }
                connection.Close();
            }
            return userData;
        }
        [WebMethod]
        public static string SaveUserData(string fullName, string email, string username, string studentID)
        {
            string userId = Convert.ToString(HttpContext.Current.Session["UserID"]);
            try
            {
                // 建立連接字串，請替換成您的資料庫資訊
                // 使用 using 以確保資源正確釋放
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
                {
                    // 開啟連接
                    connection.Open();

                    // 建立 SQL 指令
                    string query = "UPDATE Users SET Name = @Name, Email = @Email, Username = @Username, StudentID = @StudentID WHERE UserId = @UserId";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // 加入參數，避免 SQL 注入攻擊
                        command.Parameters.AddWithValue("@UserId", userId);
                        command.Parameters.AddWithValue("@Name", fullName);
                        command.Parameters.AddWithValue("@Email", email);
                        command.Parameters.AddWithValue("@Username", username);
                        command.Parameters.AddWithValue("@StudentID", studentID);

                        // 執行 SQL 指令
                        command.ExecuteNonQuery();
                    }
                    connection.Close();
                }

                return "資料儲存成功！";
            }
            catch (Exception ex)
            {
                return "儲存資料時發生錯誤：" + ex.Message;
            }
        }

        [WebMethod]
        public static string ChangePassword(string oldPassword, string newPassword)
        {
            // 獲取當前登錄用戶的 User ID
            string userId = Convert.ToString(HttpContext.Current.Session["UserID"]);

            // 驗證舊密碼是否正確
            if (!ValidateOldPassword(userId, oldPassword))
            {
                return "舊密碼不正確";
            }

            // 更新資料庫中的密碼
            if (UpdatePassword(userId, newPassword))
            {
                return "密碼已成功更改";
            }
            else
            {
                return "更改密碼失敗";
            }
        }

        private static bool ValidateOldPassword(string userId, string oldPassword)
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                connection.Open();

                // 使用參數化查詢以防止 SQL 注入
                string query = "SELECT COUNT(*) FROM Users WHERE UserID = @UserID AND Password = @OldPassword";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@UserID", userId);
                command.Parameters.AddWithValue("@OldPassword", oldPassword);

                int count = (int)command.ExecuteScalar();

                connection.Close();

                // 如果查詢結果為 1，表示密碼正確；否則為不正確
                return count == 1;
            }
        }

        private static bool UpdatePassword(string userId, string newPassword)
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                connection.Open();

                // 使用參數化查詢以防止 SQL 注入
                string query = "UPDATE Users SET Password = @NewPassword WHERE UserID = @UserID";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@UserID", userId);
                command.Parameters.AddWithValue("@NewPassword", newPassword);

                int rowsAffected = command.ExecuteNonQuery();

                connection.Close();

                // 如果更新成功，影響的行數應為 1
                return rowsAffected == 1;
            }
        }
        private DataTable LoadHistoricalAnswersFromDatabase(string userID)
        {
            DataTable dt = new DataTable();
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                connection.Open();
                string query = "SELECT AnswerDate, Topicname, HistoricalAnswers, Accuracy, Topictype FROM UserAnswerHistory WHERE UserID = @UserID";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@UserID", userID);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dt);
            }
            return dt;
        }
        private void FillGridView(DataTable dt)
        {
            if (dt.Rows.Count > 0)
            {
                gvAnswerHistory.DataSource = dt;
                gvAnswerHistory.DataBind();
            }
        }
        protected void lnkReview_Click(object sender, EventArgs e)
        {
            LinkButton lnkReview = (LinkButton)sender;
            GridViewRow row = (GridViewRow)lnkReview.NamingContainer;
            Label lblHistoricalAnswers = (Label)row.FindControl("lblHistoricalAnswers");
            string historicalAnswers = lblHistoricalAnswers.Text;
            string accuracyString = row.Cells[4].Text;
            // 将 historicalAnswers 字符串反序列化为字典
            Dictionary<string, string> answerResults = JsonConvert.DeserializeObject<Dictionary<string, string>>(historicalAnswers);
            double accuracy = 0.0f;
            double.TryParse(accuracyString, out accuracy);
            // 将反序列化后的字典存储到 Session 中
            Session["answerResults"] = answerResults;
            Session["Accuracy"] = accuracy;
            // 跳转到 ans_list 页面
            Response.Redirect("ans_list.aspx");
        }
    }
}