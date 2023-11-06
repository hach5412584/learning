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
    public partial class learning_scale : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }
        [WebMethod]
        public static void SubmitFeedback(string rating, string difficulty, string satisfaction, string intention, string suggestion)
        {
            int userID = Convert.ToInt32(HttpContext.Current.Session["UserID"]);
            // 建立與資料庫的連接
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                connection.Open();

                // 建立 SQL 指令
                string query = "INSERT INTO LearningFeedback (UserID, LearningEffect, DifficultyLevel, Satisfaction, LearningInterest, Suggestions) " +
                               "VALUES (@UserID, @LearningEffect, @DifficultyLevel, @Satisfaction, @LearningInterest, @Suggestions)";

                SqlCommand cmd = new SqlCommand(query, connection);

                // 加入參數
                cmd.Parameters.AddWithValue("@UserID", userID);
                cmd.Parameters.AddWithValue("@LearningEffect", Convert.ToInt32(rating));
                cmd.Parameters.AddWithValue("@DifficultyLevel", Convert.ToInt32(difficulty));
                cmd.Parameters.AddWithValue("@Satisfaction", Convert.ToInt32(satisfaction));
                cmd.Parameters.AddWithValue("@LearningInterest", Convert.ToInt32(intention));
                cmd.Parameters.AddWithValue("@Suggestions", suggestion);

                // 執行 SQL 指令
                cmd.ExecuteNonQuery();

                // 關閉連接
                connection.Close();
            }
        }
    }
}