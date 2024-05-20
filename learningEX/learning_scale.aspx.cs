using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;

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
            Dictionary<string, string> answerResults = HttpContext.Current.Session["answerResults"] as Dictionary<string, string>;
            float Accuracy = (float)HttpContext.Current.Session["Accuracy"];
            string topicname = HttpContext.Current.Session["topicname"].ToString();
            int userID = Convert.ToInt32(HttpContext.Current.Session["UserID"]);
            string topicType = HttpContext.Current.Session["topictype"].ToString();
            string TopicCategory = HttpContext.Current.Session["topiccategory"].ToString();
            string TopicSubcategory = HttpContext.Current.Session["topicsubcategory"].ToString();
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
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                connection.Open();

                // 建立 SQL 指令
                string query = "INSERT INTO UserAnswerHistory (UserID, AnswerDate, TopicName, HistoricalAnswers, Accuracy, topicType, TopicCategory, TopicSubcategory) " +
                               "VALUES (@UserID, @AnswerDate, @TopicName, @HistoricalAnswers, @Accuracy, @topicType, @TopicCategory, @TopicSubcategory)";

                SqlCommand cmd = new SqlCommand(query, connection);
                DateTime now = DateTime.Now;
                string json = JsonConvert.SerializeObject(answerResults);

                // 加入參數
                cmd.Parameters.AddWithValue("@UserID", userID);
                cmd.Parameters.AddWithValue("@AnswerDate", now);
                cmd.Parameters.AddWithValue("@TopicName", topicname);
                cmd.Parameters.AddWithValue("@HistoricalAnswers", json);
                cmd.Parameters.AddWithValue("@Accuracy", Accuracy);
                cmd.Parameters.AddWithValue("@Topictype", topicType);
                cmd.Parameters.AddWithValue("@TopicCategory", TopicCategory);
                cmd.Parameters.AddWithValue("@TopicSubcategory", TopicSubcategory);

                HttpContext.Current.Session.Remove("Accuracy");
                HttpContext.Current.Session.Remove("answerResults");
                HttpContext.Current.Session.Remove("topicname");
                HttpContext.Current.Session.Remove("topiccategory");
                HttpContext.Current.Session.Remove("topicsubcategory");
                // 執行 SQL 指令
                cmd.ExecuteNonQuery();

                // 關閉連接
                connection.Close();
            }
        }
    }
}