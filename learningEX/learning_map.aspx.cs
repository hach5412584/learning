using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Web.Services;
using System.Web.Script.Serialization;

namespace learningEX
{

    public partial class learningmap : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["UserName"] != null && Session["UserID"] != null)
                {
                    string username = Session["UserName"].ToString();
                    string userID = Session["UserID"].ToString();
                    lblUserName.ForeColor = System.Drawing.Color.Gray;
                    lblUserName.Text = "歡迎您" + username;
                }
            }
        }
        [WebMethod]
        public static double[] GetAccuracyData()
        {
            double[] accuracyData = new double[6]; // 假設你有六個項目
            // 連接資料庫並執行查詢
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                string query = "SELECT Topictype, AVG(Accuracy) AS AverageAccuracy FROM UserAnswerHistory GROUP BY Topictype";
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                int index = 0;
                while (reader.Read() && index < 6)
                {
                    // 讀取每個項目的正確率，並將其添加到數據數組中
                    accuracyData[index++] = Math.Round(Convert.ToDouble(reader["AverageAccuracy"]),2);
                }
                reader.Close();
            }

            return accuracyData;
        }
        [WebMethod]
        public static List<Dictionary<string, object>> GetAccuracyChartTopRight(int index)
        {
            List<Dictionary<string, object>> Topic_all = new List<Dictionary<string, object>>();
            string topicType = "";

            // 根据 index 设置不同的 topicType
            switch (index)
            {
                case 0:
                    topicType = "Algorithm";
                    break;
                case 1:
                    topicType = "ImageRecognition";
                    break;
                // 添加其他 case 处理不同的 index 值
                default:
                    topicType = "DefaultTopic";
                    break;
            }

            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                string query = @"
                    WITH AllScores AS (
                        SELECT
                            TopicCategory,
                            Accuracy
                        FROM
                            UserAnswerHistory
                        WHERE
                            TopicType = @TopicType
                    )
                    SELECT
                        TopicCategory,
                        AVG(Accuracy) AS AvgAccuracy,
                        COUNT(*) AS AnswerCount
                    FROM
                        AllScores
                    GROUP BY TopicCategory;";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@TopicType", topicType);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var record = new Dictionary<string, object>
                    {
                        { "TopicCategory", reader["TopicCategory"].ToString() },
                        { "AvgAccuracy", Convert.ToDouble(reader["AvgAccuracy"]) },
                        { "AnswerCount", Convert.ToInt32(reader["AnswerCount"]) }
                    };
                    Topic_all.Add(record);
                }
                reader.Close();
            }

            return Topic_all;
        }


        [WebMethod]
        public static List<Dictionary<string, object>> GetAccuracyChartBottomRight(string index)
        {
            List<Dictionary<string, object>> TopicCategory_all = new List<Dictionary<string, object>>();

            string TopicCategory = index;

            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                string query = @"
                    WITH AllScores AS (
                        SELECT
                            TopicSubcategory,
                            Accuracy
                        FROM
                            UserAnswerHistory
                        WHERE
                            TopicCategory = @TopicCategory
                    )
                    SELECT
                        TopicSubcategory,
                        AVG(Accuracy) AS AvgAccuracy,
                        COUNT(*) AS AnswerCount
                    FROM
                        AllScores
                    GROUP BY TopicSubcategory;";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@TopicCategory", index);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var record = new Dictionary<string, object>
                    {
                        { "TopicSubcategory", reader["TopicSubcategory"].ToString() },
                        { "AvgAccuracy", Convert.ToDouble(reader["AvgAccuracy"]) },
                        { "AnswerCount", Convert.ToInt32(reader["AnswerCount"]) }
                    };
                    TopicCategory_all.Add(record);
                }
                reader.Close();
            }

            return TopicCategory_all;
        }

        [WebMethod]
        public static string GetWeakestTopicsQuestion(string questionType)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            List<Dictionary<string, string>> weakestTopics = new List<Dictionary<string, string>>();


            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = @"
                SELECT TOP 3 TopicName, TopicCategory, TopicSubcategory
                FROM UserAnswerHistory
                WHERE Topictype = @QuestionType
                GROUP BY TopicName, TopicCategory, TopicSubcategory
                ORDER BY AVG(Accuracy) ASC";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@QuestionType", questionType);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var topicData = new Dictionary<string, string>
                    {
                        { "TopicName", reader["TopicName"].ToString() },
                        { "TopicCategory", reader["TopicCategory"].ToString() },
                        { "TopicSubcategory", reader["TopicSubcategory"].ToString() }
                    };
                    weakestTopics.Add(topicData);
                }
            }

            if (weakestTopics.Count > 0)
            {
                Random random = new Random();
                int index = random.Next(weakestTopics.Count);
                var selectedTopic = weakestTopics[index];
                JavaScriptSerializer js = new JavaScriptSerializer();
                return js.Serialize(selectedTopic);
            }

            return null;
        }
        [WebMethod]
        public static List<Dictionary<string, object>> GetAccuracyChartBottom(string index)
        {  
            List<Dictionary<string, object>> latestFiveScores = new List<Dictionary<string, object>>();

            string TopicSubcategory = index;

            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                string query = @"
                    WITH LatestFiveScores AS (
                        SELECT
                            TopicSubcategory,
                            Accuracy,
                            ROW_NUMBER() OVER (PARTITION BY TopicSubcategory ORDER BY AnswerDate DESC) AS RowNum
                        FROM
                            UserAnswerHistory
                        WHERE
                            TopicSubcategory = @TopicSubcategory
                    )
                    SELECT
                        TopicSubcategory,
                        Accuracy
                    FROM
                        LatestFiveScores
                    WHERE
                        RowNum <= 5;";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@TopicSubcategory", index);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var record = new Dictionary<string, object>
                    {
                        { "TopicSubcategory", reader["TopicSubcategory"].ToString() },
                        { "Accuracy", reader["Accuracy"] }
                    };
                    latestFiveScores.Add(record);
                }
                reader.Close();
            }

            return latestFiveScores;
        }

    }
}