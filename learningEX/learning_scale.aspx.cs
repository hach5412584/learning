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
        public static void SubmitFeedback(string new_content, string curiosity, string try_to_understand, string learn_something, string Good_results, string improve_average, string Beat_most_students, string show_my_ability)
        {
            Dictionary<string, string> answerResults = HttpContext.Current.Session["answerResults"] as Dictionary<string, string>;
            double Accuracy = (double)HttpContext.Current.Session["Accuracy"];
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
                string query = "INSERT INTO LearningFeedback (UserID, new_content, curiosity, try_to_understand, learn_something, Good_results, improve_average, Beat_most_students, show_my_ability) " +
                               "VALUES (@UserID, @new_content, @curiosity, @try_to_understand, @learn_something, @Good_results, @improve_average, @Beat_most_students, @show_my_ability)";

                SqlCommand cmd = new SqlCommand(query, connection);

                // 加入參數
                cmd.Parameters.AddWithValue("@UserID", userID);
                cmd.Parameters.AddWithValue("@new_content", Convert.ToInt32(new_content));
                cmd.Parameters.AddWithValue("@curiosity", Convert.ToInt32(curiosity));
                cmd.Parameters.AddWithValue("@try_to_understand", Convert.ToInt32(try_to_understand));
                cmd.Parameters.AddWithValue("@learn_something", Convert.ToInt32(learn_something));
                cmd.Parameters.AddWithValue("@Good_results", Convert.ToInt32(Good_results));
                cmd.Parameters.AddWithValue("@improve_average", Convert.ToInt32(improve_average));
                cmd.Parameters.AddWithValue("@Beat_most_students", Convert.ToInt32(Beat_most_students));
                cmd.Parameters.AddWithValue("@show_my_ability", Convert.ToInt32(show_my_ability));

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
                HttpContext.Current.Session.Remove("topic_cilck");
                
                // 執行 SQL 指令
                cmd.ExecuteNonQuery();

                // 關閉連接
                connection.Close();
            }
        }
    }
}